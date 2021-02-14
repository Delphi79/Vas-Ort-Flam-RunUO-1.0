//////////////////////////////////
//			           //
//      Scripted by Raelis      //
//		             	 //
//////////////////////////////////
using System;
using System.Collections;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Gumps;

namespace Server.Mobiles
{
	
	public class BaseEvo : BaseCreature
	{
		
		//	public override bool IsEvo { get { return true; } }
		
		#region KP
		
		public int m_KP;
		
		public int m_KP_Lv;
		public int m_KP_Lower;
		
		#endregion
		
		#region Stats/Skills
		
		public int m_Str, m_Int, m_Dex;//Max Stats
		public int m_Str_Gain, m_Int_Gain, m_Dex_Gain; //Stat gain
		public int m_Wrest,	m_Tact,	m_Anat,	m_Resist, m_Poison,	m_Magery, m_Med, m_Eval;// Speed of gains
		public int m_Max_Hits;// Max Hits
		#endregion
		
		#region Ability
		
		public int Ability_Cap;// max Ability skill can get to this level
		public double Ability_Skill;// Level of the ability skill
		public int Ability_Gain;// Speed in the skills gain
		
		public int Ability_ChargesMax; // Maximum charges curently
		public int Ability_ChargesCap;// Cap of the charges at this level
		public int Ability_Charges;// Current Charges
		public int Ability_ChargesGain;// Speed at the charges gain
		
		public int Ability_Time;// Duration between effects
		public int Ability_Power;// Strength of ability
		public int Ability_Consumption; // The amount of charges used
		#endregion
		
		public int Fav_Gem;
		public int Fav_Gem2;
		
		
		[CommandProperty( AccessLevel.GameMaster )]
		public double AbilitySkill
		{
			get{ return Ability_Skill; }
			set{ Ability_Skill = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int KP
		{
			get{ return m_KP; }
			set{ m_KP = value; }
		}
		
		public BaseEvo(AIType ai,FightMode mode,int iRangePerception,int iRangeFight,double dActiveSpeed, double dPassiveSpeed) : base( ai, mode, iRangePerception, iRangeFight,dActiveSpeed, dPassiveSpeed)//AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			m_KP_Lv = 10000;
			m_KP_Lower = 0;
			Ability_ChargesMax = 15;//Temp
			
			StrLock = StatLockType.Up;
			DexLock = StatLockType.Up;
			IntLock = StatLockType.Up;
			Evolve_Check();
		}
		
		public BaseEvo(Serial serial) : base(serial)
		{
		}
		
		#region KP
		
		public void KP_Gain(BaseCreature defender)
		{
			int kpgainmin, kpgainmax;
			int Max_Ratio = 0, Min_Ratio = 0;
			
			#region Ratio
			if ( m_KP_Lv != 999999999)
			{
				
				switch(m_KP_Lv)
				{
					case 10000:
						Max_Ratio = 2;
						Min_Ratio = 5;
						break;
						
					case 25000:
						Max_Ratio = 5;
						Min_Ratio = 10;
						break;
						
					case 75000:
						Max_Ratio = 20;
						Min_Ratio = 10;
						break;
						
					case 175000:
						Max_Ratio = 20;
						Min_Ratio = 30;
						break;
						
					case 350000:
						Max_Ratio = 40;
						Min_Ratio = 50;
						break;
						
					case 775000:
						Max_Ratio = 160;
						Min_Ratio = 100;
						break;
						
					case 1500000:
						Max_Ratio = 540;
						Min_Ratio = 480;
						break;
				}
				#endregion
				
				if (Min_Ratio > 0 && Max_Ratio > 0)
				{
					kpgainmin = 5 + ( defender.HitsMax ) / Min_Ratio;
					kpgainmax = 5 + ( defender.HitsMax ) / Max_Ratio;
					
					this.KP += Utility.RandomList( kpgainmin, kpgainmax );
				}
				
				Evolve_Check();
			}
			
		}
		
		public void Evolve_Check()
		{
			if ( this.KP >= m_KP_Lv && this.KP < 999999999)
			{
				Evolve();
				//this.ControlMaster.SendMessage("Evolving");
			}
		}
		public virtual void Evolve()
		{
		}
		
		
		//public override void Damage( int amount, Mobile defender )
		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			
			
			if ( defender is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)defender;
				
				KP_Gain(bc);
				
			}
			
			//base.Damage( amount, defender );
		}
		
		public override void  AlterSpellDamageTo( Mobile to, ref int damage )
		{
			base.AlterSpellDamageTo( to, ref damage );
			
			
			if ( to is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)to;
				
				KP_Gain(bc);
				
			}
			
			//base.Damage( amount, defender );
		}
		
		#endregion
		
		public void New(BaseEvo EL)
		{
			EL.Map = this.Map;
			EL.Location = this.Location;
			EL.Combatant = this.Combatant;
			
			EL.Controled = true;
			if (ControlMaster != null)
				EL.ControlMaster = this.ControlMaster;
			EL.IsBonded = true;
			EL.ControlOrder = this.ControlOrder;
			EL.ControlDest = this.ControlDest;
			
			EL.Name = this.Name;
			EL.Hue = this.Hue;
			EL.KP = this.KP;
			
			EL.Str = this.Str;
			EL.Dex = this.Dex;
			EL.Stam = EL.Dex;
			EL.Int = this.Int;
			EL.Mana = EL.Int;
			EL.HitsMaxSeed = this.HitsMaxSeed;
			EL.Hits = this.Hits;
			
			EL.Ability_Charges = this.Ability_Charges;
			EL.Ability_ChargesMax = this.Ability_ChargesMax;
			EL.Ability_Skill = this.Ability_Skill;
			EL.Ability_Time = this.Ability_Time;
			EL.Ability_Power = this.Ability_Power;
			
			
			
			for (int i = 0; i < EL.Skills.Length; i++)
			{
				EL.Skills[i].Base = this.Skills[i].Base;
				
			}
			Evolve_Min_Skills(EL);
			//EL.Skills[(int)SkillName.Magery].Base = this.Skills[(int)SkillName.Magery].Base;// Setting 1 skill
			
		}
		
		public void Evolve_Min_Skills(BaseEvo EL)
		{
			if (EL.Str < (EL.m_Str/2))
				EL.Str = (EL.m_Str/2);
			
			if (EL.Dex < (EL.m_Dex/2))
				EL.Dex = (EL.m_Dex/2);
			EL.Stam = EL.Dex;
			
			if (EL.Int < (EL.m_Int/2))
				EL.Int = (EL.m_Int/2);
			EL.Mana = EL.Int;
			
			if (EL.HitsMaxSeed < (EL.m_Max_Hits/2))
				EL.HitsMaxSeed = (EL.m_Max_Hits/2);
			
			if (EL.Ability_ChargesMax < (EL.Ability_ChargesCap/2))
				EL.Ability_ChargesMax = (EL.Ability_ChargesCap/2);
			
			if (EL.Ability_Skill < (EL.Ability_Cap/2))
				EL.Ability_Skill = (EL.Ability_Cap/2);
			
			//Skills
			
			if (EL.Skills[43].Base < (EL.Skills[43].Cap/2))
				EL.Skills[43].Base = (EL.Skills[43].Cap/2);// Wresteling
			
			if (EL.Skills[26].Base < (EL.Skills[43].Cap/2))
				EL.Skills[26].Base = (EL.Skills[43].Cap/2);// Tactics
			
			if (EL.Skills[1].Base < (EL.Skills[43].Cap/2))
				EL.Skills[1].Base = (EL.Skills[43].Cap/2);// Anatomy
			
			if (EL.Skills[27].Base < (EL.Skills[43].Cap/2))
				EL.Skills[27].Base = (EL.Skills[43].Cap/2);// Magic Resistance
			
			if (EL.Skills[30].Base < (EL.Skills[43].Cap/2))
				EL.Skills[30].Base = (EL.Skills[43].Cap/2);// Poisoning
			
			if (EL.Skills[25].Base < (EL.Skills[43].Cap/2))
				EL.Skills[25].Base = (EL.Skills[43].Cap/2);// Magery
			
			if (EL.Skills[46].Base < (EL.Skills[43].Cap/2))
				EL.Skills[46].Base = (EL.Skills[43].Cap/2);// Meditation
			
			if (EL.Skills[16].Base < (EL.Skills[43].Cap/2))
				EL.Skills[16].Base = (EL.Skills[43].Cap/2);// Evaluation Inteligence
			
		}
		
		public override void OnSkillInvalidated( Skill skill )
		{
			int happyness = 0;
			switch (Loyalty)
			{
				case PetLoyalty.Confused:
					happyness = 2;
					break;
				case PetLoyalty.ExtremelyUnhappy:
					happyness = 3;
					break;
				case PetLoyalty.RatherUnhappy: case PetLoyalty.Unhappy:
					happyness = 5;
					break;
				case PetLoyalty.SomewhatContent: case PetLoyalty.Content:
					happyness = 7;
					break;
				case PetLoyalty.Happy: case PetLoyalty.RatherHappy: case PetLoyalty.VeryHappy:
					happyness = 10;
					break;
				case PetLoyalty.ExtremelyHappy:
					happyness = 12;
					break;
				case PetLoyalty.WonderfullyHappy:
					happyness = 14;
					break;		
			}

			if ( happyness >= Utility.Random(1, 20))
				Skill_Stat_Gain( skill);
			
			LockCheck();
			
		}
		
		public void LockCheck()
		{
			if ( Str >= m_Str)
				StrLock = StatLockType.Locked;
			if ( Int >= m_Int)
				IntLock = StatLockType.Locked;
			if ( Dex >= m_Dex)
				IntLock = StatLockType.Locked;
		}
		
		public void Skill_Stat_Gain(Skill skill)
		{
			int chance = Utility.RandomMinMax( 1, 10 );
			if (skill.SkillName == SkillName.Wrestling && chance < m_Wrest)
			{
				//if (ControlMaster != null)
				//this.ControlMaster.SendMessage("no gain");
				skill.Base -= 0.1;
			}
			else if ( m_Wrest > 10)
				skill.Base += (m_Wrest/10);
			if (skill.SkillName == SkillName.Tactics && chance < m_Tact)
				skill.Base -= 0.1;
			else if ( m_Tact > 10)
				skill.Base += (m_Tact/10);
			if (skill.SkillName == SkillName.Anatomy && chance < m_Anat)
				skill.Base -= 0.1;
			else if ( m_Anat > 10)
				skill.Base += (m_Anat/10);
			if (skill.SkillName == SkillName.MagicResist && chance < m_Resist)
				skill.Base -= 0.1;
			else if ( m_Resist > 10)
				skill.Base += (m_Resist/10);
			if (skill.SkillName == SkillName.Poisoning && chance < m_Poison)
				skill.Base -= 0.1;
			else if ( m_Poison > 10)
				skill.Base += (m_Poison/10);
			if (skill.SkillName == SkillName.Magery && chance < m_Magery)
				skill.Base -= 0.1;
			else if ( m_Magery > 10)
				skill.Base += (m_Magery/10);
			if (skill.SkillName == SkillName.Meditation && chance < m_Med)
				skill.Base -= 0.1;
			else if ( m_Med > 10)
				skill.Base += (m_Med/10);
			if (skill.SkillName == SkillName.EvalInt && chance < m_Eval)
				skill.Base -= 0.1;
			else if ( m_Eval > 10)
				skill.Base += (m_Eval/10);
			//if (ControlMaster != null)
			//this.ControlMaster.SendMessage("chance {0}", chance);
			if ((skill.SkillName == SkillName.EvalInt || skill.SkillName == SkillName.Magery || skill.SkillName == SkillName.Meditation) && chance < m_Int_Gain && IntLock == StatLockType.Up)
			{
				Int += 1;
				if (ControlMaster != null)
				this.ControlMaster.SendMessage("Int");
			}
			else if ((skill.SkillName == SkillName.Wrestling || skill.SkillName == SkillName.Anatomy) && chance < m_Str_Gain && StrLock == StatLockType.Up)
			{
				Str += 1;
			//	if (ControlMaster != null)
			//	this.ControlMaster.SendMessage("Str");
			}
			else if ((skill.SkillName == SkillName.MagicResist || skill.SkillName == SkillName.Tactics) && chance < m_Dex_Gain && DexLock == StatLockType.Up)
			{
				Dex += 1;
				if (ControlMaster != null)
				this.ControlMaster.SendMessage("Dex");
			}
			
			LockCheck();
		}
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			PlayerMobile player = from as PlayerMobile;
			
			if ( player != null )
			{
				#region Dragon Dust
				if ( dropped is DragonDust )
				{
					DragonDust dust = ( DragonDust )dropped;
					
					int amount = ( dust.Amount *Utility.Random( 1, 5) );
					if ( (KP + amount) > 999999999)// Dont allow to gain more than 1 level
					{
					}
					else
					{
						this.PlaySound( 665 );
						this.KP += amount;
						dust.Delete();
						this.Say( "*"+ this.Name +" absorbs the dragon dust*" );
						Evolve_Check();
						return false;
					}
				}
				#endregion
				
				#region Gems
				else if ( dropped is StarSapphire || dropped is Diamond || dropped is Amethyst || dropped is Emerald || dropped is Sapphire || dropped is Citrine ||  dropped is Ruby || dropped is Amber || dropped is Tourmaline )
				{
					int amount = dropped.Amount;
					if ( dropped.ItemID == Fav_Gem || dropped.ItemID == Fav_Gem2)
						amount = dropped.Amount*3;
					
					string end = "*";
					if ( Ability_Charges != Ability_ChargesMax)
					{
						this.PlaySound( 723 );
						if ( Ability_Charges + amount <= Ability_ChargesMax)
						{
							this.Ability_Charges += amount;
							dropped.Delete();
						}
						else if ((Ability_ChargesMax - Ability_Charges)< amount)
						{
							amount = Ability_ChargesMax - Ability_Charges;
							this.Ability_Charges += amount;
							if ( dropped.ItemID == Fav_Gem || dropped.ItemID == Fav_Gem2)
								dropped.Amount -= (amount/3);
							else
								dropped.Amount -= amount;
						}
						if ( amount > 1)
							end = "s*";
						this.Say( "*"+ this.Name +" eats the gem"+end );
						
						if ( Ability_Charges == Ability_ChargesMax && this.Loyalty < PetLoyalty.WonderfullyHappy) 
							this.Loyalty += 1;
						
						return false;
					}
				}
				#endregion
			}
			return base.OnDragDrop( from, dropped );
		}
		
		
		#region Ability
		private DateTime m_NextAbility;
		
		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;
			
			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;
			
			if ( DateTime.Now >= m_NextAbility )
			{
				if ( Ability_Charges >= Ability_Consumption )
					Ability( combatant );
				
				m_NextAbility = DateTime.Now + TimeSpan.FromSeconds( 12.0 + (Ability_Time * Utility.RandomDouble()) ); // 12-15 seconds
			}
		}
		
		
		public virtual void Ability(Mobile target)
		{
			PlaySound( GetAngerSound() );
			Ability_Charges -= Ability_Consumption;
			KP_Gain((BaseCreature)target);
			if ( Utility.Random(1, 10) > Ability_Gain && Ability_Skill < Ability_Cap)
				Ability_Skill += 0.1;
			if ( Utility.Random(1, 10) > Ability_ChargesGain && Ability_Charges < Ability_ChargesCap)
				Ability_Skill += 0.1;
			Ability_Skill_Check();
			
		}
		
		public virtual void Ability_Skill_Check()
		{
			switch ((int)Ability_Skill)
			{
				case 0:
					Ability_Time = 20;
					Ability_Power = 5;
					break;
					
				case 5:
					Ability_Time = 15;
					Ability_Power = 10;
					break;
					
				case 10:
					Ability_Time = 12;
					Ability_Power = 10;
					break;
					
				case 15:
					Ability_Time = 12;
					Ability_Power = 15;
					break;
					
				case 20:
					Ability_Time = 10;
					Ability_Power = 15;
					break;
					
				case 25:
					Ability_Time = 10;
					Ability_Power =	20;
					break;
					
				case 30:
					Ability_Time = 8;
					Ability_Power = 20;
					break;
					
				case 35:
					Ability_Time = 8;
					Ability_Power = 25;
					break;
					
				case 40:
					Ability_Time = 6;
					Ability_Power = 25;
					break;
					
				case 45:
					Ability_Time = 6;
					Ability_Power = 30;
					break;
					
				case 50:
					Ability_Time = 3;
					Ability_Power = 30;
					break;
					
				case 55:
					Ability_Time = 3;
					Ability_Power = 35;
					break;
					
				case 60:
					Ability_Time = 0;
					Ability_Power = 40;
					break;
					
				case 65:
					Ability_Time = 0;
					Ability_Power = 45;
					break;
					
				case 70:
					Ability_Time = -2;
					Ability_Power = 45;
					break;
					
				case 75:
					Ability_Time = -2;
					Ability_Power = 60;
					break;
					
				case 80:
					Ability_Time = -4;
					Ability_Power = 60;
					break;
					
				case 90:
					Ability_Time = -4;
					Ability_Power = 70;
					break;
					
				case 100:
					Ability_Time = -6;
					Ability_Power = 70;
					break;
					
				case 105:
					Ability_Time = -6;
					Ability_Power = 80;
					break;
					
				case 110:
					Ability_Time = -8;
					Ability_Power = 80;
					break;
					
				case 115:
					Ability_Time = -8;
					Ability_Power = 90;
					break;
					
				case 120:
					Ability_Time =-10;
					Ability_Power = 100;
					break;
					
					
					
			}
		}
		
		#endregion
		
		public override bool OnBeforeDeath()
		{
			KP = m_KP_Lower;
			this.Ability_Charges -= this.Ability_Consumption;
			for (int i = 0; i < this.Skills.Length; i++)
			{
				if (this.Skills[i].Base >= (this.Skills[i].Base/23))
					this.Skills[i].Base -= (this.Skills[i].Base/23);
				
			}
			this.Loyalty -= 1;
			
			this.Ability_Charges = 0;
			
			return base.OnBeforeDeath();
		}
		
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			
			writer.Write((int) 0);
			
			writer.Write( (int) m_KP );
			
			writer.Write( (int) m_KP_Lv );
			writer.Write( (int) m_KP_Lower );
			
			writer.Write( (int) m_Str );
			writer.Write( (int) m_Int );
			writer.Write( (int) m_Dex );
			
			writer.Write( (int) m_Wrest );
			writer.Write( (int) m_Tact );
			writer.Write( (int) m_Anat );
			writer.Write( (int) m_Resist );
			writer.Write( (int) m_Poison );
			writer.Write( (int) m_Magery );
			writer.Write( (int) m_Med );
			writer.Write( (int) m_Eval );
			
			writer.Write( (int) Ability_Cap );
			writer.Write( (double) Ability_Skill );
			writer.Write( (int) Ability_Gain );
			
			writer.Write( (int) Ability_ChargesMax );
			writer.Write( (int) Ability_ChargesCap );
			writer.Write( (int) Ability_Charges );
			writer.Write( (int) Ability_ChargesGain );
			
			writer.Write( (int) Ability_Time);
			writer.Write( (int) Ability_Power);
			writer.Write( (int) Ability_Consumption);
			
			writer.Write( (int) Fav_Gem);
			writer.Write( (int) Fav_Gem2);
			
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			
			int version = reader.ReadInt();
			
			switch ( version )
			{
				case 0:
					{
						m_KP = reader.ReadInt();
						
						m_KP_Lv = reader.ReadInt();
						m_KP_Lower = reader.ReadInt();
						
						m_Str = reader.ReadInt();
						m_Int = reader.ReadInt();
						m_Dex = reader.ReadInt();
						
						m_Wrest = reader.ReadInt();
						m_Tact = reader.ReadInt();
						m_Anat = reader.ReadInt();
						m_Resist = reader.ReadInt();
						m_Poison = reader.ReadInt();
						m_Magery = reader.ReadInt();
						m_Med = reader.ReadInt();
						m_Eval = reader.ReadInt();
						
						Ability_Cap = reader.ReadInt();
						Ability_Skill = reader.ReadDouble();
						Ability_Gain = reader.ReadInt();
						
						Ability_ChargesMax = reader.ReadInt();
						Ability_ChargesCap = reader.ReadInt();
						Ability_Charges = reader.ReadInt();
						Ability_ChargesGain = reader.ReadInt();
						
						Ability_Time = reader.ReadInt();
						Ability_Power = reader.ReadInt();
						Ability_Consumption = reader.ReadInt();
						
						Fav_Gem = reader.ReadInt();
						Fav_Gem2 = reader.ReadInt();
						
						//goto case 0;
						break;
						
					}
					
					
			}
		}
	}
	
}
