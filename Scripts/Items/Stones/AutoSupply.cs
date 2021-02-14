using System; 
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class AutoSupply : Item
	{
		[Constructable]
		public AutoSupply() : base( 0x1183 )
		{
			this.Movable = false;
			this.Name = "Supply Stone";
		}

		public AutoSupply( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InLOS( this.GetWorldLocation() ) )
			{
				from.SendLocalizedMessage( 502800 ); // You can't see that.
				return;
			}		
			
			if ( from.GetDistanceToSqrt( this.GetWorldLocation() ) > 4 )
			{
				from.SendLocalizedMessage( 500446 ); // That is too far away.
				return;
			}

			from.SendMessage( "You have been given some supplies based on your skills." );

			//4 pouches
			for (int i=0;i<4;++i)
			{
				Pouch p = new Pouch();
				p.TrapType = TrapType.MagicTrap;
				p.TrapPower = 1;
				p.Hue = 0x25;
				PackItem( from, p );
			}

            if (from.Skills[SkillName.Magery].Value >= 50.0)
                GiveLeatherArmor(from);
            else
                GiveBoneArmor(from);

            if ( from.Skills[SkillName.Magery].Value >= 50.0 )
            {
                PackItem(from, new BagOfReagents());
            }

			if ( from.Skills[SkillName.Healing].Value >= 50.0 )
				PackItem( from, new Bandage( 100 ) );

			if ( from.Skills[SkillName.Fencing].Value >= 50.0 )
			{
				PackItem( from, new ShortSpear() );
				if ( from.Skills[SkillName.Parry].Value >= 50.0 )
				{
					GiveItem( from, new Kryss() );
					GiveItem( from, new MetalKiteShield() );
				}
				else
				{
					GiveItem( from, new Spear() );
				}
			}
			
			if ( from.Skills[SkillName.Swords].Value >= 50.0 )
			{
				if ( from.Skills[SkillName.Parry].Value >= 50.0 )
				{
					GiveItem( from, new MetalKiteShield() );
				}

                GiveItem(from, new Katana());
				GiveItem( from, new Halberd() );
			}
			
			if ( from.Skills[SkillName.Macing].Value >= 50.0 )
			{
				if ( from.Skills[SkillName.Parry].Value >= 50.0 )
					GiveItem( from, new MetalKiteShield() );
				GiveItem( from, new WarAxe() );
                GiveItem(from, new WarHammer());
			}
			
			if ( from.Skills[SkillName.Archery].Value >= 50.0 )
			{
				GiveItem( from, new HeavyCrossbow() );
                GiveItem(from, new Crossbow());
                GiveItem(from, new Bow());

				PackItem( from, new Bolt( 100 ) );
				PackItem( from, new Arrow( 100 ) );
			}

			if ( from.Skills[SkillName.Poisoning].Value >= 50.0 )
			{
				for (int i=0;i<5;i++)
					PackItem( from, new GreaterPoisonPotion() );
			}

            PlayerMobile pm = (PlayerMobile)(from);

            Container bag = new Bag();
            Container backpack = pm.Backpack;

            for (int i = 0; i < 20; i++)
            {
                GreaterCurePotion cure = new GreaterCurePotion();
                bag.DropItem(cure);
            }

            for (int i = 0; i < 20; i++)
            {
                GreaterHealPotion heal = new GreaterHealPotion();
                bag.DropItem(heal);
            }

            for (int i = 0; i < 20; i++)
            {
                TotalRefreshPotion refresh = new TotalRefreshPotion();
                bag.DropItem(refresh);
            }

            backpack.DropItem(bag);
		}

        public static void GiveItem(Mobile m, Item item)
		{
			if ( item is BaseArmor )
				((BaseArmor)item).Quality = ArmorQuality.Exceptional;
			else if ( item is BaseWeapon )
				((BaseWeapon)item).Quality = WeaponQuality.Exceptional;

			Item move = m.FindItemOnLayer( item.Layer );
			if ( move != null )
			{
				if ( !m.PlaceInBackpack( move ) )
				{
					item.Delete();
					return;
				}
			}

			if ( !m.EquipItem( item ) && !m.PlaceInBackpack( item ) )
				item.Delete();
		}

		public static void PackItem( Mobile m, Item item )
		{
			if ( item is BaseArmor )
				((BaseArmor)item).Quality = ArmorQuality.Exceptional;
			else if ( item is BaseWeapon )
				((BaseWeapon)item).Quality = WeaponQuality.Exceptional;

			if ( !m.PlaceInBackpack( item ) )
				item.Delete();
		}

		public static void GiveBoneArmor( Mobile m )
		{
			GiveItem( m, new BoneArms() );
			GiveItem( m, new BoneChest() );
			GiveItem( m, new BoneLegs() );
			GiveItem( m, new BoneGloves() );
		}

		public static void GiveLeatherArmor( Mobile m )
		{
			GiveItem( m, new LeatherArms() );
			GiveItem( m, new LeatherChest() );
			GiveItem( m, new LeatherGloves() );
			GiveItem( m, new LeatherGorget() );
			GiveItem( m, new LeatherLegs() );
		}
	}
}