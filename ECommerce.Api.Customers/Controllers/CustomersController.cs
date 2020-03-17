﻿using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{

  [ApiController]
  [Route("api/customers")]
  public class CustomersController : ControllerBase
  {
    private readonly ICustomersProvider customersProvider;

    public CustomersController(ICustomersProvider customersProvider)
    {
       this.customersProvider = customersProvider;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomersAsync()
    {
      var result = await customersProvider.GetCustomersAsync();
      if (result.IsSuccess)
      {
        return Ok(result.Customers);
      }
      else
      {
        return NotFound();
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerAsync(int id)
    {
      var result = await customersProvider.GetCustomerAsync(id);
      if (result.IsSuccess)
      {
        return Ok(result.Customer);
      }
      else
      {
        return NotFound();
      }
    }
  }
}
