using System;
using Server;
using Server.Items;
using System.Net;
using System.Text;
using System.Collections;
using System.Diagnostics;
using Server.Prompts;
using Server.Network;
using Server.Accounting;
using Server.Scripts.Commands;
using Server.Gumps;
using Server.Misc;
using Server.Guilds;
using Server.Factions;
using Server.Mobiles;

namespace Server.Gumps
{

	public class RankSort : IComparable
	{
		public Mobile Killer;
		public Faction KillPoints;
		public int Points;


		public RankSort(Mobile m, Faction attachment)
		{
			Killer = m;
			KillPoints = attachment;
			PlayerState pl = PlayerState.Find(m);
			if (pl != null) Points = pl.KillPoints;
		}

		public int CompareTo(object obj)
		{
			RankSort p = (RankSort)obj;

			return p.Points - Points;
		}
	}



	public class FactionPlayerGump : Gump
	{
		public Mobile m_From;
		public FactionPlayerGump m_PageType;
		public ArrayList m_List;

		private const int LabelColor = 0x7FFF;
		private const int SelectedColor = 0x421F;
		private const int DisabledColor = 0x4210;

		private const int LabelColor32 = 0xFFFFFF;
		private const int SelectedColor32 = 0x8080FF;
		private const int DisabledColor32 = 0x808080;

		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;

		public FactionPlayerGump(Mobile from, FactionPlayerGump pageType, ArrayList list, int listPage, string notice, object state)
			: base(50, 40)
		{
			from.CloseGump(typeof(FactionPlayerGump));

			m_List = list;
			m_From = from;
			m_PageType = pageType;

			ArrayList playerlist = new ArrayList();

			foreach (Mobile m in World.Mobiles.Values)
			{
				PlayerMobile pm = (PlayerMobile)(m);

				if (m.Player && (Faction.Find(m) != null) && pm.FactionKillPoints != 0)
				{					
					Faction theirfaction = Faction.Find(m);
					playerlist.Add(new RankSort(m, theirfaction));
				}
			}

			for (int i = 0; i < playerlist.Count; i++)
			{
				if (i > 9)
					break;

				RankSort p = playerlist[i] as RankSort;

			}



			AddPage(0);

			AddBackground(0, 0, 420, 540, 5054);

			AddBlackAlpha(10, 10, 400, 520);

			if (notice != null)
				AddHtml(12, 392, 396, 36, Color(notice, LabelColor32), false, false);


			AddLabel(142, 15, RedHue, "Top 20 Faction Players");
			AddLabel(20, 40, LabelHue, "Players");
			AddLabel(175, 40, LabelHue, "Faction");
			AddLabel(345, 40, LabelHue, "Killpoints");

			playerlist.Sort();

			for (int i = 0; i < playerlist.Count; ++i)
			{
				if (i >= 20)
				{

					break;

				}

				RankSort g = (RankSort)playerlist[i];

				string name = null;

				if ((name = g.Killer.Name) != null && (name = name.Trim()).Length <= 15)
					name = g.Killer.Name;

				string factionname = null;

				if (g.Killer is PlayerMobile && ((PlayerMobile)g.Killer).FactionPlayerState != null)
					factionname = ((PlayerMobile)g.Killer).FactionPlayerState.Faction.ToString();

				string factionkills = null;

				if (g.Killer is PlayerMobile && ((PlayerMobile)g.Killer).FactionPlayerState != null)
					factionkills = ((PlayerMobile)g.Killer).FactionPlayerState.KillPoints.ToString();

				AddLabel(20, 70 + ((i % 20) * 20), GreenHue, name);
				AddLabel(175, 70 + ((i % 20) * 20), GreenHue, factionname);
				AddLabel(345, 70 + ((i % 20) * 20), GreenHue, factionkills);
			}
		}

		public string Color(string text, int color)
		{
			return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
		}

		public void AddBlackAlpha(int x, int y, int width, int height)
		{
			AddImageTiled(x, y, width, height, 2624);
			AddAlphaRegion(x, y, width, height);
		}
	}
}
