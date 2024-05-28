using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands;
using Application.Queries;

namespace WebApi.Contollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) 
        { 
             _mediator = mediator;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts(GetProductByIdQuery getProductByIdQuery)
        {
            try
            {
                return Ok(await _mediator.Send(getProductByIdQuery));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand addProductCommand )
        {
            try 
            { 
                return Ok(await _mediator.Send(addProductCommand));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
