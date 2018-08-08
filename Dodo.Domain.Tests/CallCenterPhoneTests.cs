using System.Xml.Linq;
using Dodo.Core.DomainModel.Departments;
using NUnit.Framework;

namespace Dodo.Domain.Tests
{
	public class CallCenterPhoneTests
	{
		[Test]
		public void NumberWithoutMarks_IfNumberContainsDashes_ShouldReturnNumberWithoutDashes()
		{
			var phone = new CallCenterPhone
			{
				Number = "8-999-123-45-67"
			};

			var phoneNumberWithputMarks = phone.NumberWithoutMarks;

			Assert.AreEqual("89991234567", phoneNumberWithputMarks);
		}

		[Test]
		public void NumberWithoutMarks_IfSpecifiedNumberContainsBrackets_ShouldRemoveIt()
		{
			var phone = new CallCenterPhone
			{
				Number = "8(999)1234567"
			};

			var phoneNumberWithputMarks = phone.NumberWithoutMarks;
			
			Assert.AreEqual("89991234567", phoneNumberWithputMarks);
		}

		[Test]
		public void NumberWithoutMarks_IfSpecifiedNumberContainsSpaces_ShouldRemoveIt()
		{
			var phone = new CallCenterPhone
			{
				Number = "8   999 123   45 67"
			};

			var phoneNumberWithputMarks = phone.NumberWithoutMarks;
			
			Assert.AreEqual("89991234567", phoneNumberWithputMarks);
		}

		[Test]
		public void GetIconUrl_IfIconPathContainsBackSlashes_ShouldReturnUriWithReplacedBackSlashesBySlashes()
		{
			var icon = new Icon(path: "test\\test2", sitePath: "");

			var urlBackSlashesReplacedBySlashes = icon.GetUrl("www.example.com");

			Assert.AreEqual("www.example.com/test/test2", urlBackSlashesReplacedBySlashes);
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconPathWithoutSlashes_ShouldReturnCorrectUri()
		{
			var icon = new Icon(path: "test", sitePath: "");
			
			var transformedUri = icon.GetUrl("www.example.com");

			Assert.AreEqual("www.example.com/test", transformedUri);
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconSitePath_ShouldReturnIt()
		{
			var icon = new Icon(path: "", sitePath: "www.sitepath.ru/icon");

			var theSameUri = icon.GetUrl("www.example.com");

			Assert.AreEqual("www.sitepath.ru/icon", theSameUri);
		}

		[Test]
		public void CreateXmlNode_ForPhoneWithParams_ShouldReturnXmlWithCorrectAttributeValues()
		{
			var phone = new CallCenterPhone
			{
				Number = "89991234567",
				Icon = new Icon("icon", "www.images.com/icon")
			};

			var serializedPhoneAsString = phone.CreateXmlNode().ToString();

			Assert.AreEqual(
				"<Phone number=\"89991234567\" iconPath=\"icon\" iconSitePath=\"www.images.com/icon\" />",
				serializedPhoneAsString);
		}

		[Test]
		public void CreateXmlNode_ForPhoneWithEmptyParams_ShouldReturnXmlWithEmptyAttributeValues()
		{
			var phone = new CallCenterPhone
			{
				Number = null,
				Icon = new Icon("", null)
			};

			var serializedPhoneAsString = phone.CreateXmlNode().ToString();
			
			Assert.AreEqual("<Phone number=\"\" iconPath=\"\" iconSitePath=\"\" />", serializedPhoneAsString);
		}

		[Test]
		public void GetCallCenterPhonesFromXml_IfCallCenterPhonesNotExistsInXml_ShouldReturnEmptyPhonesArray()
		{
			var container = new XElement("Root");

			var deserializedXmlWhichNotContainsPhones = container.GetCallCenterPhones();

			Assert.IsEmpty(deserializedXmlWhichNotContainsPhones);
		}

		[Test]
		public void GetCallCenterPhonesFromXml_IfXmlContainsCallCenterPhone_ShouldReturnDeserializedPhone()
		{
			var container = XElement.Parse(@"
<Root>
	<CallCenterPhones>
		<Phone number=""89991234567"" iconPath=""icon"" iconSitePath=""www.images.com/icon"" />
	</CallCenterPhones>
</Root>
");

			var deserializedPhones = container.GetCallCenterPhones();

			Assert.AreEqual(1, deserializedPhones.Length);
			Assert.AreEqual("89991234567", deserializedPhones[0].Number);
			Assert.AreEqual("icon", deserializedPhones[0].Icon.Path);
			Assert.AreEqual("www.images.com/icon", deserializedPhones[0].Icon.SitePath);
		}
		
		[Test]
		public void GetCallCenterPhonesFromXml_IfXmlContainsThreeCallCenterPhones_ShouldReturnThreeDeserializedPhones()
		{
			var container = XElement.Parse(@"
<Root>
	<CallCenterPhones>
		<Phone number=""89991234567"" iconPath=""icon"" iconSitePath=""www.images.com/icon"" />
		<Phone number=""89991234567"" iconPath=""icon"" iconSitePath=""www.images.com/icon"" />
		<Phone number=""89991234567"" iconPath=""icon"" iconSitePath=""www.images.com/icon"" />
	</CallCenterPhones>
</Root>
");

			var deserializedPhones = container.GetCallCenterPhones();

			Assert.AreEqual(3, deserializedPhones.Length);
		}

	}
}
