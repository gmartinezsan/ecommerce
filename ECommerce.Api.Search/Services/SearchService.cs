﻿using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
  public class SearchService : ISearchService
  {
    private readonly IOrdersService ordersService;
    private readonly IProductsService productsService;
    private readonly ICustomersService customersService;

    public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
    {
      this.ordersService = ordersService;
      this.productsService = productsService;
      this.customersService = customersService;
    }
    public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
    {
      var ordersResult = await ordersService.GetOrdersAsync(customerId);
      var productsResult = await productsService.GetProductsAsync();
      var customersResult = await customersService.GetCustomersAsync();
      Customer c = new Customer();

      if (customersResult.IsSuccess)
      {
        c.Id = customersResult.Customers.FirstOrDefault(t => t.Id == customerId).Id;
        c.Name = customersResult.Customers.FirstOrDefault(t => t.Id == customerId).Name;
      }
      else
      { 
         c.Id = customerId;
         c.Name = "Customer information is not available";
      }

      if (ordersResult.IsSuccess)
      {       
        foreach (var order in ordersResult.Orders)
        {   
          order.Customer = c;
          foreach (var item in order.Items)
          {
            item.ProductName = productsResult.IsSuccess ?
            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId).Name :
            "Product Information is not available";
          }
        }
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
