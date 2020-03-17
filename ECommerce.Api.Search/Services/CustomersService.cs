using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
  public class CustomersService : ICustomersService
  {
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<ICustomersService> logger;

    public CustomersService(IHttpClientFactory httpClientFactory, ILogger<ICustomersService> logger)
    {
      this.httpClientFactory = httpClientFactory;
      this.logger = logger;
    }

    public async Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
    {
      try
      {
        var client = httpClientFactory.CreateClient("CustomersService");
        var response = await client.GetAsync("api/customers");
        if (response.IsSuccessStatusCode)
        {
          var content = await response.Content.ReadAsByteArrayAsync();
          var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
          var result = JsonSerializer.Deserialize<IEnumerable<Customer>>(content, options);
          return (true, result, null);
        }
        return (false, null, response.ReasonPhrase);
      }
      catch (Exception ex)
      {
         logger?.LogError(ex.ToString());
         return (false, null, ex.Message);
      }
    }
  }
}
