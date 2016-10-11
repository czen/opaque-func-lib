using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;
using FuncUnion;

namespace OpaqueFunctions
{
    [Export(typeof(Opaque1SinCos))]
    [ExportMetadata("FuncName", "sin^2 + cos^2 = 1")]
    [ExportMetadata("EquivalentArithmeticExpr", "1")]
    public class Opaque1SinCos : IFunction
    {
        public double Body(double angle, int count)
        {
            double X = 1;
            for (int i = 0; i < count; i++)
            {
                X *= Math.Sin(angle) * Math.Sin(angle) + Math.Cos(angle) * Math.Cos(angle);
            }
            return X;
        }
    }
    
}
