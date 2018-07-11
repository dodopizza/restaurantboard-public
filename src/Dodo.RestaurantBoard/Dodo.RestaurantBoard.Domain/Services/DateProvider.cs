using System;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public interface IDateProvider
    {
        DateTime Now();
    }
    
    public class DateProvider: IDateProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}