using System;
namespace LightSwitch
{
	public class QuoteViewModel : BaseViewModel
	{
		private Quote quote;

		public string Text
		{
			get { return quote.Text; }
			set
			{
				quote.Text = value;
				OnPropertyChanged();
			}
		}

		public string Reference
		{
			get { return quote.Reference; }
			set
			{
				quote.Reference = value;
				OnPropertyChanged();
			}
		}

		public QuoteViewModel()
		{
			quote = new Quote();
		}

		public QuoteViewModel(Quote quote)
		{
			this.quote = quote;
		}
	}
}
