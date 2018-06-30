using System;
using Dodo.Core.DomainModel.Departments.Parameters.Unit;
using NUnit.Framework;

namespace Dodo.Domain.Tests
{
	public class UnitLunchParametersTests
	{
		[Test]
		[Sequential]
		public void ConvertingFromXml_ShouldGenerateCorrectObject(
			[Values(1, 5)] int shiftToKitchenWorker,
			[Values(2, 6)] int shiftToCashier,
			[Values(3, 7)] int shiftToCourier,
			[Values(4, 8)] int shiftToPersonalManager)
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
				shiftToKitchenWorker,
				shiftToCashier,
				shiftToCourier,
				shiftToPersonalManager);

			var actual = UnitLunchParameters.ConvertToUnitLunchParameters(xml);
			var expected = new UnitLunchParameters(
				shiftToKitchenWorker,
				shiftToCashier,
				shiftToCourier,
				shiftToPersonalManager);

			AssertUnitLunchParameters(expected, actual);
		}

		[Test]
		[Sequential]
		public void ConvertingFromXml_ShouldGenerateCorrectObjectFrom(
			[Values(1, 5)] int shiftToKitchenWorker,
			[Values(2, 6)] int shiftToCashier,
			[Values(3, 7)] int shiftToCourier,
			[Values(4, 8)] int shiftToPersonalManager)
		{
			const string xmlTemplate =
				@"
<Document>
	<Lunch>
		<MinimalShiftToKitchenWorker>{0}</MinimalShiftToKitchenWorker>
	</Lunch>
	<Lunch>
		<MinimalShiftToCourier>{2}</MinimalShiftToCourier>
	</Lunch>
	<Lunch>
		<MinimalShiftToCashier>{1}</MinimalShiftToCashier>
	</Lunch>
	<Lunch>
		<MinimalShiftToPersonalManager>{3}</MinimalShiftToPersonalManager>
	</Lunch>
</Document>";
			var xml = String.Format(xmlTemplate,
				shiftToKitchenWorker,
				shiftToCashier,
				shiftToCourier,
				shiftToPersonalManager);

			var actual = UnitLunchParameters.ConvertToUnitLunchParameters(xml);
			var expected = new UnitLunchParameters(
				shiftToKitchenWorker,
				shiftToCashier,
				shiftToCourier,
				shiftToPersonalManager);

			AssertUnitLunchParameters(expected, actual);
		}

		private static void AssertUnitLunchParameters(UnitLunchParameters expected, UnitLunchParameters actual)
		{
			Assert.AreEqual(expected.MinimalShiftToKitchenWorker, actual.MinimalShiftToKitchenWorker);
			Assert.AreEqual(expected.MinimalShiftToCashier, actual.MinimalShiftToCashier);
			Assert.AreEqual(expected.MinimalShiftToCourier, actual.MinimalShiftToCourier);
			Assert.AreEqual(expected.MinimalShiftToPersonalManager, actual.MinimalShiftToPersonalManager);
		}
	}
}
