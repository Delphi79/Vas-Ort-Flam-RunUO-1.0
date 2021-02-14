using System;
using System.Collections;
using Server;
using Server.Mobiles;

namespace Server.Misc
{
	public class HearAll
	{
		private static bool m_ConsolePrint;
		private static ArrayList m_HearAll = new ArrayList();

		public static void Initialize()
		{
			Commands.Register( "HearAll", AccessLevel.Administrator, new CommandEventHandler( HearAll_OnCommand ) );
			Commands.Register( "ConsoleHearAll", AccessLevel.Administrator, new CommandEventHandler( ConsoleHearAll_OnCommand ) );

			EventSink.Speech += new SpeechEventHandler( OnSpeech );
		}

		private static void OnSpeech( SpeechEventArgs args )
		{
			string msg;
			if ( args.Mobile.Region.Name.Length > 0 )
				msg = String.Format( "{0} ({1}): {2}", args.Mobile.Name, args.Mobile.Region.Name, args.Speech );
			else
				msg = String.Format( "{0}: {1}", args.Mobile.Name, args.Speech );
			if ( m_ConsolePrint )
				Console.WriteLine( msg );

			ArrayList remove = null;
			for(int i=0;i<m_HearAll.Count;i++)
			{
				if ( ((Mobile)m_HearAll[i]).NetState == null )
				{
					if ( remove == null )
						remove = new ArrayList( 1 );
					remove.Add( m_HearAll[i] );
				}
				else
				{
					((Mobile)m_HearAll[i]).SendMessage( msg );
				}
			}

			if ( remove != null )
			{
				for(int i=0;i<remove.Count;i++)
					m_HearAll.Remove( remove[i] );
			}
		}

		private static void HearAll_OnCommand( CommandEventArgs args )
		{
			if ( m_HearAll.Contains( args.Mobile ) )
			{
				m_HearAll.Remove( args.Mobile );
				args.Mobile.SendMessage( "Hear all disabled." );
			}
			else
			{
				m_HearAll.Add( args.Mobile );
				args.Mobile.SendMessage( "Hear all enabled, type [hearall again to disable it." );
			}
		}

		private static void ConsoleHearAll_OnCommand( CommandEventArgs args )
		{
			m_ConsolePrint = !m_ConsolePrint;

			if ( m_ConsolePrint )
				args.Mobile.SendMessage( "Now sending all speech to the console." );
			else
				args.Mobile.SendMessage( "No longer sending speech to the console." );
		}
	}
}

