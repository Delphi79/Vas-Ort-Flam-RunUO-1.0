using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Prompts
{
	public class RenamePlayerPrompt : Prompt
	{
		private Mobile m_Player;

		public RenamePlayerPrompt( Mobile player )
		{
			m_Player = player;
		}

		public override void OnResponse( Mobile from, string text )
		{
			if ( NameVerification.Validate( text, 1, 16, true, false, true, 0, NameVerification.Empty ) )
				from.Name = text;
			else
				from.SendMessage( "That name is unacceptable." );
		}
	}
}
