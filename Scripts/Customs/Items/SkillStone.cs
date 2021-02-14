using System;
using Server.Network;
using Server.Items;
using Server.Gumps;

namespace Server.Items
{
	public class SkillStone : Item
	{
		[Constructable]
		public SkillStone()
			: base(3804)
		{
			Movable = false;
			Name = "A skill Stone";
			Hue = 1260;
		}

		public SkillStone(Serial serial)
			: base(serial)
		{
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


		public override void OnDoubleClick(Mobile from)
		{
			if (!from.InRange(GetWorldLocation(), 2))
			{
				from.SendLocalizedMessage(500446); // That is too far away. 
			}
			else
			{
				from.SendGump(new SkillStoneGump2(from, from));
			}
		}
	}
}

namespace Server.Gumps
{
	public class SkillStoneGump2 : Gump
	{
		private const int FieldsPerPage = 14;

		private Mobile m_From;
		private Mobile m_Mobile;

		public SkillStoneGump2(Mobile from, Mobile mobile)
			: base(20, 30)
		{
			m_From = from;
			m_Mobile = mobile;

			AddPage(0);
			AddBackground(0, 0, 260, 351, 5054);

			AddImageTiled(10, 10, 240, 23, 0x52);
			AddImageTiled(11, 11, 238, 21, 0xBBC);

			AddLabel(65, 11, 0, "Skills");

			AddPage(1);

			int page = 1;

			int index = 0;

			Skills skills = mobile.Skills;

			int number;
			if (Core.AOS)
				number = 0;
			else
				number = 3;

			for (int i = 0; i < (skills.Length - number); ++i)
			{
				if (index >= FieldsPerPage)
				{
					AddButton(231, 13, 0x15E1, 0x15E5, 0, GumpButtonType.Page, page + 1);

					++page;
					index = 0;

					AddPage(page);

					AddButton(213, 13, 0x15E3, 0x15E7, 0, GumpButtonType.Page, page - 1);
				}

				Skill skill = skills[i];
				AddImageTiled(10, 32 + (index * 22), 240, 23, 0x52);
				AddImageTiled(11, 33 + (index * 22), 238, 21, 0xBBC);

				AddLabelCropped(13, 33 + (index * 22), 150, 21, 0, skill.Name);
				AddImageTiled(180, 34 + (index * 22), 50, 19, 0x52);
				AddImageTiled(181, 35 + (index * 22), 48, 17, 0xBBC);
				AddLabelCropped(182, 35 + (index * 22), 234, 21, 0, skill.Base.ToString("F1"));

				if (from.AccessLevel >= AccessLevel.Player)
					AddButton(231, 35 + (index * 22), 0x15E1, 0x15E5, i + 1, GumpButtonType.Reply, 0);
				else
					AddImage(231, 35 + (index * 22), 0x2622);

				++index;
			}
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			if (info.ButtonID > 0)
				m_From.SendGump(new EditSkillsGump(m_From, m_Mobile, info.ButtonID - 1));
		}
	}

	public class EditSkillsGump : Gump
	{
		private Mobile m_From;
		private Mobile m_Mobile;
		private Skill m_Skill;

		public EditSkillsGump(Mobile from, Mobile mobile, int skillNo)
			: base(20, 30)
		{
			m_From = from;
			m_Mobile = mobile;
			m_Skill = mobile.Skills[skillNo];

			if (m_Skill == null)
				return;

			AddPage(0);

			AddBackground(0, 0, 90, 60, 5054);

			AddImageTiled(10, 10, 72, 22, 0x52);
			AddImageTiled(11, 11, 70, 20, 0xBBC);
			AddTextEntry(11, 11, 70, 20, 0, 0, m_Skill.Base.ToString("F1"));
			AddButton(15, 35, 0xFB7, 0xFB8, 1, GumpButtonType.Reply, 0);
			AddButton(50, 35, 0xFB1, 0xFB2, 0, GumpButtonType.Reply, 0);
		}

		public override void OnResponse(NetState state, RelayInfo info)
		{
			if (info.ButtonID > 0)
			{
				TextRelay text = info.GetTextEntry(0);

				if (text != null)
				{
					try
					{
						double count = 0;
						for (int i = 0; i < state.Mobile.Skills.Length; ++i)
						{
							if (m_Skill != state.Mobile.Skills[i])
								count = count + state.Mobile.Skills[i].Base;
						}

						double value = Convert.ToDouble(text.Text);
						if (value < 0.0 || value > 100.0)
						{
							state.Mobile.SendMessage("Value too high. 0-100 only .");
						}
						else if ((count + value) > 700)
						{
							state.Mobile.SendMessage("You may edit your skills, but with a max of 7 skills with up to 80 skillpoints each. Try setting another skill lower first.");
						}
						else
						{
							m_Skill.Base = value;
						}
					}
					catch
					{
						state.Mobile.SendMessage("Bad format. ###.# expected");
					}
				}
			}
		}
	}
}