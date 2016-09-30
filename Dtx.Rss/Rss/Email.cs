namespace Dtx.Rss
{
	/// <summary>
	/// Email
	/// </summary>
	public class Email : object
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="address"></param>
		/// <param name="fullName"></param>
		public Email(string address, string fullName) : base()
		{
			Address = address;
			FullName = fullName;
		}

		private string _address;

		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			get
			{
				return (_address);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					throw (new System.Exception("[Email]: Address is required!"));
				}

				if (System.Text.RegularExpressions.Regex.IsMatch
					(value, Dtx.Text.RegularExpressions.Patterns.Email) == false)
				{
					throw (new System.Exception
						("[Email]: Address value is not a valid email address!"));
				}

				_address = value.Trim();
			}
		}

		private string _fullName;

		/// <summary>
		/// 
		/// </summary>
		public string FullName
		{
			get
			{
				return (_fullName);
			}
			set
			{
				if (String.IsNullOrEmptyOrWhiteSpace(value))
				{
					throw (new System.Exception("[Email]: FullName is required!"));
				}

				_fullName = value.Trim();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string strResult =
				string.Format("{0} ({1})", Address, FullName);

			return (strResult);
		}
	}
}
