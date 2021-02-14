using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a young dragon corpse" )]
	public class EvoWyvSnake : BaseWyvern
	{
		//	public override bool IsEvo { get { return true; } }
		
		[Constructable]
		public EvoWyvSnake () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ControlSlots = 1;
			MinTameSkill = 93.9;
			
			Body = 52;
			
			this.Skills[43].Cap = 10;// Wresteling
			this.Skills[26].Cap = 10;// Tactics
			this.Skills[1].Cap = 10;// Anatomy
			this.Skills[27].Cap = 10;// Magic Resistance
			this.Skills[30].Cap = 0;// Poisoning
			
			this.Skills[25].Cap = 10;// Magery
			this.Skills[46].Cap = 10;// Meditation
			this.Skills[16].Cap = 10;// Evaluation Inteligence
			
			m_Wrest = 10;
			m_Tact = 10;
			m_Anat = 10;
			m_Resist = 10;
			m_Poison = 0;
			m_Magery = 10;
			m_Med = 10;
			m_Eval = 10;	 
				
			if ( Utility.RandomDouble() <= 0.35 )
			{
				int amount = Utility.RandomMinMax( 1, 5 );
				
				PackItem( new DragonDust(amount) );
			}
		}
		
		public EvoWyvSnake( Serial serial ) : base( serial )
		{
		}
		
		public override void Evolve()
		{
			//if (ControlMaster != null)
				//this.ControlMaster.SendMessage("Evolved");
			EvoWyvSerp EL = new EvoWyvSerp();
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
