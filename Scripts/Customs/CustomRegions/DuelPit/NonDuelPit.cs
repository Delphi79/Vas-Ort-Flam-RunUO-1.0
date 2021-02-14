using System;
using Server;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fourth;
using Server.Spells.Sixth;
using Server.Factions;

namespace Server.Regions
{
	public class NonDuelPit : GuardedRegion
	{
		public static void Initialize()
		{
			Region.AddRegion(new NonDuelPit(Map.Felucca));
			EventSink.Logout += new LogoutEventHandler(EventSink_Logout);
			EventSink.Login += new LoginEventHandler(EventSink_Login);
		}

		public NonDuelPit(Map map)
			: base("", "NonDuelPit", map, typeof(WarriorGuard))
		{
		}

		private static void EventSink_Logout(LogoutEventArgs e)
		{
			Mobile m = e.Mobile;
			if (m.Region.Name == "NonDuelPit")
			{
				m.MoveToWorld(new Point3D(1416, 1700, 1), Map.Felucca);
				m.Blessed = false;
			}
		}

		private static void EventSink_Login(LoginEventArgs e)
		{
			Mobile m = e.Mobile;
			if (m.Region.Name == "NonDuelPit")
			{
				m.MoveToWorld(new Point3D(1416, 1700, 1), Map.Felucca);
				m.Blessed = false;
			}
		}

		public override bool AllowHousing(Mobile from, Point3D p)
		{
			if (from.AccessLevel == AccessLevel.Player)
				return false;
			else
				return true;
		}

		public override void OnEnter(Mobile m)
		{
			m.Blessed = true;
		}

		public override void OnExit(Mobile m)
		{
			m.Blessed = false;
		}

		public override bool AllowReds { get { return true; } }

		public override bool OnBeginSpellCast(Mobile m, ISpell s)
		{
			//Console.WriteLine("0");
			m.SendMessage("You can not cast that here");
			return false;
		}
	}
}
