using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Scripts.Commands 
{
	public class BlowPouch 
	{ 
		public static void Initialize() 
		{ 
			Server.Commands.Register( "BlowPouch", AccessLevel.Player, new CommandEventHandler( BlowPouch_OnCommand ) );
		} 

		[Usage( "BlowPouch" )] 
		public static void BlowPouch_OnCommand( CommandEventArgs e ) //do the command and set the command event argumant as "e"
		{ 
			// e.Mobile is the player that used the command
			Item[] items = e.Mobile.Backpack.FindItemsByType( typeof( Pouch ) ); //set a list of items that is typeof pouch in the player backpack.

			bool b_Found = false; //we set a bool variable to check if we found a trapped pauch or not

			foreach( Pouch tl in items ) //for every item that is in the list we made do this loop, that'll loop through ALL the pauches untill you'll break it which is what you did before.
			{
				if ( tl.TrapType == TrapType.MagicTrap ) //If we find trapped pauch we do this:
				{
					tl.OnDoubleClick(e.Mobile);//we double click the pauch
					b_Found = true;//since we found a pauch we set it to true so we'll know
					break;
				}

			}
			if (b_Found) //if b_Found == true we do this, in other words if we found a pouch
				e.Mobile.SendMessage("You have used a trapped pouch."); //send message that we blow up a pouch (it exploded so it might not be realy usefull)
			else //if b_Found == false do this, in other words if we didn't find ANY trapped pouches.
				e.Mobile.SendMessage("You dont have any trapped pouches."); //send a message to the player that we didn't found any pouches.
		} 
	} 
}