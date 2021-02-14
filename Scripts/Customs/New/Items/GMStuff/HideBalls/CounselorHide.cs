using System;
using Server.Network;
using Server.Targeting;
using Server.Items;
using Server.Scripts.Commands;

namespace Server.Items
{
	public class CounselorHide : BaseGMJewel
	{
		[Constructable]
		public CounselorHide() : base(AccessLevel.Counselor, 4, 0xE73 )
		{
			Name = "Counselor Hide Ball";
		}

		public CounselorHide( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( Parent != from ) 
			if (from.AccessLevel < AccessLevel.Counselor)
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
					string prefix = Server.Commands.CommandPrefix;
					Commands.Handle( from, String.Format( "{0}hide", prefix ) );
				} 
				else 
				{ 
					string prefix = Server.Commands.CommandPrefix;
					Commands.Handle( from, String.Format( "{0}unhide", prefix ) );
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