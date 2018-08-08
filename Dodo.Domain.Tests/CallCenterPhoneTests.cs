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
			var phone = new CallCenterPhone
			{
				IconPath = "test\\test2"
			};

			var urlBackSlashesReplacedBySlashes = phone.GetIconUrl("www.example.com");

			Assert.AreEqual("www.example.com/test/test2", urlBackSlashesReplacedBySlashes);
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconPathWithoutSlashes_ShouldReturnCorrectUri()
		{
			var phone = new CallCenterPhone
			{
				IconPath = "test"
			};

			var transformedUri = phone.GetIconUrl("www.example.com");

			Assert.AreEqual("www.example.com/test", transformedUri);
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconSitePath_ShouldReturnIt()
		{
			var phone = new CallCenterPhone
			{
				IconSitePath = "www.sitepath.ru/icon"
			};

			var theSameUri = phone.GetIconUrl("www.example.com");

			Assert.AreEqual("www.sitepath.ru/icon", theSameUri);
		}

		[Test]
		public void CreateXmlNode_ForPhoneWithParams_ShouldReturnXmlWithCorrectAttributeValues()
		{
			var phone = new CallCenterPhone
			{
				Number = "89991234567",
				IconPath = "icon",
				IconSitePath = "www.images.com/icon"
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
				IconPath = "",
				IconSitePath = null
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
			Assert.AreEqual("icon", deserializedPhones[0].IconPath);
			Assert.AreEqual("www.images.com/icon", deserializedPhones[0].IconSitePath);
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
