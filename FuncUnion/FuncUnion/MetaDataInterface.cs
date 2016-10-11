using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncUnion
{
    public interface MetaDataInterface
    {
        string FuncName { get; }
        int EquivalentIntConstant { get; }
    }
}
