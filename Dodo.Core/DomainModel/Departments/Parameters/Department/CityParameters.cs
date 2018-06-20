using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Xml.Linq;
using NLog;

namespace Dodo.Core.DomainModel.Departments.Parameters.Department
{
	[Serializable]
	public class CityParameters : DepartmentParameters
	{
		private static readonly ConcurrentDictionary<string, CityParameters> _cache = new ConcurrentDictionary<string, CityParameters>();
	
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public String LinkToDeliveryArea { get; private set; }
		public String HeaderText { get; private set; }
		public String VkontakteLink { get; private set; }
		public String FacebookLink { get; private set; }
		public String TwitterLink { get; private set; }
		public String InstagramLink { get; private set; }
		public String OKLink { get; private set; }
		public String MyWorldLink { get; private set; }
		public String KroogiLink { get; private set; }
		public String EmailFeedBack { get; private set; }
		public Decimal MinOrderPrice { get; private set; }
		public Boolean CardPaymentIsDisabled { get; private set; }
		public Decimal MaxPersonalOrderPrice { get; private set; }
		public String CallCenterPhone { get; private set; }
		public CallCenterPhoneParameter[] CallCenterPhonesParameter { get; private set; }
		public Boolean IsCalculateSalesTax { get; private set; }
		public Boolean IsCalculateVatTowardsSales { get; private set; }
		public String YandexMetrikaId { get; private set; }
		public String GoogleAnalyticsId { get; private set; }
		public String GoogleTagManagerId { get; private set; }
		public Boolean Only25 { get; private set; }

		public CityParameters
			(
			String linkToDeliveryArea,
			String headerText,
			String emailFeedBack,
			String vkontakteLink,
			String facebookLink,
			String twitterLink,
			String instagramLink,
			String okLink,
			String myWorldLink,
			String kroogiLink,
			Decimal minOrderPrice,
			Decimal maxPersonalOrderPrice,
			String phone,
			CallCenterPhoneParameter[] callCenterPhonesParameter,
			Boolean isCalculateSalesTax,
			Boolean isCalculateVatTowardsSales,
			String yandexMetrikaId,
			String googleAnalyticsId,
			String googleTagManagerId,
			Boolean only25 = false
			)
		{
			LinkToDeliveryArea = linkToDeliveryArea;
			HeaderText = headerText;
			EmailFeedBack = emailFeedBack;
			MinOrderPrice = minOrderPrice;
			VkontakteLink = vkontakteLink;
			FacebookLink = facebookLink;
			TwitterLink = twitterLink;
			InstagramLink = instagramLink;
			OKLink = okLink;
			MyWorldLink = myWorldLink;
			KroogiLink = kroogiLink;
			MaxPersonalOrderPrice = maxPersonalOrderPrice;
			CallCenterPhone = phone;
			CallCenterPhonesParameter = callCenterPhonesParameter;
			IsCalculateSalesTax = isCalculateSalesTax;
		    IsCalculateVatTowardsSales = isCalculateVatTowardsSales;
			YandexMetrikaId = yandexMetrikaId;
			GoogleAnalyticsId = googleAnalyticsId;
			GoogleTagManagerId = googleTagManagerId;
			Only25 = only25;
		}

		private static String GetStringFromXml(XContainer container, String name, String defaultValue = "")
		{
			var item = container.Element(name);

			return (item == null ? defaultValue : item.Value);
		}

		private static Decimal GetDecimalFromXml(XContainer container, String name, Decimal defaultValue = 0)
		{
			var item = container.Element(name);
			if (item != null) Decimal.TryParse(item.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out defaultValue);

			return defaultValue;
		}

		private static Boolean GetBooleanFromXml(XContainer container, String name, Boolean defaultValue = false)
		{
			var item = container.Element(name);

			return (item != null ? Convert.ToBoolean(item.Value) : defaultValue);
		}


		public static CityParameters FromXmlString(String value)
		{
			return _cache.GetOrAdd(value, _ =>
			{
				var document = XDocument.Parse(value);

				var root = document.Root;
				if (root == null) return null;

				var container = root.Element("Main");
				if (container == null) return null;

				return new CityParameters
					(
					 GetStringFromXml(container, "LinkToDeliveryArea"),
					 GetStringFromXml(container, "HeaderText"),
					 GetStringFromXml(container, "EmailFeedBack"),
					 GetStringFromXml(container, "VkontakteLink"),
					 GetStringFromXml(container, "FacebookLink"),
					 GetStringFromXml(container, "TwitterLink"),
					 GetStringFromXml(container, "InstagramLink"),
					 GetStringFromXml(container, "OKLink"),
					 GetStringFromXml(container, "MyWorldLink"),
					 GetStringFromXml(container, "KroogiLink"),
					 GetDecimalFromXml(container, "MinOrderPrice", 295),
					 GetDecimalFromXml(container, "MaxPersonalOrderPrice"),
					 GetStringFromXml(container, "CallCenterPhone"),
					 CallCenterPhoneParameter.GetCallCenterPhonesFromXml(container),
					 GetBooleanFromXml(container, "IsCalculateSalesTax"),
					 GetBooleanFromXml(container, "IsCalculateVatTowardsSales"),
					 GetStringFromXml(container, "YandexMetrikaId"),
					 GetStringFromXml(container, "GoogleAnalyticsId"),
					 GetStringFromXml(container, "GoogleTagManagerId"),
					 GetBooleanFromXml(container, "Only25")
					);
			});
		}

		public static CityParameters Default
		{
			get
			{
				return new CityParameters(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty,
					String.Empty, String.Empty, String.Empty, String.Empty, 0, 0, String.Empty, new CallCenterPhoneParameter[0], false, false, String.Empty,
					String.Empty, String.Empty);
			}
		}

		private static bool TryParseUrl(string s, out Uri url)
		{
			if (string.IsNullOrEmpty(s))
			{
				url = null;
				return true;
			}

			try
			{
				url = new Uri(s, UriKind.Absolute);
				return true;
			}
			catch (Exception ex)
			{
				_logger.Warn(ex, "Could not parse url {0}", s);
				url = null;
				return false;
			}
		}
	}
}