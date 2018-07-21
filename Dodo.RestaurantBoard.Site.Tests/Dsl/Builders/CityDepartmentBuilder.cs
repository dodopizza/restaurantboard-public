using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Products;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Builders
{
    public class CityDepartmentBuilder
    {
        private MenuSpecializationType _menuSpecializationType;
        private Country _country;

        public CityDepartmentBuilder WithMenuSpecializationTypeAsEuropean()
        {
            _menuSpecializationType = MenuSpecializationType.European;
            return this;
        }

        public CityDepartmentBuilder WithContry(Country country = null)
        {
            _country = country ?? new Country(777, string.Empty, string.Empty, null, string.Empty, Currency.Ruble,
                           string.Empty);

            return this;
        }

        public CityDepartment Please()
        {
            return new CityDepartment
            { 
                Country = _country,
                MenuSpecializationType = _menuSpecializationType,
            };
        }
    }
}