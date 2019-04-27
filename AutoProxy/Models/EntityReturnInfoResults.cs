using System.Runtime.Serialization;

namespace AutoProxy.Models
{
    [DataContract(Name = "EntityReturnInfoResults", Namespace = "")]
    public class EntityReturnInfoResults
    {
        [DataMember(Name = "EntityReturnInfo")]
        public EntityReturnInfo EntityReturnInfo { get; set; }
    }
}
