//By Nerun
using System;
using System.IO;
using System.Collections;
using System.Reflection;
using Server;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Items;
using Server.Misc; 

namespace Server.Scripts.Commands
{
	public class CustomCmdHandlers
	{
		public static void Initialize()
		{
			Register( "GMbody", AccessLevel.Counselor, new CommandEventHandler( GM_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
			Server.Commands.Register( command, access, handler );
		}
		
		[Usage( "GMbody" )]
		[Description( "Helps senior staff members set their body to GM style." )]
		public static void GM_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new GMmeTarget();
		}

		private class GMmeTarget : Target
		{
			public GMmeTarget() : base( -1, false, TargetFlags.None )
			{
			}

			private static void DisRobe( Mobile m_from, Container cont, Layer layer ) 
			{ 
				if ( m_from.FindItemOnLayer( layer ) != null )
				{
					Item item = m_from.FindItemOnLayer( layer );
					cont.AddItem( item );
				}
			}

			private static Mobile m_Mobile;

			private static void EquipItem( Item item )
			{
				EquipItem( item, false );
			}

			private static void EquipItem( Item item, bool mustEquip )
			{
				if ( !Core.AOS )
					item.LootType = LootType.Newbied;

				if ( m_Mobile != null && m_Mobile.EquipItem( item ) )
					return;

				Container pack = m_Mobile.Backpack;

				if ( !mustEquip && pack != null )
					pack.DropItem( item );
				else
					item.Delete();
			}

			private static void PackItem( Item item )
			{
				if ( !Core.AOS )
					item.LootType = LootType.Newbied;

				Container pack = m_Mobile.Backpack;

				if ( pack != null )
					pack.DropItem( item );
				else
					item.Delete();
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					Mobile targ = (Mobile)targeted;
					if ( from != targ ) 
						from.SendMessage( "You may only set your own body to GM style." );

					else 
					{
						m_Mobile = from;

						CommandLogging.WriteLine( from, "{0} {1} is assuming a GM body", from.AccessLevel, CommandLogging.Format( from ) );

						Container pack = from.Backpack;

						if ( pack == null )
						{
							pack = new Backpack();
							pack.Movable = false;

							from.AddItem( pack );
						}

						from.Body = 0x190;
						from.Fame = 0;
						from.Karma = 0;
						from.Kills = 0;
						from.Hidden = true;
						from.Hits = from.HitsMax;
						from.Mana = from.ManaMax;
						from.Stam = from.StamMax;

						if(from.AccessLevel >= AccessLevel.GameMaster)
						{
							EquipItem( new StaffRing() );
							Spellbook book1 = new Spellbook( (ulong)18446744073709551615 );
							Spellbook book2 = new NecromancerSpellbook( (ulong)0xffff );
							Spellbook book3 = new BookOfChivalry();

							PackItem( book1 );
							PackItem( book2 );
							PackItem( book3 );
							PackItem( new EtherealHorse() );

							from.RawStr = 100;
							from.RawDex = 100;
							from.RawInt = 100;
							from.Hits = from.HitsMax;
							from.Mana = from.ManaMax;
							from.Stam = from.StamMax;

							for ( int i = 0; i < targ.Skills.Length; ++i )
								targ.Skills[i].Base = 120;
						}

						if(from.AccessLevel == AccessLevel.Counselor)
						{
							EquipItem( new CounselorRobe() );
							EquipItem( new ThighBoots( 3 ) );
							PackItem( new CounselorHide() );
							from.Title = "[Counselor]";
							from.AddItem( new ShortHair(3) );
							from.AddItem( new Vandyke(3) );
						}

						if(from.AccessLevel == AccessLevel.GameMaster)
						{
							EquipItem( new GMRobe() );
							EquipItem( new ThighBoots( 39 ) );
							PackItem( new SeerHide( 39 ) );
							from.Title = "[GM]";
							from.AddItem( new ShortHair(39) );
							from.AddItem( new Vandyke(39) );
						}

						if(from.AccessLevel == AccessLevel.Seer)
						{
							EquipItem( new SeerRobe() );
							EquipItem( new ThighBoots( 467 ) );
							PackItem( new SeerHide() );
							from.Title = "[Seer]";
							from.AddItem( new ShortHair(467) );
							from.AddItem( new Vandyke(467) );
						}

						if(from.AccessLevel == AccessLevel.Administrator)
						{
							EquipItem( new AdminRobe() );
							EquipItem( new ThighBoots( 1001 ) );
							PackItem( new AdminHide() );
							from.Title = "[Admin]";
							from.AddItem( new ShortHair(1001) );
							from.AddItem( new Vandyke(1001) );
						}
					}
				}
			}
		}
	}
}