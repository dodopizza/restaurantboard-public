using System;
using Xunit;
using FluentAssertions;
using Dodo.Core.DomainModel.Departments.Parameters.Unit;
using System.Xml;

namespace Dodo.Tests
{
    public class UnitLunchParametersShould
    {
        

        private const string incompleteXml =
        @"<qwe>
	        <Lunch>
		        <MinimalShiftToKitchenWorker> 111 </MinimalShiftToKitchenWorker>
	        </Lunch>
        </qwe>";

        private const string wrongXml =
        @"<qwe>
	        <Lunch>
		        <MinimalShiftToKitchenWorker> string data </MinimalShiftToKitchenWorker>
	        </Lunch>
        </qwe>";

        [Fact]
        public void ConvertNullXmlToDefaultUnitLunchParameters()
        {
            string xmlParameter = null;

            var result = UnitLunchParameters.ConvertToUnitLunchParameters(xmlParameter);

            Assert.Equal(8, result.MinimalShiftToCashier);
            Assert.Equal(8, result.MinimalShiftToCourier);
            Assert.Equal(8, result.MinimalShiftToKitchenWorker);
            Assert.Equal(8, result.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void ConvertWhitespaceXmlToDefaultUnitLunchParameters()
        {
            var xmlParameter = "    ";

            var result = UnitLunchParameters.ConvertToUnitLunchParameters(xmlParameter);

            Assert.Equal(8, result.MinimalShiftToCashier);
            Assert.Equal(8, result.MinimalShiftToCourier);
            Assert.Equal(8, result.MinimalShiftToKitchenWorker);
            Assert.Equal(8, result.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void ThrowExceptionWhenConvertInvalidXmlParameter()
        {
            var invalidXml = "hguygfhgfj";

            Action convert = () => UnitLunchParameters.ConvertToUnitLunchParameters(invalidXml);

            Assert.Throws<XmlException>(convert);
        }

        [Fact]
        public void SetMinimalShiftsFromXmlValues()
        {
            var testXml =
            @"<qwe>
	            <Lunch>
		            <MinimalShiftToKitchenWorker> 23 </MinimalShiftToKitchenWorker>
	            </Lunch>
	            <Lunch>
		            <MinimalShiftToCashier> 45 </MinimalShiftToCashier>
	            </Lunch>
	            <Lunch>
		            <MinimalShiftToCourier> 67 </MinimalShiftToCourier>
	            </Lunch>
                <Lunch>
		            <MinimalShiftToPersonalManager> 333 </MinimalShiftToPersonalManager>
	            </Lunch>
            </qwe>";

            var lunchParams = UnitLunchParameters.ConvertToUnitLunchParameters(testXml);
            
            Assert.Equal(23, lunchParams.MinimalShiftToKitchenWorker);
            Assert.Equal(45, lunchParams.MinimalShiftToCashier);
            Assert.Equal(67, lunchParams.MinimalShiftToCourier);
            Assert.Equal(333, lunchParams.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void ConvertToUnitLunchParameters_IncompleteXml()
        {
            var lunchParams = UnitLunchParameters.ConvertToUnitLunchParameters(incompleteXml);

            Assert.Equal(111, lunchParams.MinimalShiftToKitchenWorker);
            Assert.Equal(defaultValue, lunchParams.MinimalShiftToCashier);
            Assert.Equal(defaultValue, lunchParams.MinimalShiftToCourier);
            Assert.Equal(defaultValue, lunchParams.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void ConvertToUnitLunchParameters_WrongParameter()
        {
            Action convert = () => UnitLunchParameters.ConvertToUnitLunchParameters(wrongXml);

            Assert.Throws<FormatException>(convert);
        }
    }
}
