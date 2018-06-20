using System;

namespace Dodo.Core.DomainModel.Departments.Parameters.Unit
{
	[Serializable]
	public class PizzeriaAddress
	{
		public Int32? LocalityId  { get; set; }
		public String LocalityName { get; set; }
		public Int32? StreetId { get; set; }
		public String StreetName { get; set; }
		public String HouseNumber { get; set; }

		public Boolean HasValue
		{
			get
			{
				return !(String.IsNullOrEmpty(LocalityName) &&
						String.IsNullOrEmpty(StreetName) &&
						String.IsNullOrEmpty(HouseNumber));
			}
		}

		public override String ToString()
		{
			return $"{LocalityName}, {StreetName} {HouseNumber}, ";
		}
	}
}