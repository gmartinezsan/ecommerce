using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
  [ApiController]
  [Route("api/products")]
  public class ProductsController : ControllerBase
  {
    
   private readonly IProductsProvider productsProvider;

    public ProductsController(IProductsProvider productsProvider)
    {
      this.productsProvider = productsProvider;
    }

     
      [HttpGet]
      public async Task<IActionResult> GetProductsAsync()
      {
        var result = await productsProvider.GetProductsAsync();
        if (result.IsSuccess)
        {
          return Ok(result.Products);
        }
        else
        {
          return NotFound();
        }
      }

      [HttpGet("{id}")]
      public async Task<IActionResult> GetProductAsync(int id)
      {
        var result = await productsProvider.GetProductAsync(id);
        if (result.IsSuccess)
        {
          return Ok(result.Product);
        }
        else
        {
          return NotFound();
        }
      }

  }
}
