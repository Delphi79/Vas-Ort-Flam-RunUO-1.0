using System;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	public class SeerHide : BaseGMJewel
	{
		[Constructable]
		public SeerHide() : base(AccessLevel.GameMaster, 69, 0xE73 )
		{
			Name = "Seer Hide Ball";
		}

		public SeerHide( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from ) 
		{
			if ( Parent != from ) 
			if (from.AccessLevel < AccessLevel.GameMaster)
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
					from.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
					from.PlaySound( 0x225 );
					from.Hidden = true;
				} 
				else 
				{ 
					from.Hidden=false;
					from.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
					from.PlaySound( 0x225 );
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