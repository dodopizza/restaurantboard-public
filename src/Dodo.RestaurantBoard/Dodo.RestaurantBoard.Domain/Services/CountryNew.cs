using System;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Finance;

internal class CountryNew : Country
{
    public CountryNew(string name, string phoneCode, Currency currency)
        : base(1, name, phoneCode, null, String.Empty, currency, String.Empty)
    {
    }
}