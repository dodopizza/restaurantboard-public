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
			var phone = new CallCenterPhoneParameter
			{
				Number = "8-999-123-45-67"
			};

			var phoneNumberWithputMarks = phone.NumberWithoutMarks;

			Assert.AreEqual("89991234567", phoneNumberWithputMarks);
		}

		[Test]
		public void NumberWithoutMarks_IfSpecifiedNumberContainsBrackets_ShouldRemoveIt()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = "8(999)1234567"
			};

			var phoneNumberWithputMarks = phone.NumberWithoutMarks;
			
			Assert.AreEqual("89991234567", phoneNumberWithputMarks);
		}

		[Test]
		public void NumberWithoutMarks_IfSpecifiedNumberContainsSpaces_ShouldRemoveIt()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = "8   999 123   45 67"
			};

			var phoneNumberWithputMarks = phone.NumberWithoutMarks;
			
			Assert.AreEqual("89991234567", phoneNumberWithputMarks);
		}

		[Test]
		public void GetIconUrl_IfIconPathContainsBackSlashes_ShouldReturnUriWithReplacedBackSlashesBySlashes()
		{
			var phone = new CallCenterPhoneParameter
			{
				IconPath = "test\\test2"
			};

			var urlBackSlashesReplacedBySlashes = phone.GetIconUrl("www.example.com");

			Assert.AreEqual("www.example.com/test/test2", urlBackSlashesReplacedBySlashes);
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconPathWithoutSlashes_ShouldReturnCorrectUri()
		{
			var phone = new CallCenterPhoneParameter
			{
				IconPath = "test"
			};

			var transformedUri = phone.GetIconUrl("www.example.com");

			Assert.AreEqual("www.example.com/test", transformedUri);
		}

		[Test]
		public void GetIconUrl_IfSpecifiedIconSitePath_ShouldReturnIt()
		{
			var phone = new CallCenterPhoneParameter
			{
				IconSitePath = "www.sitepath.ru/icon"
			};

			var theSameUri = phone.GetIconUrl("www.example.com");

			Assert.AreEqual("www.sitepath.ru/icon", theSameUri);
		}

		[Test]
		public void CreateXmlNode_ForPhoneWithParams_ShouldReturnXmlWithCorrectAttributeValues()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = "89991234567",
				IconPath = "icon",
				IconSitePath = "www.images.com/icon"
			};

			const string expected =
				"<Phone number=\"89991234567\" iconPath=\"icon\" iconSitePath=\"www.images.com/icon\" />";
			var serializedPhoneAsString = phone.CreateXmlNode().ToString();

			Assert.AreEqual(expected, serializedPhoneAsString);
		}

		[Test]
		public void CreateXmlNode_ForPhoneWithEmptyParams_ShouldReturnXmlWithEmptyAttributeValues()
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = null,
				IconPath = "",
				IconSitePath = null
			};

			const string expected = "<Phone number=\"\" iconPath=\"\" iconSitePath=\"\" />";
			var serializedPhoneAsString = phone.CreateXmlNode().ToString();
			
			Assert.AreEqual(expected, serializedPhoneAsString);
		}

		[Test]
		public void GetCallCenterPhonesFromXml_IfCallCenterPhonesNotExistsInXml_ShouldReturnEmptyPhonesArray()
		{
			var container = new XElement("Root");

			var deserializedXmlWhichNotContainsPhones = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(container);

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

			var expectedPhone = new CallCenterPhoneParameter()
			{
				Number = "89991234567",
				IconPath = "icon",
				IconSitePath = "www.images.com/icon"
			};
			var deserializedPhones = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(container);

			Assert.AreEqual(1, deserializedPhones.Length);
			Assert.AreEqual(expectedPhone.Number, deserializedPhones[0].Number);
			Assert.AreEqual(expectedPhone.IconPath, deserializedPhones[0].IconPath);
			Assert.AreEqual(expectedPhone.IconSitePath, deserializedPhones[0].IconSitePath);
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

			var deserializedPhones = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(container);

			Assert.AreEqual(3, deserializedPhones.Length);
		}

	}
}
