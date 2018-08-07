using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.DomainModel.Departments.Units
{
	[Serializable]
	public class Pizzeria : Unit
	{
		public PizzeriaFormat Format { get; private set; }
		public Double Square { get; private set; }
		public DateTime? BeginDateTimeWork { get; private set; }
		public String Orientation { get; private set; }
		public Boolean? CardPaymentPickup { get; private set; }
		public Decimal? CoordinateX { get; private set; }
		public Decimal? CoordinateY { get; private set; }
		public ClientTreatment ClientTreatment { get; private set; }
        public Boolean TerminalAtCourier { get; private set; }

		public Boolean IsExistsPizzeriaFormat { get { return Format != null; }}

		public Pizzeria(Int32 id, Uuid uuid, String name, String alias, String translitAlias, UnitApprove approve, UnitState state, 
            Int32 departmentId, Uuid departmentUuid, Int32 countryId, OrganizationShortInfo organization, Double square,
			DateTime? beginDateTimeWork, String orientation, Boolean? cardPaymentPickup, Decimal? coordinateX, Decimal? coordinateY,
			ClientTreatment clientTreatment, Boolean terminalAtCourier, PizzeriaFormat pizzeriaFormat)
			: base(id, uuid, name, alias, UnitType.Pizzeria, state, departmentId, departmentUuid, countryId, organization)
		{
			Format = pizzeriaFormat;
			Square = square;
			BeginDateTimeWork = beginDateTimeWork;
			Orientation = orientation;
			CardPaymentPickup = cardPaymentPickup;
			CoordinateX = coordinateX;
			CoordinateY = coordinateY;
			ClientTreatment = clientTreatment;

			Approve = approve;
			TranslitAlias = translitAlias;
            TerminalAtCourier = terminalAtCourier;

        }

		public Int32 GetYearsOld(DateTime currentDateTime)
		{
			if (!BeginDateTimeWork.HasValue) return 0;

			DateTime zeroDate = new DateTime(1,1,1);
			TimeSpan diffDate = currentDateTime.Subtract(BeginDateTimeWork.Value);

			return (zeroDate + diffDate).Year;
		}

		public Int32 GetMonthsOld(DateTime currentDateTime)
		{
			const Int32 monthInYear = 12;

			if (!BeginDateTimeWork.HasValue) return 0;

			DateTime zeroDate = new DateTime(1, 1, 1);
			TimeSpan diffDate = currentDateTime.Subtract(BeginDateTimeWork.Value);

			var previousYearsMonths = monthInYear * GetYearsOld(currentDateTime);
			var currentYearMonths = (zeroDate + diffDate).Month - 1;

			return previousYearsMonths + currentYearMonths;
		}
	}
}