using ECommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
  public class SearchService : ISearchService
  {
    private readonly IOrdersService ordersService;
    public SearchService(IOrdersService ordersService)
    {
      this.ordersService = ordersService;
    }
    public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
    {
      var ordersResult = await ordersService.GetOrdersAsync(customerId);
      if (ordersResult.IsSuccess)
      { 
        var result = new
        {
          Orders = ordersResult.Orders
        };
        return (true, result);
      }
      return (false, null);     
    }
  }
}
