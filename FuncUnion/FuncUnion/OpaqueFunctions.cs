using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;
using FuncUnion;

namespace OpaqueFunctions
{

    /// <summary>
    /// Реализует основное тригонометрическое тождество sin^2 (x) + cos^2 (x) = 1,  
    /// где угол X задается параметром в радианах <paramref name="angle"/>. 
    /// Результатом функции является целое число X - результат умножения левой части тождества само на себя столько раз,
    /// сколько задано параметром <paramref name="count"/>.
    /// </summary>
    /// <param name="angle">Угол в радианах</param>
    /// <param name="count">Количество требуемых перемножений</param>
    /// <returns>1</returns>
    [Export(typeof(TrigonometricIdentity))]
    [ExportMetadata("FuncName", "sin^2 + cos^2 = 1")]
    [ExportMetadata("EquivalentArithmeticExpr", "1")]
    public class TrigonometricIdentity : IFunction
    {
        public double Body(IEnumerable<double> args)
        {
            double X = 1;
            int count = (int)args.ElementAt(1);

            for (int i = 0; i < count; i++)
            {
                double angle = args.ElementAt(0);
                X *= Math.Sin(angle) * Math.Sin(angle) + Math.Cos(angle) * Math.Cos(angle);
            }
            return X;
        }
    }

    /// <summary>
    /// Реализует 86.​ f(x) = th(2x) – (2*th(x))/( 1 + th(x)*th(x))
    /// </summary>
    /// <param name="angle">Угол в радианах</param>
    /// <returns>0</returns>
    [Export(typeof(TanHibDiff))]
    [ExportMetadata("FuncName", "th(2x) – (2*th(x))/( 1 + th(x)*th(x)")]
    [ExportMetadata("EquivalentArithmeticExpr", "0")]
    public class TanHibDiff : IFunction
    {
        public double Body(IEnumerable<double> args)
        {
            double X, thX, th2X;
            double angle = args.ElementAt(0);
            thX = Math.Tanh(angle);
            th2X = Math.Tanh(2 * angle);
            X = th2X - (2 * thX) / (1 + thX * thX);
            return X;
        }
    }

    /// <summary>
    /// Реализует 87.​ f(x,y) = sh(x) + sh(y) – 2 * sh((x+y)/2) * ch((x-y)/2)
    /// </summary>
    /// <param name="angle">Угол X в радианах</param>
    /// <param name="angle2">Угол Y в радианах</param>
    /// <returns>0</returns>
    [Export(typeof(SinCosHib))]
    [ExportMetadata("FuncName", "th(2x) – (2*th(x))/( 1 + th(x)*th(x)")]
    [ExportMetadata("EquivalentArithmeticExpr", "0")]
    public static class SinCosHib : IFunction
    {
        public double Body(IEnumerable<double> args)
        {
            double X, shX, shY, shXpY, chXmY;
            double angle = args.ElementAt(0);
            double angle2 = args.ElementAt(1);

            shX = Math.Sinh(angle);
            shY = Math.Sinh(angle2);
            shXpY = Math.Sinh((angle + angle2) / 2);
            chXmY = Math.Cosh((angle - angle2) / 2);

            X = shX + shY - 2 * shXpY * chXmY;
            return X;
        }
    }
    



    
}
