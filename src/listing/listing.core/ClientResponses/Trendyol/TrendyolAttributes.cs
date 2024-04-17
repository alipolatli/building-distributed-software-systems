    namespace listing.core.ClientResponses.Trendyol;

	public class TrendyolAttributeRootObject
	{
		public int id { get; set; }
		public string name { get; set; } = null!;
		public string displayName { get; set; } = null!;
		public List<TrendyolAttribute> categoryAttributes { get; set; } = new();
	}

	public class TrendyolAttribute
	{
		public int categoryId { get; set; }
		public TrendyolAttributeContent attribute { get; set; }
		public bool required { get; set; }
		public bool allowCustom { get; set; }
		public bool varianter { get; set; }
		public bool slicer { get; set; }
		public List<TrendyolAttributeValue> attributeValues { get; set; } = new();
	}

	public class TrendyolAttributeContent
	{
		public int id { get; set; }
		public string name { get; set; } = null!;
	}

	public class TrendyolAttributeValue
	{
		public int id { get; set; }
		public string name { get; set; } = null!;
	}