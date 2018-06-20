using Dodo.Core.DomainModel.Departments.Units;

namespace Dodo.RestaurantBoard.Site.Models
{
	public class OrdersReadinessToStationaryModel
	{
		public int DepartmentId { get; }
		public int CountryId { get; }
		public int UnitId { get; }
		public bool IsNewBoard { get; }
		public ClientTreatment ClientTreatment { get; }

		public OrdersReadinessToStationaryModel(int departmentId, int countryId, int unitId, bool isNewBoard, ClientTreatment clientTreatment)
		{
			UnitId = unitId;
			CountryId = countryId;
			DepartmentId = departmentId;
			IsNewBoard = isNewBoard;
			ClientTreatment = clientTreatment;
		}
	}
}
