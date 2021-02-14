using System;
using Server;
using System.Collections;
using System.Net;
using Server.Accounting;
using Server.Scripts.Commands;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps; 
using Server.Items;
using Server.Challenge;
using Server.Spells;
using Server.Spells.Second;
using Server.Spells.Third;

namespace Server.Challenge
{
	public class Challenge 
	{
		private static ArrayList worldStones = new ArrayList();

		public static void Initialize() 
		{
			Server.Commands.Register( "Duel", AccessLevel.Player, new CommandEventHandler( Challenge_OnCommand ) );
			EventSink.Logout += new LogoutEventHandler(EventSink_Logout);
			EventSink.Login += new LoginEventHandler(EventSink_Login);
			Configure();
		}

		//Mobile.Region.Name
		private static void EventSink_Logout(LogoutEventArgs e)
		{
			Mobile m = e.Mobile;
			if (m.Region.Name == "DuelPit")
			{
				m.MoveToWorld(new Point3D(1416, 1700, 1), Map.Felucca);
			}
		}

		private static void EventSink_Login(LoginEventArgs e)
		{
			Mobile m = e.Mobile;
			if (m.Region.Name == "DuelPit")
			{
				m.MoveToWorld(new Point3D(1416, 1700, 1), Map.Felucca);
			}
		}

		public static void Configure()
		{
			EventSink.WorldLoad+= new WorldLoadEventHandler(onLoad);
		}

		public static ArrayList WorldStones
		{			
			get { return worldStones; }
			set { worldStones = value;  }				   
		}
		public static void onLoad() 
		{
			foreach ( Mobile mob in World.Mobiles.Values ) 
			{ 
				if ( mob is PlayerMobile ) 
				{ 
					PlayerMobile pm = (PlayerMobile)mob;
					if( pm.IsInChallenge )
					{
						pm.IsInChallenge = false;
						int kills = 0;
						
						if( pm.FindItemOnLayer( Layer.Ring ) is ChallengeRing )
						{
							pm.FindItemOnLayer( Layer.Ring ).Delete();
							ChallengeRing ring = pm.FindItemOnLayer(Layer.Ring )as ChallengeRing;
							kills = ring.Kills;
						}
						
						if( kills >= 5 )
							pm.LogoutLocation = new Point3D(1416, 1700, 1);
						else
							pm.LogoutLocation = new Point3D(1416, 1700, 1);
						
						if( pm.TempMount != null )
						{
							pm.TempMount.Rider = pm;
							pm.TempMount = null;
						}
					}
				} 
			}
			foreach ( Item item in World.Items.Values ) 
			{ 
				if (item is ChallengeStone) 
				{ 
					WorldStones.Add(item);
				} 
			} 
		}		

		[Usage( "Challenge" )] 
		[Description( "Initiates Challenge Game!" )] 
		public static void Challenge_OnCommand( CommandEventArgs e ) 
		{    
			Mobile from = e.Mobile;

			if (from.Criminal)
			{
				from.SendMessage("You cannot duel while you are flagged as a criminal!");
				return;
			}
			else if (Server.Spells.SpellHelper.CheckCombat(from))
			{
				from.SendMessage("You must lose all aggro before dueling");
				return;
			}
			else
			{
				from.CloseGump(typeof(BeginGump));
				from.SendGump(new BeginGump((PlayerMobile)from, WorldStones));
			}

			return;
		}
	}
} 

namespace Server.Items
{ 
	public enum ChallengeGameType 
	{ 
		OnePlayerTeam, TwoPlayerTeam
	}

	[Serializable()] 
	public class ChallengeStone : Item 
	{ 
		public bool m_Active;
		public bool m_SameAccount = false;
		public bool m_LastKiller = false;
		public Point3D m_ChallengerPointDest;
		public Point3D m_OpponentPointDest;
		public Point3D m_ChallengerExitPointDest;
		public Point3D m_OpponentExitPointDest;
		private Map m_MapDest;
		private Mobile m_OpponentMobile;
		private Mobile m_ChallengerMobile;
		private ChallengeGameType m_Game = ChallengeGameType.OnePlayerTeam;
		private ArrayList m_ChallengePlayers = new ArrayList();
		private ArrayList m_OpponentPlayers = new ArrayList();
		private ArrayList m_ChallengerDead = new ArrayList();
		private ArrayList m_OpponentDead = new ArrayList();
		private int dualLength = 30;

		public ArrayList ChallengeTeam{ get{ return m_ChallengePlayers; } }
		public ArrayList OpponentTeam{ get{ return m_OpponentPlayers; } }
		public ArrayList ChallengerDead{ get{ return m_ChallengerDead; } }
		public ArrayList OpponentDead{ get{ return m_OpponentDead; }}

		[CommandProperty( AccessLevel.GameMaster )] 
		public int DualLength 
		{ 
			get { return dualLength; } 
			set { dualLength = value; } 
		}
		[CommandProperty( AccessLevel.GameMaster )] 
		public ChallengeGameType Game 
		{ 
			get { return m_Game; } 
			set { m_Game = value; } 
		} 

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get { return m_Active; }
			set 
			{
				m_Active = value; InvalidateProperties();
				if ( m_Active == true )
					Name = "Challenge Stone[Not in use]" ;
				if ( m_Active == false )
					Name = "Challenge Stone[In use]" ;
			}
			
		}
		   	   
		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D ChallengerPointDest
		{
			get { return m_ChallengerPointDest; }
			set { m_ChallengerPointDest = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D OpponentPointDest
		{
			get { return m_OpponentPointDest; }
			set { m_OpponentPointDest = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map MapDest
		{
			get { return m_MapDest; }
			set { m_MapDest = value; }
		}

		[Constructable]
		public ChallengeStone() : this( new Point3D( 0, 0, 0 ), new Point3D( 0, 0, 0 ), new Point3D( 0, 0, 0 ), new Point3D( 0, 0, 0 ), null, false )
		{
		}

		[Constructable]
		public ChallengeStone( Point3D challengerpointDest, Point3D opponentpointDest, Point3D challengerexit, Point3D opponentexit, Map mapDest, bool creatures ) : base( 0xED4 )
		{
			Movable = false; 
			Hue = 0; 
			Name = "a Ladder Dueling stone"; 

			Active = true;
			m_ChallengerPointDest = challengerpointDest;
			m_OpponentPointDest = opponentpointDest;
			m_ChallengerExitPointDest = challengerexit;
			m_OpponentExitPointDest = opponentexit;
			m_MapDest = mapDest;
			m_ChallengePlayers = new ArrayList();
			m_OpponentPlayers = new ArrayList();
			m_ChallengerDead = new ArrayList();
			m_OpponentDead = new ArrayList();
			Challenge.Challenge.WorldStones.Add( this );
		}
		public override void OnSingleClick( Mobile from )
		{
			if ( Active == true )
			{
				LabelTo( from, Name );
				LabelTo( from, "Not in use" );
			}
			if ( Active == false )
			{
				LabelTo( from, Name );
				LabelTo( from, "In Use" );
			}
		}

		public void AddChallengePlayer( PlayerMobile player)
		{ 
			ChallengeTeam.Add(player);
		}
		public void AddOpponentPlayer( PlayerMobile player)
		{ 
			OpponentTeam.Add(player);
		}
		public void RemoveChallengePlayer( PlayerMobile player)
		{ 
			ChallengeTeam.Remove(player); 
		}
		public void RemoveOpponentPlayer( PlayerMobile player)
		{ 
			OpponentTeam.Remove(player); 
		}
		public void AddChallengerDead( PlayerMobile player )
		{
			ChallengerDead.Add(player);
		}
		public void AddOpponentDead( PlayerMobile player )
		{
			OpponentDead.Add(player);
		}
		public void ClearAll()
		{
			Active = true;
			ArrayList Players = new ArrayList();
			foreach ( PlayerMobile opponent in OpponentTeam )
			{
				opponent.IsInChallenge = false;
				Players.Add(opponent);
				opponent.CloseGump(typeof(ReportMurdererGump));
			}
			foreach( PlayerMobile challenger in ChallengeTeam )
			{
				challenger.IsInChallenge = false;
				Players.Add(challenger);
				challenger.CloseGump(typeof(ReportMurdererGump));
			}
			ChallengeTeam.Clear();
			ChallengerDead.Clear();
			OpponentTeam.Clear();
			OpponentDead.Clear();
			new InvulTimer(Players).Start();
		}  
	
		public override void OnDelete()
		{
			base.OnDelete ();
			if( Challenge.Challenge.WorldStones.Contains( this ))
			{
				Challenge.Challenge.WorldStones.Remove( this );
			}
		}

		public override void OnDoubleClick( Mobile from ) 
		{
			if( Active )
			{
				PlayerMobile pm = from as PlayerMobile;
				Active = false;
				pm.IsInChallenge = true;

				from.Hidden = false;
				this.ChallengeTeam.Add( pm );
				from.Target = new ChallengeTarget( pm,this, 0 );

				if( m_Game == ChallengeGameType.TwoPlayerTeam )
					from.SendMessage( 33, "Please Choose Your Partner " );
				else
					from.SendMessage( 33, "Please Choose Your Opponent " );	
			}

		}
		public void makeready(Mobile who)    
		{	
			PlayerMobile c = (PlayerMobile) who;
			if( !who.Alive )
				who.Resurrect();
			
			Container bp = c.Backpack;
			Container bankbag = new Bag();
			bankbag.Hue = 63;
			BankBox bank = c.BankBox;
			Item oncurs = c.Holding;
						
			if(oncurs != null)
				bp.DropItem(oncurs);
						
			c.SendMessage( "You have 10 seconds until the duel begins" );
			c.SendMessage( 63, "After one of you dies, both of you will be teleported out" );
						
			c.Criminal = true;
			c.CurePoison(c);
	
			c.Blessed = true;
			c.Frozen = true;

			c.Hits = c.HitsMax;
			c.Mana = c.ManaMax;
			c.Stam = c.StamMax;
			
			c.StatMods.Clear();

			if(bp != null)
			{
				Item toDisarm = c.FindItemOnLayer( Layer.OneHanded );
				
				if ( toDisarm == null || !toDisarm.Movable )
					toDisarm = c.FindItemOnLayer( Layer.TwoHanded );
				
				if (toDisarm != null)
					bp.DropItem(toDisarm);

				if ( c.Mount != null )
				{
					IMount mount = c.Mount;	
					mount.Rider = null;
					if( mount is BaseMount )
					{
						BaseMount oldMount = (BaseMount)mount;	
						oldMount.Map = Map.Internal;
						c.TempMount = oldMount;
					}
				}
				
				//while((BasePotion)bp.FindItemByType(typeof(BasePotion)) != null)
				//{
				//	BasePotion potion = (BasePotion)bp.FindItemByType(typeof(BasePotion)); 
				//	bankbag.DropItem(potion);
				//}
				while((EtherealMount)bp.FindItemByType(typeof(EtherealMount)) != null)
				{
					EtherealMount mount = (EtherealMount)bp.FindItemByType(typeof(EtherealMount)); 
					bankbag.DropItem(mount);
				}
				/*Item[] weps = bp.FindItemsByType( typeof( BaseWeapon ) ); 
				for ( int i = 0; i < weps.Length; ++i ) 
				{ 
					Item item = (Item)weps[i];
					BaseWeapon weapon = item as BaseWeapon;
					if( weapon.DamageLevel > WeaponDamageLevel.Regular ) 
								bankbag.DropItem( item );
				}*/
				if( bankbag.Items.Count > 0 )
					bank.DropItem(bankbag);
				else
					bankbag.Delete();
			} 
		}

		public class InvulTimer : Timer
		{
			private ArrayList m_Players;
			private int secs = 0;

			public InvulTimer(ArrayList players)
				: base(TimeSpan.FromSeconds(.5), TimeSpan.FromSeconds(.5))
			{
				m_Players = players;
			}

			protected override void OnTick()
			{
				secs++;
				foreach (PlayerMobile m in m_Players)
				{
					m.CloseGump(typeof(ReportMurdererGump));
				}

				if (secs >= 20)
				{
					foreach (PlayerMobile m in m_Players)
					{
						m.Blessed = false;
						m.SendMessage(63, "You are no longer Invulnerable!");
						m.CloseGump(typeof(ReportMurdererGump));
						Stop();						
					}
				}
			}
		}

		public void TimerStart()
		{
			new InternalTimer( this ).Start();
		}
		public class InternalTimer : Timer 
		{  
			private int m_Count = 10;
			private ChallengeStone m_Item;
			private ArrayList m_Players = new ArrayList();
			private DateTime dualstart;
			
			public InternalTimer( ChallengeStone item ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Item = item;
				m_Players.AddRange( item.ChallengeTeam );
				m_Players.AddRange( item.OpponentTeam );
			} 
			
			public void nocheat(Mobile who)    
			{
				Targeting.Target.Cancel( who );
				who.MagicDamageAbsorb = 0;
				who.MeleeDamageAbsorb = 0;
				ProtectionSpell.Registry.Remove( who );
				DefensiveSpell.Nullify( who );
			}
			protected override void OnTick() 
			{  
				m_Count--; 
                		
				if ( (m_Count % 5) == 0 && m_Count > 10)
				{	
					foreach ( PlayerMobile pm in m_Players )
						pm.SendMessage( "{0} seconds until the duel begins.", m_Count );
				}
				else if(m_Count <= 10 && m_Count > 0)
				{
					foreach ( PlayerMobile pm in m_Players )
						pm.SendMessage( "{0}", m_Count );
				}
				else if ( m_Count == 0 ) 
				{
					foreach ( PlayerMobile pm in m_Players )
					{
						nocheat((Mobile)pm);
						pm.Frozen = false;
						pm.Blessed = false;
						ChallengeRing ring = new ChallengeRing( m_Item, m_Players );
						pm.EquipItem( ring );
						pm.SendMessage( 43, "GO GO GO GO GO GO GO GO GO GO!" );
					}
					new FightTimer( m_Item, m_Players ).Start();
					Stop();
				} 
			}			
		}
		public class FightTimer : Timer 
		{  
			private double m_Count;
			private int m_Seconds;
			private ChallengeStone m_Item;
			private ArrayList m_Players;
				
			public FightTimer( ChallengeStone item, ArrayList players ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.0 ))
			{	
				m_Item = item;
				m_Players = players;
				m_Count = item.DualLength;
				m_Seconds = item.DualLength * 60;
			} 
		
			public void attrib(Mobile who)
			{
				PlayerMobile m = who as PlayerMobile;
			 	
				RemoveRing(m);
				m.Aggressed.Clear();
				m.Aggressors.Clear();
				if(!who.Alive)
					who.Resurrect();

				if(m.TempMount != null)
				{
					m.TempMount.Rider = m;
					m.TempMount = null;
				}			 	
				m.Criminal = false;
				m.PlaySound( 0x214 );
				m.FixedEffect( 0x376A, 10, 16 );
				m.Hits = 125;
				m.Mana = 125;
				m.Stam = 125;					
			}
			public void RemoveRing( PlayerMobile m )
			{
				ChallengeRing ring =  new ChallengeRing();

				foreach( Item item in m.Items )
				{
					if( item is ChallengeRing )
						ring = item as ChallengeRing;
				}
				if( ring != null )
					ring.Delete();
			}
			public void winner(PlayerMobile winner)
			{
				winner.BankBox.Open();
				if (m_Item.m_SameAccount)
					winner.SendMessage(38, "You will not recieve a win for this duel do to your account IP is shared with your contestant!  Nice try though.");
				else if( m_Item.m_LastKiller )
					winner.SendMessage(38, "You must duel someone else before recieving a win point from this person again.");
				else
					winner.Wins += 1;

				winner.Blessed = true;
				winner.SendMessage( 43, "Congratulations on winning the duel!");
				winner.SendMessage( 43, "Don't forget to retreive your items from the bank!!");
				winner.SendMessage( 63, "You have been made Invulnerable for 10 seconds. Use this time to re-equip and tab to un-aggro");
				winner.CloseGump(typeof(ReportMurdererGump));
			}

			public void loser(PlayerMobile loser)
			{
				loser.BankBox.Open();

				if (m_Item.m_SameAccount)
				{
					loser.SendMessage(38, "You will not recieve a lose for this duel due to your account IP is shared with your contestant!  Nice try though.");
					m_Item.m_SameAccount = false;
				}
				else if (m_Item.m_LastKiller)
				{
					loser.SendMessage(38, "You must duel someone else before recieving a lose point from this person again.");
					m_Item.m_LastKiller = false;
				}
				else
					loser.Loses += 1;
					
				loser.Blessed = true;
				loser.SendMessage( 43, "You lost! Better luck next time!");
				loser.SendMessage( 43, "Don't forget to retreive your items from the bank!!");
				loser.SendMessage( 63, "You have been made Invulnerable for 10 seconds. Use this time to re-equip and tab to un-aggro");
				loser.CloseGump(typeof(ReportMurdererGump));
			}

			protected override void OnTick() 
			{ 
				m_Seconds--;
				m_Count = (m_Seconds / 60);

				if ( (m_Count % 5) == 0 && m_Count <= 30 && (double)m_Count == (double)m_Seconds/60)
				{	
					foreach ( PlayerMobile pm in m_Players )
						pm.SendMessage( "The Duel will end in {0} minutes ", m_Count );	
				}
				else if ( m_Count == 1 && (double)m_Count == (double)m_Seconds/60 )
				{	
					foreach ( PlayerMobile pm in m_Players )
						pm.SendMessage( "1 minute left" );
				}
				else if ( m_Seconds <= 10 )
				{	
					foreach ( PlayerMobile pm in m_Players )
						pm.SendMessage( "{0}", m_Seconds );
				}
				else if ( m_Count == 0 )
				{	
					foreach ( PlayerMobile pm in m_Players )
					{
						pm.SendMessage( 43, "You failed to kill your opponent. The game is ending..." );
						pm.SendMessage( 43, "Don't forget to retreive your items from the bank!!");
						attrib( pm as Mobile );
					}

					foreach( PlayerMobile challenger in m_Item.ChallengeTeam )
						challenger.Location = m_Item.m_ChallengerExitPointDest;
						
					foreach (PlayerMobile opponent in m_Item.OpponentTeam )
						opponent.Location = m_Item.m_OpponentExitPointDest;		

					m_Item.ClearAll();
					Stop();
				}
				foreach( PlayerMobile challenger in m_Item.ChallengeTeam )
				{
					if ( !challenger.Alive )
					{
						if ( !m_Item.ChallengerDead.Contains( challenger ))
							m_Item.AddChallengerDead( challenger );
					}
				}		
				foreach (PlayerMobile opponent in m_Item.OpponentTeam )
				{
					if ( !opponent.Alive )
					{
						if ( !m_Item.OpponentDead.Contains( opponent ))
							m_Item.AddOpponentDead( opponent );
					}
				}
				if (m_Item.ChallengerDead.Count == m_Item.ChallengeTeam.Count || m_Item.OpponentDead.Count == m_Item.OpponentTeam.Count)
				{
					foreach (PlayerMobile pm in m_Players)
					{
						attrib(pm as Mobile);
					}
					foreach (PlayerMobile challenger in m_Item.ChallengeTeam)
					{
						if (challenger.NetState != null)
							challenger.Location = m_Item.m_ChallengerExitPointDest;
						else
							challenger.LogoutLocation = new Point3D(1416, 1700, 1);
					}
					foreach (PlayerMobile opponent in m_Item.OpponentTeam)
					{
						if (opponent.NetState != null)
							opponent.Location = m_Item.m_OpponentExitPointDest;
						else
							opponent.LogoutLocation = new Point3D(1416, 1700, 1);
					}
					if (m_Item.ChallengerDead.Count == m_Item.ChallengeTeam.Count && m_Item.OpponentDead.Count == m_Item.OpponentTeam.Count)
					{
						foreach (PlayerMobile challenger in m_Item.ChallengeTeam)
							challenger.SendMessage(63, "Neither side lost, due to all fighters being dead..");

						foreach (PlayerMobile opponent in m_Item.OpponentTeam)
							opponent.SendMessage(63, "Neither side lost, due to all fighters being dead..");
					}
					if (m_Item.ChallengerDead.Count == m_Item.ChallengeTeam.Count)
					{
						foreach (PlayerMobile opponent in m_Item.OpponentTeam)
						{
							Account killerAccount = opponent.Account as Account;

							foreach (PlayerMobile challenger in m_Item.ChallengeTeam)
							{
								Account victimAccount = challenger.Account as Account;

								ArrayList killerList;
								ArrayList victimList;

								killerList = new ArrayList(killerAccount.LoginIPs);
								victimList = new ArrayList(victimAccount.LoginIPs);

								for (int i = 0; i < killerList.Count; i++)
								{
									for (int j = 0; j < victimList.Count; j++)
									{
										if (Convert.ToString(victimList[j] as IPAddress) == Convert.ToString(killerList[i] as IPAddress))
										{
											m_Item.m_SameAccount = true;
											break;
										}
									}
									if (m_Item.m_SameAccount)
										break;
								}
								if (m_Item.m_SameAccount)
									break;
							}
							if (m_Item.m_SameAccount)
								break;
						}

						foreach (PlayerMobile opponent in m_Item.OpponentTeam)
						{
							for (int i = 0; i < m_Item.ChallengeTeam.Count; i++)
							{
								PlayerMobile challenger = (PlayerMobile)(m_Item.ChallengeTeam[i]);
								if (opponent.Name == challenger.Name)
								{
									m_Item.m_LastKiller = true;
								}
							}
							winner(opponent);
						}
						foreach (PlayerMobile challenger in m_Item.ChallengeTeam)
						{
							loser(challenger);
						}
					}
					if (m_Item.OpponentDead.Count == m_Item.OpponentTeam.Count)
					{
						foreach (PlayerMobile opponent in m_Item.OpponentTeam)
						{
							Account killerAccount = opponent.Account as Account;

							foreach (PlayerMobile challenger in m_Item.ChallengeTeam)
							{
								Account victimAccount = challenger.Account as Account;

								ArrayList killerList;
								ArrayList victimList;

								killerList = new ArrayList(killerAccount.LoginIPs);
								victimList = new ArrayList(victimAccount.LoginIPs);

								for (int i = 0; i < killerList.Count; i++)
								{
									for (int j = 0; j < victimList.Count; j++)
									{
										if (Convert.ToString(victimList[j] as IPAddress) == Convert.ToString(killerList[i] as IPAddress))
										{
											m_Item.m_SameAccount = true;
											break;
										}
									}
									if (m_Item.m_SameAccount)
										break;
								}
								if (m_Item.m_SameAccount)
									break;
							}
							if (m_Item.m_SameAccount)
								break;
						}

						foreach (PlayerMobile challenger in m_Item.ChallengeTeam)
						{
							for (int i = 0; i < m_Item.OpponentTeam.Count; i++)
							{
								PlayerMobile opponent = (PlayerMobile)(m_Item.OpponentTeam[i]);
								if (challenger.Name == opponent.Name)
								{
									m_Item.m_LastKiller = true;
								}
							}
							winner(challenger);
						}
						foreach (PlayerMobile opponent in m_Item.OpponentTeam)
							loser(opponent);

					}
					m_Item.ClearAll();
					Stop();

				}
				else if( m_Count != 0)
				{
					bool someoneleft = false;
					foreach (PlayerMobile pm in m_Players)
					{
						if(pm.Region.Name != "DuelPit")
						{
							someoneleft = true;
							break;
						}
					}

					if (someoneleft)
					{
						foreach (PlayerMobile pm in m_Players)
						{
							pm.SendMessage(43, "Someone lost connection. The game is ending...");
							pm.SendMessage(43, "Don't forget to retreive your items from the bank!!");
							attrib(pm as Mobile);
						}

						foreach (PlayerMobile challenger in m_Item.ChallengeTeam)
							challenger.Location = m_Item.m_ChallengerExitPointDest;

						foreach (PlayerMobile opponent in m_Item.OpponentTeam)
							opponent.Location = m_Item.m_OpponentExitPointDest;

						m_Item.ClearAll();
						Stop();
					}
				}
			}
		}
		public ChallengeStone( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 3 );// version

			writer.Write( (int)dualLength);
			writer.Write( (int)m_Game );
			writer.Write( m_ChallengerExitPointDest );
			writer.Write( m_OpponentExitPointDest );
			writer.Write( true );
			writer.Write( m_ChallengerPointDest );
			writer.Write( m_OpponentPointDest );
			writer.Write( m_MapDest );
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt();
 
			switch ( version )
			{	  
				case 3:
				{
					dualLength = reader.ReadInt();
					goto case 2;
				}
				case 2:
				{
					m_Game = (ChallengeGameType)reader.ReadInt();
					goto case 1;
				}			  

				case 1:
				{
					m_ChallengerExitPointDest = reader.ReadPoint3D();
					m_OpponentExitPointDest = reader.ReadPoint3D();
					goto case 0;
				}
			  
				case 0:
				{
					m_Active = reader.ReadBool();
					m_ChallengerPointDest = reader.ReadPoint3D();
					m_OpponentPointDest = reader.ReadPoint3D();
					m_MapDest = reader.ReadMap();
					break;
				}
			}
		}
	} 
} 