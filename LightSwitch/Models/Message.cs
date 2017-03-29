using System;
namespace LightSwitch
{
	public class Message : IComparable<Message>
	{
		public int ID { get; set; } = -1;
		public string Text { get; set; } = string.Empty;

		public Message()
		{
		}

		public Message(MessageDO messageDO)
		{
			ID = messageDO.ID;
			Text = messageDO.Text;
		}

		public override bool Equals(object obj)
		{
			return CompareTo((Message)obj) == 0;
		}

		public int CompareTo(Message other)
		{
			var idVal = ID.CompareTo(other.ID);
			var textVal = Text.CompareTo(other.Text);

			if (idVal == 0)
			{
				return textVal;
			}

			return idVal;
		}
	}
}
