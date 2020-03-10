using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Controllers
{
  [ApiController]
  [Route("api/orders")]
  public class OrdersController : ControllerBase
  {
    private readonly IOrdersProvider ordersProvider;

    public OrdersController(IOrdersProvider ordersProvider)
    {
      this.ordersProvider = ordersProvider;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetOrdersAsync(int customerId)
    {
      var result = await ordersProvider.GetOrdersAsync(customerId);
      if (result.IsSuccess)
      {
        return Ok(result.Orders);
      }
      else
      {
        return NotFound();
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderAsync(int id)
    {
      var result = await ordersProvider.GetOrderAsync(id);
      if (result.IsSuccess)
      {
        return Ok(result.Order);
      }
      else
      {
        return NotFound();
      }
    }
  }
}
