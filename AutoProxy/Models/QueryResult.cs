using System.Runtime.Serialization;

namespace AutoProxy.Models
{
    [DataContract(Name = "queryResult", Namespace = "")]
    public partial class QueryResult<T> where T : Entity
    {
        [DataMember(Name = "ReturnCode", Order = 1)]
        public int ReturnCode { get; set; }

        [DataMember(Name = "EntityResults", Order = 2)]
        public T[] EntityResults { get; set; }

        [DataMember(Name = "EntityResultType", Order = 3)]
        public string EntityResultType { get; set; }

        [DataMember(Name = "Errors", Order = 4)]
        public ATWSError[] Errors { get; set; }

        [DataMember(Name = "EntityReturnInfoResults", Order = 5)]
        public EntityReturnInfoResults EntityReturnInfoResults { get; set; }
    }
}
