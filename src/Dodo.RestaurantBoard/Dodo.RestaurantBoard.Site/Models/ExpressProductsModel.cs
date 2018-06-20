using Dodo.Core.DomainModel.Departments.Units;

namespace Dodo.RestaurantBoard.Site.Models
{
	public class ExpressProductsModel
	{
		public int DepartmentId { get; }
		public int CountryId { get; }
		public int UnitId { get; }
		public ClientTreatment ClientTreatment { get; }

		public ExpressProductsModel(int departmentId, int countryId, int unitId, ClientTreatment clientTreatment)
		{
			DepartmentId = departmentId;
			CountryId = countryId;
			UnitId = unitId;
			ClientTreatment = clientTreatment;
		}
	}
}
