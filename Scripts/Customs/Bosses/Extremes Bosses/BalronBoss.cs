using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a balron boss corpse" )]
	public class BalronBoss : BaseCreature
	{
		[Constructable]
		public BalronBoss () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "King Of Hythloth";
			Body = 40;
			BaseSoundID = 357;
			Hue = 1174;

			SetStr( 376, 405 );
			SetDex( 176, 195 );
			SetInt( 201, 225 );


			SetHits( 4500, 5000 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 45, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 125.1, 150.0 );
			SetSkill( SkillName.EvalInt, 290.1, 300.0 );
			SetSkill( SkillName.Magery, 295.5, 300.0 );
			SetSkill( SkillName.Meditation, 225.1, 250.0 );
			SetSkill( SkillName.MagicResist, 95.5, 100.0 );
			SetSkill( SkillName.Tactics, 190.1, 200.0 );
			SetSkill( SkillName.Wrestling, 190.1, 200.0 );

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

		public BalronBoss( Serial serial ) : base( serial )
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

