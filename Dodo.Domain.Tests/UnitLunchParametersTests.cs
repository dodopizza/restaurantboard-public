using System;
using System.Collections.Generic;
using System.Text;
using Dodo.Core.DomainModel.Departments.Parameters.Unit;
using NUnit.Framework;

namespace Dodo.Domain.Tests
{
	public class UnitLunchParametersTests
	{
		[Test]
		[Sequential]
		public void ConvertingFromXml_ShouldGenerateCorrectObject(
			[Values(1, 5)]int minimalShiftToKitchenWorker,
			[Values(2, 6)]int minimalShiftToCashier,
			[Values(3, 7)]int minimalShiftToCourier,
			[Values(4, 8)]int minimalShiftToPersonalManager)
		{
			const string xmlTemplate =
				@"
<Lunch>
	<MinimalShiftToKitchenWorker>{0}</MinimalShiftToKitchenWorker>
	<MinimalShiftToCashier>{1}</MinimalShiftToCashier>
	<MinimalShiftToCourier>{2}</MinimalShiftToCourier>
	<MinimalShiftToPersonalManager>{3}</MinimalShiftToPersonalManager>
</Lunch>";
			var xml = String.Format(xmlTemplate,
				minimalShiftToKitchenWorker,
				minimalShiftToCashier,
				minimalShiftToCourier,
				minimalShiftToPersonalManager);

			var result = UnitLunchParameters.ConvertToUnitLunchParameters(xml);

			Assert.AreEqual(minimalShiftToKitchenWorker, result.MinimalShiftToKitchenWorker);
			Assert.AreEqual(minimalShiftToCashier, result.MinimalShiftToCashier);
			Assert.AreEqual(minimalShiftToCourier, result.MinimalShiftToCourier);
			Assert.AreEqual(minimalShiftToPersonalManager, result.MinimalShiftToPersonalManager);
		}
	}
}
