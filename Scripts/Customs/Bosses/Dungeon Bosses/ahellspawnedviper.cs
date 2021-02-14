// Created by Script Creator

using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName(" corpse of a hellspawned viper")]
    public class ahellspawnedviper : SilverSerpent
    {
        [Constructable]
        public ahellspawnedviper()
            : base()
        {
            Name = "a hellspawned viper";
            Hue = 1256;
            SetStr(800);
            SetDex(300);
            SetInt(225);
            SetHits(6500);
            SetDamage(65);
            SetDamageType(ResistanceType.Physical, 100);
            SetDamageType(ResistanceType.Cold, 0);
            SetDamageType(ResistanceType.Fire, 0);
            SetDamageType(ResistanceType.Energy, 0);
            SetDamageType(ResistanceType.Poison, 0);

            SetResistance(ResistanceType.Physical, 100);
            SetResistance(ResistanceType.Cold, 0);
            SetResistance(ResistanceType.Fire, 0);
            SetResistance(ResistanceType.Energy, 0);
            SetResistance(ResistanceType.Poison, 0);
            Fame = 24000;
            Karma = -24000;
            VirtualArmor = 200;

            ControlSlots = 0;
            PackGold(4000, 6000);

        }
        public override bool HasBreath { get { return true; } }
        public override bool AutoDispel { get { return true; } }
        public override bool BardImmune { get { return true; } }
        public override bool Unprovokable { get { return true; } }

        public override void GenerateLoot()
        {
            if (Utility.Random(100) < 40)
            {
                Lantern lantern = new Lantern();
                lantern.Hue = Utility.RandomMinMax(2107, 2112);
                PackItem(lantern);
            }

            if (Utility.Random(100) < 40)
            {
                Item item;

                switch (Utility.Random(111))
                {
                    case 0: item = new Tapestry6W(); break;
                    case 1: item = new MetalChest(); break;
                    case 2: item = new MetalGoldenChest(); break;
                    case 3: item = new MetalBox(); break;
                    case 4: item = new AniLargeVioletFlask(); break;
                    case 5: item = new AniRedRibbedFlask(); break;
                    case 6: item = new AniSmallBlueFlask(); break;
                    case 7: item = new BlueBeaker(); break;
                    case 8: item = new BlueCurvedFlask(); break;
                    case 9: item = new EmptyVial(); break;
                    case 10: item = new EmptyVialsWRack(); break;
                    case 11: item = new LargeFlask(); break;
                    case 12: item = new LargeVioletFlask(); break;
                    //case 13: item = new HourGlass(); break;
                    //case 14: item = new HourGlassAni(); break;
                    case 15: item = new DecorativeDAxeWest(); break;
                    case 16: item = new DecorativeDAxeNorth(); break;
                    case 17: item = new DecorativeBowWest(); break;
                    case 18: item = new DecorativeBowNorth(); break;
                    case 19: item = new DecorativeAxeWest(); break;
                    case 20: item = new DecorativeAxeNorth(); break;
                    case 21: item = new DecorativeShieldSword2West(); break;
                    case 22: item = new DecorativeShieldSword2North(); break;
                    case 23: item = new DecorativeShieldSword1West(); break;
                    case 24: item = new DecorativeShieldSword1North(); break;
                    case 25: item = new DecorativeShield11(); break;
                    case 26: item = new DecorativeShield10(); break;
                    case 27: item = new DecorativeShield9(); break;
                    case 28: item = new DecorativeShield8(); break;
                    case 29: item = new DecorativeShield7(); break;
                    case 30: item = new DecorativeShield6(); break;
                    case 31: item = new DecorativeShield5(); break;
                    case 32: item = new DecorativeShield4(); break;
                    case 33: item = new DecorativeShield3(); break;
                    case 34: item = new DecorativeShield2(); break;
                    case 35: item = new DecorativeShield1(); break;
                    case 36: item = new Whip(); break;
                    case 37: item = new SilverWire(); break;
                    case 38: item = new Rope(); break;
                    case 39: item = new PaintsAndBrush(); break;
                    case 40: item = new IronWire(); break;
                    case 41: item = new GoldWire(); break;
                    case 42: item = new CopperWire(); break;
                    case 43: item = new WoodDebris(); break;
                    case 44: item = new RuinedPainting(); break;
                    case 45: item = new RuinedFallenChairB(); break;
                    case 46: item = new FullVialsWRack(); break;
                    case 47: item = new FullJar(); break;
                    case 48: item = new FullJars2(); break;
                    case 49: item = new FullJars3(); break;
                    case 50: item = new FullJars4(); break;
                    case 51: item = new GreenBeaker(); break;
                    case 52: item = new GreenBottle(); break;
                    case 53: item = new VioletStemmedBottle(); break;
                    case 54: item = new SpinningHourglass(); break;
                    case 55: item = new ArcheryButte(); break;
                    case 56: item = new PhillipsWoodenSteed(); break;
                    case 57: item = new PileOfGlacialSnow(); break;
                    case 58: item = new RedPoinsettia(); break;
                    case 59: item = new RoseOfTrinsic(); break;
                    case 60: item = new RaiseSwitch(); break;
                    case 61: item = new SpecialFishingNet(); break;
                    case 62: item = new SpecialHairDye(); break;
                    case 63: item = new SpecialBeardDye(); break;
                    case 64: item = new SnowPile(); break;
                    case 65: item = new StatueEast2(); break;
                    case 66: item = new StatuePegasus2(); break;
                    case 67: item = new StatueSouth2(); break;
                    case 68: item = new StatueSouthEast(); break;
                    case 69: item = new StatuetteDyeTub(); break;
                    case 70: item = new StatueWest(); break;
                    case 71: item = new TapestryOfSosaria(); break;
                    case 72: item = new WhitePoinsettia(); break;
                    case 73: item = new WindChimes(); break;
                    case 74: item = new ZoogiFungus(); break;
                    case 75: item = new RuinedFallenChairA(); break;
                    case 76: item = new RuinedDrawers(); break;
                    case 77: item = new RuinedClock(); break;
                    case 78: item = new RuinedChair(); break;
                    case 79: item = new RuinedBooks(); break;
                    case 80: item = new RuinedBookcase(); break;
                    case 81: item = new RuinedArmoire(); break;
                    case 82: item = new SmallStretchedHideSouthDeed(); break;
                    case 83: item = new SmallStretchedHideEastDeed(); break;
                    case 84: item = new PolarBearRugSouthDeed(); break;
                    case 85: item = new PolarBearRugEastDeed(); break;
                    case 86: item = new MediumStretchedHideSouthDeed(); break;
                    case 87: item = new MediumStretchedHideEastDeed(); break;
                    case 88: item = new LightFlowerTapestrySouthDeed(); break;
                    case 89: item = new LightFlowerTapestryEastDeed(); break;
                    case 90: item = new DarkFlowerTapestrySouthDeed(); break;
                    case 91: item = new DarkFlowerTapestryEastDeed(); break;
                    case 92: item = new BrownBearRugSouthDeed(); break;
                    case 93: item = new BrownBearRugEastDeed(); break;
                    case 94: item = new WallSconce(); break;
                    case 95: item = new WallTorch(); break;
                    case 96: item = new CandleShort(); break;
                    case 97: item = new CandleLarge(); break;
                    case 98: item = new DecorativeSwordNorth(); break;
                    case 99: item = new DecorativeSwordWest(); break;
                    case 100: item = new Tapestry1N(); break;
                    case 101: item = new Tapestry2N(); break;
                    case 102: item = new Tapestry2W(); break;
                    case 103: item = new Tapestry3N(); break;
                    case 104: item = new Tapestry3W(); break;
                    case 105: item = new Tapestry4N(); break;
                    case 106: item = new Tapestry4W(); break;
                    case 107: item = new Tapestry5N(); break;
                    case 108: item = new Tapestry5W(); break;
                    case 109: item = new Tapestry6N(); break;
                    default: item = new CandleSkull(); break;
                }

                item.LootType = LootType.Regular;
                item.Movable = true;
                PackItem(item);
            }
        }

        public ahellspawnedviper(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
