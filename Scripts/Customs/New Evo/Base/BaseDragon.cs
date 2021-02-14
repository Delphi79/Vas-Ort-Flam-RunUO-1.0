using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	
	public class BaseDragon : BaseEvo
	{
		//	public override bool IsEvo { get { return true; } }
		
		private Timer m_BreatheTimer;
		private DateTime m_EndBreathe;
		
		[Constructable]
		public BaseDragon (AIType ai,FightMode mode,int iRangePerception,int iRangeFight,double dActiveSpeed, double dPassiveSpeed) : base( ai, mode, iRangePerception, iRangeFight,dActiveSpeed, dPassiveSpeed)
		{
			Tamable = true;
			
			BaseSoundID = 362;
			Fav_Gem = 3859;//Ruby
			Fav_Gem2 = 3861;// Citrine
		}
		
		public override void Ability(Mobile target)
		{
			Breathe(target);
			base.Ability(target);
		}
		
	
	
		public void Breathe( Mobile m )
		{
			DoHarmful( m );
			
			m_BreatheTimer = new BreatheTimer( m, this, this, TimeSpan.FromSeconds( 1.0 ) );
			m_BreatheTimer.Start();
			m_EndBreathe = DateTime.Now + TimeSpan.FromSeconds( 1.0 );
			
			this.Frozen = true;
			
			this.MovingParticles( m, 0x36D4, 7, 0, false, true, 9502, 4019, 0x160 );
		}
		
		private class BreatheTimer : Timer
		{
			private BaseDragon ed;
			private Mobile m_Mobile, m_From;
			
			public BreatheTimer( Mobile m, BaseDragon owner, Mobile from, TimeSpan duration ) : base( duration )
			{
				ed = owner;
				m_Mobile = m;
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}
			
			protected override void OnTick()
			{
				int damagemin = (ed.Ability_Power*10) / 15;
				int damagemax = ed.Ability_Power;
				ed.Frozen = false;
				
				m_Mobile.PlaySound( 0x11D );
				AOS.Damage( m_Mobile, m_From, Utility.RandomMinMax( damagemin, damagemax ), 0, 100, 0, 0, 0 );
				Stop();
			}
		}
		
		public BaseDragon( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			
			writer.WriteDeltaTime( m_EndBreathe );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_EndBreathe = reader.ReadDeltaTime();
			m_BreatheTimer = new BreatheTimer( this, this, this, m_EndBreathe - DateTime.Now );
			m_BreatheTimer.Start();
		}
	}
}
