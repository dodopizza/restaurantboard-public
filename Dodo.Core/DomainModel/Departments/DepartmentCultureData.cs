using System;
using System.ComponentModel.DataAnnotations;
using Dodo.Core.Resources.GlobalResources;


namespace Dodo.Core.DomainModel.Departments
{
	public class DepartmentCultureData
	{
		public Int32 Id { get; set; }
		public Int32 DepartmentId { get; set; }
		public String CultureName { set; get; }

        [Required(ErrorMessageResourceName = "GlobalValidationMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [StringLength(255)]
		public String Name { get; set; }


		public Boolean IsFilled()
		{
			if ( String.IsNullOrEmpty(Name) )
				return false;
			return true;
		}

		public DepartmentCultureData(Int32 id, String cultureName, Int32 departmentId, String name)
		{
			Id = id;
			CultureName = cultureName;
			DepartmentId = departmentId;

			Name = name;
		}
		public DepartmentCultureData()
		{

		}
	}
}
