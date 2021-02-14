// Created by Script Creator

using System;
using Server.Items;
using Server.Engines.Plants;

namespace Server.Mobiles
{
	[CorpseName(" corpse of the undead summon balron")]
	public class UndeadBalron : Balron
	{
		[Constructable]
		public UndeadBalron()
			: base()
		{
			Name = "an undead summoned balron";
			BodyValue = 38;
			SetStr(200);
			SetDex(100);
			SetInt(1000);
			SetHits(30000);
			SetDamage(90);
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
			Fame = 9999;
			Karma = -9999;
			VirtualArmor = 200;

			ControlSlots = 6;
			PackGold(5000, 10000);
			PackItem(new Longsword());

		}

        public override void GenerateLoot()
        {
            if (Utility.Random(100) <= 20)
            {
                int hue = Utility.RandomMinMax(1, 1001);

                Sandals sandals = new Sandals();

                sandals.Hue = hue;

                PackItem(sandals);
            }

            if (Utility.Random(100) <= 10)
            {
                int hue = Utility.RandomMinMax(1, 1001);

                Sandals sandals = new Sandals();

                sandals.Hue = hue;

                PackItem(sandals);
            }

            if (Utility.Random(100) <= 5)
            {
                int hue = Utility.RandomMinMax(1, 1001);

                Sandals sandals = new Sandals();

                sandals.Hue = hue;

                PackItem(sandals);
            }

            if (Utility.Random(100) <= 5)
            {
                int hue = Utility.RandomMinMax(1201, 1255);

                Sandals sandals = new Sandals();

                sandals.Hue = hue;

                PackItem(sandals);
            }

            if (Utility.Random(100) <= 2)
            {
                int hue = Utility.RandomMinMax(2101, 2130);

                Sandals sandals = new Sandals();

                sandals.Hue = hue;

                PackItem(sandals);
            }


            if (Utility.Random(100) <= 1)
            {
                int hue = 2936;

                Sandals sandals = new Sandals();

                sandals.Hue = hue;

                PackItem(sandals);
            }

            if (Utility.Random(1000) <= 2)
            {
                int hue;
                Sandals sandals = new Sandals();

                switch (Utility.Random(7))
                {
                    case 0: hue = 1155; break;
                    case 1: hue = 1156; break;
                    case 2: hue = 1157; break;
                    case 3: hue = 1158; break;
                    case 4: hue = 1160; break;
                    case 5: hue = 1172; break;
                    default: hue = 1175; break;
                }

                sandals.Hue = hue;
                PackItem(sandals);
            }

            if (Utility.Random(1000) <= 2)
            {
                int hue;
                Sandals sandals = new Sandals();

                switch (Utility.Random(5))
                {
                    case 0: hue = 1176; break;
                    case 1: hue = 1171; break;
                    case 2: hue = 1159; break;
                    case 3: hue = 1170; break;
                    default: hue = 1161; break;
                }

                sandals.Hue = hue;
                PackItem(sandals);
            }


            double chance = Utility.RandomDouble();

            if (chance <= 0.10)
            {
                Seed seed;

                PlantType type;
                switch (Utility.Random(17))
                {
                    case 0: type = PlantType.CampionFlowers; break;
                    case 1: type = PlantType.Poppies; break;
                    case 2: type = PlantType.Snowdrops; break;
                    case 3: type = PlantType.Bulrushes; break;
                    case 4: type = PlantType.Lilies; break;
                    case 5: type = PlantType.PampasGrass; break;
                    case 6: type = PlantType.Rushes; break;
                    case 7: type = PlantType.ElephantEarPlant; break;
                    case 8: type = PlantType.Fern; break;
                    case 9: type = PlantType.PonytailPalm; break;
                    case 10: type = PlantType.SmallPalm; break;
                    case 11: type = PlantType.CenturyPlant; break;
                    case 12: type = PlantType.WaterPlant; break;
                    case 13: type = PlantType.SnakePlant; break;
                    case 14: type = PlantType.PricklyPearCactus; break;
                    case 15: type = PlantType.BarrelCactus; break;
                    default: type = PlantType.TribarrelCactus; break;
                }

                PlantHue hue;
                switch (Utility.Random(19))
                {
                    case 0: hue = PlantHue.Plain; break;
                    case 1: hue = PlantHue.Red; break;
                    case 2: hue = PlantHue.Blue; break;
                    case 3: hue = PlantHue.Yellow; break;
                    case 4: hue = PlantHue.BrightRed; break;
                    case 5: hue = PlantHue.BrightBlue; break;
                    case 6: hue = PlantHue.BrightYellow; break;
                    case 7: hue = PlantHue.Purple; break;
                    case 8: hue = PlantHue.Green; break;
                    case 9: hue = PlantHue.Orange; break;
                    case 10: hue = PlantHue.BrightPurple; break;
                    case 11: hue = PlantHue.BrightGreen; break;
                    case 12: hue = PlantHue.BrightOrange; break;
                    case 13: hue = PlantHue.Black; break;
                    case 14: hue = PlantHue.White; break;
                    case 15: hue = PlantHue.Pink; break;
                    case 16: hue = PlantHue.Magenta; break;
                    case 17: hue = PlantHue.Aqua; break;
                    default: hue = PlantHue.FireRed; break;
                }

                seed = new Seed(type, hue, false);
                PackItem(seed);

                if (Utility.Random(500) == 1)
                {
                    PackItem(new BlackDyeTub());
                }

                if (Utility.Random(100) < 15)
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

                if (Utility.Random(100) < 10)
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
        }

		public override bool AutoDispel { get { return true; } }
		public override bool BardImmune { get { return true; } }
		public override bool Unprovokable { get { return true; } }
		public override Poison HitPoison { get { return Poison.Lethal; } }

		public UndeadBalron(Serial serial)
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
