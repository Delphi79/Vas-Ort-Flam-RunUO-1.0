using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Accounting;
using Server.Engines.Help;
using Server.Scripts.Commands;

namespace Server.Admin
{
	public enum LoginResponse : byte
	{
		NoUser = 0,
		BadIP,
		BadPass,
		NoAccess,
		OK
	}

	public sealed class AdminCompressedPacket : Packet
	{
		public AdminCompressedPacket( byte[] CompData, int CDLen, int unCompSize ) : base( 0x01 )
		{
			EnsureCapacity( 1 + 2 + 2 + CDLen );
			m_Stream.Write( (ushort)unCompSize );
			m_Stream.Write( CompData, 0, CDLen );
		}
	}
	
	public sealed class Login : Packet
	{
		public Login( LoginResponse resp ) : base( 0x02, 2 )
		{
			m_Stream.Write( (byte)resp );
		}
	}

	public sealed class ConsoleData : Packet
	{
		public ConsoleData( string str ) : base( 0x03 )
		{
			EnsureCapacity( 1 + 2 + 1 + str.Length + 1 );
			m_Stream.Write( (byte) 2 );

			m_Stream.WriteAsciiNull( str );
		}

		public ConsoleData( char ch ) : base( 0x03 )
		{
			EnsureCapacity( 1 + 2 + 1 + 1 );
			m_Stream.Write( (byte) 3 );

			m_Stream.Write( (byte) ch );
		}
	}

	public sealed class ServerInfo : Packet
	{
		public ServerInfo() : base( 0x04 )
		{
			string netVer = Environment.Version.ToString();
			string os = Environment.OSVersion.ToString();

			EnsureCapacity( 1 + 2 + (10*4) + netVer.Length+1 + os.Length+1 );
			int banned = 0;
			int active = 0;

			foreach ( Account acct in Accounts.Table.Values )
			{
				if ( acct.Banned )
					++banned;
				else
					++active;
			}

			m_Stream.Write( (int) active );
			m_Stream.Write( (int) banned );
			m_Stream.Write( (int) Firewall.List.Count );
			m_Stream.Write( (int) NetState.Instances.Count );

			m_Stream.Write( (int) World.Mobiles.Count );
			m_Stream.Write( (int) Core.ScriptMobiles );
			m_Stream.Write( (int) World.Items.Count );
			m_Stream.Write( (int) Core.ScriptItems );

			m_Stream.Write( (uint)(DateTime.Now - Clock.ServerStart).TotalSeconds );
			m_Stream.Write( (uint) GC.GetTotalMemory( false ) );
			m_Stream.WriteAsciiNull( netVer );
			m_Stream.WriteAsciiNull( os );
		}
	}

	public sealed class AccountSearchResults : Packet
	{
		public AccountSearchResults( ArrayList results ) : base( 0x05 )
		{
			EnsureCapacity( 1 + 2 + 2 );

			m_Stream.Write( (byte)results.Count );
			
			foreach ( Account a in results )
			{
				m_Stream.WriteAsciiNull( a.Username );

				string pwToSend = a.PlainPassword;

				if ( pwToSend == null )
					pwToSend = "(hidden)";

				m_Stream.WriteAsciiNull( pwToSend );
				m_Stream.Write( (byte)a.AccessLevel );
				m_Stream.Write( a.Banned );

				long ticks = a.LastLogin.Ticks;
				m_Stream.Write( (uint)(ticks >> 32) ); m_Stream.Write( (uint)ticks );

				m_Stream.Write( (ushort)a.LoginIPs.Length );
				for (int i=0;i<a.LoginIPs.Length;i++)
					m_Stream.WriteAsciiNull( a.LoginIPs[i].ToString() );

				m_Stream.Write( (ushort)a.IPRestrictions.Length );
				for (int i=0;i<a.IPRestrictions.Length;i++)
					m_Stream.WriteAsciiNull( a.IPRestrictions[i] );

				m_Stream.Write( (ushort)a.Comments.Count );
				for (int i=0;i<a.Comments.Count;i++)
				{
					m_Stream.WriteAsciiNull( ((AccountComment)a.Comments[i]).AddedBy );
					m_Stream.WriteAsciiNull( ((AccountComment)a.Comments[i]).Content );
					ticks = ((AccountComment)a.Comments[i]).LastModified.Ticks;
					m_Stream.Write( (uint)(ticks >> 32) ); m_Stream.Write( (uint)ticks );
				}
			}
		}
	}

	public sealed class StateInfo : Packet
	{
		public StateInfo( NetState state ) : base( 0x06 )
		{
			Account account = (Account)state.Account;
			AccessLevel accessLevel = account.AccessLevel;

			ArrayList list = new ArrayList();

			int count = account.Count;

			int packetLength = 3;

			for ( int i = 0; i < count; i++ )
			{
				Mobile m = account[i];
				if ( m != null && m.AccessLevel >= accessLevel )
				{
					list.Add(m);
					packetLength += 4 + m.Name.Length + 1;
				}
			}

			EnsureCapacity( packetLength );

			m_Stream.Write( (byte)accessLevel );
			m_Stream.Write( (byte)list.Count );
			foreach ( Mobile m in list )
			{
				m_Stream.Write( m.Serial );
				m_Stream.WriteAsciiNull( m.Name );
			}
		}
	}

	public sealed class UOGInfo : Packet
	{
		public UOGInfo( string str ) : base( 0x52, str.Length+6 ) // 'R'
		{
			m_Stream.WriteAsciiFixed( "unUO", 4 );
			m_Stream.WriteAsciiNull( str );
		}
	}

	public sealed class MessageBoxMessage : Packet
	{
		public MessageBoxMessage( string msg, string caption ) : base( 0x08 )
		{
			EnsureCapacity( 1 + 2 + msg.Length + 1 + caption.Length + 1 );

			m_Stream.WriteAsciiNull( msg );
			m_Stream.WriteAsciiNull( caption );
		}
	}

	public sealed class StaffChat : Packet
	{
		public StaffChat( string text ) : base( 0x09 )
		{
			EnsureCapacity( 1 + 2 + ( text.Length + 1 ) * 2 );
			m_Stream.WriteBigUniNull( text );
		}
	}

	public sealed class AllPages : Packet
	{
		public AllPages() : base( 0x07 )
		{
			ArrayList list = PageQueue.List;

			int length = 0;

			for ( int i = 0; i < list.Count; )
			{
				PageEntry e = (PageEntry)list[i];

				if ( e.Sender.Deleted || e.Sender.NetState == null )
					PageQueue.Remove( e );
				else
				{
					length += 4 + 1 + e.Sent.ToString().Length + 1 + e.Sender.Name.Length + e.Message.Length + 1 + ( e.Handler == null ? 0 : ( e.Handler.Name.Length + 1 ) );
					++i;
				}
			}

			EnsureCapacity( 1 + 2 + 1 + length );

			m_Stream.Write( (byte)0x01 ); // All pages

			byte pageCount = (byte)Math.Min( list.Count, 255 );

			m_Stream.Write( pageCount );

			for ( byte i = 0; i < pageCount; i++ )
			{
				PageEntry e = (PageEntry)list[i];

				m_Stream.Write( e.Sender.Serial );
				m_Stream.Write( (byte)e.Type );
				long ticks = e.Sent.Ticks;
				m_Stream.Write( (uint)(ticks >> 32) ); m_Stream.Write( (uint)ticks );
				m_Stream.WriteAsciiNull( e.Sender.Name );
				m_Stream.WriteAsciiNull( e.Message );
				m_Stream.Write( e.Handler != null );
				if ( e.Handler != null )
					m_Stream.WriteAsciiNull( e.Handler.Name );
			}
		}
	}

	public sealed class NewPage : Packet
	{
		public NewPage( PageEntry e ) : base( 0x07 )
		{
			EnsureCapacity( 1 + 2 + 1 + 1 + 4 + 1 + e.Sent.ToString().Length + 1 + e.Sender.Name.Length + 1 + e.Message.Length + 1 + 1 + ( e.Handler == null ? 0 : ( e.Handler.Name.Length + 1 ) ) );

			m_Stream.Write( (byte)0x02 ); // New page

			m_Stream.Write( e.Sender.Serial );
			m_Stream.Write( (byte)e.Type );
			long ticks = e.Sent.Ticks;
			m_Stream.Write( (uint)(ticks >> 32) ); m_Stream.Write( (uint)ticks );
			m_Stream.WriteAsciiNull( e.Sender.Name );
			m_Stream.WriteAsciiNull( e.Message );
			m_Stream.Write( e.Handler != null );
			if ( e.Handler != null )
				m_Stream.WriteAsciiNull( e.Handler.Name );
		}
	}

	public sealed class HandlePage : Packet
	{
		public HandlePage( PageEntry e, Mobile handler ) : base( 0x07 )
		{
			EnsureCapacity( 1 + 2 + 1 + 4 + 1 + ( handler == null ? 0 : ( handler.Name.Length + 1 ) ) );

			m_Stream.Write( (byte)0x03 ); // Handle page

			m_Stream.Write( e.Sender.Serial );
			m_Stream.Write( handler != null );
			if ( handler != null )
				m_Stream.WriteAsciiNull( handler.Name );
		}
	}

	public sealed class RemovePage : Packet
	{
		public RemovePage( PageEntry e ) : base( 0x07 )
		{
			EnsureCapacity( 1 + 2 + 1 + 4 );

			m_Stream.Write( (byte)0x04 ); // Remove page

			m_Stream.Write( e.Sender.Serial );
		}
	}
}
