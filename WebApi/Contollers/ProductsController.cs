using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands;
using Application.Queries;
using Asp.Versioning;

namespace WebApi.Contollers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) 
        { 
             _mediator = mediator;
            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProducts(Guid id)
        {
            try
            {
                var product = await _mediator.Send(new GetProductByIdQuery(id));
                return product == null ?  NotFound() : Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                return Ok(await _mediator.Send(new GetProductsQuery()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductCommand addProductCommand )
        {
            try 
            {
                var product = await _mediator.Send(addProductCommand);
                return Created($"/api/v{ApiVersion.Default}/Products/{product.Id}", product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProducts(Guid id, UpdateProductCommand updateProductCommand)
        {
            try
            {
                if (id != updateProductCommand.Id)
                {
                    return ValidationProblem("Mismatch between id in uri and request body");
                }
                var result = await _mediator.Send(updateProductCommand);
                if (result == -1) return NotFound();
                return NoContent();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var res = await _mediator.Send(new DeleteProductCommand(id));
                if(res == -1) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
