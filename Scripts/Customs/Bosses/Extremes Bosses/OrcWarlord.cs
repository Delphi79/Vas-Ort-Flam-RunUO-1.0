using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an orc boss corpse" )]
	public class OrcWarLord : BaseCreature
	{


		[Constructable]
		public OrcWarLord () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Lord Grom the Despoiler";
			Body = 138;
			BaseSoundID = 0x45A;
			Hue = 767;

			SetStr( 376, 405 );
			SetDex( 176, 195 );
			SetInt( 201, 225 );

			SetHits( 5000, 5000 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 465, 480 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 125.1, 150.0 );
			SetSkill( SkillName.EvalInt, 190.1, 200.0 );
			SetSkill( SkillName.Magery, 195.5, 200.0 );
			SetSkill( SkillName.Meditation, 125.1, 150.0 );
			SetSkill( SkillName.MagicResist, 90.5, 95.0 );
			SetSkill( SkillName.Tactics, 190.1, 200.0 );
			SetSkill( SkillName.Wrestling, 190.1, 200.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 90;
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

		public OrcWarLord( Serial serial ) : base( serial )
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

