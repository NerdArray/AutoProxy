using AutoProxy.Extensions;
using AutoProxy.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AutoProxy.Services
{
    internal class AutotaskService
    {
        readonly XNamespace _ns = "http://schemas.xmlsoap.org/soap/envelope/";
        readonly XNamespace _myns = "http://autotask.net/ATWS/v1_6/";
        readonly XNamespace _xsi = "http://www.w3.org/2001/XMLSchema-instance";
        readonly XNamespace _xsd = "http://www.w3.org/2001/XMLSchema";

        private readonly IConfiguration _config;
        private readonly ILogger<AutotaskService> _logger;

        private readonly string _serviceUrl;

        private readonly HttpClient _client;

        private string _regionUri;

        public AutotaskService(
            IConfiguration config,
            IHttpClientFactory factory,
            ILogger<AutotaskService> logger)
        {
            _config = config;
            _client = factory.CreateClient("Autotask");
            
            _logger = logger;

            var baseUri = new Uri(_config["AutotaskBaseUrl"]);

            // Set HttpClient connections to timeout after 1m.
            var sp = ServicePointManager.FindServicePoint(baseUri);
            sp.ConnectionLeaseTimeout = 60000; // 1 minute

            _serviceUrl = baseUri + "/atservices/1.6/atws.asmx";
        }

        public async Task<QueryResult<T>> Query<T>(IEnumerable<string> conditions) where T : Entity
        {
            // build a queryxml query... 
            StringBuilder sb = new StringBuilder();
            sb.Append("<queryxml><entity>" + typeof(T).Name + "</entity><query>");

            // add conditions...
            if (conditions != null)
            {
                foreach (var c in conditions)
                {
                    var args = c.Split(',');
                    if (args.Length == 3)
                    {
                        sb.Append("<condition><field>" + args[0] + "<expression op='" + args[1] + "'>" + args[2] + "</expression></field></condition>");
                    }
                }
            }

            // close the queryxml
            sb.Append("</query></queryxml>");

            // convert the queryxml string to xml
            var xml =
                new XElement(_myns + "query",
                    new XElement(_myns + "sXML",
                        new XCData(sb.ToString())));

            // build a soap request from the query
            var request = BuildSoapEnvelope(xml);

            // make the request
            var response = await PostXmlRequest(request);

            if (response.IsSuccessStatusCode) // Received a 2xx response...
            {
                // convert response to string.
                var decodedResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    // convert response to XDocument.
                    var xdoc = XDocument.Parse(decodedResponse);

                    // strip all namespaces from the document.
                    xdoc.StripNamespaces();

                    // create a new XDocument with just children
                    // of the getZoneInfoResponse node.
                    var body = xdoc.Descendants("queryResult");
                    xdoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "no"),
                        body);

                    // serialize the data to an object.
                    var serializer = new DataContractSerializer(typeof(QueryResult<T>));
                    using (var stream = new MemoryStream())
                    {
                        // convert the XDocument to a stream so
                        // we can deserialize to an object.
                        await xdoc.SaveAsync(stream, SaveOptions.None, new CancellationToken());

                        // reset pointer to beginning of stream.
                        // otherwise you'll get 'missing root element'
                        // errors.
                        stream.Position = 0;

                        // deserialize result to a complex object and return it.
                        var result = (QueryResult<T>)serializer.ReadObject(stream);
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    var err = "Exception: " + ex.Message;
                    if (ex.InnerException != null)
                    {
                        err += "\r\nInner Exception: " + ex.InnerException.Message;
                    }
                    err += "\r\nStack Trace: " + ex.StackTrace;
                    _logger.LogError(err); // log error.
                    return null;
                }
            }
            else
            {
                // TODO: Handle error status codes.
                return null;
            }
        }

        private string BuildSoapEnvelope(XElement action)
        {
            XDocument xml = new XDocument(
                new XDeclaration("1.0", "utf-8", "no"),
                new XElement(_ns + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "xsd", _xsd),
                    new XAttribute(XNamespace.Xmlns + "xsi", _xsi),
                    new XAttribute(XNamespace.Xmlns + "soap", _ns),
                    new XElement(_ns + "Header",
                        new XElement(_myns + "AutotaskIntegrations",
                            new XElement(_myns + "IntegrationCode", _config["IntegrationCode"]))),
                    new XElement(_ns + "Body",
                        action)));

            return xml.ToString();
        }

        private async Task<HttpResponseMessage> PostXmlRequest(string xml)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));

            var request = new HttpRequestMessage(HttpMethod.Post, _serviceUrl);
            request.Content = new StringContent(xml, Encoding.UTF8, "text/xml");
            request.Content.Headers.Clear();
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");

            return await _client.SendAsync(request);
        }

        public async Task SetZone(string zoneUser)
        {
            var xml =
                new XElement(_myns + "getZoneInfo",
                    new XElement(_myns + "UserName", zoneUser));

            var response = await PostXmlRequest(BuildSoapEnvelope(xml));

            if (response.IsSuccessStatusCode)
            {
                // convert response to string.
                var decodedResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    // convert response to XDocument.
                    var xdoc = XDocument.Parse(decodedResponse);

                    // strip all namespaces from the document.
                    xdoc.StripNamespaces();

                    // create a new XDocument with just children
                    // of the getZoneInfoResponse node.
                    var body = xdoc.Descendants("getZoneInfoResult");
                    xdoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", "no"),
                        body);

                    // serialize the data to an object.
                    var serializer = new XmlSerializer(typeof(GetZoneInfoResult));
                    using (var stream = new MemoryStream())
                    {
                        // convert the XDocument to a stream so
                        // we can deserialize to an object.
                        await xdoc.SaveAsync(stream, SaveOptions.None, new CancellationToken());

                        // reset pointer to beginning of stream.
                        // otherwise you'll get 'missing root element'
                        // errors.
                        stream.Position = 0;

                        // deserialize result to a complex object.
                        var zoneInfo = (GetZoneInfoResult)serializer.Deserialize(stream);

                        // set the region for the class.
                        _regionUri = zoneInfo.URL;
                        _logger.LogInformation("Autotask region set: " + zoneInfo.WebUrl);
                    }
                }
                catch (Exception ex)
                {
                    var err = "Exception: " + ex.Message;
                    if (ex.InnerException != null)
                    {
                        err += "\r\nInner Exception: " + ex.InnerException.Message;
                    }
                    err += "\r\nStack Trace: " + ex.StackTrace;
                    _logger.LogError(err); // log error.
                }
            }
            else
            {
                // TODO: Handle error status codes.
            }

            await Task.CompletedTask;
        }
    }
}
