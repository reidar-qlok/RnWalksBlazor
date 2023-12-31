﻿using Blazored.LocalStorage;
using RnWalksBlazor.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RnWalksBlazor.Services
{
    public class RegionService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        private List<Region> items;
        public RegionService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }
        public async Task<List<Region>> GetAllRegionsAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("JwtToken");
            if (token == null)
            {
                // Starta en autoinlog med hårdkodat data
            }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            try
            {
                var response = await _httpClient.GetAsync("api/Regions");
                if (response.IsSuccessStatusCode)
                {
                    // jag är inloggad i apiet
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Region>>
                        (jsonString, new JsonSerializerOptions
                        { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    // Jag lyckades inte logga in i apiet
                    throw new Exception("Error connecting server");
                }
            }
            catch (Exception)
            {
                throw new Exception("Error retrieving customers");
            }
        }
    }
}
