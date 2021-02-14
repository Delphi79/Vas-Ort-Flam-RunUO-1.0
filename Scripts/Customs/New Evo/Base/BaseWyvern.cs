using System;
using Server;
using Server.Items;
using Server.Spells;
using System.Collections;

namespace Server.Mobiles
{
	
	public class BaseWyvern : BaseEvo
	{
		//	public override bool IsEvo { get { return true; } }
		
		[Constructable]
		public BaseWyvern (AIType ai,FightMode mode,int iRangePerception,int iRangeFight,double dActiveSpeed, double dPassiveSpeed) : base( ai, mode, iRangePerception, iRangeFight,dActiveSpeed, dPassiveSpeed)
		{
			Tamable = true;
			
			BaseSoundID = 362;
			Fav_Gem = 3877;//Amber
			Fav_Gem2 = 3885;//Tourmaline
			
		}
		
		public BaseWyvern( Serial serial ) : base( serial )
		{
		}
		
		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }
		
		public override void Ability(Mobile target)
		{
			ArrayList contact = new ArrayList();
				foreach (Mobile m in this.GetMobilesInRange( 1 ))
			{
				contact.Add(m);
			}
			if ( contact.Contains( target ))
			{
			Poisons(target);
			base.Ability(target);
			}
		}
		
		public void Poisons(Mobile targ)
		{
			int level = 0;
			
			switch (Ability_Power)
			{
				case 0: case 5:
					level = 0;
					break;
					
				case 10: case 15:
					if(Utility.RandomDouble() > 0.5)
						level = 0;
					else
						level = 1;
					break;
					
				case 20:
					level = 1;
					break;
					
				case 25: case 30:
					if(Utility.RandomDouble() > 0.5)
						level = 1;
					else
						level = 2;
					break;
					
				case 35:
					level = 2;
					break;
					
				case 40: case 45:
					if(Utility.RandomDouble() > 0.5)
						level = 2;
					else
						level = 3;
					break;
					
				case 60:
					level = 3;
					break;
					
				case 70:
					if(Utility.RandomDouble() > 0.1)
						level = 3;
					else
						level = 4;
					break;
					
				case 80:
					if(Utility.RandomDouble() > 0.3)
						level = 3;
					else
						level = 4;
					break;
					
				case 90:
					if(Utility.RandomDouble() > 0.5)
						level = 3;
					else
						level = 4;
					break;
					
				case 100:
					if(Utility.RandomDouble() > 0.7)
						level = 3;
					else
						level = 4;
					break;
					
			}
			targ.ApplyPoison( this, Poison.GetPoison( level ) );
			//	public override Poison PoisonImmune{ get{ return Poison.GetPoison( level ); } }
			//PoisonImmune = Poison.GetPoison( level );
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
