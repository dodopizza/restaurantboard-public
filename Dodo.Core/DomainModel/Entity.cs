using System;

namespace Dodo.Core.DomainModel
{
	[Serializable]
	public abstract class Entity : IEntityKey<int>
	{
		public virtual int Id { get; set; }
	}
}