using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpaqueFunctions
{
    interface IFunction
    {
        string Function { get; } 

        ArgumentDescription[] Arguments { get; }
    }


    public class ArgumentDescription
    {
        public double? MinValue = null;
        public double? MaxValue = null;
        public Type ArgType;
        public bool IsInput = false;

        public ArgumentDescription(Type argType, bool isInput = false, double? minValue = null, double? maxValue = null)
        {
            ArgType = argType;
            MinValue = minValue;
            MaxValue = maxValue;
            IsInput = isInput;
        }
    }
}
