using Dodo.RestaurantBoard.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.Tests
{
    public class DateTimeProviderUtcNowTests
    {
        [Fact]
        // Специально добавили этот тест как документацию и проверку,
        // что провайдер в реализации возвращает именно текущее время UTC.
        void ReturnsDateTimeUtcNow_WhenGetDateTime()
        {
            var dateTimeProvider = new DateTimeProviderUtcNow();

            var dateTimeBefore = DateTime.UtcNow;
            var resultDateTime = dateTimeProvider.GetDateTime();
            var dateTimeAfter = DateTime.UtcNow;

            Assert.True(resultDateTime >= dateTimeBefore && resultDateTime <= dateTimeAfter);
        }
    }
}
