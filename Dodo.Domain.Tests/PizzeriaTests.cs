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
			[Values("2005 02 22", "2005 02 22", "2005 02 22")] string beginDate,
			[Values("2008 01 11", "2008 02 11", "2008 03 11")] string currentDate,
			[Values(2, 2, 3)] int expectedYears)
		{
			var beginDateTimeWork = DateTime.Parse(beginDate);
			var currentDateTime = DateTime.Parse(currentDate);
			var pizzeria = CreatePizzeria(beginDateTimeWork);

			var actualYears = pizzeria.GetYearsOld(currentDateTime);

			Assert.AreEqual(expectedYears, actualYears);
		}

		[Test]
		public void GetYearsOld_IfBeginDateTimeWorkNull_ShouldReturnZeroYears()
		{
			const int expectedYears = 0;
			var currentDateTime = DateTime.Now;
			var pizzeria = CreatePizzeria(beginDateTimeWork: null);

			var actualYears = pizzeria.GetYearsOld(currentDateTime);

			Assert.AreEqual(expectedYears, actualYears);
		}
		
		[Test]
		[Sequential]
		public void GetMonthsOld_ShouldReturnCountMonthsFromBeginDateTimeWork(
			[Values("2008 02 22")] string beginDate,
			[Values("2008 06 11")] string currentDate,
			[Values(3)] int expectedMonths)
		{
			var beginDateTimeWork = DateTime.Parse(beginDate);
			var currentDateTime = DateTime.Parse(currentDate);
			var pizzeria = CreatePizzeria(beginDateTimeWork);

			var actualMonths = pizzeria.GetMonthsOld(currentDateTime);

			Assert.AreEqual(expectedMonths, actualMonths);
		}

		[Test]
		public void GetMonthsOld_IfBeginDateTimeWorkNull_ShouldReturnZeroMonths()
		{
			const int expectedMonths = 0;
			var currentDateTime = DateTime.Now;
			var pizzeria = CreatePizzeria(beginDateTimeWork: null);

			var actualMonths = pizzeria.GetMonthsOld(currentDateTime);

			Assert.AreEqual(expectedMonths, actualMonths);
		}
		
		private static Pizzeria CreatePizzeria(DateTime? beginDateTimeWork)
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
