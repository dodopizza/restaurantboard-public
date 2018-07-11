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
		public int Id { get; set; }
		
		[DataMember]
		public int Number { get; set; }

		[DataMember]
		public string ClientName { get; set; }

		[DataMember]
		public DateTime? ChangeDate { get; set; }

        public bool IsExpiring(DateTime now)
        {
            return ChangeDate?.AddHours(1) < now;
        }
	}
}