using System;
namespace LightSwitch
{
	public class MessageViewModel : BaseViewModel
	{
		private Message message;

		public string Text
		{
			get { return message.Text; }
			set
			{
				message.Text = value;
				OnPropertyChanged();
			}
		}

		public MessageViewModel()
		{
			message = new Message();
		}

		public MessageViewModel(Message message)
		{
			this.message = message;
		}
	}
}
