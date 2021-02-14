using System; 
using Server.Items;
using System.Collections;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Regions;

namespace Server.Items
{ 
   [Serializable()] 
   public class VoteStone : Item 
   { 
      [Constructable] 
      public VoteStone() : base( 0xEDB ) 
      { 
         Movable = false;  
         Hue = 1154;
	 Name = "Top 100 Voting Stone"; 
      
      } 

      public override void OnDoubleClick( Mobile sender ) 
      { 
		  if ( sender.InRange( this, 2) )
		  {
			  sender.SendMessage( "Opening a browser to the home page!" );
			  sender.LaunchBrowser( "http://www.topshards.com/index.php?do=votes&id=180" );
		  }
		  else
		  {
			  sender.SendMessage( "You are too far away to use that!" );
		  }
      }

      public VoteStone( Serial serial ) : base( serial ) 
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
   } 
} 
