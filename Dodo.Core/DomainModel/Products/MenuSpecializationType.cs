using System.ComponentModel.DataAnnotations;
using Dodo.Core.Resources.Products;

namespace Dodo.Core.DomainModel.Products
{
	public enum MenuSpecializationType
	{
		[Display(ResourceType = typeof(Text), Name = "WithoutSpecialization")]
		None = 0,

		[Display(ResourceType = typeof(Text), Name = "Halal")]
		Halal = 1,

		[Display(ResourceType = typeof(Text), Name = "HalfHalal")]
		HalfHalal = 2,

        [Display(ResourceType = typeof(Text), Name = "European")]
        European = 3
	}
}