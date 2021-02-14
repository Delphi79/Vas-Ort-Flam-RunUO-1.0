using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Guilds;
using Server.Gumps;
using Server.Mobiles; 
using Server.Targeting;

namespace Server.Items
{
	public class PheonixDeed : Item 
	{
		[Constructable]
		public PheonixDeed() : base( 0x14F0 )
		{
			Weight = 0.1;
			Name = "Random Pheonix Armor Piece Deed";
			Hue = 43;
		}

		public PheonixDeed( Serial serial ) : base( serial )
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
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override bool DisplayLootType{ get{ return true; } }

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
		 this.Delete(); 
				from.SendMessage ( 69,"You have Been Awarded a Random Pice Of Pheonix Armor." );
						switch ( Utility.Random( 5 ) ) //Random chance of armor 
                        { 
			//	from.AddToBackpack( new NapalmShield() );
                          case 0: from.AddToBackpack( new PheonixLegs( ) ); 
                          break; 
                          case 1: from.AddToBackpack( new PheonixGloves( ) ); 
                          break; 
                          case 2: from.AddToBackpack( new PheonixGorget( ) ); 
                          break; 
                          case 3: from.AddToBackpack( new PheonixHelm( ) ); 
                          break; 
                          case 4: from.AddToBackpack( new PheonixChest( ) ); 
                          break; 
                          case 5: from.AddToBackpack( new PheonixArms( ) ); 
                          break; 
                        } 

			 }
		}	
	}
}


