using Application.Commands;
using Application.Queries;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Contollers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
        {
            _mediator = mediator;
            _logger = logger; 
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            try
            {
                _logger.LogDebug($"Sending GetOrderByIdQuery with Id: {id}");
                var order = await _mediator.Send(new GetOrderByIdQuery(id));

                return order == null ? NotFound() : Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOrder action");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery getOrdersQuery)
        {
            try
            {
                _logger.LogDebug($"Sending GetOrdersQuery: {getOrdersQuery}");

                var response = await _mediator.Send(getOrdersQuery);
                _logger.LogInformation($"Recived GetOrdersQuery with {response.Orders?.Count()} Orders");

                if (response.Orders!.Any() && getOrdersQuery.Filter != null)
                {
                    return NotFound("No Orders Found");
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetOrders action");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderCommand createOrderCommand)
        {
            try
            {
                _logger.LogInformation($"Sending createOrderCommand: {createOrderCommand}");

                var order = await _mediator.Send(createOrderCommand);

                _logger.LogDebug($"Recived createOrderCommand with orderId: {order.Id}");

                return Created($"/api/v{ApiVersion.Default}/Orders/{order.Id}", order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddOrder action"); // Logging error
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderCommand updateOrderCommand)
        {
            try
            {
                if (id != updateOrderCommand.Id)
                {
                    _logger.LogInformation("Failed Validation, mismatch between Id  in URI {id} and request body {updateOrderCommand}", id, updateOrderCommand);
                    return ValidationProblem("Mismatch between Id in URI and request body");
                }
                _logger.LogInformation("Sending UpdateOrderCommand with Id {id}", id);
                
                var result = await _mediator.Send(updateOrderCommand);
                
                _logger.LogInformation("Recived UpdateOrder with result {result}", result);
                return result == -1 ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in UpdateOrder action");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                _logger.LogInformation("Sending DeleteOrderCommand with Id {id}", id);

                var result = await _mediator.Send(new DeleteOrderCommand(id));
               

                if (result == -1)
                {
                    _logger.LogInformation("Order with Id {id} not found", id);
                    return NotFound();
                }
                else if (result == -2)
                {
                    _logger.LogInformation("Order with Id {id} has already been delivered", id);
                    return Conflict("Order has already been delivered");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteOrder action");
                return BadRequest(ex.Message);
            }
        }
    }
    
}
