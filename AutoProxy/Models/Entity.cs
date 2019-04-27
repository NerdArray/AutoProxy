using System.Runtime.Serialization;

namespace AutoProxy.Models
{
    [KnownType(typeof(Account))]
    [DataContract(Name = "Entity", Namespace = "")]
    public abstract class Entity
    {
    }
}
