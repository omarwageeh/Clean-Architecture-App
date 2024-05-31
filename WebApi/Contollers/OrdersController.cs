using Application.Commands;
using Application.Dtos;
using Application.Queries;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Contollers
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            try
            {
                var order = await _mediator.Send(new GetOrderByIdQuery(id));
                return order == null ? NotFound() : Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery getOrdersQuery)
        {
            try
            {
                var response = await _mediator.Send(getOrdersQuery);
                if(response.Orders.Any() && getOrdersQuery.Filter != null )
                {
                    return NotFound("No Orders Found");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderCommand createOrderCommand)
        {
            try
            {
                var order = await _mediator.Send(createOrderCommand);
                return Created($"/api/v{ApiVersion.Default}/Orders/{order.Id}", order);
            }
            catch (Exception ex)
            {
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
                    return ValidationProblem("Mismatch between Id in URI and request body");
                }
                var result = await _mediator.Send(updateOrderCommand);
                return result == -1 ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteOrderCommand(id));
                
                if(result == -1)
                {
                    return NotFound();
                }
                else if(result == -2)
                {
                    return Conflict("Order has already been delivered");
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
