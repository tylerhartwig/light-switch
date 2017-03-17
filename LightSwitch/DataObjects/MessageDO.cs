using System;
namespace LightSwitch
{
	public class MessageDO
	{
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
