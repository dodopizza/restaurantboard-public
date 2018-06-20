using System.ComponentModel.DataAnnotations;
using Dodo.Core.Resources.Products;

namespace Dodo.Core.DomainModel.Products
{
	public enum ProductCategory
	{
		[Display(ResourceType = typeof(Text), Name = "Unknown")]
		Unknown = 0,

		[Display(ResourceType = typeof(Text), Name = "CategoryPizza")]
		Pizza = 1,

		[Display(ResourceType = typeof(Text), Name = "CategoryDrinks")]
		Drinks = 2,

		[Display(ResourceType = typeof(Text), Name = "CategorySnacks")]
		Snacks = 3,

		[Display(ResourceType = typeof(Text), Name = "CategorySauces")]
		Sauces = 4,

		[Display(ResourceType = typeof(Text), Name = "CategoryGoods")]
		Goods = 5,

		[Display(ResourceType = typeof(Text), Name = "CategoryDesserts")]
		Desserts = 6,

		[Display(ResourceType = typeof(Text), Name = "CategoryPieces")]
		Pieces = 7
	}
}