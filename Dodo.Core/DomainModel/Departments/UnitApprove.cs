using System.ComponentModel.DataAnnotations;
using Dodo.Core.Resources.Departments;

namespace Dodo.Core.DomainModel.Departments
{
	public enum UnitApprove
	{
		[Display(ResourceType = typeof(Text), Name = "NotApproved")]
        NotApproved = 0,

		[Display(ResourceType = typeof(Text), Name = "Approved")]
        Approved = 1,
	}
}