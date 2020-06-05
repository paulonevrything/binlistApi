using BinlistAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BinlistAPI.Services
{
    public class BinlistService : IBinlistService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BinlistService> _logger;
        public BinlistService(IConfiguration configuration, ILogger<BinlistService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<BinlistResponseModel> GetBinDetails(string cardIin)
        {
            BinlistResponseModel result = new BinlistResponseModel();

            var binlistBaseUrl = _configuration["BinlistBaseUrl"];

            var client = new RestClient($"{binlistBaseUrl}/{cardIin}");

            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse<BinlistResponseModel> response = await client.ExecuteAsync<BinlistResponseModel>(request);

                _logger.LogInformation($"[BinlistService][GetBinlist][ApiResponse] {response.Content}");

                result = JsonConvert.DeserializeObject<BinlistResponseModel>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[BinlistService][GetBinlist] An error occured while making request {ex.Message} {ex.InnerException}");
            }

            return result;

        }
    }
}
