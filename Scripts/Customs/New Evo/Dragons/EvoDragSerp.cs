using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a young dragon corpse" )]
	public class EvoDragSerp : BaseDragon
	{
		//	public override bool IsEvo { get { return true; } }
		
		[Constructable]
		public EvoDragSerp () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ControlSlots = 1;
			MinTameSkill = 93.9;
			
			Body = 89;
			
			this.Skills[43].Cap = 20;// Wresteling
			this.Skills[26].Cap = 20;// Tactics
			this.Skills[1].Cap = 20;// Anatomy
			this.Skills[27].Cap = 20;// Magic Resistance
			this.Skills[30].Cap = 0;// Poisoning
			
			this.Skills[25].Cap = 20;// Magery
			this.Skills[46].Cap = 20;// Meditation
			this.Skills[16].Cap = 20;// Evaluation Inteligence
			
			m_KP_Lv = 25000;
			m_KP_Lower = 10000;
			
						
			// Max Stats
			m_Str = 105;
			m_Int = 80;
			m_Dex = 80;
						
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
			
			Ability_ChargesMax = 30;// Cap of the charges at this level
			Ability_ChargesGain = 5;// Speed at the charges gain
			
			Ability_Consumption = 2; // The amount of charges used
			
			VirtualArmor = 20;
			
			StrLock = StatLockType.Up;
			DexLock = StatLockType.Up;
			IntLock = StatLockType.Up;
			
			if ( Utility.RandomDouble() <= 0.35 )
			{
				int amount = Utility.RandomMinMax( 1, 5 );
				
				PackItem( new DragonDust(amount) );
			}
		}
		
		public EvoDragSerp( Serial serial ) : base( serial )
		{
		}
		
		public override void Evolve()
		{
			//if (ControlMaster != null)
				//this.ControlMaster.SendMessage("Evolved");
			EvoDragAli EL = new EvoDragAli();
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
