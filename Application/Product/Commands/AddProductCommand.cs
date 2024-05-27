using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitties;

namespace Application.Commands
{
    public class AddProductCommand : IRequest<Product>
    {
    }
}
