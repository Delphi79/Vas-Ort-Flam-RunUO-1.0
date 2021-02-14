using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Evil Lord corpse" )]
	public class IAmTheBoss : BaseCreature
	{
		[Constructable]
		public IAmTheBoss () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Evil Lord";
			Body = 400;
			Hue = 1002;
			BaseSoundID = 357;
			
			SetStr( 376, 405 );
			SetDex( 176, 195 );
			SetInt( 201, 225 );

			SetHits( 5000, 5000 );


			SetHits( 1592, 1711 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 125.1, 150.0 );
			SetSkill( SkillName.EvalInt, 120.1, 150.0 );
			SetSkill( SkillName.Magery, 125.5, 150.0 );
			SetSkill( SkillName.Meditation, 125.1, 150.0 );
			SetSkill( SkillName.MagicResist, 90.5, 95.0 );
			SetSkill( SkillName.Tactics, 120.1, 150.0 );
			SetSkill( SkillName.Wrestling, 120.1, 150.0 );

       		       	Nightmare mare = new Nightmare();
        	       	mare.Hue =  1175;
        	       	mare.Rider = this;


			Fame = 0;
			Karma = 0;

			VirtualArmor = 90;

        		AddItem( new Bonnet( 998 ) );
			AddItem( new FancyShirt( 998 ) );
			AddItem( new Doublet( 438 ) ); 
         		AddItem( new Skirt( 998 ) ); 
         		AddItem( new Sandals( 0 ) );


			switch ( Utility.Random( 8 ))
			{
				case 0: PackItem( new SpidersSilk( 3 ) ); break;
				case 1: PackItem( new BlackPearl( 3 ) ); break;
				case 2: PackItem( new Bloodmoss( 3 ) ); break;
				case 3: PackItem( new Garlic( 3 ) ); break;
				case 4: PackItem( new MandrakeRoot( 3 ) ); break;
				case 5: PackItem( new Nightshade( 3 ) ); break;
				case 6: PackItem( new SulfurousAsh( 3 ) ); break;
				case 7: PackItem( new Ginseng( 3 ) ); break;
			}


			PackGold(2000, 5000);
			PackScroll( 2, 8 );
			if( Utility.Random( 25 ) == 1 ) 
			{
    			PackItem( new Sandals( 1 ) );
                        }
			if( Utility.Random( 200 ) == 1 )
		        {
    			PackItem( new HolyBlessDeed() );
		}
	}
	
	
			
		

		

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public IAmTheBoss( Serial serial ) : base( serial )
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