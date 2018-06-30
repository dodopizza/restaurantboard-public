using System.Xml;
using System.Xml.Linq;
using Dodo.Core.DomainModel.Departments;
using NUnit.Framework;

namespace Dodo.Domain.Tests
{
	public class CallCenterPhoneTests
	{
		private const string Host = "www.example.com";

		[Test]
		[Sequential]
		public void NumberWithoutMarks_ShouldReturnFormattedNumber(
			[Values("", "8 999 999 25 46", "8 (987) 123 45 67", "8-903-768-98-22")] string unformattedNumber,
			[Values("", "89999992546", "89871234567", "89037689822")] string expectedNumber)
		{
			var phone = new CallCenterPhoneParameter
			{
				Number = unformattedNumber
			};

			Assert.AreEqual(expectedNumber, phone.NumberWithoutMarks);
		}

		[Test]
		[Sequential]
		public void GetIconUrl_WithIconPath_UrlContainsIconPath(
			[Values("test", "test\\test2")] string iconPath,
			[Values(Host + "/test", Host + "/test/test2")] string expectedIconUrl)
		{
			var phone = new CallCenterPhoneParameter
			{
				IconPath = iconPath
			};
			
			var actualIconUrl = phone.GetIconUrl(Host);

			Assert.AreEqual(expectedIconUrl, actualIconUrl);
		}

		[Test]
		public void GetIconUrl_WithIconSitePath_UrlContainsIconSitePath()
		{
			const string expected = "www.sitepath.ru/icon";
			var phone = new CallCenterPhoneParameter
			{
				IconSitePath = expected
			};

			var actualIconUrl = phone.GetIconUrl(Host);

			Assert.AreEqual(expected, actualIconUrl);
		}

		[Test]
		[Combinatorial]
		public void SerializeObject_ShouldReturnedXmlElement(
			[Values("123", null)]string number,
			[Values("www.example.com", null)]string iconSitePath,
			[Values("www.tedt.com/test", null)]string iconPath)
		{
			var obj = new CallCenterPhoneParameter()
			{
				Number = number,
				IconPath = iconPath,
				IconSitePath = iconSitePath
			};

			var result = obj.CreateXmlNode().ToString();
			var expected = $"<Phone number=\"{number}\" iconPath=\"{iconPath}\" iconSitePath=\"{iconSitePath}\" />";
			
			Assert.AreEqual(expected, result);
		}

		[Test]
		public void DeserializeXml_IfEmptyContainer_ShouldReturnEmptyArray()
		{
			var container = new XElement("Phone");
			
			var result = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(container);
			
			Assert.IsEmpty(result);
		}
		
		[Test]
		public void DeserializeXml_ShouldReturnObject(
			[Values("123", "")]string number,
			[Values("www.example.com", "")]string iconSitePath,
			[Values("www.tedt.com/test", "")]string iconPath)
		{
			var expected = new CallCenterPhoneParameter()
			{
				Number = number,
				IconPath = iconPath,
				IconSitePath = iconSitePath
			};
			var container = new XElement("Root",
				new XElement("CallCenterPhones", expected.CreateXmlNode()));
			
			var result = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(container);
			
			Assert.AreEqual(1, result.Length);
			Assert.AreEqual(expected.Number, result[0].Number);
			Assert.AreEqual(expected.IconPath, result[0].IconPath);
			Assert.AreEqual(expected.IconSitePath, result[0].IconSitePath);
		}
	}
}