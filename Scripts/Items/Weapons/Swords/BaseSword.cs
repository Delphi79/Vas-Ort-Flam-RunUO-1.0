using System;
using Server;
using Server.Items;
using Server.Targets;
using System.Collections;

namespace Server.Items
{
	public abstract class BaseSword : BaseMeleeWeapon
	{
		public override SkillName DefSkill{ get{ return SkillName.Swords; } }
		public override WeaponType DefType{ get{ return WeaponType.Slashing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Slash1H; } }

		public BaseSword( int itemID ) : base( itemID )
		{
		}

		public BaseSword( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendLocalizedMessage( 1010018 ); // What do you want to use this item on?
			from.Target = new BladedItemTarget( this );
		}

		public static bool SkilledHit(Mobile attacker)
		{
			int a = (int)((attacker.Skills[SkillName.Anatomy].Value / 1000) * 100);
			int b = (int)(Utility.RandomDouble() * 100);

			//Console.WriteLine("{0} / {1}", a, b);

			if (a >= b)
				return true;
			else
				return false;
		}

		public override void OnHit( Mobile attacker, Mobile defender )
		{
			base.OnHit( attacker, defender );

     		//Console.WriteLine("{0} is attacking {1} at attacker skill of {2} with a randomdouble of {3}", attacker.Name, defender.Name, ( attacker.Skills[SkillName.Anatomy].Value / 400.0), a);

			if ( SkilledHit(attacker) && Layer != Layer.TwoHanded )
			{
				defender.SendMessage("You receive a slashing blow!"); // Is this not localized?
				defender.PlaySound(0x133);
				defender.FixedParticles(0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist);

				BeginBleed(defender, attacker);
				DoBleed(defender, attacker, 1);

				attacker.SendMessage("You deliver a slashing blow!"); // Is this not localized?

			}

			if ( !Core.AOS && Poison != null && PoisonCharges > 0 )
			{
				--PoisonCharges;

				if ( Utility.RandomDouble() >= 0.5 ) // 50% chance to poison
					defender.ApplyPoison( attacker, Poison );
			}
		}

		private static Hashtable m_Table = new Hashtable();

		public static void BeginBleed(Mobile m, Mobile from)
		{
			Timer t = (Timer)m_Table[m];

			if (t != null)
				t.Stop();

			t = new InternalTimer(from, m);
			m_Table[m] = t;

			t.Start();
		}

		public static void DoBleed(Mobile m, Mobile from, int level)
		{
			if (m.Alive)
			{
				int damage = level;

				if (!m.Player)
					damage *= 2;

				m.PlaySound(0x133);
				m.Damage(damage, from);

				Blood blood = new Blood();

				blood.ItemID = Utility.Random(0x122A, 5);

				blood.MoveToWorld(m.Location, m.Map);
			}
			else
			{
				EndBleed(m, false);
			}
		}

		public static void EndBleed(Mobile m, bool message)
		{
			Timer t = (Timer)m_Table[m];

			if (t == null)
				return;

			t.Stop();
			m_Table.Remove(m);

			m.SendLocalizedMessage(1060167); // The bleeding wounds have healed, you are no longer bleeding!
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;
			private Mobile m_Mobile;
			private int m_Count;

			public InternalTimer(Mobile from, Mobile m)
				: base(TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(2.0))
			{
				m_From = from;
				m_Mobile = m;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				DoBleed(m_Mobile, m_From, 5 - m_Count);

				if (++m_Count == 5)
					EndBleed(m_Mobile, true);
			}
		}
	}
}