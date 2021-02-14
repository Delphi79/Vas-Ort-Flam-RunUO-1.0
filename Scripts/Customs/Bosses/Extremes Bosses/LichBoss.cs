using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a lich boss corpse" )]
	public class LichBoss : BaseCreature
	{
		[Constructable]
		public LichBoss () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "King Of Deceit";
			Body = 78;
			BaseSoundID = 357;
			Hue = 1150;

			SetStr( 1000, 1000 );
			SetDex( 300, 300 );
			SetInt( 500, 600 );

			SetHits( 4500, 5000 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 190.1, 200.0 );
			SetSkill( SkillName.Magery, 295.5, 300.0 );
			SetSkill( SkillName.Meditation, 225.1, 350.0 );
			SetSkill( SkillName.MagicResist, 90.5, 95.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 90;


			if( Utility.Random( 200 ) == 1 ) {
    			PackItem( new HolyBlessDeed() );
		}
	}

		public override void GenerateLoot()
		{
			AddLoot(LootPack.OldSuperBoss, 2);
			AddLoot(LootPack.Gems);
		}		

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public LichBoss( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}

