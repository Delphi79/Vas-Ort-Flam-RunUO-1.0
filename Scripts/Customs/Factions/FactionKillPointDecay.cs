#region AuthorHeader
//
//	Motd.cs version 1.1, by Xanthos
//
//	This is a RunUO assembly to display a message of the day (motd) gump
//	at login.  It supports archiving and display of previous motd content.
//	You may use this code as you please as long as you leave this AuthorHeader
//	in place.
//
#endregion AuthorHeader
#region Header
//
//	Using this is pretty simple.  Place this file in your custom directory.
//	It will create a Motd subdirectory in your data directory and an Archive
//	directory beneath that.  Place a file named motd.txt in the Motd sub-
//	directory, enter the game as an admin and type [motd.  Select the reload
//	button and all players will see the new motd on login. Once the motd is
//	presented to a player, their account is tagged so they will not see it
//	again on login unless the Reload button is used as described above.
//	Players may, at any time, see the motd by typing motd. The Reload
//	option allows an admin to make small changes to the motd and then get
//	them brought into the motd cache without a restart of the server.
//
//	The [motd admin command also allows archiving of the current motd.txt
//	into the Archive directory and replacing the existing motd file with one 
//	named new.txt.  This action will also tag all accounts so that the motd
//	will be displayed at login.  The archives are given names in sequence
//	starting at 1.txt.  The current motd, when archived is always given the
//	name 1.txt after any older files are propagated to higher numbered names.
//	By default, a maximum of nine archives will be shown in the gump.  The
//	number of archives maintained in the Archive directory is limited only
//	by the file system.  Archiving may take longer as the number of files
//	increases however.  If an motd.txt file is not present when the Reload
//	or Archive functions are used, all accounts are tagged so that the motd
//	will not be displayed.
//
//	There are a number of static variables that can be used to customize
//	aspects of this program.  In most cases no modifications are needed.
//
//	This work was done by Xanthos, based on code written by Viago and others, 
//	see the credits page for more details.
//	- Xanthos.
//
//	editor tabs = 4
//
#endregion Header
using System;
using System.IO;
using System.Text;
using System.Threading;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Factions.FactionDecay
{
	public class FactionDecay
	{
		private static Timer m_Timer;

		public static void Initialize()
		{
			m_Timer = Timer.DelayCall(TimeSpan.FromHours(20.0), TimeSpan.FromMinutes(20.0), new TimerCallback(DecayFactionKills));
		}

		public static void DecayFactionKills()
		{
			Console.WriteLine("Faction Decay Initiated");
			int playersDecayed = 0;
			foreach (Mobile m in World.Mobiles.Values)
			{			
				if (m.Player && (Faction.Find(m) != null))
				{					
					PlayerMobile pm = m as PlayerMobile;
					if (pm.FactionPlayerState.KillPoints > 0)
					{
						pm.FactionPlayerState.KillPoints -= 1;
						playersDecayed++;
					}
				}
			}
			Console.WriteLine("Number of players who received faction killpoint decay = {0}", playersDecayed);
		}
	}
}