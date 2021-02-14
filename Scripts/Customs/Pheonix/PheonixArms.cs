using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13ee, 0x13ef )]
	public class PheonixArms : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 7; } }
		public override int BaseColdResistance{ get{ return 1; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 40; } }
		public override int OldStrReq{ get{ return 20; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 22; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Ringmail; } }

		[Constructable]
		public PheonixArms() : base( 0x13EE )
		{
			Weight = 15.0;
			Hue = 43;
			LootType = LootType.Blessed;
			Name = "Pheonix Arms";
		}

		public PheonixArms( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 15.0;
		}
	}
}