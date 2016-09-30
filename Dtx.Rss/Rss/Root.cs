namespace Dtx.Rss
{
	/// <summary>
	/// Root
	/// </summary>
	public class Root : object
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="creatorUrl"></param>
		/// <param name="channel"></param>
		/// <param name="outputStream"></param>
		public Root(string creatorUrl, Channel channel) : base()
		{
			Channel = channel;
			CreatorUrl = creatorUrl;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="creatorUrl"></param>
		/// <param name="channel"></param>
		/// <param name="image"></param>
		/// <param name="outputStream"></param>
		public Root(string creatorUrl, Channel channel, Image image) : this(creatorUrl, channel)
		{
			Image = image;
		}

		private string _creatorUrl;

		/// <summary>
		/// [comments] is an optional sub-element of [item].
		/// If present, it is the URL of the comments page for the item
		/// </summary>
		/// <example>
		/// http://www.IranianExperts.ir/Rss.aspx
		/// </example>
		public string CreatorUrl
		{
			get
			{
				return (_creatorUrl);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					throw (new System.Exception("[Root]: CreatorUrl is required!"));
				}

				if (System.Text.RegularExpressions.Regex.IsMatch
					(value, Dtx.Text.RegularExpressions.Patterns.Url) == false)
				{
					throw (new System.Exception("[Root]: CreatorUrl value is not a valid url!"));
				}

				_creatorUrl = value;
			}
		}

		/// <summary>
		/// Image
		/// </summary>
		public Image Image { get; set; }

		private Channel _channel;

		/// <summary>
		/// Channel
		/// </summary>
		public Channel Channel
		{
			get
			{
				return (_channel);
			}
			set
			{
				if (value == null)
				{
					throw (new System.Exception("[Root]: Channel is required!"));
				}

				_channel = value;
			}
		}

		private System.Collections.Generic.List<Item> _items;

		/// <summary>
		/// Items of RSS Feed
		/// </summary>
		public System.Collections.Generic.List<Item> Items
		{
			get
			{
				if (_items == null)
				{
					_items = new System.Collections.Generic.List<Item>();
				}
				return (_items);
			}
		}
	}
}
