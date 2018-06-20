using System;

namespace Dodo.Core.DomainModel.Departments.Units
{
	[Serializable]
	public class PizzeriaFormat
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public PizzeriaFormat(Int32 id, String name, String description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
