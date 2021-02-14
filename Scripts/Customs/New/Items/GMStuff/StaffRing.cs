// By Nerun

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class StaffRing : BaseRing
	{
		[Constructable]
		public StaffRing() : base( 0x108a )
		{
			Weight = 0.1;
			Name = "The Staff Ring";
			Attributes.LowerRegCost = 100;
			Attributes.LowerManaCost = 100;
			Attributes.RegenHits = 200;
			Attributes.RegenStam = 200;
			Attributes.RegenMana = 200;
			Attributes.SpellDamage = 500;
			Attributes.CastRecovery = 12;
			Attributes.CastSpeed = 12;
			LootType = LootType.Blessed;
		}

		public StaffRing( Serial serial ) : base( serial )
		{
		}

		public override bool OnEquip( Mobile from )
		{
			base.OnEquip( from );
			if ( from.AccessLevel >= AccessLevel.Counselor )
			{
				from.LightLevel = 25;
				from.SendMessage( "You start using nightsight ability." );
				return true;
			}
			else
			{
				from.SendMessage( "Your not a Staff member, you may not wear this Item..." );
				return false;
			}
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent );
			Mobile mobile = parent as Mobile;
			if(mobile != null)
			{
				mobile.LightLevel = 0;
				mobile.CheckLightLevels( true );
				mobile.SendMessage( "You stop using nightsight ability." );
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel < AccessLevel.Counselor )
			{
				from.SendMessage ( "Your not a Staff member, you may not wear this Item..." ); 
				this.Delete();
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