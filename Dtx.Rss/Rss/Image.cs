namespace Dtx.Rss
{
	/// <summary>
	/// Specifies a GIF, JPEG or PNG image that can be displayed with the channel.
	/// [image] is an optional sub-element of [channel],
	/// which contains three required and three optional sub-elements.
	/// </summary>
	public class Image : object
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="url">[url] is the URL of a GIF, JPEG or PNG image that represents the channel.</param>
		/// <param name="link">[link] is the URL of the site, when the channel is rendered, the image is a link to the site.</param>
		/// <param name="title">[title] describes the image, it's used in the ALT attribute of the HTML [img] tag when the channel is rendered in HTML.</param>
		public Image(string url, string link, string title) : base()
		{
			Width = 88;
			Height = 31;

			Url = url;
			Link = link;
			Title = title;
		}

		private string _url;

		/// <summary>
		/// [url] is the URL of a GIF, JPEG or PNG image that represents the channel.
		/// </summary>
		/// <example>
		/// http://www.IranianExperts.ir/Images/Banner.gif
		/// </example>
		public string Url
		{
			get
			{
				return (_url);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					throw (new System.Exception("[Image]: Url is required!"));
				}

				if (System.Text.RegularExpressions.Regex.IsMatch
					(value, Dtx.Text.RegularExpressions.Patterns.Url) == false)
				{
					throw (new System.Exception("[Image]: Url value is not a valid url!"));
				}

				_url = value;
			}
		}

		private string _link;

		/// <summary>
		/// [link] is the URL of the site, when the channel is rendered, the image is a link to the site.
		/// Note: In practice the image [title] and [link] should have the same value as
		/// the channel's [title] and [link].
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
					throw (new System.Exception("[Image]: Link is required!"));
				}

				if (System.Text.RegularExpressions.Regex.IsMatch
					(value, Dtx.Text.RegularExpressions.Patterns.Url) == false)
				{
					throw (new System.Exception("[Image]: Link value is not a valid url!"));
				}

				_link = value;
			}
		}

		private string _title;

		/// <summary>
		/// [title] describes the image, it's used in the ALT attribute of
		/// the HTML [img] tag when the channel is rendered in HTML.
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
					throw (new System.Exception("[Image]: Title is required!"));
				}

				_title = value;
			}
		}

		private string _description;

		/// <summary>
		/// [description] contains text that is included in the TITLE attribute of
		/// the link formed around the image in the HTML rendering.
		/// </summary>
		/// <example>
		/// Iranian Experts
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

		private int? _width;

		/// <summary>
		/// [width] is indicating the width of the image in pixels.
		/// Maximum value for width is 144, default value is 88.
		/// </summary>
		/// <example>
		/// 120
		/// </example>
		public int? Width
		{
			get
			{
				return (_width);
			}
			set
			{
				if (value.HasValue == false)
				{
					_width = null;
					return;
				}

				if ((value.Value >= 0) && (value.Value <= 144))
				{
					_width = value.Value;
				}
				else
				{
					throw (new System.Exception
						("[Image]: The width value should be between (0) to (144)!"));
				}
			}
		}

		private int? _height;

		/// <summary>
		/// [height] is indicating the height of the image in pixels.
		/// Maximum value for height is 400, default value is 31
		/// </summary>
		/// <example>
		/// 40
		/// </example>
		public int? Height
		{
			get
			{
				return (_height);
			}
			set
			{
				if (value.HasValue == false)
				{
					_height = null;
					return;
				}

				if ((value.Value >= 0) && (value.Value <= 400))
				{
					_height = value.Value;
				}
				else
				{
					throw (new System.Exception
						("[Image]: The height value should be between (0) to (400)!"));
				}
			}
		}
	}
}
