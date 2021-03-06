using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Network;
using Server.ContextMenus;

namespace Server.Mobiles
{
	public class Atvendor : BaseVendor
	{
		private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public Atvendor() : base( "the Attin Vendor" )
		{
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAthome() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.Sandals; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override  void InitBody() 
		{
			base.InitBody();
		}

		public override void InitOutfit()
		{
			AddItem( new Server.Items.Bandana( 0x1 ) );     //印花手帕
			AddItem( new Server.Items.FullApron( 0x1 ) );   //全围裙
			AddItem( new Server.Items.Shirt( 0x47E ) );     //汗衫
			AddItem( new Server.Items.Shoes( 0x47E ) );     //普通鞋
			AddItem( new Server.Items.ShortPants( 0x47E ) );//短裤
			int hairHue=0x481;
			switch ( Utility.Random( 9 ) )
			{
				case 0: AddItem( new Afro( hairHue ) ); break;
				case 1: AddItem( new KrisnaHair( hairHue ) ); break;
				case 2: AddItem( new PageboyHair( hairHue ) ); break;
				case 3: AddItem( new PonyTail( hairHue ) ); break;
				case 4: AddItem( new ReceedingHair( hairHue ) ); break;
				case 5: AddItem( new TwoPigTails( hairHue ) ); break;
				case 6: AddItem( new ShortHair( hairHue ) ); break;
				case 7: AddItem( new LongHair( hairHue ) ); break;
				case 8: AddItem( new BunsHair( hairHue ) ); break;
			}
		}

		public Atvendor( Serial serial ) : base( serial )
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
