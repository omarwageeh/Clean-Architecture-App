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
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProducts(Guid id)
        {
            try
            {
                _logger.LogDebug($"Sending GetProductsByIdQuery called with id: {id}");
                var product = await _mediator.Send(new GetProductByIdQuery(id));
                _logger.LogInformation($"Receiving GetProductsByIdQuery returned product: {product}");
                return product == null ? NotFound() : Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProducts");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery getProductsQuery)
        {
            try
            {
                _logger.LogDebug($"Sending GetProductsQuery: {getProductsQuery}");
                var response = await _mediator.Send(getProductsQuery);
                _logger.LogInformation($"GetProductsQuery returned response: {response}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetProducts");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductCommand createProductCommand)
        {
            try
            {
                _logger.LogDebug($"Sending CreateProductCommand: {createProductCommand}");
                var response = await _mediator.Send(createProductCommand);
                _logger.LogInformation($"CreateProductCommand returned : {response}");
                return response.Exists ? Conflict("Product already exists") :
                    Created($"/api/v{ApiVersion.Default}/Products/{response.Product}", response.Product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddProduct");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductCommand updateProductCommand)
        {
            try
            {
                if (id != updateProductCommand.Id)
                {
                    return ValidationProblem("Mismatch between id in uri and request body");
                }
                _logger.LogDebug($"Sending UpdateProductCommand with id: {id}");
                var result = await _mediator.Send(updateProductCommand);
                _logger.LogInformation("UpdateProductCommand returned result: {result}", result);
                return result == -1 ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateProducts");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                _logger.LogDebug($"Sending DeleteProduct called with id: {id}");
                var result = await _mediator.Send(new DeleteProductCommand(id));

                if (result == -1)
                {
                    return NotFound();
                }
                else if (result == -2)
                {
                    return Conflict("Product is in use");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteProduct");
                return BadRequest(ex.Message);
            }
        }
    }
}
