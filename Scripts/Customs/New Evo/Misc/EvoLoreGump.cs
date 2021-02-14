using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Targeting;

namespace Server.SkillHandlers
{
public class EvoLoreGump : Gump
	{
		private static string FormatSkill( BaseCreature c, SkillName name )
		{
			Skill skill = c.Skills[name];

			if ( skill.Base == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0:F1}</div>", skill.Base );
		}

		private static string FormatAttributes( int cur, int max )
		{
			if ( max == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}/{1}</div>", cur, max );
		}

		private static string FormatStat( int val )
		{
			if ( val == 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}</div>", val );
		}

		private static string FormatElement( int val )
		{
			if ( val <= 0 )
				return "<div align=right>---</div>";

			return String.Format( "<div align=right>{0}%</div>", val );
		}
		
		private const int LabelColor = 0;

		public EvoLoreGump( BaseCreature c ) : base( 250, 50 )
		{
			AddPage( 0 );

			AddImage( 100, 100, 2080 );
			AddImage( 118, 137, 2081 );
			AddImage( 118, 207, 2081 );
			AddImage( 118, 277, 2081 );
			AddImage( 118, 347, 2083 );

			AddHtml( 147, 108, 210, 18, String.Format( "<center><i>{0}</i></center>", c.Name ), false, false );

			AddButton( 240, 77, 2093, 2093, 2, GumpButtonType.Reply, 0 );

			AddImage( 140, 138, 2091 );
			AddImage( 140, 335, 2091 );

			int pages = 3;
			int page = 0;


			#region Attributes
			AddPage( ++page );

			AddImage( 128, 152, 2086 );
			AddHtmlLocalized( 147, 150, 160, 18, 1049593, 200, false, false ); // Attributes

			AddHtmlLocalized( 153, 168, 160, 18, 1049578, LabelColor, false, false ); // Hits
			AddHtml( 280, 168, 75, 18, FormatAttributes( c.Hits, c.HitsMax ), false, false );

			AddHtmlLocalized( 153, 186, 160, 18, 1049579, LabelColor, false, false ); // Stamina
			AddHtml( 280, 186, 75, 18, FormatAttributes( c.Stam, c.StamMax ), false, false );

			AddHtmlLocalized( 153, 204, 160, 18, 1049580, LabelColor, false, false ); // Mana
			AddHtml( 280, 204, 75, 18, FormatAttributes( c.Mana, c.ManaMax ), false, false );

			AddHtmlLocalized( 153, 222, 160, 18, 1028335, LabelColor, false, false ); // Strength
			AddHtml( 320, 222, 35, 18, FormatStat( c.Str ), false, false );

			AddHtmlLocalized( 153, 240, 160, 18, 3000113, LabelColor, false, false ); // Dexterity
			AddHtml( 320, 240, 35, 18, FormatStat( c.Dex ), false, false );

			AddHtmlLocalized( 153, 258, 160, 18, 3000112, LabelColor, false, false ); // Intelligence
			AddHtml( 320, 258, 35, 18, FormatStat( c.Int ), false, false );

			
				AddImage( 128, 278, 2086 );
				AddHtmlLocalized( 147, 276, 160, 18, 3001016, 200, false, false ); // Miscellaneous

				AddHtmlLocalized( 153, 294, 160, 18, 1049581, LabelColor, false, false ); // Armor Rating
				AddHtml( 320, 294, 35, 18, FormatStat( c.VirtualArmor ), false, false );
			

			AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
			AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, pages );
			#endregion

			#region Skills
			AddPage( ++page );

			AddImage( 128, 152, 2086 );
			AddHtmlLocalized( 147, 150, 160, 18, 3001030, 200, false, false ); // Combat Ratings

			AddHtmlLocalized( 153, 168, 160, 18, 1044103, LabelColor, false, false ); // Wrestling
			AddHtml( 320, 168, 35, 18, FormatSkill( c, SkillName.Wrestling ), false, false );

			AddHtmlLocalized( 153, 186, 160, 18, 1044087, LabelColor, false, false ); // Tactics
			AddHtml( 320, 186, 35, 18, FormatSkill( c, SkillName.Tactics ), false, false );

			AddHtmlLocalized( 153, 204, 160, 18, 1044086, LabelColor, false, false ); // Magic Resistance
			AddHtml( 320, 204, 35, 18, FormatSkill( c, SkillName.MagicResist ), false, false );

			AddHtmlLocalized( 153, 222, 160, 18, 1044061, LabelColor, false, false ); // Anatomy
			AddHtml( 320, 222, 35, 18, FormatSkill( c, SkillName.Anatomy ), false, false );

			AddHtml( 153, 240, 160, 18, String.Format( "Firebreath"), false, false ); // Ability(poison 1044090/firebreath/dispell
			if ( ((BaseEvo)c).Ability_Skill > 0)
			AddHtml( 320, 240, 35, 18, String.Format( "<div align=right>{0:F1}</div>", ((BaseEvo)c).Ability_Skill ), false, false );
			else 
				AddHtml( 320, 240, 35, 18, String.Format( "<div align=right>---</div>"), false, false );
			AddImage( 128, 260, 2086 );
			AddHtmlLocalized( 147, 258, 160, 18, 3001032, 200, false, false ); // Lore & Knowledge

			AddHtmlLocalized( 153, 276, 160, 18, 1044085, LabelColor, false, false ); // Magery
			AddHtml( 320, 276, 35, 18, FormatSkill( c, SkillName.Magery ), false, false );

			AddHtmlLocalized( 153, 294, 160, 18, 1044076, LabelColor, false, false ); // Evaluating Intelligence
			AddHtml( 320, 294, 35, 18,FormatSkill( c, SkillName.EvalInt ), false, false );

			AddHtmlLocalized( 153, 312, 160, 18, 1044106, LabelColor, false, false ); // Meditation
			AddHtml( 320, 312, 35, 18, FormatSkill( c, SkillName.Meditation ), false, false );

			AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, page + 1 );
			AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			#endregion

			#region Misc
			AddPage( ++page );

			AddImage( 128, 152, 2086 );
			AddHtmlLocalized( 147, 150, 160, 18, 1049563, 0, false, false ); // Preferred Foods
						
			AddHtml( 183, 168, 210, 18, String.Format( ", Gems" ), false, false );//X Axis
			AddHtmlLocalized( 153, 168, 160, 18, 1049564, LabelColor, false, false );
			
			AddImage( 128, 188, 2086 );
			AddHtml( 147, 186, 160, 18, String.Format( "Kill Points" ), false, false ); // Pack Instincts

			AddHtml( 153, 204, 160, 18, String.Format( "{0}/{1}", ((BaseEvo)c).KP, ((BaseEvo)c).m_KP_Lv ), false, false ); // KP

			
			AddImage( 128, 224, 2086 );
			AddHtml( 147, 222, 160, 18, String.Format( "Dragons Breath" ), false, false ); // Ability
			AddHtml( 153, 240, 160, 18, String.Format( "{0}/{1}", ((BaseEvo)c).Ability_Charges, ((BaseEvo)c).Ability_ChargesMax ), false, false );
			
			
			AddImage( 128, 260, 2086 );// Y Axis
			AddHtmlLocalized( 147, 258, 160, 18, 1049594, 0, false, false ); // Loyalty Rating Y Axis

			AddHtmlLocalized( 153, 276, 160, 18, (!c.Controled || c.Loyalty == PetLoyalty.None) ? 1061643 : 1049594 + (int)c.Loyalty, LabelColor, false, false );
			

			AddButton( 340, 358, 5601, 5605, 0, GumpButtonType.Page, 1 );
			AddButton( 317, 358, 5603, 5607, 0, GumpButtonType.Page, page - 1 );
			#endregion
		}
	}
}
