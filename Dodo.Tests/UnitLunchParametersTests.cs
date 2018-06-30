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
    }
}
