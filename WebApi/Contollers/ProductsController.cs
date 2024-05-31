using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Commands;
using Application.Queries;
using Asp.Versioning;
using Application.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;

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
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery getProductsQuery)
        {
            try
            {
                var response = await _mediator.Send(getProductsQuery);
               
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductCommand createProductCommand )
        {
            try 
            {
                var response = await _mediator.Send(createProductCommand);

                return response.Exists ?  Conflict("Product already exists") : 
                    Created($"/api/v{ApiVersion.Default}/Products/{response.Product}", response.Product);
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
                
                return result == -1 ? NotFound() : NoContent();
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
                var result = await _mediator.Send(new DeleteProductCommand(id));

                if (result == -1)
                {
                    return NotFound();
                }
                else if(result == -2)
                {
                    return Conflict("Product is in use");
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
