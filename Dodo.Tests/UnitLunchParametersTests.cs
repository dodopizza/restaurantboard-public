using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using System;
using Xunit;
using FluentAssertions;
using Dodo.Core.DomainModel.Departments.Parameters.Unit;
using System.Xml;

namespace Dodo.Tests
{
    public class UnitLunchParametersTests
    {
        private const string testXml =
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
		        <MinimalShiftToKitchenWorker> 100 </MinimalShiftToKitchenWorker>
	        </Lunch>
            <Lunch>
		        <MinimalShiftToPersonalManager> 333 </MinimalShiftToPersonalManager>
	        </Lunch>
        </qwe>";

        private const string wrongXml =
        @"<qwe>
	        <Lunch>
		        <MinimalShiftToKitchenWorker> string data </MinimalShiftToKitchenWorker>
	        </Lunch>
        </qwe>";

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ConvertToUnitLunchParameters_SetNullOrEmptyString(string xmlParameters)
        {
            var expected = new UnitLunchParameters(8, 8, 8, 8);
            var result = UnitLunchParameters.ConvertToUnitLunchParameters(xmlParameters);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ConvertToUnitLunchParameters_WrongXml_ShouldThrowException()
        {
            Action convert = () => UnitLunchParameters.ConvertToUnitLunchParameters("hguygfhgfj");

            Assert.Throws<XmlException>(convert);
        }

        [Fact]
        public void ConvertToUnitLunchParameters_ValidXml()
        {
            var lunchParams = UnitLunchParameters.ConvertToUnitLunchParameters(testXml);
            
            Assert.Equal(100, lunchParams.MinimalShiftToKitchenWorker);
            Assert.Equal(45, lunchParams.MinimalShiftToCashier);
            Assert.Equal(67, lunchParams.MinimalShiftToCourier);
            Assert.Equal(333, lunchParams.MinimalShiftToPersonalManager);
        }

        [Fact]
        public void ConvertToUnitLunchParameters_WrongParameter()
        {
            Action convert = () => UnitLunchParameters.ConvertToUnitLunchParameters(wrongXml);

            Assert.Throws<FormatException>(convert);
        }
    }
}
