namespace Dtx.Rss
{
	/// <summary>
	/// Channel
	/// </summary>
	public class Channel : object
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">The name of the channel.</param>
		/// <param name="link">The URL to the HTML website corresponding to the channel.</param>
		/// <param name="description">Is phrase or sentence describing the channel.</param>
		public Channel(string title, string link, string description) : base()
		{
			Link = link;
			Title = title;
			Description = description;
		}

		private string _link;

		/// <summary>
		/// [link] is the URL to the HTML website corresponding to the channel.
		/// </summary>
		/// <example>
		/// http://www.IranianExperts.ir
		/// </example>
		public string Link
		{
			get
			{
				return (_link);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					throw (new System.Exception("[Channel]: Link is required!"));
				}

				if (System.Text.RegularExpressions.Regex.IsMatch
					(value, Dtx.Text.RegularExpressions.Patterns.Url) == false)
				{
					throw (new System.Exception("[Channel]: Link value is not a valid url!"));
				}

				_link = value;
			}
		}

		private string _title;

		/// <summary>
		/// [title] is the name of the channel.
		/// It's how people refer to your service.
		/// If you have an HTML website that contains the same information as your RSS file,
		/// the title of your channel should be the same as the title of your website.
		/// </summary>
		/// <example>
		/// Iranian Experts
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
					throw (new System.Exception("[Channel]: Title is required!"));
				}

				_title = value;
			}
		}

		private string _description;

		/// <summary>
		/// [description] is phrase or sentence describing the channel.
		/// </summary>
		/// <example>
		/// Iranian Experts Description
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
					throw (new System.Exception("[Channel]: Description is required!"));
				}

				_description = value;
			}
		}

		private string _docs;

		/// <summary>
		/// [docs] is a URL that points to the documentation for the format used in
		/// the RSS file. It's probably a pointer to this page. It's for people
		/// who might stumble across an RSS file on a Web server 25 years from now and wonder what it is.
		/// </summary>
		/// <example>
		/// http://www.IranianExperts.ir/Rss.aspx
		/// </example>
		public string Docs
		{
			get
			{
				return (_docs);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					_docs = null;
					return;
				}

				if (System.Text.RegularExpressions.Regex.IsMatch
					(value, Dtx.Text.RegularExpressions.Patterns.Url) == false)
				{
					throw (new System.Exception("[Channel]: Docs value is not a valid url!"));
				}

				_docs = value;
			}
		}

		/// <summary>
		/// [pubDate] is The publication date for the content in the channel.
		/// For example, the New York Times publishes on a daily basis,
		/// the publication date flips once every 24 hours.
		/// That's when the pubDate of the channel changes.
		/// All date-times in RSS conform to the Date and Time Specification of RFC 822,
		/// with the exception that the year may be expressed with two characters or
		/// four characters (four preferred).
		/// </summary>
		/// <example>
		/// Sat, 07 Sep 2002 00:00:01 GMT
		/// </example>
		public System.DateTime? PubDate { get; set; }

		public string FormattedPubDate
		{
			get
			{
				return (Utility.GetFormattedDateTime(PubDate));
			}
		}

		/// <summary>
		/// [copyright] is a notice for content in the channel.
		/// </summary>
		/// <example>
		/// Copyright 2004 - 2008, Iranian Experts Co.
		/// </example>
		public string Copyright { get; set; }

		/// <summary>
		/// [generator] is a string indicating the program used to generate the channel.
		/// </summary>
		/// <example>
		/// Iranian Experts Rss Generator
		/// </example>
		public string Generator
		{
			get
			{
				return ("Cute RSS - http://www.CuteRSS.ir - 1393/04/24 - Version 1.0.5");
			}
		}

		/// <summary>
		/// [webMaster] is an email address for person responsible for technical issues relating to channel.
		/// </summary>
		/// <example>
		/// DariushT@Gmail.com (Mr. Dariush Tasdighi)
		/// </example>
		public Email WebMaster { get; set; }

		/// <summary>
		/// [lastBuildDate] is the last time the content of the channel changed.
		/// </summary>
		/// <example>
		/// Sat, 07 Sep 2002 09:42:31 GMT
		/// </example>
		public System.DateTime? LastBuildDate { get; set; }

		public string FormattedLastBuildDate
		{
			get
			{
				return (Utility.GetFormattedDateTime(LastBuildDate));
			}
		}

		/// <summary>
		/// [managingEditor] is email address for person responsible for editorial content.
		/// </summary>
		/// <example>
		/// DariushT@Gmail.com (Mr. Dariush Tasdighi)
		/// </example>
		public Email ManagingEditor { get; set; }

		private string _language;

		/// <summary>
		/// The [language] the channel is written in.
		/// This allows aggregators to group all Italian language sites, for example,
		/// on a single page. A list of allowable values for this element,
		/// as provided by Netscape, is in http://www.rssboard.org/rss-language-codes.
		/// You may also use values defined by the W3C.
		/// </summary>
		/// <example>
		/// en-us
		/// </example>
		public string Language
		{
			get
			{
				return (_language);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					_language = null;
					return;
				}

				value = value.Replace(" ", string.Empty);

				try
				{
					System.Globalization.CultureInfo oCultureInfo =
						new System.Globalization.CultureInfo(value);
				}
				catch
				{
					throw (new System.Exception("[Channel]: Language value is not valid!"));
				}

				_language = value.ToLower();
			}
		}
	}
}
