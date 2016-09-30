namespace Dtx.Rss
{
	/// <summary>
	/// Guid stands for globally unique identifier.
	/// It's a string that uniquely identifies the item.
	/// When present, an aggregator may choose to use this string to determine if an item is new.
	/// </summary>
	public class Guid : object
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="isPermaLink"></param>
		/// <param name="value"></param>
		public Guid(bool isPermaLink, string value) : base()
		{
			Value = value;
			IsPermaLink = isPermaLink;
		}

		/// <summary>
		/// If the guid element has an attribute named isPermaLink with a value of true,
		/// the reader may assume that it is a permalink to the item, that is,
		/// a url that can be opened in a Web browser, that points to the full item described by the [item] element.
		/// [isPermaLink] is optional, its default value is true.
		/// If its value is false, the guid may not be assumed to be a url, or a url to anything in particular.
		/// </summary>
		public bool IsPermaLink { get; set; }

		private string _value;

		/// <summary>
		/// There are no rules for the syntax of a guid value.
		/// Aggregators must view them as a string.
		/// It's up to the source of the feed to establish the uniqueness of the string.
		/// </summary>
		public string Value
		{
			get
			{
				return (_value);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					throw (new System.Exception("[Guid]: Value can not be null or empty!"));
				}

				value = value.Trim();

				if (IsPermaLink == false)
				{
					_value = value;
				}
				else
				{
					if (System.Text.RegularExpressions.Regex.IsMatch
						(value, Dtx.Text.RegularExpressions.Patterns.Url) == false)
					{
						throw (new System.Exception("[Guid]: Value is not a valid url!"));
					}

					_value = value;
				}
			}
		}
	}
}
