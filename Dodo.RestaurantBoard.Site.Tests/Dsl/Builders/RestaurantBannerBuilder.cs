using Dodo.Core.DomainModel.Management;
using Dodo.Core.DomainModel.Products;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Builders
{
    public class RestaurantBannerBuilder
    {
        private MenuSpecializationType _menuSpecializationType;

        public RestaurantBannerBuilder WithMenuSpecializationTypeAsEuropean()
        {
            _menuSpecializationType = MenuSpecializationType.European;
            return this;
        }

        public RestaurantBanner Please()
        {
            return new RestaurantBanner()
            {
                MenuSpecializationTypes = new[]
                {
                    MenuSpecializationType.European
                },
                Url = "ya.ru",
                DisplayTime = 15
            };
        }
    }
}