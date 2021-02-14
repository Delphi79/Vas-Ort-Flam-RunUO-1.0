using System;
using Server;
using Server.Network;

namespace Server.Items
{
    public class DemonsSoul : Food
    {
        private DateTime m_EatStamp;

        [Constructable]
        public DemonsSoul()
            : base(1, 0x993)
        {
            Name = "a pile of demon souls";
            Weight = 2.0;
            FillFactor = 10;
            Stackable = false;
            Hue = 38;
        }

        public DemonsSoul(Serial serial)
            : base(serial)
        {
        }

        public override bool Eat(Mobile from)
        {
            if (!base.Eat(from))
                return false;

            if (m_EatStamp < DateTime.Now)
            {
                m_EatStamp += TimeSpan.FromMinutes(2);
                from.AddToBackpack(new Basket());
                from.Hits += 10;
                m_EatStamp = DateTime.Now;
                return true;
            }
            else
            {
                from.SendMessage("You may only feed on a demon souls every 2 minutes");
                return false;
            }


        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class HellSpawnGargoyleHeart : Food
    {
        private DateTime m_EatStamp;

        [Constructable]
        public HellSpawnGargoyleHeart()
            : this(1)
        {
        }

        [Constructable]
        public HellSpawnGargoyleHeart(int amount)
            : base(amount, 0x1728)
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
            this.Name = "a hellspawn gargoyle's heart";
            this.Hue = 1908;
        }

        public HellSpawnGargoyleHeart(Serial serial)
            : base(serial)
        {
        }

        public override bool Eat(Mobile from)
        {
            if (!base.Eat(from))
                return false;

            if (m_EatStamp < DateTime.Now)
            {
                m_EatStamp += TimeSpan.FromMinutes(2);
                from.AddToBackpack(new Basket());
                from.Mana += 10;
                m_EatStamp = DateTime.Now;
                return true;
            }
            else
            {
                from.SendMessage("You may only feed on a hellspawn gargoyle's heart every 2 minutes");
                return false;
            }


        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Lemon(), amount);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class HellSpawnViperHeart : Food
    {
        private DateTime m_EatStamp;

        [Constructable]
        public HellSpawnViperHeart()
            : this(1)
        {
        }

        [Constructable]
        public HellSpawnViperHeart(int amount)
            : base(amount, 0x172a)
        {
            this.Weight = 1.0;
            this.FillFactor = 1;
            this.Name = "a hellspawn viper's heart";
            this.Hue = 1256;
        }

        public HellSpawnViperHeart(Serial serial)
            : base(serial)
        {
        }

        public override bool Eat(Mobile from)
        {
            if (!base.Eat(from))
                return false;

            if (m_EatStamp < DateTime.Now)
            {
                m_EatStamp += TimeSpan.FromMinutes(2);
                from.AddToBackpack(new Basket());
                from.Stam += 10;
                m_EatStamp = DateTime.Now;
                return true;
            }
            else
            {
                from.SendMessage("You may only feed on a hellspawn viper's heart every 2 minutes");
                return false;
            }
        }

        public override Item Dupe(int amount)
        {
            return base.Dupe(new Lime(), amount);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}