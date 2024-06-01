using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureAssembly
    {
        public static readonly Assembly Instance = typeof(InfrastructureAssembly).Assembly;
    }
}
