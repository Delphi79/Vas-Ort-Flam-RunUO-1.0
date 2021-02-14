using System;
using System.Collections;
using System.Text;
using Server;
using Server.Network;
using Server.Gumps;
using Server.Factions;



namespace Server.Items
{
[Flipable( 0x1E5E, 0x1E5F )]
public class FactionBoard : BaseFactionBoard
{

public int itemID;

[Constructable]
public FactionBoard( ) : base( 0x1E5E )
{

}

public FactionBoard( Serial serial ) : base( serial )
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

public abstract class BaseFactionBoard : Item
{
private string m_BoardName;
public int hue;
public Faction m_Faction;


[CommandProperty( AccessLevel.GameMaster )]
public string BoardName
{
get{ return m_BoardName; }
set{ m_BoardName = value; }
}

public BaseFactionBoard( int itemID ) : base( itemID )
{
m_BoardName = "faction board";
Movable = false;
this.hue = 0x0544;
}

public virtual void Cleanup()
{
ArrayList items = this.Items;

for ( int i = items.Count - 1; i >= 0; --i )
{
if ( i >= items.Count )
continue;

}
}

public override void OnDoubleClick( Mobile from )
{
if ( CheckRange( from ) )
{
Cleanup();
if ( !from.InRange( GetWorldLocation(), 2 ) )
from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
else 
{
from.SendGump( new FactionPlayerGump( from, null, null, 0, null, null ));
}
}

}

public virtual bool CheckRange( Mobile from )
{
if ( from.AccessLevel >= AccessLevel.GameMaster )
return true;

return ( from.Map == this.Map && from.InRange( GetWorldLocation(), 2 ) );
}

public BaseFactionBoard( Serial serial ) : base( serial )
{
}

public override void Serialize( GenericWriter writer )
{
base.Serialize( writer );

writer.Write( (int) 0 ); // version

writer.Write( (string) m_BoardName );
}

public override void Deserialize( GenericReader reader )
{
base.Deserialize( reader );

int version = reader.ReadInt();

switch ( version )
{
case 0:
{
m_BoardName = reader.ReadString();
break;
}
}
}
}
}
