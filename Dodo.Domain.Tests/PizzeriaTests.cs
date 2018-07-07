﻿using System;
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
		public void GetMonthsOld_IfBeginDateTimeWorkDifferFromCurrentDateByMonths_ShouldReturnCountOfMonths()
		{
			var beginDateTimeWork = new DateTime(2008, 3, 10);
			var pizzeria = CreatePizzeria(beginDateTimeWork);
			var currentDateTime = beginDateTimeWork.AddMonths(3);

			var cntMonthsOld = pizzeria.GetMonthsOld(currentDateTime);
			
			Assert.AreEqual(3, cntMonthsOld);
		}

		[Test]
		public void GetMonthsOld_IfBeginDateTimeWorkDifferFromCurrentDateByYearAndMonths_ShouldReturnTotalMonths()
		{
			var beginDateTimeWork = new DateTime(2008, 3, 10);
			var pizzeria = CreatePizzeria(beginDateTimeWork);
			var currentDateTime = beginDateTimeWork
				.AddYears(3)
				.AddMonths(3);

			var cntMonthsOld = pizzeria.GetMonthsOld(currentDateTime);
			
			Assert.AreEqual(39, cntMonthsOld);
		}

		[Test]
		public void GetYearsOld_IfBeginDateTimeWorkDifferFromCurrentDateByYears_ShouldReturnCountOfYears()
		{
			var beginDateTimeWork = new DateTime(2008, 3, 10);
			var pizzeria = CreatePizzeria(beginDateTimeWork);
			var currentDateTime = new DateTime(2008, 3, 10)
				.AddYears(3);

			var cntYearsOld = pizzeria.GetYearsOld(currentDateTime);

			Assert.AreEqual(3, cntYearsOld);
		}

		[Test]
		public void GetYearsOld_IfBeginDateTimeWorkIsNull_ShouldReturnZeroYears()
		{
			var currentDateTime = DateTime.Now;
			var pizzeria = CreatePizzeria(beginDateTimeWork: null);

			var cntYearsOld = pizzeria.GetYearsOld(currentDateTime);
			
			Assert.AreEqual(0, cntYearsOld);
		}

		[Test]
		public void GetMonthsOld_IfBeginDateTimeWorkIsNull_ShouldReturnZeroMonths()
		{
			var currentDateTime = DateTime.Now;
			var pizzeria = CreatePizzeria(beginDateTimeWork: null);

			var cntMonthsOld = pizzeria.GetMonthsOld(currentDateTime);
			
			Assert.AreEqual(0, cntMonthsOld);
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
