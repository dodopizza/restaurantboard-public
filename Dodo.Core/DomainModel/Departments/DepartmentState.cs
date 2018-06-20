using System.ComponentModel.DataAnnotations;
using Dodo.Core.Resources.Departments;

namespace Dodo.Core.DomainModel.Departments
{
	public enum DepartmentState
	{
		[Display(ResourceType = typeof(Text), Name = "Close")]
		Close = 0,

		[Display(ResourceType = typeof(Text), Name = "Open")]
		Open = 1,

		[Display(ResourceType = typeof(Text), Name = "SoonOpening")]
		SoonOpening = 2
	}
}