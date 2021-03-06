using System; 
using System.Collections; 
using Server.Items;
using Server.Engines.BulkOrders;

namespace Server.Mobiles 
{ 
   public class SBAthome : SBInfo 
   { 
      private ArrayList m_BuyInfo = new InternalBuyInfo(); 
      private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

      public SBAthome() 
      { 
      } 

      public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
      public override ArrayList BuyInfo { get { return m_BuyInfo; } } 

      public class InternalBuyInfo : ArrayList 
      { 
         public InternalBuyInfo() 
         {  
            Add( new GenericBuyInfo( "1025367", typeof( ClothingBlessDeed ),1000000,20,0x14F0, 0 ) );
            Add( new GenericBuyInfo( "1025369", typeof( NameChangeDeed ),300000,20,0x14F0, 0 ) );
            Add( new GenericBuyInfo( "1025369", typeof( SpecialHairDye ),350000,20,0xE26, 0 ) );
			Add(new GenericBuyInfo("1025369", typeof(SpecialBeardDye), 350000, 20, 0xE26, 0));
			Add(new GenericBuyInfo("1025369", typeof(BrownBearRugEastDeed), 1000000, 20, 0x14F0, 0));
			Add(new GenericBuyInfo("1025369", typeof(BrownBearRugSouthDeed), 1000000, 20, 0x14F0, 0));
			Add(new GenericBuyInfo("1025369", typeof(PolarBearRugEastDeed), 1000000, 20, 0x14F0, 0));
			Add(new GenericBuyInfo("1025369", typeof(PolarBearRugEastDeed), 1000000, 20, 0x14F0, 0));
			Add(new GenericBuyInfo("1025369", typeof(PolarBearMask), 750000, 20, 0x1545, 0)); 
            Add(new GenericBuyInfo( "1025369", typeof( SkillBall),500000,20,0x1869, 0 ) );
			Add(new GenericBuyInfo("1025369", typeof(BulkOrderBook), 30000, 20, 0x2259, 0)); 
	} 
      

	} 
      		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				
            }
		} 
	} 
}
