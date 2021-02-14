using System;
using System.Collections;
using System.Diagnostics;
using Server;
using Server.Network;
using Server.Accounting;
using Server.Engines.Help;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Scripts.Commands;  

namespace Server.Admin
{
	public class RemoteAdminHandlers
	{
		public enum AcctSearchType : byte
		{
			Username = 0,
			IP = 1,
		}

		public enum ServerAction : byte
		{
			Save,
			RestartSave,
			RestartNoSave,
			ShutdownSave,
			ShutdownNoSave,
		}

		public enum PageAction : byte
		{
			None,
			Handle,
			Remove,
		}

		public struct RemoteAdminHandler
		{
			private OnPacketReceive m_PacketHandler;
			private AccessLevel m_AccessLevel;

			public RemoteAdminHandler( OnPacketReceive handler, AccessLevel accessLevel )
			{
				m_PacketHandler = handler;
				m_AccessLevel = accessLevel;
			}

			public OnPacketReceive PacketHandler { get { return m_PacketHandler; } set { m_PacketHandler = value; } }
			public AccessLevel AccessLevel { get { return m_AccessLevel; } set { m_AccessLevel = value; } }
		}

		public static Mobile ConfirmMobile( Account acc, int serial )
		{
			Mobile m = World.FindMobile( serial );
			if ( m != null && acc != null && m.Account == acc && m.AccessLevel >= acc.AccessLevel )
				return m;
			else
				return null;
		}

		private static RemoteAdminHandler[] m_Handlers = new RemoteAdminHandler[256];

		static RemoteAdminHandlers()
		{
			//0x02 = login request, handled by AdminNetwork
			Register( 0x04, new OnPacketReceive( ServerInfoRequest ), AccessLevel.Administrator );
			Register( 0x05, new OnPacketReceive( AccountSearch ), AccessLevel.Administrator );
			Register( 0x06, new OnPacketReceive( RemoveAccount ), AccessLevel.Administrator );
			Register( 0x07, new OnPacketReceive( UpdateAccount ), AccessLevel.Administrator );

			Register( 0x08, new OnPacketReceive( Pages ), AccessLevel.Counselor );
			Register( 0x09, new OnPacketReceive( ServerControl ), AccessLevel.Administrator );
			Register( 0x0A, new OnPacketReceive( Broadcast ), AccessLevel.GameMaster );
			Register( 0x0B, new OnPacketReceive( StaffChat ), AccessLevel.Counselor );
			Register( 0x0C, new OnPacketReceive( NoOp ), AccessLevel.Counselor );
		}

		public static void Register( byte command, OnPacketReceive handler, AccessLevel accessLevel )
		{
			m_Handlers[command].PacketHandler = handler;
			m_Handlers[command].AccessLevel = accessLevel;
		}

		public static bool Handle( byte command, NetState state, PacketReader pvSrc )
		{
			if ( m_Handlers[command].PacketHandler == null )
			{
				Console.WriteLine( "REMOTE: Invalid packet 0x{0:X2} from {1}, disconnecting", command, state );
				return false;
			}
			else
			{
				if ( state.Account != null && ((Account)state.Account).AccessLevel >= m_Handlers[command].AccessLevel )
					m_Handlers[command].PacketHandler( state, pvSrc );
				return true;
			}
		}

		private static void ServerInfoRequest( NetState state, PacketReader pvSrc )
		{
			state.Send( AdminNetwork.Compress( new ServerInfo() ) );
		}

		private static void AccountSearch( NetState state, PacketReader pvSrc )
		{
			AcctSearchType type = (AcctSearchType)pvSrc.ReadByte();
			string term = pvSrc.ReadString();

			if ( type == AcctSearchType.IP && !Utility.IsValidIP( term ) )
			{
				state.Send( new MessageBoxMessage( "Invalid search term.\nThe IP sent was not valid.", "Invalid IP" ) );
				return;
			}
			else
			{
				term = term.ToUpper();
			}

			ArrayList list = new ArrayList();

			foreach ( Account a in Accounts.Table.Values )
			{
				switch ( type )
				{
					case AcctSearchType.Username:
					{
						if ( a.Username.ToUpper().IndexOf( term ) != -1 )
							list.Add( a );
						break;
					}
					case AcctSearchType.IP:
					{
						for( int i=0;i<a.LoginIPs.Length;i++ )
						{
							if ( Utility.IPMatch( term, a.LoginIPs[i] ) )
							{
								list.Add( a );
								break;
							}
						}
						break;
					}
				}
			}

			if ( list.Count > 0 )
			{
				if ( list.Count <= 25 )
					state.Send( AdminNetwork.Compress( new AccountSearchResults( list ) ) );
				else
					state.Send( new MessageBoxMessage( "There were more than 25 matches to your search.\nNarrow the search parameters and try again.", "Too Many Results" ) );
			}
			else
			{
				state.Send( new MessageBoxMessage( "There were no results to your search.\nPlease try again.", "No Matches" ) );
			}
		}

		private static void RemoveAccount( NetState state, PacketReader pvSrc )
		{
			Account a = Accounts.GetAccount( pvSrc.ReadString() );

			if ( a == null )
			{
				state.Send( new MessageBoxMessage( "The account could not be found (and thus was not deleted).", "Account Not Found" ) );
			}
			else if ( a == state.Account )
			{
				state.Send( new MessageBoxMessage( "You may not delete your own account.", "Not Allowed" ) );
			}
			else
			{
				for (int i=0;i<a.Length;i++)
				{
					if ( a[i] != null )
						a[i].Delete();
				}

				Accounts.Table.Remove( a.Username );
				state.Send( new MessageBoxMessage( "The requested account (and all it's characters) has been deleted.", "Account Deleted" ) );
			}
		}

		private static void UpdateAccount( NetState state, PacketReader pvSrc )
		{ 
			string username = pvSrc.ReadString();
			string pass = pvSrc.ReadString();

			Account a = Accounts.GetAccount( username );

			if ( a == null )
				a = Accounts.AddAccount( username, pass );
			else if ( pass != "(hidden)" )
				a.SetPassword( pass );

			if ( a != state.Account )
			{
				a.AccessLevel = (AccessLevel)pvSrc.ReadByte();
				a.Banned = pvSrc.ReadBoolean();
			}
			else
			{
				pvSrc.ReadInt16();//skip both
				state.Send( new MessageBoxMessage( "Warning: When editing your own account, account status and accesslevel cannot be changed.", "Editing Own Account" ) );
			}
			
			ArrayList list = new ArrayList();
			ushort length = pvSrc.ReadUInt16();
			bool invalid = false;
			for (int i=0;i<length;i++)
			{
				string add = pvSrc.ReadString();
				if ( Utility.IsValidIP( add ) )
					list.Add( add );
				else
					invalid = true;
			}
			
			if ( list.Count > 0 )
				a.IPRestrictions = (string[])list.ToArray( typeof( string ) );
			else
				a.IPRestrictions = new string[0];

			if ( invalid )
				state.Send( new MessageBoxMessage( "Warning: one or more of the IP Restrictions you specified was not valid.", "Invalid IP Restriction" ) );
			state.Send( new MessageBoxMessage( "Account updated successfully.", "Account Updated" ) );
		}

		private static void Pages( NetState state, PacketReader pvSrc ) // 0x08
		{
			ArrayList list = PageQueue.List;
			int identifier = pvSrc.ReadInt32();
			byte subID = pvSrc.ReadByte();

			Mobile sender = World.FindMobile( identifier );
			if ( sender == null )
				return;

			PageEntry e = PageQueue.GetEntry( sender );
			if ( e == null ) // no such page
				return;

			switch ( subID )
			{
				case 0x01: // Handle
				{
					bool handle = pvSrc.ReadBoolean();
					Mobile from = RemoteAdminHandlers.ConfirmMobile( (Account)state.Account, pvSrc.ReadInt32() );
					if ( from != null && ( e.Handler == null || e.Handler == from ) )
						e.Handler = handle ? from : null;
					//Console.WriteLine( "Handle: {0} handler: {1}", handle, from != null ? from.Name : "none" );
					break;
				}
				case 0x02: // Remove
				{
					PageQueue.Remove( e );
					break;
				}
				case 0x03: // Predefined
				{
					byte messageIndex = pvSrc.ReadByte();
					Mobile from = RemoteAdminHandlers.ConfirmMobile( (Account)state.Account, pvSrc.ReadInt32() );
					ArrayList preresp = PredefinedResponse.List;

					if ( from != null && messageIndex >= 0 && messageIndex < preresp.Count )
						e.Sender.SendGump( new MessageSentGump( e.Sender, from.Name, ((PredefinedResponse)preresp[messageIndex]).Message ) );
					break;
				}
				case 0x04: // Answer
				{
					Mobile from = RemoteAdminHandlers.ConfirmMobile( (Account)state.Account, pvSrc.ReadInt32() );
					string message = pvSrc.ReadString();
					if ( from != null )
						e.Sender.SendGump( new MessageSentGump( e.Sender, from.Name, message ) );
					break;
				}
			}
		}

		private static void ServerControl( NetState state, PacketReader pvSrc ) // 0x09
		{
			bool save = false;
			bool restart = false;
			bool kill = false;

			ServerAction action = (ServerAction)pvSrc.ReadByte();
			switch (action)
			{
				case ServerAction.Save: save = true; break;
				case ServerAction.RestartSave: save = true; restart = true; kill = true; break;
				case ServerAction.RestartNoSave: restart = true; kill = true; break;
				case ServerAction.ShutdownSave: save = true; kill = true; break;
				case ServerAction.ShutdownNoSave: kill = true; break;
			}

			if ( save )
				AutoSave.Save();

#if MONO
			if ( restart )
				Process.Start( "mono", Core.ExePath );
#else
			if ( restart )
				Process.Start( Core.ExePath );
#endif

			if ( kill )
				Core.Process.Kill();
		}

		private static void Broadcast( NetState state, PacketReader pvSrc ) // 0x0A
		{
			Mobile from = RemoteAdminHandlers.ConfirmMobile( (Account)state.Account, pvSrc.ReadInt32() );
			string message = pvSrc.ReadUnicodeString();

			if ( from != null && message.Length > 0 )
			{
				World.Broadcast( 0x482, true, String.Format("Staff message from {0}:", from.Name) );
				World.Broadcast( 0x482, true, message );
			}
		}

		private static void StaffChat( NetState state, PacketReader pvSrc ) // 0x0B
		{
			Mobile from = RemoteAdminHandlers.ConfirmMobile( (Account)state.Account, pvSrc.ReadInt32() );
			string message = pvSrc.ReadUnicodeString();

			if ( from != null && message.Length > 0 )
				CommandHandlers.StaffChat( from, message );
		}

		private static void NoOp( NetState state, PacketReader pvSrc ) // 0x0C
		{
			// No Operation. Just to keep the connection alive.
		}
	}
}
