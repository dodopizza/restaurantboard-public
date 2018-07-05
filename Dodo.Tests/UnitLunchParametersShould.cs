using System;
using Xunit;
using FluentAssertions;
using Dodo.Core.DomainModel.Departments.Parameters.Unit;
using System.Xml;

namespace Dodo.Tests
{
    public class UnitLunchParametersShould
    {

        [Fact]
        public void ConvertNullXmlToDefaultUnitLunchParameters()
        {
            string xmlParameter = null;

            var unitLunchParameters = UnitLunchParameters.ConvertToUnitLunchParameters(xmlParameter);

            Assert.Equal(8, unitLunchParameters.MinimalShiftToCashier);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToCourier);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToKitchenWorker);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void ConvertWhitespaceXmlToDefaultUnitLunchParameters()
        {
            var xmlParameter = "    ";

            var unitLunchParameters = UnitLunchParameters.ConvertToUnitLunchParameters(xmlParameter);

            Assert.Equal(8, unitLunchParameters.MinimalShiftToCashier);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToCourier);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToKitchenWorker);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void ThrowExceptionWhenConvertingInvalidXmlParameter()
        {
            var invalidXml = "hguygfhgfj";

            Action convert = () => UnitLunchParameters.ConvertToUnitLunchParameters(invalidXml);

            Assert.Throws<XmlException>(convert);
        }

        [Fact]
        public void SetMinimalShiftToKitchenWorkerFromXmlValue()
        {
            var testXml =
            @"<qwe>
	            <Lunch>
		            <MinimalShiftToKitchenWorker> 23 </MinimalShiftToKitchenWorker>
	            </Lunch>
            </qwe>";

            var unitLunchParameters = UnitLunchParameters.ConvertToUnitLunchParameters(testXml);

            Assert.Equal(23, unitLunchParameters.MinimalShiftToKitchenWorker);
        }

        [Fact]
        public void SetMinimalShiftToCashierFromXmlValue()
        {
            var testXml =
            @"<qwe>
	            <Lunch>
		            <MinimalShiftToCashier> 45 </MinimalShiftToCashier>
	            </Lunch>
            </qwe>";

            var unitLunchParameters = UnitLunchParameters.ConvertToUnitLunchParameters(testXml);

            Assert.Equal(45, unitLunchParameters.MinimalShiftToCashier);
        }

        [Fact]
        public void SetMinimalShiftToCourierFromXmlValue()
        {
            var testXml =
            @"<qwe>
	            <Lunch>
		            <MinimalShiftToCourier> 67 </MinimalShiftToCourier>
	            </Lunch>
            </qwe>";

            var unitLunchParameters = UnitLunchParameters.ConvertToUnitLunchParameters(testXml);

            Assert.Equal(67, unitLunchParameters.MinimalShiftToCourier);
        }

        [Fact]
        public void SetMinimalShiftToPersonalManagerFromXmlValue()
        {
            var testXml =
            @"<qwe>
                <Lunch>
		            <MinimalShiftToPersonalManager> 333 </MinimalShiftToPersonalManager>
	            </Lunch>
            </qwe>";

            var unitLunchParameters = UnitLunchParameters.ConvertToUnitLunchParameters(testXml);

            Assert.Equal(333, unitLunchParameters.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void SetMinimalShiftToKitchenWorkerFromLastXmlValue()
        {
            var testXml =
            @"<qwe>
	            <Lunch>
		            <MinimalShiftToKitchenWorker> 1 </MinimalShiftToKitchenWorker>
	            </Lunch>
	            <Lunch>
		            <MinimalShiftToKitchenWorker> 2 </MinimalShiftToKitchenWorker>
	            </Lunch>
            </qwe>";

            var unitLunchParameters = UnitLunchParameters.ConvertToUnitLunchParameters(testXml);

            Assert.Equal(2, unitLunchParameters.MinimalShiftToKitchenWorker);
        }

        [Fact]
        public void SetDefaultMinimalShiftToKitchenWorkerForMissingXmlValue()
        {
            var incompleteXml =
            @"<qwe>
	            <Lunch>
		            <MinimalShiftToKitchenWorker> 111 </MinimalShiftToKitchenWorker>
	            </Lunch>
            </qwe>";

            var unitLunchParameters = UnitLunchParameters.ConvertToUnitLunchParameters(incompleteXml);

            Assert.Equal(111, unitLunchParameters.MinimalShiftToKitchenWorker);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToCashier);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToCourier);
            Assert.Equal(8, unitLunchParameters.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void ThrowFormatExceptionWhenConvertingNonIntXmlValue()
        {
            var invalidValueXml =
            @"<qwe>
	            <Lunch>
		            <MinimalShiftToKitchenWorker> string data </MinimalShiftToKitchenWorker>
	            </Lunch>
            </qwe>";

            Action convert = () => UnitLunchParameters.ConvertToUnitLunchParameters(invalidValueXml);

            Assert.Throws<FormatException>(convert);
        }
    }
}
