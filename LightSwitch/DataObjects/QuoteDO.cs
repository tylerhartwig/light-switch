using System;
namespace LightSwitch
{
	public class QuoteDO
	{
		public int ID { get; set; }
		public string Text { get; set; }
		public string Reference { get; set; }

		public QuoteDO()
		{
		}

		public QuoteDO(Quote quote)
		{
			ID = quote.ID;
			Text = quote.Text;
			Reference = quote.Reference;
		}
	}
}
