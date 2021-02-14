using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Gumps
{
	public class HeroGump : Gump
	{
		public HeroGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(20, 34, 705, 390, 9270);
			this.AddImageTiled(45, 64, 656, 3, 9107);
			this.AddImageTiled(45, 391, 656, 3, 9107);
			this.AddImageTiled(90, 65, 2, 327, 9105);
			this.AddImageTiled(700, 65, 2, 329, 9105);
			this.AddLabel(113, 46, 10, @"Power");
			this.AddLabel(209, 46, 10, @"Words");
			this.AddLabel(391, 45, 10, @"Description");
			this.AddLabel(569, 46, 10, @"Sphere");
			this.AddLabel(633, 46, 10, @"Lifeforce");
			this.AddImageTiled(175, 65, 2, 329, 9105);
			this.AddImageTiled(289, 65, 2, 329, 9105);
			this.AddImageTiled(560, 65, 2, 328, 9105);
			this.AddImageTiled(621, 64, 2, 330, 9105);
			this.AddImageTiled(46, 106, 656, 3, 9107);
			this.AddImageTiled(45, 147, 656, 3, 9107);
			this.AddImageTiled(45, 188, 656, 3, 9107);
			this.AddImageTiled(46, 229, 656, 3, 9107);
			this.AddImageTiled(45, 270, 656, 3, 9107);
			this.AddImageTiled(45, 311, 656, 3, 9107);
			this.AddImageTiled(45, 351, 656, 3, 9107);
			this.AddImageTiled(45, 64, 2, 329, 9105);
			this.AddButton(53, 75, 4005, 4015, (int)Buttons.BlackArmor, GumpButtonType.Reply, 0);
			this.AddButton(53, 117, 4005, 4015, (int)Buttons.DetectGood, GumpButtonType.Reply, 0);
			this.AddButton(53, 159, 4005, 4015, (int)Buttons.VileBlade, GumpButtonType.Reply, 0);
			this.AddButton(53, 199, 4005, 4015, (int)Buttons.SummonFamiliar, GumpButtonType.Reply, 0);
			this.AddButton(53, 240, 4005, 4015, (int)Buttons.UnholySteed, GumpButtonType.Reply, 0);
			this.AddButton(53, 281, 4005, 4015, (int)Buttons.Blight, GumpButtonType.Reply, 0);
			this.AddButton(53, 321, 4005, 4015, (int)Buttons.MonsterIgnore, GumpButtonType.Reply, 0);
			this.AddButton(53, 361, 4005, 4015, (int)Buttons.UnholyWord, GumpButtonType.Reply, 0);
			this.AddLabel(94, 76, 1152, @"White Armor");
			this.AddLabel(95, 117, 1152, @"Detect Evil");
			this.AddLabel(106, 192, 1152, @"Summon");
			this.AddLabel(106, 207, 1152, @"Familiar");
			this.AddLabel(110, 233, 1152, @"Holy");
			this.AddLabel(112, 249, 1152, @"Steed");
			this.AddLabel(114, 281, 1152, @"Bless");
			this.AddLabel(107, 314, 1152, @"Holy");
			this.AddLabel(114, 330, 1152, @"Shield");
			this.AddLabel(110, 354, 1152, @"Holy");
			this.AddLabel(113, 370, 1152, @"Word");
			this.AddLabel(104, 158, 1152, @"Holy Blade");
			this.AddLabel(193, 76, 149, @"Vidda K'balc");
			this.AddLabel(184, 117, 149, @"Drewrok Velgo");
			this.AddLabel(184, 157, 149, @"Trubechs Vingir");
			this.AddLabel(190, 199, 149, @"Erstok Reyam");
			this.AddLabel(182, 242, 149, @"Trubechs Yeliab");
			this.AddLabel(193, 281, 149, @"Erstok Ontawl");
			this.AddLabel(195, 322, 149, @"Erstok K'blac");
			this.AddLabel(189, 361, 149, @"Erstok Oostrac");
			this.AddLabel(298, 64, 1152, @"Invoking this power turns equipment white ");
			this.AddLabel(298, 77, 1152, @"When unequipped, the item returns to");
			this.AddLabel(298, 89, 1152, @"its natural color.");
			this.AddLabel(589, 76, 149, @"1");
			this.AddLabel(657, 76, 92, @"1");
			this.AddLabel(297, 110, 1152, @"This power returns information on the ");
			this.AddLabel(298, 123, 1152, @"proximity of evils.");
			this.AddLabel(297, 192, 1152, @"This conjures up a white wolf to serve");
			this.AddLabel(298, 207, 1152, @"as a pet for a period of time.");
			this.AddLabel(298, 151, 1152, @"Causes the selected weapon to do");
			this.AddLabel(299, 165, 1152, @"double damage to evils & undead.");
			this.AddLabel(296, 233, 1152, @"This conjures up a mystical white steed");
			this.AddLabel(297, 247, 1152, @"to serve as the your mount.");
			this.AddLabel(297, 274, 1152, @"This targetable area effect power a temp");
			this.AddLabel(297, 288, 1152, @"boost of stats to all innocents in range.");
			this.AddLabel(296, 314, 1152, @"This creates a shield that evil's and");
			this.AddLabel(296, 327, 1152, @"monsters cannot pass through");
			this.AddLabel(296, 350, 1152, @"This allows you to target a evil and simply ");
			this.AddLabel(298, 362, 1152, @"kill them dead.  You will recieve 0 points");
			this.AddLabel(298, 374, 1152, @"for this kill.");
			this.AddLabel(588, 118, 149, @"2");
			this.AddLabel(588, 159, 149, @"3");
			this.AddLabel(588, 200, 149, @"4");
			this.AddLabel(588, 241, 149, @"5");
			this.AddLabel(588, 280, 149, @"6");
			this.AddLabel(588, 321, 149, @"7");
			this.AddLabel(588, 362, 149, @"8");
			this.AddLabel(656, 118, 92, @"0");
			this.AddLabel(656, 159, 92, @"5");
			this.AddLabel(656, 200, 92, @"10");
			this.AddLabel(656, 241, 92, @"30");
			this.AddLabel(656, 280, 92, @"15");
			this.AddLabel(656, 321, 92, @"20");
			this.AddLabel(656, 362, 92, @"100");

		}
		
		public enum Buttons
		{
			BlackArmor = 0,
			DetectGood = 1,
			VileBlade = 2,
			SummonFamiliar = 3,
			UnholySteed = 4,
			Blight = 5,
			MonsterIgnore = 6,
			UnholyWord = 7,
		}

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
				case 1:
                {
					from.CloseGump(typeof(EvilGump));
                    break;
                }

				case 2:
                {
					from.CloseGump(typeof(EvilGump));
                    break;
                }

				case 3:
                {
					from.CloseGump(typeof(EvilGump));
                    break;
                }

                case 4:
                {
					from.CloseGump(typeof(EvilGump));
                    break;
                }

                case 5:
                {
					from.CloseGump(typeof(EvilGump));
                    break;
                }

                case 6:
                {
					from.CloseGump(typeof(EvilGump));
                    break;
                }

                case 7:
                {
					from.CloseGump(typeof(EvilGump));
                    break;
                }

                case 8:
                {
					from.CloseGump(typeof(EvilGump));
                    break;
                }
            }
        }
	}
}