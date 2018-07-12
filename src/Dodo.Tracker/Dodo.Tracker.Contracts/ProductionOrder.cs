using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Dodo.Tracker.Contracts
{
	[Serializable]
	[DataContract]
	[DebuggerDisplay("{" + nameof(Id) + "} : {" + nameof(Number) + "}")]
	public class ProductionOrder
	{
		[DataMember]
		public virtual int Id { get; set; }
		
		[DataMember]
		public virtual int Number { get; set; }

		[DataMember]
		public virtual string ClientName { get; set; }

		[DataMember]
		public virtual DateTime? ChangeDate { get; set; }
	}
}