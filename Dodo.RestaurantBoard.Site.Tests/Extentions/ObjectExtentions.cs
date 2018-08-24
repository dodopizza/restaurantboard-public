using Microsoft.AspNetCore.Mvc;

namespace Dodo.RestaurantBoard.Site.Tests.Extentions
{
    public static class ObjectExtentions
    {
        public static T GetValue<T>(this object result, string name) where T : class
        {
            return result.GetType().GetProperty(name).GetValue(result) as T;
        }
    }
}
