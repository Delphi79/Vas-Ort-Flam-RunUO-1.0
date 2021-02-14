using System;
using Server;
using System.Collections;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fourth;
using Server.Spells.Sixth;

namespace Server.Regions
{
    public class DuelPit : Region
    {
        public static void Initialize()
        {
            Region.AddRegion(new DuelPit(Map.Felucca));
        }

        public DuelPit(Map map) : base("", "DuelPit", map)
        {

        }

		public override bool AllowHousing(Mobile from, Point3D p)
        {
            if (from.AccessLevel == AccessLevel.Player)
                return false;
            else
                return true;
        }

        private static Layer[] EquipmentLayers = new Layer[]
		{
			Layer.Cloak,
			Layer.Bracelet,
			Layer.Ring,
			Layer.Shirt,
			Layer.Pants,
			Layer.InnerLegs,
			Layer.Shoes,
			Layer.Arms,
			Layer.InnerTorso,
			Layer.MiddleTorso,
			Layer.OuterLegs,
			Layer.Neck,
			Layer.Waist,
			Layer.Gloves,
			Layer.OuterTorso,
			Layer.OneHanded,
			Layer.TwoHanded,
			Layer.FacialHair,
			Layer.Hair,
			Layer.Helm
		};

        public override void OnSpeech(SpeechEventArgs e)
        {
            Mobile m = e.Mobile;

            if (e.HasKeyword(0x0002))
            {
                e.Handled = true;
            }
        }

        public override void OnEnter(Mobile m)
        {
            if (m is PlayerMobile && m.AccessLevel < AccessLevel.Counselor)
            {
                PlayerMobile pm = (PlayerMobile)m;
                if (!m.Alive)
                    return;

                Container bp = pm.Backpack;
                Container bankbag = new Bag();
                bankbag.Hue = 38;
                BankBox bank = pm.BankBox;
                Item oncurs = pm.Holding;
                Item[] items = bp.FindItemsByType(typeof(Item));

                if (oncurs != null)
                    bp.DropItem(oncurs);

                foreach (Layer layer in EquipmentLayers)
                {
                    Item item = pm.FindItemOnLayer(layer);

                    if (item != null)
                    {
                        if (item.LootType == LootType.Newbied)
                        {
                            pm.DuelItems.Add(item);
                        }
                    }
                }

                foreach (Item i in items)
                {
                    if (i.LootType == LootType.Newbied)
                    {
                        pm.DuelItems.Add(i);
                    }
                }

                foreach (Item i in pm.DuelItems)
                {
                    i.LootType = LootType.Blessed;
                }
            }
        }

        public override void OnExit(Mobile m)
        {
            if (m is PlayerMobile)
			{
                PlayerMobile pm = (PlayerMobile)m;

                foreach (Item i in pm.DuelItems)
                {
                    i.LootType = LootType.Newbied;
                }
            }
        }

        public override bool OnBeginSpellCast(Mobile m, ISpell s)
        {
            return true;
        }
    }        
}
