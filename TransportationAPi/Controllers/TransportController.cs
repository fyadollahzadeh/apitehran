using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Validation;

namespace TransportationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        string bearerToken = "eyJhbGciOiJSUzI1NiIsInR5cGUiOiJKV1QifQ.eyJpc3MiOiJFU0JTU08iLCJBcGlLZXkiOiJlYzg2MjNkZi0xN2Q3LTRmMDMtYjhjNi0wMmE1YmNiYjgzYjQiLCJleHAiOjIwLCJzdWIiOiJodHRwOi8vdGVocmFuLmlyL2VzYi9zZXJ2aWNlcyIsImF1ZCI6IiIsInVzZXJuYW1lIjoiZmF0ZW1laF95YWRvbGxhaHphZGVAbXl0ZWhyYW4uaXIiLCJkb21haW4iOiJteXRlaHJhbi5pciIsInRpbWVzdGFtcCI6IjIwMTkxMjE4MDgxNDQwMDM2MSIsInBlcm1pc3Npb24iOm51bGwsImVudmlyb21lbnRBY2Nlc3MiOiJzYW5kYm94IiwiY3JlYXRlX2RhdGUiOiIxMi8xOC8yMDE5IDg6MTQ6NDAgQU0iLCJUb2tlbklkIjowLCJhdXRob3JpemVfaXRlbXMiOiJsSW91dUxxdGpOYk5YYzNEbnFJYms3TWpESk9iZTcyd0laS1pkTVRIVks5em1oa0ZOekFpbnRCUzlESkh1NU5HQXhwRGtsOXdMQXRtQm9WaklQRHgrUmZEV2dJNVR5NU8zcUZaNlY1bk1GWUNveWtBUHNMcFVKMytuU3VjT2o2Wm1DcDFzNURSUnZwMExyVEhvenF1aklNSFJEVE9BcTVyaWI4RkNzNmJXSWJIZ1J3blkxUzE1b3dRWllKOXV4N2xUOFZUZkRLb2NKRVF0aWNaYXJmeEhCdmtQUkVYMFZDdDRYSmVyR3grQnFCU1JvdlUrc0twWS90NnVyaWY3VllDVWlZZGhQcjVDL2QwemdVcHJsMkJxL3ZKblo5ZXJoV1RxSFMxNmZ2R2RUS2FOSUJ1TjVkb3djVkxOY1M4akdocU01a3MwVzRRWWJKQzNnYjg3YzFGNVdrM0Y4R2lwMUxkN3k0OWgwZHZVM25zSFJqNmxVY0N1SkpCZkhPMEh2cTZHcVYrbnAycytFMERtbk5rZ3BjS2NVcGtEUThXWDYyRW5qdDRzQUdZMHFJMHdGTFpEY2V2Vk1LVmc0QUkxRTJneXFXandocThvaHVDeWs2ZnJPZG9pYmhadHNTUVNkdGRmT2IvcFRCUjY4MGJyK3pHaFJDaG5MVjF1ejNyNS9KSnBaR0hoQW40NnZFL2dTcnZPUTBvVDVZNkdEOFdFL3NoZGtzcTJzTVlqekpZN08zQkpHNGJ3ekhMRWozRk9TeGEzNFcyWGRVN3dzRkQ0cFhpL0I5eWExVGxHcU04OHVHZ1hYcUt0ZUh4ZjJiY2ZwM1JhdWZRZGR3N2VmbUpFNk1jNHhIWlZMeUNWWGFZb2pyOStaSVp6a0dYd0pUbFlIdFNpeUxDY1lRVFVWalZ1QXlxM0pjaldvMjNPT2E3UDJTV2Exc21tQnVpZFFDb3BZOENtMXJEanREL2NMRWtsTXJML0hPeHdZb2l5OEZwMjFpTHN5M2MxWDN2QnhkdFhEVDlWaFlxWVVnUmo2WEVWeGNVdmdVTm1vaGhVZENmaVh2Nm5yZE9yTU5hMmU1dFVRbnlaekZmRUpNcFRranhLbUtmeTlVbjAyUGk4RHFxSGtOYmVIREdPaDBJT3gwRW9STUZ4SWIzb05oNUd4V2RMVms5NFN6ek9hcFYwZ0hDY0poZ3N5bmg4SHRndUZJamQ2ZnVRaFdRdnBURFF1NmMvS204ZFZ0RUFSUS80dGJHQnRRTVB3Z3AzcmhUUDh1d1JFWjZ3emRJQW5scHF4VjZHbG96Q0ExbWtUQUtBTkhUMGZtSnJWYS9DRTVrWXJWZ002UVppNFp0Ynh3emdBUXVQMTlpUW1WM0pQcGw1SWJqZzZZUm1oKzdZU1BOU3ltWk5KRXhPUFpHSUpuaXRjcXZTQms1dHVreklwbkJmb3krYS9YRmlnS21vQm1qZjlxcnRoOTc4aWZqWlgzSUxyOG1lWlN4ZGNPZFI3Uk1kdjIwK0FkQlpIT1B6dEU2dnNTS1ZTVzgxRFptVW0rOEQ3VWR0cjNjZ20zRnNScklJb1hMVHlJd3RnTXZod0NRbE9naWlyN1JUMXNCUWRoaS9vS3E0VWtsRlV1cVFUcDFQL09mdVF3Z2xCcDBzWmwxbEwwc3NjMWdPQVVGMGxjR1RGTjZqOTBPVjhnMEViVVBJcCt6UVdiK1Q5TFBjM3pWUXhDRlNFOEFwWkFtMXFJdERKZS85U2dBVXZGQ0sxSm90akV1QjV4TUFUMXkyWmxna25rWVUvT1I0Zm1yeFZ2ekFJeEZxS0I1bHJ2S3NFWDJtNGg3SCtETnAwaVlKOHBQUzdaeHFQN0NpVXRRTlhvRVlEQVI4aWlQSDIzQnpjQlZmNEN0MVM1NUc4cU8yV1YxTGVhclBuRVZkYTEzVjRReXpzRFc3V0Raak9Za28zV01LQVRmMmpUS3p1Rk4wMnpDU1JVejZ3dHZ3S0lINUQxaTA4cTNRYUpKVXBPQThzN21qQmtJS0VpZWhhNDhWZDFFWVBOUDgyR2theFU2aVpBZEtIWUVqVHRhN1VYdnI3bVMvVTR4b0FaR2xmRFhSVllCcHY1eHdiZktld1p1TkFwc2t0WHdDS1pKOWEzUDAzUE9GYjlTU0ZMWldvdVBxNlBMOHh4ektSQ3hZa1JFWFFyUWFWd1hSTDkvZ2RURmZmZTk2T2k3NFBqb1lCMjFQR3h1NU5RREl5TTlVNWZ5SHc1TE9EN3FPK3YyczFrNUhNV1JMQkNUbFFsQ2pnZWt1OEtQZ0p3NUxsajNLN2M2MlZvOTVoVzFqQVZaSUtKMm96TmtTb2pNWERQNnJzYkxiU0xVRTlIdTUyM1AxNVdUYmwwTWF5ZTVwL3lDd0Z4d3JGSzc3TDlQZ3JxV2VZTTlUMTE1WXZzU1g2RlpHeDlGcm5DWU5aU1NvUlNxT3pnVXQxNC9mTGJsRHh1YXpJWFNFckhxVHQyaiszelk1MHh2NUlwbDN6cFY3NGoyVjg1MHZoU1VkemxTa1EySDJlQkN3MmsvUVhsUlFQcm94Q1pIVUwvZ3pEcjFGTmFlcktHbGtKa1Jub1d0ZllhMU1EOFlWcGsvSFByZWp5VTQybUpEK2JiWnREK0lFRjRZKzlTRUlYUEUxbXY3OHBnc09TaXBCL25ZQ3dpRDlsc2N4MkMyTTNmYklGWExQb1BxL0RnbEFDUkFFUXJzMFVNTVZzSGlLa0NuZlRYZi9XS1JqNTRZdmJFL1NUMWJSQ2VCeHFnTXA3V2pGUGk0ZEtyNFlwR0thR0tCaGZoNE1EL0pkeWtMcVZsdW1xYU9GMzltUUV1MnIzRVdneGVNYlR1KzVqcWlMNlZjY054UmpscXdlQWVvdTcwNlZXdXNiMXAxYnZKdmFlY0FRU1ZSbVVVODljeEVNbHlSU2lvd2FwN2hkOEFMbWp2WTRPVT0iLCJvaWQiOiIvMmd6NWNaeU9qUGI2MUNuS2Vxb3FNeW1lL1pYQkNTdi9xeWpSTU03KzluZFJHTVZBYzVPYStuNjVUeEJGNmdJIiwiQ0hLQXR0ciI6ZmFsc2UsIlJlc3VsdE1lc3NhZ2VzIjpudWxsfQ.aPWXrYLwJxo2gb6xcGz-qMTb037tMP4dfB439tRy5A0";

        // GET: api/Transport
        [HttpGet]
        public async Task<List<MetroStation>> GetAsync()
        {
            var location = new Location { X = 535654, Y= 3952855,BufferSize=30000 };
            var result= CallForNearMetroStation(location);
            return  result;
        }

        
        private List<MetroLine> CallForNearMetro(Location location)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
            var client = new RestClient("https://31.24.236.154:8082/TM.Services.GIS.Map.ver1/api/GetTrafficMetroLine");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "57");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "31.24.236.154:8082");
            request.AddHeader("Postman-Token", "042d55db-ef80-41b3-99bb-fe23ea82e704,ed3a79f4-792f-4dc1-8e1e-95ead72238de");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", JsonConvert.SerializeObject(location), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result=JsonConvert.DeserializeObject<MetroRoot>(response.Content);
            return result.MetroLine;
        }


        private List<BusTerminal> CallForNearBus(Location location)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
            var client = new RestClient("https://31.24.236.154:8082/TM.Services.GIS.Map.ver1/api/GetTrafficBusTerminal");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "57");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "31.24.236.154:8082");
            request.AddHeader("Postman-Token", "042d55db-ef80-41b3-99bb-fe23ea82e704,ed3a79f4-792f-4dc1-8e1e-95ead72238de");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", JsonConvert.SerializeObject(location), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<BusRoot>(response.Content);
            return result.BusTerminal;
        }


        private List<TaxiTerminal> CallForNearTaxi(Location location)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
            var client = new RestClient("https://31.24.236.154:8082/TM.Services.GIS.Map.ver1/api/GetTrafficTaxiTerminal");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "57");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "31.24.236.154:8082");
            request.AddHeader("Postman-Token", "042d55db-ef80-41b3-99bb-fe23ea82e704,ed3a79f4-792f-4dc1-8e1e-95ead72238de");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", JsonConvert.SerializeObject(location), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<TaxiRoot>(response.Content);
            return result.TaxiTerminal;
        }

        private List<MetroStation> CallForNearMetroStation(Location location)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, certificate, chain, sslPolicyErrors) => true;
            var client = new RestClient("https://31.24.236.154:8082/TM.Services.GIS.Map.ver1/api/GetTrafficMetroStop");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Content-Length", "57");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
            request.AddHeader("Host", "31.24.236.154:8082");
            request.AddHeader("Postman-Token", "042d55db-ef80-41b3-99bb-fe23ea82e704,ed3a79f4-792f-4dc1-8e1e-95ead72238de");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.20.1");
            request.AddHeader("Authorization", "Bearer " + bearerToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", JsonConvert.SerializeObject(location), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<MetroStationRoot>(response.Content);
            return result.MetroStation;
        }


        // GET: api/Transport/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Transport
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
        }

        // PUT: api/Transport/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
