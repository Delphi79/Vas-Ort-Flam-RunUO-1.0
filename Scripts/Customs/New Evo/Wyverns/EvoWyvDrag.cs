using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a young dragon corpse" )]
	public class EvoWyvDrag : BaseWyvern
	{
		//	public override bool IsEvo { get { return true; } }
		
		[Constructable]
		public EvoWyvDrag () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ControlSlots = 3;
			MinTameSkill = 93.9;
					
			Body = Utility.RandomList( 59, 12 );
			
			this.Skills[43].Cap = 100;// Wresteling
			this.Skills[26].Cap = 100;// Tactics
			this.Skills[1].Cap = 100;// Anatomy
			this.Skills[27].Cap = 100;// Magic Resistance
			this.Skills[30].Cap = 0;// Poisoning
			
			this.Skills[25].Cap = 100;// Magery
			this.Skills[46].Cap = 100;// Meditation
			this.Skills[16].Cap = 100;// Evaluation Inteligence
			
			m_KP_Lv = 775000;
			m_KP_Lower = 350000;
			
						// Max Stats
			m_Str = 355;
			m_Int = 140;
			m_Dex = 120;
						
			m_Str_Gain = 3;
			m_Int_Gain  = 3;
			m_Dex_Gain  = 3;
			
			
			//Skill Gain Speeds
			m_Wrest = 5;
			m_Tact = 5;
			m_Anat = 5;
			m_Resist = 10;
			m_Poison = 0;
			m_Magery = 3;
			m_Med = 3;
			m_Eval = 3;
			
			// Ability
			Ability_Cap = 10;// max Ability skill can get to this level
			Ability_Gain = 10;// Speed in the skills gain
			
			Ability_ChargesMax = 155;// Cap of the charges at this level
			Ability_ChargesGain = 5;// Speed at the charges gain
			
			Ability_Consumption = 6; // The amount of charges used
			
			VirtualArmor = 60;
			
			StrLock = StatLockType.Up;
			DexLock = StatLockType.Up;
			IntLock = StatLockType.Up;
			
			if ( Utility.RandomDouble() <= 0.35 )
			{
				int amount = Utility.RandomMinMax( 1, 5 );
				
				PackItem( new DragonDust(amount) );
			}
		}
		
		public EvoWyvDrag( Serial serial ) : base( serial )
		{
		}
		
		public override void Evolve()
		{
			//if (ControlMaster != null)
				//this.ControlMaster.SendMessage("Evolved");
			EvoWyvAncie EL = new EvoWyvAncie();
			New(EL);
			
			this.Delete();
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
