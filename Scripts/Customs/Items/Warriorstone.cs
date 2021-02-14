using System; 
using Server.Items;

namespace Server.Items
{ 
public class Warriorstone : Item 
{ 
[Constructable] 
public Warriorstone() : base( 0xED4 ) 
{ 
Movable = false; 
Hue = 1266; 
Name = "Stat Stone"; 
} 

public override void OnDoubleClick( Mobile m ) 
{ 
m.Str = 90;
m.Int = 35;
m.Dex = 100;
} 

public Warriorstone( Serial serial ) : base( serial ) 
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