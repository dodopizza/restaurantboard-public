using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.OrderProcessing;

namespace Dodo.Tests.DSL
{
    public class ObjectMother
    {
        public RestaurantReadnessOrders CreateRestaurantReadnessOrderWithNumber(int number)
        {
            return new RestaurantReadnessOrders(
                orderId: 1,
                orderNumber: number,
                clientName: "TestClient",
                orderReadyDateTime: DateTime.Now
            );
        }

        public Pizzeria CreatePizzeria()
        {
            return new Pizzeria(
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
                beginDateTimeWork: new DateTime(2018, 1, 1),
                orientation: string.Empty,
                cardPaymentPickup: null,
                coordinateX: null,
                coordinateY: null,
                clientTreatment: ClientTreatment.Name,
                terminalAtCourier: true,
                pizzeriaFormat: null
            );
        }

        public Pizzeria CreatePizzeriaWithClientTreatment(ClientTreatment clientTreatment)
        {
            return new Pizzeria(
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
                beginDateTimeWork: new DateTime(2018, 1, 1),
                orientation: string.Empty,
                cardPaymentPickup: null,
                coordinateX: null,
                coordinateY: null,
                clientTreatment: clientTreatment,
                terminalAtCourier: true,
                pizzeriaFormat: null
            );
        }

        public Unit CreateUnit()
        {
            return CreatePizzeria();
        }

        public Unit CreateUnitWithUuid(Uuid uuid)
        {
            return new Pizzeria(
                id: 29,
                uuid: uuid,
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
                beginDateTimeWork: new DateTime(2018, 1, 1),
                orientation: string.Empty,
                cardPaymentPickup: null,
                coordinateX: null,
                coordinateY: null,
                clientTreatment: ClientTreatment.Name,
                terminalAtCourier: true,
                pizzeriaFormat: null
            );
        }

        public Department CreateDepartment()
        {
            return new CityDepartment
            {
                Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty),
            };
        }
    }
}