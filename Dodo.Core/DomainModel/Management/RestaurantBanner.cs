using System;
using System.ComponentModel.DataAnnotations;
using Dodo.Core.DomainModel.Products;
using Dodo.Core.Resources.GlobalResources;

namespace Dodo.Core.DomainModel.Management
{
	[Serializable]
	public class RestaurantBanner
	{
		public Int32 Id { get; set; }
		public String Url { get; set; }
		public String Path { get; set; }

		[Required]
		[Range(0, 10)]
		public Int32 Priority { get; set; }

		[Required]
		public Int32 CountryId { get; set; }

		public DateTime? ShowAfter { get; set; }
		public DateTime? ShowUntil { get; set; }

        [Required(ErrorMessageResourceName = "GlobalValidationMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        [Range(1, 10000)]
		public Int32 DisplayTime { get; set; }

        [Required(ErrorMessageResourceName = "GlobalValidationMessage", ErrorMessageResourceType = typeof(GlobalResources))]
        public MenuSpecializationType[] MenuSpecializationTypes { get; set; }
	}
}