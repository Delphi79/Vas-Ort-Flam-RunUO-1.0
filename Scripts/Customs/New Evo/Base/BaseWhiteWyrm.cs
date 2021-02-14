using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	
	public class BaseWhiteWyrm : BaseEvo
	{
		//	public override bool IsEvo { get { return true; } }
		
		[Constructable]
		public BaseWhiteWyrm (AIType ai,FightMode mode,int iRangePerception,int iRangeFight,double dActiveSpeed, double dPassiveSpeed) : base( ai, mode, iRangePerception, iRangeFight,dActiveSpeed, dPassiveSpeed)
		{
			Tamable = true;
			
			BaseSoundID = 362;
			Fav_Gem = 3878;//Diamond
			Fav_Gem2 = 3873;//StarSapphire
			
			Dex = 50;
			
		}
		
		public BaseWhiteWyrm( Serial serial ) : base( serial )
		{
		}
		
		public void DispellCheck(Mobile targ)
		{
			bool dis = false;
			switch (Ability_Power)
			{
				case 0: case 5:
					if ( ((BaseCreature)targ) is BladeSpirits && Utility.RandomDouble() > 0.25)
						dis = true;
					break;
					
				case 10: case 15:
					if ( ((BaseCreature)targ) is BladeSpirits && Utility.RandomDouble() > 0.5)
						dis = true;
					break;
					
				case 20:
					if ( ((BaseCreature)targ) is BladeSpirits)
						dis = true;
					break;
					
				case 25: case 30:
					if ( ((BaseCreature)targ) is BladeSpirits || ((BaseCreature)targ) is EnergyVortex   && Utility.RandomDouble() > 0.25)
						dis = true;
					break;
					
				case 35:
					if ( ((BaseCreature)targ) is BladeSpirits || ((BaseCreature)targ) is EnergyVortex   && Utility.RandomDouble() > 0.5)
						dis = true;
					break;
					
				case 40: case 45:
					if ( ((BaseCreature)targ) is BladeSpirits || ((BaseCreature)targ) is EnergyVortex  )
						dis = true;
					break;
					
				case 60:
					dis = true;
					break;
					
				case 70:
					dis = true;
					break;
					
				case 80:
					dis = true;
					break;
					
				case 90:
					dis = true;
					break;
					
				case 100:
					dis = true;
					break;
					
			}
			if (dis == true)
				Dispel(targ);
		}
		
		
		
		
		
		public override void Ability(Mobile attacker)
		{
			if (attacker is BaseCreature && ((BaseCreature)attacker).Summoned && !((BaseCreature)attacker).IsAnimatedDead )
			{
				base.Ability(attacker);
				DispellCheck(attacker);
			}
			else if ( this.Poisoned == true)
			{
				base.Ability(attacker);
				Cure();
			}
//			else if (StatMods.Count > 0)
//			{
//				RemoveMods(attacker);
//			}
		}
		
//		public void RemoveMods(Mobile attacker)
//		{
//			int done = 0;
//			for ( int i = 0; i < StatMods.Count; ++i )
//			{
//				StatMod check = (StatMod)StatMods[i];
//				if (check.Offset < 0)
//				{
//				RemoveStatMod(check.Name);
//				done += 1;
//				}
//			}
//			
//			if (done > 0){
//				base.Ability(attacker);
//				this.ControlMaster.SendMessage( "Your pet has removed any delbilitating effects {0}", done ); 
//			}
//		}
		
		public void Cure()
		{
			int poison_power = this.Poison.Level *10;
				
			if ( (Ability_Power - poison_power) >= Utility.Random(1, 100))
			{
				this.FixedParticles( 0x373A, 10, 15, 5012, EffectLayer.Waist );
				this.PlaySound( 0x1E0 );
				this.CurePoison(this);
				this.ControlMaster.SendMessage( "Your pet has cured himself of all poisons" ); 
			}
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
