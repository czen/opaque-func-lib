using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpaqueFunctions
{

    public interface IMetaData
    {
        string FuncName { get; }
        string EquivalentArithmeticExpr { get; }
    }

}
