using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an evil womans corpse" )]
	public class Leith : BaseCreature
	{
		[Constructable]
		public Leith() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Leith";
			Title = "The Evil Queen";
			Body = 401;
			Hue = 6;
			Female = true;
			BaseSoundID = 1154;

			SetStr( 1800 );
			SetDex( 1510, 1750 );
			SetInt( 1710, 2200 );

			SetHits( 4500, 5000 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 0 );
			SetResistance( ResistanceType.Fire, 45 );
			SetResistance( ResistanceType.Cold, 45 );
			SetResistance( ResistanceType.Poison, 45 );
			SetResistance( ResistanceType.Energy, 45 );

			SetSkill( SkillName.EvalInt, 170.0 );
			SetSkill( SkillName.Magery, 170.0 );
			SetSkill( SkillName.Meditation, 170.0 );
			SetSkill( SkillName.MagicResist, 90.0 );
			SetSkill( SkillName.Tactics, 170.0 );
			SetSkill( SkillName.Wrestling, 170.0 );

       		       Nightmare mare = new Nightmare();
        	       mare.Hue = 1175;
        	       mare.Rider = this;


			Fame = 20000;
			Karma = -20000;

			VirtualArmor = 64;


        	AddItem( new FloppyHat( 1153 ) );
			AddItem( new FancyShirt( 1153 ) );
			AddItem( new Doublet( 1153 ) ); 
         	AddItem( new Skirt( 1153 ) ); 
         	AddItem( new Sandals( 1 ) );

			PackGem();
			
		}

		public override void GenerateLoot()
		{
			AddLoot(LootPack.OldSuperBoss, 2);
			AddLoot(LootPack.Gems);
		}
	
                        
                 



		public override bool Unprovokable{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool Uncalmable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public Leith( Serial serial ) : base( serial )
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