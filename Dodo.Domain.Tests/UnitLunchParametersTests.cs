using Dodo.Core.DomainModel.Departments.Parameters.Unit;
using NUnit.Framework;

namespace Dodo.Domain.Tests
{
	public class UnitLunchParametersTests
	{
		[Test]
		public void ConvertToUnitLunchParameters_XmlDocumentWithSingleLunch_ShouldParseParameters()
		{
			const string source = @"
<Lunch>
	<MinimalShiftToKitchenWorker>1</MinimalShiftToKitchenWorker>
	<MinimalShiftToCashier>2</MinimalShiftToCashier>
	<MinimalShiftToCourier>3</MinimalShiftToCourier>
	<MinimalShiftToPersonalManager>4</MinimalShiftToPersonalManager>
</Lunch>";

			var parameters = UnitLunchParameters.ConvertToUnitLunchParameters(source);

			Assert.AreEqual(1, parameters.MinimalShiftToKitchenWorker);
			Assert.AreEqual(2, parameters.MinimalShiftToCashier);
			Assert.AreEqual(3, parameters.MinimalShiftToCourier);
			Assert.AreEqual(4, parameters.MinimalShiftToPersonalManager);
		}

		[Test]
		public void ConvertToUnitLunchParameters_XmlDocumentWithMultipleLunch_ShouldParseParameters()
		{
			var source = $@"
<Document>
	<Lunch>
		<MinimalShiftToKitchenWorker>1</MinimalShiftToKitchenWorker>
	</Lunch>
	<Lunch>
		<MinimalShiftToCashier>2</MinimalShiftToCashier>
	</Lunch>
	<Lunch>
		<MinimalShiftToCourier>3</MinimalShiftToCourier>
	</Lunch>
	<Lunch>
		<MinimalShiftToPersonalManager>4</MinimalShiftToPersonalManager>
	</Lunch>
</Document>";

			var parameters = UnitLunchParameters.ConvertToUnitLunchParameters(source);

			Assert.AreEqual(1, parameters.MinimalShiftToKitchenWorker);
			Assert.AreEqual(2, parameters.MinimalShiftToCashier);
			Assert.AreEqual(3, parameters.MinimalShiftToCourier);
			Assert.AreEqual(4, parameters.MinimalShiftToPersonalManager);
		}
	}
}
