/////////////////////////////////////////////////
//
// Automatically generated by the
// AddonGenerator script by Arya
//
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class minicarpet6eastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new minicarpet6eastAddonDeed();
			}
		}

		[ Constructable ]
		public minicarpet6eastAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 2804 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 2803 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 2803 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 2803 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 2802 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 2801 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 2799 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 2805 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 2805 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 2805 );
			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 2804 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 2804 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 2804 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 2802 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 2802 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 2802 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 2800 );
			AddComponent( ac, -2, 3, 0 );
			ac = new AddonComponent( 2798 );
			AddComponent( ac, 2, 3, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 2796 );
			AddComponent( ac, 1, 2, 0 );

		}

		public minicarpet6eastAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class minicarpet6eastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new minicarpet6eastAddon();
			}
		}

		[Constructable]
		public minicarpet6eastAddonDeed()
		{
			Name = "minicarpet6east";
		}

		public minicarpet6eastAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}