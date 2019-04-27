using System;
using System.Xml.Serialization;

namespace AutoProxy.Models
{
    [Serializable()]
    [XmlRoot("getZoneInfoResult")]
    public partial class GetZoneInfoResult
    {
        public string URL { get; set; }

        public byte ErrorCode { get; set; }

        public string DataBaseType { get; set; }

        public ushort CI { get; set; }

        public string WebUrl { get; set; }
    }
}
