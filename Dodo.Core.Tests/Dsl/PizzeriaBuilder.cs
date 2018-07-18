using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;

namespace Tests.Dsl
{
    public class PizzeriaBuilder
    {
        public PizzeriaBuilder WhichBeginsWorkAt(DateTime beginDateTimeWork)
        {
            _beginDateTimeWork = beginDateTimeWork;
            return this;
        }

        public PizzeriaBuilder WhichNotOpenedYet()
        {
            return this;
        }

        public Pizzeria Please()
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
                beginDateTimeWork: _beginDateTimeWork,
                orientation: string.Empty,
                cardPaymentPickup: null,
                coordinateX: null,
                coordinateY: null,
                clientTreatment: ClientTreatment.Name,
                terminalAtCourier: true,
                pizzeriaFormat: null
            );
        }

        private DateTime? _beginDateTimeWork;
    }
}