using System;
using SQLite;

namespace LightSwitch
{
	public class MessageDO
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Text { get; set; }

		public MessageDO()
		{
		}

		public MessageDO(Message message)
		{
			ID = message.ID;
			Text = message.Text;
		}
	}
}
