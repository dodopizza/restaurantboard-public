using System;
using Dodo.Core.DomainModel.Management.Organizations;
using Xunit;

namespace Dodo.Tests
{
	public class OrganizationTests
	{
		internal class OrganizationFake : Organization
		{
			public OrganizationFake(string headManagerName) :
				base(1, Core.Common.Uuid.NewUUId(), "", "", "", "", headManagerName, null, "", "", "")
			{
			}

			public override string GetShortInfo() =>
				throw new NotImplementedException();

			public override string GetFullDescription() =>
				throw new NotImplementedException();

			public override string GetShortDescription() =>
				throw new NotImplementedException();

			public override OrganizationShortInfo OrganizationShortInfo { get; }
		}

		[Fact]
		public void ShortHeadManagerName_IfNameLongerThanSurname_ShouldBeSurnameWithInitials()
		{
			var organization = new OrganizationFake("Гендальф серый верховный маг");

			var actualManagerName = organization.ShortHeadManagerName;

			Assert.Equal("Гендальф С.В.М.", actualManagerName);
		}

		[Fact]
		public void ShortHeadManagerName_IfNameHasOnlySurname_ShouldBeSurnameWithSingleSpace()
		{
			var organization = new OrganizationFake("Гендальф");

			var actualManagerName = organization.ShortHeadManagerName;

			Assert.Equal("Гендальф ", actualManagerName);
		}

		[Fact]
		public void ShortHeadManagerName_IfNameHasSurnameAndSeparators_ShouldBeSurnameOnly()
		{
			var organization = new OrganizationFake("Гендальф . . . ");

			var actualManagerName = organization.ShortHeadManagerName;

			Assert.Equal("Гендальф ", actualManagerName);
		}
	}
}
