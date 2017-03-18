using System;
namespace LightSwitch
{
	public class Quote
	{
		public int ID { get; set; }
		public string Text { get; set; }
		public string Reference { get; set; }

		public Quote()
		{
		}

		public Quote(QuoteDO quoteDO)
		{
			ID = quoteDO.ID;
			Text = quoteDO.Text;
			Reference = quoteDO.Reference;
		}
	}
}
