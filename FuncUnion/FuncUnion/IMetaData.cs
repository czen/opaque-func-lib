using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncUnion
{

    public interface IMetaData
    {
        string FuncName { get; }
        string EquivalentArithmeticExpr { get; }
        
    }

    public class ArgumentDescription
    {
        public int? MinValue = null;
        public int? MaxValue = null;
        public Type ArgType;
        public bool IsInput = false;

        public ArgumentDescription(Type argType, bool isInput = false, int? minValue = null, int? maxValue = null)
        {
            ArgType = argType;
            MinValue = minValue;
            MaxValue = maxValue;
            IsInput = isInput;
        }
    }
}
