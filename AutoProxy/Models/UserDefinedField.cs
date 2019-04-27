using System.Runtime.Serialization;

namespace AutoProxy.Models
{
    [DataContract(Name = "UserDefinedField", Namespace = "")]
    public class UserDefinedField
    {
        [DataMember(Name = "Name", Order = 1)]
        public string Name { get; set; }

        [DataMember(Name = "Value", Order = 2)]
        public string Value { get; set; }
    }
}
