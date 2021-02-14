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
   public class Webstone : Item 
   { 
      [Constructable] 
      public Webstone() : base( 0xEDB ) 
      { 
         Movable = false;  
         Hue = 1154;
	 Name = "Website Stone"; 
      
      } 

      public override void OnDoubleClick( Mobile sender ) 
      { 
		  if ( sender.InRange( this, 2) )
		  {
			  sender.SendMessage( "Opening a browser to the home page!" );
			  sender.LaunchBrowser( "http://www.slither.co.uk/vasortflam/index.php");
		  }
		  else
		  {
			  sender.SendMessage( "You are too far away to use that!" );
		  }
      }

      public Webstone( Serial serial ) : base( serial ) 
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
