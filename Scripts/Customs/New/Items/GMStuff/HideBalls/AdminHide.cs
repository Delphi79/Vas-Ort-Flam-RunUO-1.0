using System;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	public class AdminHide : BaseGMJewel
	{
		[Constructable]
		public AdminHide() : base(AccessLevel.Administrator, 90, 0xE73 )
		{
			Name = "Admin Hide Ball";
		}

		public AdminHide( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( Parent != from ) 
			if (from.AccessLevel < AccessLevel.Administrator)
			{
				from.SendMessage( "When you touch, it vanishes without trace..." );
				this.Consume() ;
				return ;
			}

			{
				if ( !IsChildOf( from.Backpack ) )
				{
					from.Say ( "That must be in your pack for you to use it" );
					return;
				}
           	 
				if ( !from.Hidden == true )
				{ 
					from.BoltEffect( 0 );
					from.Hidden = true;
				} 
				else 
				{ 
					from.Hidden=false;
					from.BoltEffect ( 0 );
				} 
			} 
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