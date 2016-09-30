namespace Dtx.Rss
{
	/// <summary>
	/// Utility
	/// </summary>
	public static class Utility
	{
		/// <summary>
		/// 
		/// </summary>
		static Utility()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		internal static string GetFormattedDateTime(System.DateTime? dateTime)
		{
			if (dateTime.HasValue == false)
			{
				return (null);
			}
			else
			{
				System.Globalization.CultureInfo oCultureInfo =
					new System.Globalization.CultureInfo("en-US");

				System.Threading.Thread.CurrentThread.CurrentCulture = oCultureInfo;
				System.Threading.Thread.CurrentThread.CurrentUICulture = oCultureInfo;

				return (dateTime.Value.ToString("ddd, dd MMM yyyy HH:mm:ss G\\MT"));
			}
		}

		/// <summary>
		/// Get some DataTable from Rss file
		/// </summary>
		/// <param name="source"></param>
		/// <param name="address"></param>
		/// <param name="section"></param>
		/// <returns></returns>
		public static System.Data.DataTable GetRssFeed
			(Enums.Sources source, string address, Enums.Sections section)
		{
			System.Data.DataSet oDataSet = null;
			System.IO.StreamReader oStreamReader = null;
			System.Xml.XmlTextReader oXmlTextReader = null;

			try
			{
				switch (source)
				{
					case Enums.Sources.Url:
					{
						oXmlTextReader = new System.Xml.XmlTextReader(address);
						break;
					}

					case Enums.Sources.Drive:
					{
						oStreamReader =
							new System.IO.StreamReader(address, System.Text.Encoding.UTF8);

						oXmlTextReader =
							new System.Xml.XmlTextReader(oStreamReader);
						break;
					}
				}

				oDataSet = new System.Data.DataSet();

				oDataSet.ReadXml(oXmlTextReader, System.Data.XmlReadMode.Auto);

				int intIndex = 0;
				int intItemTableIndex = -1;

				while ((intIndex <= oDataSet.Tables.Count - 1) && (intItemTableIndex == -1))
				{
					if (string.Compare(oDataSet.Tables[intIndex].TableName,
						section.ToString(), ignoreCase: true) == 0)
					{
						intItemTableIndex = intIndex;
					}
					intIndex++;
				}

				if (intItemTableIndex == -1)
				{
					return (null);
				}
				else
				{
					return (oDataSet.Tables[intItemTableIndex]);
				}
			}
			catch
			{
				if (oDataSet != null)
				{
					oDataSet.Dispose();
					oDataSet = null;
				}
				return (null);
			}
			finally
			{
				if (oStreamReader != null)
				{
					oStreamReader.Dispose();
					oStreamReader = null;
				}

				if (oXmlTextReader != null)
				{
					oXmlTextReader.Close();
					oXmlTextReader = null;
				}
			}
		}

		/// <summary>
		/// Create Rss Feed and write it to the stream.
		/// </summary>
		/// <param name="root"></param>
		/// <returns></returns>
		public static System.IO.Stream Publish(Root root)
		{
			if (root == null)
			{
				throw (new System.ArgumentNullException
					("Root", "[Root] value can not be null!"));
			}

			if (root.Channel == null)
			{
				throw (new System.ArgumentNullException
					("Root.Channel", "[Channel] property of [Root] value can not be null!"));
			}

			System.IO.Stream oStream =
				new System.IO.MemoryStream();

			System.Xml.XmlTextWriter oXmlTextWriter =
				new System.Xml.XmlTextWriter(oStream, System.Text.Encoding.UTF8);

			oXmlTextWriter.WriteStartElement("rss");
			oXmlTextWriter.WriteAttributeString("version", "2.0");
			oXmlTextWriter.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");

			oXmlTextWriter.WriteStartElement("channel");

			oXmlTextWriter.WriteStartElement("atom:link");
			oXmlTextWriter.WriteAttributeString("href", root.CreatorUrl);
			oXmlTextWriter.WriteAttributeString("rel", "self");
			oXmlTextWriter.WriteAttributeString("type", "application/rss+xml");
			oXmlTextWriter.WriteEndElement();

			oXmlTextWriter.WriteElementString("title", root.Channel.Title);
			oXmlTextWriter.WriteElementString("link", root.Channel.Link);
			oXmlTextWriter.WriteElementString("description", root.Channel.Description);

			oXmlTextWriter.WriteElementString("language", root.Channel.Language);

			if (root.Channel.PubDate.HasValue)
			{
				oXmlTextWriter.WriteElementString("pubDate", root.Channel.FormattedPubDate);
			}

			if (root.Channel.LastBuildDate.HasValue)
			{
				oXmlTextWriter.WriteElementString("lastBuildDate", root.Channel.FormattedLastBuildDate);
			}

			if (string.IsNullOrEmpty(root.Channel.Docs) == false)
			{
				oXmlTextWriter.WriteElementString("docs", root.Channel.Docs);
			}

			if (string.IsNullOrEmpty(root.Channel.Generator) == false)
			{
				oXmlTextWriter.WriteElementString("generator", root.Channel.Generator);
			}

			if (root.Channel.ManagingEditor != null)
			{
				oXmlTextWriter.WriteElementString("managingEditor", root.Channel.ManagingEditor.ToString());
			}

			if (root.Channel.WebMaster != null)
			{
				oXmlTextWriter.WriteElementString("webMaster", root.Channel.WebMaster.ToString());
			}

			if (string.IsNullOrEmpty(root.Channel.Copyright) == false)
			{
				oXmlTextWriter.WriteElementString("copyright", root.Channel.Copyright);
			}

			if (root.Image != null)
			{
				oXmlTextWriter.WriteStartElement("image");

				oXmlTextWriter.WriteElementString("url", root.Image.Url);

				// **************************************************
				string strLink = root.Image.Link;
				if (string.IsNullOrEmpty(strLink))
				{
					strLink = root.Channel.Link;
				}
				oXmlTextWriter.WriteElementString("link", strLink);
				// **************************************************

				// **************************************************
				string strTitle = root.Image.Title;
				if (string.IsNullOrEmpty(strTitle))
				{
					strTitle = root.Channel.Title;
				}
				oXmlTextWriter.WriteElementString("title", strTitle);
				// **************************************************

				if (string.IsNullOrEmpty(root.Image.Description) == false)
				{
					oXmlTextWriter.WriteElementString("description", root.Image.Description);
				}

				if (root.Image.Width.HasValue)
				{
					oXmlTextWriter.WriteElementString("width", root.Image.Width.Value.ToString());
				}

				if (root.Image.Height.HasValue)
				{
					oXmlTextWriter.WriteElementString("height", root.Image.Height.Value.ToString());
				}

				oXmlTextWriter.WriteEndElement();
			}

			foreach (Item itmCurrent in root.Items)
			{
				oXmlTextWriter.WriteStartElement("item");

				if (string.IsNullOrEmpty(itmCurrent.Title) == false)
				{
					oXmlTextWriter.WriteElementString("title", itmCurrent.Title);
				}

				if (string.IsNullOrEmpty(itmCurrent.Link) == false)
				{
					oXmlTextWriter.WriteElementString("link", itmCurrent.Link);
				}

				if (string.IsNullOrEmpty(itmCurrent.Description) == false)
				{
					oXmlTextWriter.WriteElementString("description", itmCurrent.Description);
				}

				if (itmCurrent.Author != null)
				{
					oXmlTextWriter.WriteElementString("author", itmCurrent.Author.ToString());
				}

				if (string.IsNullOrEmpty(itmCurrent.Comments) == false)
				{
					oXmlTextWriter.WriteElementString("comments", itmCurrent.Comments);
				}

				if (itmCurrent.Guid != null)
				{
					oXmlTextWriter.WriteStartElement("guid");

					if (itmCurrent.Guid.IsPermaLink)
					{
						oXmlTextWriter.WriteAttributeString("isPermaLink", "true");
					}
					else
					{
						oXmlTextWriter.WriteAttributeString("isPermaLink", "false");
					}

					oXmlTextWriter.WriteString(itmCurrent.Guid.Value);

					oXmlTextWriter.WriteEndElement();
				}

				if (itmCurrent.PubDate.HasValue)
				{
					oXmlTextWriter.WriteElementString("pubDate", itmCurrent.FormattedPubDate);
				}

				oXmlTextWriter.WriteEndElement();
			}

			oXmlTextWriter.WriteEndElement();
			oXmlTextWriter.WriteEndElement();

			oXmlTextWriter.Flush();

			//oXmlTextWriter.Close();
			//oXmlTextWriter = null;

			oStream.Position = 0;

			return (oStream);
		}
	}
}
