using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Dodo.Tracker.Contracts
{
    public interface IProductionOrder
    {
        int Id { get; set; }
        int Number { get; set; }
        string ClientName { get; set; }
        DateTime? ChangeDate { get; set; }
        DateTime OrderDate { get; set; }

        bool IsExpired(DateTime now);

    }

    [Serializable]
	[DataContract]
	[DebuggerDisplay("{" + nameof(Id) + "} : {" + nameof(Number) + "}")]
	public class ProductionOrder : IProductionOrder
    {
		[DataMember]
		public int Id { get; set; }
		
		[DataMember]
		public int Number { get; set; }

		[DataMember]
		public string ClientName { get; set; }

		[DataMember]
		public DateTime? ChangeDate { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }

        public bool IsExpired(DateTime now)
        {
            return (now - OrderDate).TotalSeconds>10;
        }
    }
}