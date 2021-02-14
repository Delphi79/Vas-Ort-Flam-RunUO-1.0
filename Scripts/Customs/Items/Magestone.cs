using System; 
using Server.Items;

namespace Server.Items
{ 
public class Magestone : Item 
{ 
[Constructable] 
public Magestone() : base( 0xED4 ) 
{ 
Movable = false; 
Hue = 1266; 
Name = "Stat Stone"; 
} 

public override void OnDoubleClick( Mobile m ) 
{ 
m.RawStr = 90;
m.RawInt = 100;
m.RawDex = 35;
} 

public Magestone( Serial serial ) : base( serial ) 
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