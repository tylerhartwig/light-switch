using System;
namespace LightSwitch
{
	public class Quote : IComparable<Quote>
	{
		public int ID { get; set; } = -1;
		public string Text { get; set; } = string.Empty;
		public string Reference { get; set; } = string.Empty;

		public Quote()
		{
		}

		public Quote(QuoteDO quoteDO)
		{
			ID = quoteDO.ID;
			Text = quoteDO.Text;
			Reference = quoteDO.Reference;
		}

		public override bool Equals(object obj)
		{
			return CompareTo((Quote)obj) == 0;
		}

		public int CompareTo(Quote other)
		{
			var idVal = ID.CompareTo(other.ID);
			var textVal = Text.CompareTo(other.Text);
			var referenceVal = Reference.CompareTo(other.Reference);

			if (idVal == 0)
			{
				if (referenceVal == 0)
				{
					return textVal;
				}
				return referenceVal;
			}

			return idVal;
		}
	}
}
