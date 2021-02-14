using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class EvoDragSnake : BaseDragon
	{
		//	public override bool IsEvo { get { return true; } }
		
		[Constructable]
		public EvoDragSnake () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ControlSlots = 1;
			MinTameSkill = 93.9;
			
			Body = 52;
			
			// Skill Caps
			this.Skills[43].Cap = 10;// Wresteling
			this.Skills[26].Cap = 10;// Tactics
			this.Skills[1].Cap = 10;// Anatomy
			this.Skills[27].Cap = 10;// Magic Resistance
			this.Skills[30].Cap = 0;// Poisoning
			
			this.Skills[25].Cap = 10;// Magery
			this.Skills[46].Cap = 10;// Meditation
			this.Skills[16].Cap = 10;// Evaluation Inteligence
			
			
			// Max Stats
			m_Str = 60;
			m_Int = 50;
			m_Dex = 50;
			m_Max_Hits = 60;
			
			SetStr( 35, 55 );
			SetDex( 35, 40 );
			SetInt( 30, 45 );
			
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
			
			Ability_ChargesMax = 15;// Cap of the charges at this level
			Ability_ChargesGain = 5;// Speed at the charges gain
			
			Ability_Consumption = 1; // The amount of charges used
			
			Ability_ChargesCap = 10;
			
			VirtualArmor = 10;
			
			StrLock = StatLockType.Up;
			DexLock = StatLockType.Up;
			IntLock = StatLockType.Up;
			
			if ( Utility.RandomDouble() <= 0.35 )
			{
				int amount = Utility.RandomMinMax( 1, 5 );
				
				PackItem( new DragonDust(amount) );
			}
		}
		
		public EvoDragSnake( Serial serial ) : base( serial )
		{
		}
		
		public override void Evolve()
		{
			//if (ControlMaster != null)
			//this.ControlMaster.SendMessage("Evolved");
			EvoDragSerp EL = new EvoDragSerp();
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
