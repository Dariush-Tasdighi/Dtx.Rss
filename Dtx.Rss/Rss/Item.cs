namespace Dtx.Rss
{
	/// <summary>
	/// A channel may contain any number of [item]s.
	/// An item may represent a "story" -- much like a story in a newspaper or magazine;
	/// if so its description is a synopsis of the story, and the link points to the full story.
	/// An item may also be complete in itself, if so, the description contains the text
	/// (entity-encoded HTML is allowed), and the link and title may be omitted.
	/// All elements of an item are optional, however at least one of title or description must be present.
	/// </summary>
	public class Item : object
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public Item() : base()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">The title of the item.</param>
		public Item(string title) : this()
		{
			Title = title;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">The title of the item.</param>
		/// <param name="link">The URL of the item.</param>
		public Item(string title, string link) : this(title)
		{
			Link = link;
		}

		private string _link;

		/// <summary>
		/// [link] is the URL of the item.
		/// </summary>
		/// <example>
		/// http://www.IranianExperts.ir/News.aspx?ID=10
		/// </example>
		public string Link
		{
			get
			{
				return (_link);
			}
			set
			{
				if (System.Text.RegularExpressions.Regex.IsMatch
					(value, Dtx.Text.RegularExpressions.Patterns.Url) == false)
				{
					throw (new System.Exception("[Item]: Link value is not a valid url!"));
				}

				_link = value;
			}
		}

		private string _title;

		/// <summary>
		/// [title] is the title of the item.
		/// </summary>
		/// <example>
		/// Venice Film Festival Tries to Quit Sinking.
		/// </example>
		public string Title
		{
			get
			{
				return (_title);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					_title = null;
				}
				else
				{
					_title = value.Trim();
				}
			}
		}

		/// <summary>
		/// [author] is email address of the author of the item.
		/// For newspapers and magazines syndicating via RSS,
		/// the author is the person who wrote the article that
		/// the [item] describes. For collaborative weblogs,
		/// the author of the item might be different from the managing editor or webmaster.
		/// For a weblog authored by a single individual it would make sense to omit the [author] element.
		/// </summary>
		/// <example>
		/// Dariush@IranianExperts.ir (Dariush Tasdighi)
		/// </example>
		public Email Author { get; set; }

		/// <summary>
		/// [pubDate] Indicates when the item was published.
		/// Its value is a date, indicating when the item was published.
		/// If it's a date in the future, aggregators may choose to not display the item until that date.
		/// </summary>
		/// <example>
		/// Sun, 19 May 2002 15:21:36 GMT
		/// </example>
		public System.DateTime? PubDate { get; set; }

		public string FormattedPubDate
		{
			get
			{
				return (Utility.GetFormattedDateTime(PubDate));
			}
		}

		private string _comments;

		/// <summary>
		/// [comments] is URL of a page for comments relating to the item.
		/// </summary>
		/// <example>
		/// http://www.IranianExperts.ir/News/Comments
		/// </example>
		public string Comments
		{
			get
			{
				return (_comments);
			}
			set
			{
				if (System.Text.RegularExpressions.Regex.IsMatch
					(value, Dtx.Text.RegularExpressions.Patterns.Url) == false)
				{
					throw (new System.Exception("[Item]: Comments value is not a valid url!"));
				}

				_comments = value;
			}
		}

		private string _description;

		/// <summary>
		/// [description] is the item synopsis.
		/// </summary>
		/// <example>
		/// Some of the most heated chatter at the Venice Film Festival this week was about the way that
		/// the arrival of the stars at the Palazzo del Cinema was being staged.
		/// </example>
		public string Description
		{
			get
			{
				return (_description);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					_description = null;
				}
				else
				{
					_description = value.Trim();
				}
			}
		}

		/// <summary>
		/// [guid] is a string that uniquely identifies the item.
		/// guid stands for globally unique identifier.
		/// It's a string that uniquely identifies the item.
		/// When present, an aggregator may choose to use this string to determine if an item is new.
		/// </summary>
		/// <example>
		/// http://some.server.com/weblogItem3207
		/// </example>
		public Guid Guid { get; set; }
	}
}
