using System;
using System.ComponentModel;
using Dodo.Core.DomainModel.Products;

namespace Dodo.Core.DomainModel.Departments.Departments
{
	[Serializable]
	public class CityDepartment : Department
	{
		public MenuSpecializationType MenuSpecializationType { get; set; }

		[Description("Just for tests")]
		public CityDepartment(UtcOffsetProvider dateTimeProvider = null) : base(dateTimeProvider)
        {
        }
	}
}
