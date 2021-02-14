//////////////////////////////////
//			           //
//      Scripted by Raelis      //
//		             	 //
//////////////////////////////////
using System;
using System.Collections;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public enum Egg_Types
	{
		Dragon,
		WhiteWyrm,
		Wyvern
	}
	public class EvoEgg: Item
	{
		public bool m_AllowEvolution;
		private DateTime m_End;
		private Egg_Types  m_Egg_Type;
		private bool m_Hatch_Override;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public Egg_Types Egg_Type
		{
			get{ return m_Egg_Type; }
			set{ m_Egg_Type = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Hatch_Override
		{
			get{ return m_Hatch_Override; }
			set{ m_Hatch_Override = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowEvolution
		{
			get{ return m_AllowEvolution; }
			set{ m_AllowEvolution = value; }
		}
		
		[Constructable]
		public EvoEgg() : base( 0x0C5D )
		{
			RND_Egg_Type();
			Name = "an egg";
			Hue = Hueing();
			AllowEvolution = true;
			Weight = 16.0;
			m_End = DateTime.Now + TimeSpan.FromDays( 1.0 );
		}
		
		public EvoEgg( Serial serial ) : base ( serial )
		{
		}
		
		public void RND_Egg_Type()
		{
			switch(Utility.Random(3))
			{
				case 0:
					Egg_Type = Egg_Types.Dragon;
					break;
					
				case 1:
					Egg_Type = Egg_Types.WhiteWyrm;
					break;
					
				case 2:
					Egg_Type = Egg_Types.Wyvern;
					break;
			}
		}
		
		public int Hueing()
		{
			int hue = 0;
			switch (m_Egg_Type)
			{
				case Egg_Types.Dragon:
					hue = Utility.RandomList( 337, 2205, 1157, 1194, 1654, 1645, 2117, 1157, 2118 );
					break;
				case Egg_Types.WhiteWyrm:
					hue = Utility.RandomList( 396, 2219, 2224, 2219, 1150, 1154, 1153, 1150, 1319);
					break;
				case Egg_Types.Wyvern:
					hue = Utility.RandomList( 1746, 743, 1030, 1110, 1052, 1118, 1446, 1801 );
					break;
			}
			return hue;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendMessage( "You must have the egg in your backpack to hatch it." );
			}
			else if ( from.Skills[(int)SkillName.AnimalTaming].Base < 93.9) 
				from.SendMessage( "You wouldnt know the first thing about looking after a hatchinling." );
			else if ( from.Followers >= 5 )
				from.SendMessage( "You have too many charges currently to look after and couldnt possibaly look after another." );
			else if ( (this.AllowEvolution == true && DateTime.Now >= m_End)|| Hatch_Override )
			{
				this.Delete();
				from.SendMessage( "You are now the proud owner of a hatchling!!" );
				
				Hatch(from);
			}
			else
			{
				from.SendMessage( "This egg is not yet ready to be hatched." );
			}
		}
		
		public void Hatch(Mobile from)
		{
			switch (m_Egg_Type)
			{
				case Egg_Types.Dragon:
					Drag(from);
					break;
				case Egg_Types.WhiteWyrm:
					WW(from);
					break;
				case Egg_Types.Wyvern:
					Wyv(from);
					break;
			}
		}
		
		public void WW(Mobile from)
		{
			EvoWWSnake Evo = new EvoWWSnake();
			New(from, Evo);
		}
		
		public void Drag(Mobile from)
		{
			EvoDragSnake Evo = new EvoDragSnake();
			New(from, Evo);
		}
		
		public void Wyv(Mobile from)
		{
			EvoWyvSnake Evo = new EvoWyvSnake();
			New(from, Evo);
		}
		
		public void New(Mobile from, BaseCreature Evo)
		{
			Evo.Map = from.Map;
			Evo.Location = from.Location;
			
			Evo.Controled = true;
			
			Evo.ControlMaster = from;
			
			Evo.IsBonded = true;
			
			Evo.Hue = Hue;
			
			Evo.Name = "a hatchling";
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.WriteDeltaTime( m_End );
			writer.Write( (int) m_Egg_Type );
			writer.Write( (bool) m_AllowEvolution );
			writer.Write( (bool) m_Hatch_Override );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			switch ( version )
			{
				case 0:
					m_End = reader.ReadDeltaTime();
					m_Egg_Type = (Egg_Types)reader.ReadInt();
					m_AllowEvolution = reader.ReadBool();
					m_Hatch_Override = reader.ReadBool();
					break;
			}
			
		}
		
	}
}
