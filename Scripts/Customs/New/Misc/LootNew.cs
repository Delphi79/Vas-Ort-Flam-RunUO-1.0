using System;
using System.IO;
using System.Reflection;
using Server;
using Server.Items;

namespace Server
{
	public class LootNew: Loot
	{
		#region List definitions
        private static Type[] m_WandTypes = new Type[]
            {
                typeof( ClumsyWand ),               typeof( FeebleWand ),           typeof( FireballWand ),
                typeof( GreaterHealWand ),          typeof( HarmWand ),             typeof( HealWand ),
                typeof( IDWand ),                  typeof( LightningWand ),        typeof( MagicArrowWand ),
                typeof( ManaDrainWand ),            typeof( WeaknessWand )
            };
        public static Type[] WandTypes{ get{ return m_WandTypes; } }


        private static Type[] m_ClothingTypes = new Type[]
            {
                typeof( Cloak ),		typeof( FurCape ),
                typeof( Bonnet ),               typeof( Cap ),		        typeof( FeatheredHat ),
                typeof( FloppyHat ),            typeof( FlowerGarland ),	typeof( JesterHat ),
                typeof( SkullCap ),             typeof( StrawHat ),	        typeof( TallStrawHat ),
                typeof( TricorneHat ),		typeof( WideBrimHat ),          typeof( WizardsHat ),
                typeof( BodySash ),             typeof( Doublet ),              typeof( FormalShirt ),
                typeof( FullApron ),            typeof( JesterSuit ),           typeof( Surcoat ),
                typeof( Tunic ),
                typeof( FurSarong ),            typeof( Kilt ),                 typeof( Skirt ),
                typeof( FancyDress ),           typeof( GildedDress ),          typeof( HoodedShroudOfShadows ),
                typeof( PlainDress ),           typeof( Robe ),
                typeof( LongPants ),            typeof( ShortPants ),
                typeof( FancyShirt ),           typeof( Shirt ),
                typeof( Boots ),                typeof( FurBoots ),             typeof( Sandals ),
                typeof( Shoes ),                typeof( ThighBoots ),
                typeof( HalfApron )
            };
        public static Type[] ClothingTypes{ get{ return m_ClothingTypes; } }

		#endregion

		#region Accessors

		public static BaseWand RandomWand()
		{
			return Construct( m_WandTypes ) as BaseWand;
		}

		public static BaseClothing RandomClothing()
		{
			return Construct( m_ClothingTypes ) as BaseClothing;
		}

		#endregion

		#region Construction methods
		public static Item Construct( Type type )
		{
			try
			{
				return Activator.CreateInstance( type ) as Item;
			}
			catch
			{
				return null;
			}
		}

		public static Item Construct( Type[] types )
		{
			if ( types.Length > 0 )
				return Construct( types, Utility.Random( types.Length ) );

			return null;
		}

		public static Item Construct( Type[] types, int index )
		{
			if ( index >= 0 && index < types.Length )
				return Construct( types[index] );

			return null;
		}

		public static Item Construct( params Type[][] types )
		{
			int totalLength = 0;

			for ( int i = 0; i < types.Length; ++i )
				totalLength += types[i].Length;

			if ( totalLength > 0 )
			{
				int index = Utility.Random( totalLength );

				for ( int i = 0; i < types.Length; ++i )
				{
					if ( index >= 0 && index < types[i].Length )
						return Construct( types[i][index] );

					index -= types[i].Length;
				}
			}

			return null;
		}
		#endregion
	}
}