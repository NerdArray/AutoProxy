using System.Runtime.Serialization;

namespace AutoProxy.Models
{
    [DataContract]
    public class DuplicateStatus
    {
        [DataMember]
        public bool Found { get; set; }

        [DataMember]
        public bool Ignored { get; set; } 
    }
}
