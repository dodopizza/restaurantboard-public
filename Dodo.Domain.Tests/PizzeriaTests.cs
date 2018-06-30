using System;
using AutoFixture;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Management.Organizations;
using NUnit.Framework;

namespace Dodo.Domain.Tests
{
	public class PizzeriaTests
	{
		[Test]
		[Sequential]
		public void GetYearsOld_ShouldReturnCountYearsFromBeginDateTimeWork(
			[Values("2005 02 22")] string beginDate,
			[Values("2008 01 11")] string currentDate,
			[Values(3)] int expectedYears)
		{
			var beginDateTimeWork = DateTime.Parse(beginDate);
			var currentDateTime = DateTime.Parse(currentDate);
			var pizzeria = CreatePizzeria(beginDateTimeWork);

			var actualYears = pizzeria.GetYearsOld(currentDateTime);

			Assert.AreEqual(expectedYears, actualYears);
		}

		private static Pizzeria CreatePizzeria(DateTime beginDateTimeWork)
		{
			var fixture = new Fixture();

			return new Pizzeria(
				fixture.Create<int>(),
				Uuid.NewUUId(),
				fixture.Create<string>(),
				fixture.Create<string>(),
				fixture.Create<string>(),
				UnitApprove.Approved,
				UnitState.Open,
				fixture.Create<int>(),
				Uuid.NewUUId(),
				fixture.Create<int>(),
				fixture.Create<OrganizationShortInfo>(),
				fixture.Create<double>(),
				beginDateTimeWork,
				fixture.Create<string>(),
				false,
				fixture.Create<int>(),
				fixture.Create<int>(),
				ClientTreatment.DefaultName,
				false,
				fixture.Create<PizzeriaFormat>());
		}
	}
}
