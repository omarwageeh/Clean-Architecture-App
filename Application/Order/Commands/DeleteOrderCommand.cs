using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteOrderCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public DeleteOrderCommand(Guid id) => Id = id;
    }
}
