using BinlistAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace BinlistAPI.Services
{
    public class BinlistService : IBinlistService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BinlistService> _logger;
        private readonly IMemoryCache _memoryCache;
        public BinlistService(IConfiguration configuration, ILogger<BinlistService> logger, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _logger = logger;
            _memoryCache = memoryCache;
        }
        public async Task<BinlistResponseModel> GetBinDetails(string cardIin)
        {

            // Check cache for iin key
            var cacheKey = cardIin;

            if(!_memoryCache.TryGetValue(cacheKey, out BinlistResponseModel result))
            {
                result = await CallBinlistNet(cardIin);

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6),
                    Priority = CacheItemPriority.Normal,
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };

                _memoryCache.Set(cacheKey, result, cacheExpiryOptions);

            }
            return result;
        }

        private async Task<BinlistResponseModel> CallBinlistNet(string cardIin)
        {
            BinlistResponseModel apirResult = new BinlistResponseModel();

            var binlistBaseUrl = _configuration["BinlistBaseUrl"];

            var client = new RestClient($"{binlistBaseUrl}/{cardIin}");

            var request = new RestRequest(Method.GET);

            try
            {
                IRestResponse<BinlistResponseModel> response = await client.ExecuteAsync<BinlistResponseModel>(request);

                _logger.LogInformation($"[BinlistService][GetBinlist][ApiResponse] {response.Content}");

                apirResult = JsonConvert.DeserializeObject<BinlistResponseModel>(response.Content);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[BinlistService][GetBinlist] An error occured while making request {ex.Message} {ex.InnerException}");
            }

            return apirResult;
        }
    }
}
