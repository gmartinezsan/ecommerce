﻿using AutoMapper;
using ECommerce.Api.Orders.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
  public class OrdersProvider : IOrdersProvider
  {

    private readonly OrdersDbContext dbContext;
    private readonly ILogger<OrdersProvider> logger;
    private readonly IMapper mapper;

    public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
    {
      this.dbContext = dbContext;
      this.logger = logger;
      this.mapper = mapper;

      SeedData();

    }

    private void SeedData()
    {
      if (!dbContext.Orders.Any())
      {
        dbContext.Orders.Add(new Order()
        {
          Id = 1,
          CustomerId = 1,
          OrderDate = DateTime.Now,
          Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
          Total = 100
        });
        dbContext.Orders.Add(new Order()
        {
          Id = 2,
          CustomerId = 1,
          OrderDate = DateTime.Now.AddDays(-1),
          Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
          Total = 100
        });
        dbContext.Orders.Add(new Order()
        {
          Id = 3,
          CustomerId = 2,
          OrderDate = DateTime.Now,
          Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
          Total = 100
        });
        dbContext.SaveChanges();
      }
    }

    public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(int Id)
    {
      try
      {
        var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == Id);
        if (order != null)
        {
          var result = mapper.Map<Order, Models.Order>(order);
          return (true, result, null);
        }
        return (false, null, "Not found");
      }
      catch (Exception ex)
      {
        logger?.LogError(ex.ToString());
        return (false, null, ex.Message);
      }
    }

    public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
    {
      try
      {
        var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();
        if (orders != null && orders.Any())
        {
          var result = mapper.Map<IEnumerable<Order>, IEnumerable<Models.Order>>(orders);
          return (true, result, null);
        }
        return (false, null, "Not found");
      }
      catch (Exception ex)
      {
        logger?.LogError(ex.ToString());
        return (false, null, ex.Message);
      }
    }
  }
}
