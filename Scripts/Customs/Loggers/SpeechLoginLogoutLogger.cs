using System;
using System.IO;
using Server;
using Server.Mobiles;
using Server.Network;
using System.Collections;
using Server.Accounting;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Engines.Help;
using Server.ContextMenus;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.Seventh;
using Server.Targeting;
using Server.Factions;
using Server.Regions;

namespace Server.Misc
{
    public class LogRecorder
    {
        private static bool m_Log = false;
        private static bool m_Listen = false;

        public static void Initialize()
        {
            //Login & Logout
            Server.Commands.Register("LogConversation", AccessLevel.Administrator, new CommandEventHandler(LogConversation_OnCommand));
            Server.Commands.Register("ListenToGlobal", AccessLevel.Administrator, new CommandEventHandler(ListenToGlobal_OnCommand));
            EventSink.Login += new LoginEventHandler(EventSink_Login);
            EventSink.Logout += new LogoutEventHandler(EventSink_Logout);
            EventSink.Speech += new SpeechEventHandler(OnSpeech);
            if (!Directory.Exists("logs")) Directory.CreateDirectory("logs");
            if (!Directory.Exists("logs/Login")) Directory.CreateDirectory("logs/Login");
            if (!Directory.Exists("logs/Logout")) Directory.CreateDirectory("logs/Logout");
            if (!Directory.Exists("logs/Chat")) Directory.CreateDirectory("logs/Chat");
        }
        private static void EventSink_Login(LoginEventArgs args)
        {
            Stream fileStream = null;
            StreamWriter writeAdapter = null;
            Mobile m = args.Mobile;
            try
            {
                fileStream = File.Open("logs/Login/" + args.Mobile.Name + ".log", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                writeAdapter = new StreamWriter(fileStream);
                writeAdapter.WriteLine(args.Mobile.Name + " " + DateTime.Now + "  Login");
                writeAdapter.Close();
            }
            catch
            {
                Console.WriteLine("Record Error......{0} Login", args.Mobile.Name);
                return;
            }
        }
        private static void EventSink_Logout(LogoutEventArgs args)
        {
            Stream fileStream = null;
            StreamWriter writeAdapter = null;
            Mobile m = args.Mobile;
            try
            {
                fileStream = File.Open("logs/Logout/" + args.Mobile.Name + ".log", FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                writeAdapter = new StreamWriter(fileStream);
                writeAdapter.WriteLine(args.Mobile.Name + " " + DateTime.Now + "  Logout");
                writeAdapter.Close();
            }
            catch
            {
                Console.WriteLine("Record Error......{0} Logout", args.Mobile.Name);
                return;
            }
        }
        public static void OnSpeech(SpeechEventArgs e)
        {

            string msg = String.Format("({0}): {1}", e.Mobile.Name, e.Speech);
            Stream fileStream = null;
            StreamWriter writeAdapter = null;

			//if (m_Listen)
			//{
			//    foreach (NetState state in NetState.Instances)
			//    {
			//        Mobile m = state.Mobile;
			//        PlayerMobile pm = (PlayerMobile)(m);
			//        if (pm.ListenToGlobal)
			//        {
			//            pm.SendMessage("<{0}> {1}", e.Mobile.Name, e.Speech);
			//        }
			//    } 
			//}

            if (m_Log)
            {
                try
                {
                    fileStream = File.Open(String.Format("logs/chat/{0}.log", DateTime.Now.ToLongDateString()), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    writeAdapter = new StreamWriter(fileStream);
                    writeAdapter.WriteLine("< " + msg + " >" + " at " + DateTime.Now);
                    writeAdapter.Close();
                }
                catch
                {
                    Console.WriteLine("Record Error......{0}", msg);
                    return;
                }
            }
        }

        public static void LogConversation_OnCommand(CommandEventArgs e)
        {
            if (m_Log)
                m_Log = false;
            else
                m_Log = true;

            string status;

            if(m_Log)
                status = "on";
            else
                status = "off";

            e.Mobile.SendMessage("LogConversation has been turned {0}", status);
        }

        public static void ListenToGlobal_OnCommand(CommandEventArgs e)
        {
            if (m_Listen)
                m_Listen = false;
            else
                m_Listen = true;
            
            string status;

            if(m_Listen)
                status = "on";
            else
                status = "off";

            PlayerMobile pm = (PlayerMobile)e.Mobile;

            pm.ListenToGlobal = m_Listen;

            e.Mobile.SendMessage("ListenToGlobal has been turned {0}", status);
        }
    }
}