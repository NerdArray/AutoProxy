using System.Runtime.Serialization;

namespace AutoProxy.Models
{
    [DataContract(Name = "ATWSError", Namespace = "")]
    public partial class ATWSError
    {
        [DataMember(Name = "Message", Order = 1)]
        public string Message { get; set; }
    }
}
