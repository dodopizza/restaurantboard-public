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
		public void GetIconUrl_WithIconPath_ShouldGenerateUrl(
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
		public void GetIconUrl_WithIconSitePath_ShouldGenerateUrl()
		{
			const string expected = "www.sitepath.ru/icon";
			var phone = new CallCenterPhoneParameter
			{
				IconSitePath = "www.sitepath.ru/icon"
			};

			var actualIconUrl = phone.GetIconUrl(Host);

			Assert.AreEqual(expected, actualIconUrl);
		}
	}
}