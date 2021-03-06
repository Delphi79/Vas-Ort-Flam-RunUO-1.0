// By Neon
// Improved By Dddie

using System; 
using Server; 
using Server.Network; 

namespace Server.Items 
{ 
   public class MagicCrystalBall : Item 
   { 
      [Constructable] 
      public MagicCrystalBall() : base( 0xE2E ) 
      { 
         this.Weight = 10; 
         this.Stackable = false; 
         this.LootType = LootType.Blessed; 
         this.Light = LightType.Circle150; 
      }

	public override void OnSingleClick( Mobile from )
	{
		if ( this.Name == null )
		{
			LabelTo( from, "a crystal ball" );
		}
		else
		{
			LabelTo( from, this.Name );
		}
	}

      public override void OnDoubleClick( Mobile from ) 
      { 
         this.PublicOverheadMessage( MessageType.Regular, 0x3B2, 1007000 + Utility.Random( 28 )); 
      } 

      public MagicCrystalBall( Serial serial ) : base( serial ) 
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