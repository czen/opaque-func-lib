using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncUnion
{
    interface IFunction
    {
        double Body(IEnumerable<double> args);
    }
}
