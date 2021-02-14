//////////////////////////////////////
/////  Advanced Pheonix Armor
///// By Viago
///////////////////////////////////////

using System;
using Server;

namespace Server.Items
{
	public class PheonixHelm : PlateHelm
	{
		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 1; } }
		public override int BasePoisonResistance{ get{ return 6; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public PheonixHelm()
		{
			Weight = 5.0;
			Name = "Pheonix Helm";
			Hue = 43;
			LootType = LootType.Blessed;
		}

		public PheonixHelm( Serial serial ) : base( serial )
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
				Weight = 5.0;
		}
	}
}