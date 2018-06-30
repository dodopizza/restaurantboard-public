using Dodo.Core.DomainModel.Departments.Parameters.Unit;
using NUnit.Framework;

namespace Dodo.Domain.Tests
{
	public class UnitLunchParametersTests
	{
		[Test]
		[Sequential]
		public void ConvertingFromXml_SingleLunch_ShouldGenerateCorrectObject(
			[Values(1, 5)] int minimalShiftToKitchenWorker,
			[Values(2, 6)] int minimalShiftToCashier,
			[Values(3, 7)] int minimalShiftToCourier,
			[Values(4, 8)] int minimalShiftToPersonalManager)
		{
			var xml = $@"
<Lunch>
	<MinimalShiftToKitchenWorker>{minimalShiftToKitchenWorker}</MinimalShiftToKitchenWorker>
	<MinimalShiftToCashier>{minimalShiftToCashier}</MinimalShiftToCashier>
	<MinimalShiftToCourier>{minimalShiftToCourier}</MinimalShiftToCourier>
	<MinimalShiftToPersonalManager>{minimalShiftToPersonalManager}</MinimalShiftToPersonalManager>
</Lunch>";
			var expected = new UnitLunchParameters(
				minimalShiftToKitchenWorker,
				minimalShiftToCashier,
				minimalShiftToCourier,
				minimalShiftToPersonalManager);

			var actual = UnitLunchParameters.ConvertToUnitLunchParameters(xml);

			AssertUnitLunchParameters(expected, actual);
		}

		[Test]
		[Sequential]
		public void ConvertingFromXml_MultipleLunch_ShouldGenerateCorrectObject(
			[Values(1, 5)] int minimalShiftToKitchenWorker,
			[Values(2, 6)] int minimalShiftToCashier,
			[Values(3, 7)] int minimalShiftToCourier,
			[Values(4, 8)] int minimalShiftToPersonalManager)
		{
			var xml = $@"
<Document>
	<Lunch>
		<MinimalShiftToKitchenWorker>{minimalShiftToKitchenWorker}</MinimalShiftToKitchenWorker>
	</Lunch>
	<Lunch>
		<MinimalShiftToCourier>{minimalShiftToCourier}</MinimalShiftToCourier>
	</Lunch>
	<Lunch>
		<MinimalShiftToCashier>{minimalShiftToCashier}</MinimalShiftToCashier>
	</Lunch>
	<Lunch>
		<MinimalShiftToPersonalManager>{minimalShiftToPersonalManager}</MinimalShiftToPersonalManager>
	</Lunch>
</Document>";
			var expected = new UnitLunchParameters(
				minimalShiftToKitchenWorker,
				minimalShiftToCashier,
				minimalShiftToCourier,
				minimalShiftToPersonalManager);

			var actual = UnitLunchParameters.ConvertToUnitLunchParameters(xml);

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
