namespace Dtx.Rss
{
	/// <summary>
	/// Sample
	/// </summary>
	public static class Sample
	{
		/// <summary>
		/// 
		/// </summary>
		static Sample()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static string GetRssFeed()
		{
			string strTitle = "Iranian Experts!";
			string strUrl = "http://www.IranianExperts.ir";
			System.DateTime dtm = System.DateTime.Now.AddDays(-10);

			string strFullName = "Mr. Dariush Tasdighi";
			string strEmailAddress = "DariushT@GMail.com";

			// **************************************************
			Image oImage =
				new Image(strUrl + "/Images/rss.jpg", strUrl, strTitle);

			oImage.Width = 120;
			oImage.Height = 200;
			// **************************************************

			// **************************************************
			Channel oChannel =
				new Channel(strTitle, strUrl, "RSS Feed Creator!");

			oChannel.Docs = strUrl;
			oChannel.Link = strUrl;

			oChannel.PubDate = dtm;
			oChannel.LastBuildDate = dtm;

			oChannel.Language = "fa-ir";
			oChannel.Copyright = oChannel.Generator;

			oChannel.WebMaster = new Email(strEmailAddress, strFullName);
			oChannel.ManagingEditor = new Email(strEmailAddress, strFullName);
			// **************************************************

			Item oItem = null;

			Root oRoot =
				new Root(strUrl, oChannel, oImage);

			for (int intIndex = 1; intIndex <= 5; intIndex++)
			{
				oItem =
					new Item("Title " + intIndex, strUrl + "/post.aspx?id=" + intIndex);

				oItem.PubDate = dtm;
				oItem.Description = "Description " + intIndex;
				oItem.Author = new Email(strEmailAddress, strFullName);
				oItem.Comments = strUrl + "/Comments.aspx?id=" + intIndex;
				oItem.Guid = new Guid(false, System.Guid.NewGuid().ToString());

				oRoot.Items.Add(oItem);
			}

			System.IO.Stream oStream =
				Utility.Publish(oRoot);

			oStream.Position = 0;
			var oStreamReader =
				new System.IO.StreamReader(oStream);
			string strResult = oStreamReader.ReadToEnd();

			return (strResult);
		}
	}
}
