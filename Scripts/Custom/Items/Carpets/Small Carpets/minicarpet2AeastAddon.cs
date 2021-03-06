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
	public class minicarpet2AeastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new minicarpet2AeastAddonDeed();
			}
		}

		[ Constructable ]
		public minicarpet2AeastAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 2767 );
			AddComponent( ac, 2, 0, 0 );
			ac = new AddonComponent( 2767 );
			AddComponent( ac, 2, -1, 0 );
			ac = new AddonComponent( 2766 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 2766 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 2766 );
			AddComponent( ac, -1, -2, 0 );
			ac = new AddonComponent( 2765 );
			AddComponent( ac, -2, 0, 0 );
			ac = new AddonComponent( 2765 );
			AddComponent( ac, -2, -1, 0 );
			ac = new AddonComponent( 2764 );
			AddComponent( ac, 2, -2, 0 );
			ac = new AddonComponent( 2762 );
			AddComponent( ac, -2, -2, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, -1, -1, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, -1, 0, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 2768 );
			AddComponent( ac, 1, 3, 0 );
			ac = new AddonComponent( 2768 );
			AddComponent( ac, 0, 3, 0 );
			ac = new AddonComponent( 2768 );
			AddComponent( ac, -1, 3, 0 );
			ac = new AddonComponent( 2767 );
			AddComponent( ac, 2, 2, 0 );
			ac = new AddonComponent( 2767 );
			AddComponent( ac, 2, 1, 0 );
			ac = new AddonComponent( 2765 );
			AddComponent( ac, -2, 2, 0 );
			ac = new AddonComponent( 2765 );
			AddComponent( ac, -2, 1, 0 );
			ac = new AddonComponent( 2763 );
			AddComponent( ac, -2, 3, 0 );
			ac = new AddonComponent( 2761 );
			AddComponent( ac, 2, 3, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, -1, 1, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, -1, 2, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 2760 );
			AddComponent( ac, 1, 2, 0 );

		}

		public minicarpet2AeastAddon( Serial serial ) : base( serial )
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

	public class minicarpet2AeastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new minicarpet2AeastAddon();
			}
		}

		[Constructable]
		public minicarpet2AeastAddonDeed()
		{
			Name = "minicarpet2Aeast";
		}

		public minicarpet2AeastAddonDeed( Serial serial ) : base( serial )
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