using System;
using Server;
using Server.Network;
using Server.Gumps;
using System.Collections;
using Server.Mobiles;

namespace Server.EvilHeroSystem
{
    public class EvilHeroCore
    {
        public static void Initialize()
        {
            EventSink.Speech += new SpeechEventHandler(EventSink_Speech);
        }

        public EvilHeroCore()
        {

        }

        public EvilHeroCore(Serial serial)
        {

        }

        private static void EventSink_Speech(SpeechEventArgs args)
        {
            Mobile m = args.Mobile;
            PlayerMobile from = (PlayerMobile)(m);

            if (args.Speech.ToLower() == "i am evil incarnate" && !from.IsEvil && !from.IsHero)
            {
                //from.Name = from.Name + " (evil)";
                from.Title = "the Evil";
                from.IsEvil = true;
                //from.Spheres += 1;
                from.Lifeforce += 110;
            }

            if (args.Speech.ToLower() == "i will defend the virtues" && !from.IsEvil && !from.IsHero && from.Kills < 5)
            {
                //from.Name = from.Name + " (hero)";
                from.IsHero = true;
                //from.Spheres += 1;
                from.Lifeforce += 110;
            }

            if (args.Speech.ToLower() == "total powers" && (from.IsHero || from.IsEvil))
            {
                string score = "You have mastered sphere " + from.Spheres + " and have " + from.Lifeforce + " units of life force remaining";
                from.PublicOverheadMessage(MessageType.Regular, from.SpeechHue, true, score);
		    }

            if (args.Speech.ToLower() == "i invoke my evil powers" && from.IsEvil)
            {
                from.SendGump(new EvilGump());
            }

            if (args.Speech.ToLower() == "i invoke my good powers" && from.IsHero)
            {
				from.SendGump(new HeroGump());
            }
        }
    }
}