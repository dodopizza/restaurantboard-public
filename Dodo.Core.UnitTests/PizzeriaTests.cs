using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Management.Organizations;
using NUnit.Framework;

namespace Dodo.Core.UnitTests
{
    [TestFixture]
    public class PizzeriaTests
    {
        private Pizzeria _pizzeria;

        [SetUp]
        public void Setup()
        {
            _pizzeria = new Pizzeria(
                id: 29,
                uuid: new Uuid("000D3A240C719A8711E68ABA13F83227"),
                name: "Сык-1",
                alias: string.Empty,
                translitAlias: string.Empty,
                approve: UnitApprove.Approved,
                state: UnitState.Open,
                departmentId: 2,
                departmentUuid: new Uuid("000D3A240C719A8711E68ABA13FC4A39"),
                countryId: 1,
                organization: null,
                square: 100,
                beginDateTimeWork: new DateTime(2010, 1, 2),
                orientation: string.Empty,
                cardPaymentPickup: null,
                coordinateX: null,
                coordinateY: null,
                clientTreatment: ClientTreatment.Name,
                terminalAtCourier: true,
                pizzeriaFormat: null);
        }

        [Test]
        public void When_passed_DateTime_returns_correct_pizzeria_years_old()
        {
            var date = new DateTime(2018, 1, 1);

            Assert.AreEqual(8, _pizzeria.GetYearsOld(date));
        }
    }
}