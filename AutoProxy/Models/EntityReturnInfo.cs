using System.Runtime.Serialization;

namespace AutoProxy.Models
{
    [DataContract(Name = "EntityReturnInfo", Namespace = "")]
    public class EntityReturnInfo
    {
        [DataMember(Name = "EntityId", Order = 1)]
        public long EntityId { get; set; }

        [DataMember(Name = "DatabaseAction", Order = 2)]
        public string DatabaseAction { get; set; }

        [DataMember(Name = "DuplicateStatus", Order = 3)]
        public DuplicateStatus DuplicateStatus { get; set; }
    }
}
