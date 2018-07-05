using System.Xml.Linq;
using Dodo.Core.DomainModel.Departments;
using NUnit.Framework;

namespace Dodo.Domain.Tests
{
	public class CallCenterPhoneTests
	{
		[Test]
		public void NumberWithoutMarks_IfSpecifiedNumberContainsDashes_ShouldRemoveIt()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = "8-999-123-45-67"
			};

			Assert.AreEqual("89991234567", phone.NumberWithoutMarks);
		}

		[Test]
		public void NumberWithoutMarks_IfSpecifiedNumberContainsBrackets_ShouldRemoveIt()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = "8(999)1234567"
			};

			Assert.AreEqual("89991234567", phone.NumberWithoutMarks);
		}

		[Test]
		public void NumberWithoutMarks_IfSpecifiedNumberContainsSpaces_ShouldRemoveIt()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = "8   999 123   45 67"
			};

			Assert.AreEqual("89991234567", phone.NumberWithoutMarks);
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconPathWithBackSlashes_ShouldReturnCorrectUri()
		{
			var phone = new CallCenterPhoneParameter
			{
				IconPath = "test\\test2"
			};

			Assert.AreEqual("www.example.com/test/test2", phone.GetIconUrl("www.example.com"));
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconPathWithoutSlashes_ShouldReturnCorrectUri()
		{
			var phone = new CallCenterPhoneParameter
			{
				IconPath = "test"
			};

			Assert.AreEqual("www.example.com/test", phone.GetIconUrl("www.example.com"));
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconSitePath_ShouldReturnIt()
		{
			var phone = new CallCenterPhoneParameter
			{
				IconSitePath = "www.sitepath.ru/icon"
			};

			Assert.AreEqual("www.sitepath.ru/icon", phone.GetIconUrl("www.example.com"));
		}

		[Test]
		public void CreateXmlNode_IfPhoneParametersSpecified_ShouldReturnXmlWithCorrectAttributeValues()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = "89991234567",
				IconPath = "icon",
				IconSitePath = "www.images.com/icon"
			};

			const string expected =
				"<Phone number=\"89991234567\" iconPath=\"icon\" iconSitePath=\"www.images.com/icon\" />";

			Assert.AreEqual(expected, phone.CreateXmlNode().ToString());
		}

		[Test]
		public void CreateXmlNode_IfPhoneParametersNotSpecified_ShouldReturnXmlWithEmptyAttributeValues()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = null,
				IconPath = "",
				IconSitePath = null
			};

			Assert.AreEqual("<Phone number=\"\" iconPath=\"\" iconSitePath=\"\" />", phone.CreateXmlNode().ToString());
		}

		[Test]
		public void GetCallCenterPhonesFromXml_IfCallCenterPhonesNotExistsInDocument_ShouldReturnEmptyArray()
		{
			var container = new XElement("Root");

			Assert.IsEmpty(CallCenterPhoneParameter.GetCallCenterPhonesFromXml(container));
		}

		[Test]
		public void GetCallCenterPhonesFromXml_IfDocumentContainsCallCenterPhones_ShouldReturnCollectionOfObjects()
		{
			var container = XElement.Parse(@"
<Root>
	<CallCenterPhones>
		<Phone number=""89991234567"" iconPath=""icon"" iconSitePath=""www.images.com/icon"" />
	</CallCenterPhones>
</Root>
");

			var phones = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(container);

			Assert.AreEqual(1, phones.Length);
			Assert.AreEqual("89991234567", phones[0].Number);
			Assert.AreEqual("icon", phones[0].IconPath);
			Assert.AreEqual("www.images.com/icon", phones[0].IconSitePath);
		}
	}
}
