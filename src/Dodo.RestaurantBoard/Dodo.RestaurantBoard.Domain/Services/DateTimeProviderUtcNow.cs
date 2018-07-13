using Dodo.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class DateTimeProviderUtcNow : IDateTimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
