using System.ComponentModel.DataAnnotations;
using Dodo.Core.Resources.Finance;

namespace Dodo.Core.DomainModel.Finance
{
	public enum Currency
	{
		[Display(ResourceType = typeof(Text), Name = "Name_Unknown", ShortName = "Currency_Unknown")]
		Unknown = 0,

		[Display(ResourceType = typeof(Text), Name = "Name_Ruble", ShortName = "Currency_Ruble")]
		Ruble = 1,

		[Display(ResourceType = typeof(Text), Name = "Name_Euro", ShortName = "Currency_Euro")]
		Euro = 2,

		[Display(ResourceType = typeof(Text), Name = "Name_Dollar", ShortName = "Currency_Dollar")]
		Dollar = 3,

		[Display(ResourceType = typeof(Text), Name = "Name_Grivna", ShortName = "Currency_Grivna")]
		Grivna = 4,

		[Display(ResourceType = typeof(Text), Name = "Name_RomanianLeu", ShortName = "Currency_RomanianLeu")]
		RomanianLeu = 5,

		[Display(ResourceType = typeof(Text), Name = "Name_Tenge", ShortName = "Currency_Tenge")]
		Tenge = 6,

		[Display(ResourceType = typeof(Text), Name = "Name_Yuan", ShortName = "Currency_Yuan")]
		Yuan = 7,

		[Display(ResourceType = typeof(Text), Name = "Name_Som", ShortName = "Currency_Som")]
		Som = 8,

		[Display(ResourceType = typeof(Text), Name = "Name_Sum", ShortName = "Currency_Sum")]
		Sum = 9,

		[Display(ResourceType = typeof(Text), Name = "Name_PoundSterling", ShortName = "Currency_PoundSterling")]
		PoundSterling = 10
	}
}