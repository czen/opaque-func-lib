using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;

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
    [Export(typeof(IFunction))]
    [ExportMetadata("FuncName", "sin^2 + cos^2 = 1")]
    [ExportMetadata("EquivalentArithmeticExpr", "1")]
    public class TrigonometricIdentity : IFunction
    {
        public double Body(IEnumerable<double> args)
        {
            double X = 1;
            int count = (int)args.ElementAt(1);

            double angle = args.ElementAt(0);
            for (int i = 0; i < count; i++)
                X *= Math.Sin(angle) * Math.Sin(angle) + Math.Cos(angle) * Math.Cos(angle);
            
            return X;
        }

        public string Function { get; private set; } = @"(angle, count) => 
        {
            double X = 1;
            for (int i = 0; i < count; i++)
                X *= Math.Sin(angle) * Math.Sin(angle) + Math.Cos(angle) * Math.Cos(angle);
            return X;
        }";

        public ArgumentDescription[] Arguments { get; private set; } = 
            new ArgumentDescription[] { new ArgumentDescription(typeof(double), false),
                                        new ArgumentDescription(typeof(int), false, 1) };
        
    }


	/// <summary>
	/// Реализует тригонометрическое тождество tg(x) * cos(x) / sin(x) = 1,  
	/// где угол X задается параметром в радианах <paramref name="a"/>. 
	/// Результатом функции является целое число X - результат умножения левой части тождества само на себя столько раз,
	/// сколько задано параметром <paramref name="count"/>.
	/// </summary>
	/// <param name="a">Угол в радианах</param>
	/// <returns>1</returns>

	[Export(typeof(IFunction))]
	[ExportMetadata("FuncName", "tg(x) * cos(x) / sin(x) = 1")]
	[ExportMetadata("EquivalentArithmeticExpr", "1")]
	public class TgSinCosTo1 : IFunction
	{
		public double Body(IEnumerable<double> args)
		{
			double angle = args.ElementAt(0);

			return Math.Tan(angle) * Math.Cos(angle) / Math.Sin(angle);
		}

		public string Function { get; private set; } = @"(angle) => 
        {
			return Math.Tan(angle) * Math.Cos(angle) / Math.Sin(angle);
        }";

		public ArgumentDescription[] Arguments { get; private set; } =
			new ArgumentDescription[] { new ArgumentDescription(typeof(double), false) };
	}

	/// <summary>
	/// Реализует тригонометрическое тождество ctg(x) * sin(x) / cos(x) = 1,  
	/// где угол X задается параметром в радианах <paramref name="a"/>. 
	/// Результатом функции является целое число X - результат умножения левой части тождества само на себя столько раз,
	/// сколько задано параметром <paramref name="count"/>.
	/// </summary>
	/// <param name="a">Угол в радианах</param>
	/// <param name="count">Количество требуемых перемножений</param>
	/// <returns>1</returns>
	[Export(typeof(IFunction))]
	[ExportMetadata("FuncName", "ctg(x) * sin(x) / cos(x) = 1")]
	[ExportMetadata("EquivalentArithmeticExpr", "1")]
	public class CL01_3_1_CtgSinCos : IFunction
	{
		public double Body(IEnumerable<double> args)
		{
			double angle = args.ElementAt(0);
			double F, f1, f2, f3;
			f1 = 1 / Math.Tan(angle);
			f2 = Math.Sin(angle);
			f3 = Math.Cos(angle);
			F = f1 * f2 / f3;
			return F;
		}

		public string Function { get; private set; } = @"(angle) => 
        {
			double F, f1, f2, f3;
			f1 = 1 / Math.Tan(angle);
			f2 = Math.Sin(angle);
			f3 = Math.Cos(angle);
			F = f1 * f2 / f3;
			return F;
        }";

		public ArgumentDescription[] Arguments { get; private set; } =
			new ArgumentDescription[] { new ArgumentDescription(typeof(double), false) };
	}

	/// <summary>
	/// Реализует тригонометрическое тождество sec(x) * cos(x) = 1,  
	/// где угол X задается параметром в радианах <paramref name="a"/>. 
	/// Результатом функции является целое число X - результат умножения левой части тождества само на себя столько раз,
	/// сколько задано параметром <paramref name="count"/>.
	/// </summary>
	/// <param name="a">Угол в радианах</param>
	/// <param name="count">Количество требуемых перемножений</param>
	/// <returns>1</returns>
	[Export(typeof(IFunction))]
	[ExportMetadata("FuncName", "sec(x) * cos(x) = 1")]
	[ExportMetadata("EquivalentArithmeticExpr", "1")]
	public class CL01_4_1_SecCos : IFunction
	{
		public double Body(IEnumerable<double> args)
		{
			double angle = args.ElementAt(0);
			double F, f1, f2;
			f1 = 1 / Math.Cos(angle);
			f2 = Math.Cos(angle);
			F = f1 * f2;
			return F;
		}

		public string Function { get; private set; } = @"(angle) => 
        {
			double F, f1, f2;
			f1 = 1 / Math.Cos(angle);
			f2 = Math.Cos(angle);
			F = f1 * f2;
			return F;
        }";

		public ArgumentDescription[] Arguments { get; private set; } =
			new ArgumentDescription[] { new ArgumentDescription(typeof(double), false) };
	}

	/// <summary>
	/// Реализует тригонометрическое тождество cosec(x) * sin(x) = 1,  
	/// где угол X задается параметром в радианах <paramref name="a"/>. 
	/// Результатом функции является целое число X - результат умножения левой части тождества само на себя столько раз,
	/// сколько задано параметром <paramref name="count"/>.
	/// </summary>
	/// <param name="a">Угол в радианах</param>
	/// <param name="count">Количество требуемых перемножений</param>
	/// <returns>1</returns>
	[Export(typeof(IFunction))]
	[ExportMetadata("FuncName", "cosec(x) * sin(x) = 1")]
	[ExportMetadata("EquivalentArithmeticExpr", "1")]
	public class CL01_5_1_CosecSin : IFunction
	{
		public double Body(IEnumerable<double> args)
		{
			double angle = args.ElementAt(0);
			double F, f1, f2;
			f1 = 1 / Math.Sin(angle);
			f2 = Math.Sin(angle);
			F = f1 * f2;
			return F;
		}

		public string Function { get; private set; } = @"(angle) => 
        {
			double F, f1, f2;
			f1 = 1 / Math.Sin(angle);
			f2 = Math.Sin(angle);
			F = f1 * f2;
        }";

		public ArgumentDescription[] Arguments { get; private set; } =
			new ArgumentDescription[] { new ArgumentDescription(typeof(double), false) };
	}

    /// <summary>
    /// Реализует 86.​ f(x) = th(2x) – (2*th(x))/( 1 + th(x)*th(x))
    /// </summary>
    /// <param name="angle">Угол в радианах</param>
    /// <returns>0</returns>
    [Export(typeof(IFunction))]
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

        public string Function { get; private set; } = @"(angle) => 
        {
            double X, thX, th2X;
            thX = Math.Tanh(angle);
            th2X = Math.Tanh(2 * angle);
            X = th2X - (2 * thX) / (1 + thX * thX);
            return X;
        }";

        public ArgumentDescription[] Arguments { get; private set; } =
            new ArgumentDescription[] { new ArgumentDescription(typeof(double), false) };

    }

    /// <summary>
    /// Реализует 87.​ f(x,y) = sh(x) + sh(y) – 2 * sh((x+y)/2) * ch((x-y)/2)
    /// </summary>
    /// <param name="angle">Угол X в радианах</param>
    /// <param name="angle2">Угол Y в радианах</param>
    /// <returns>0</returns>
    [Export(typeof(IFunction))]
    [ExportMetadata("FuncName", "th(2x) – (2*th(x))/( 1 + th(x)*th(x)")]
    [ExportMetadata("EquivalentArithmeticExpr", "0")]
    public class SinCosHib : IFunction
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

        public string Function { get; private set; } = @"(angle, angle2) => 
        {
            double X, shX, shY, shXpY, chXmY;

            shX = Math.Sinh(angle);
            shY = Math.Sinh(angle2);
            shXpY = Math.Sinh((angle + angle2) / 2);
            chXmY = Math.Cosh((angle - angle2) / 2);

            X = shX + shY - 2 * shXpY * chXmY;
            return X;
        }";

        public ArgumentDescription[] Arguments { get; private set; } =
            new ArgumentDescription[] { new ArgumentDescription(typeof(double), false, -10, 10),
                                        new ArgumentDescription(typeof(double), false, -10, 10) };
    }



	/// <summary>
	/// Реализует нахождение логарифмической функции f(x)=ln(1+x),  
	/// где число x задается параметром, удовлетворяющим области определения <paramref name="x"/>. 
	/// Результатом функции является число F , вычисляющее логарифм, используя ряд тейлора,
	/// количество итераций задано параметром <paramref name="count"/>.
	///  Обратной к этой функции является функция E^(f(x))=(1+x).
	/// На самом деле область определения (-1, 1),так как в указаном условии (-1, 2) промежутке
	/// после х=1 погрешность резко возрастает.
    /// ПРИМЕЧАНИЕ:
    /// Вместо исходная функции ln(1+x) реализует ln(x), отнимая 1 от x перед началом вычисления. 
    /// Изменение было сделано в целях упрощения поиска этой функции в процессе инлайнинга.
	/// </summary>
	/// <param name="x">число, удовлетворяющее области определения</param>
	/// <param name="count">Количество требуемых итераций</param>

	[Export(typeof(IFunction))]
	[ExportMetadata("FuncName", "ln(x)")]
	[ExportMetadata("EquivalentArithmeticExpr", "Math.Log(x)")]
	public class СMath_9_2_ln : IFunction
	{
		public double Body(IEnumerable<double> args)
		{
			double x = args.ElementAt(0) - 1;
			double count = args.ElementAt(1);
            double F = 0, X = x * x;

            for (int i = 1; i < count; i++)
            {
                F = X / (i * (i + 1)) + F;
                X = -X * x;
            }
            F = (x + F) / (x + 1);
            return F;
        }

		public string Function { get; private set; } = @"(x, count) => 
        {
            x -= 1;
			double F = 0, X = x * x;

            for (int i = 1; i < count; i++)
            {
                F = X / (i * (i + 1)) + F;
                X = -X * x;
            }
            F = (x + F) / (x + 1);
            return F;
        }";

		public ArgumentDescription[] Arguments { get; private set; } =
			new ArgumentDescription[] { new ArgumentDescription(typeof(double), true, 0, 2),
										new ArgumentDescription(typeof(int), false, 200) };
	}

	/// <summary>
	/// Реализует сложную функцию  f(x) = tg(arctg(x)), -π/2≤ x ≤ π/2,  
	/// где угол X задается параметром в радианах <paramref name="angle"/>. 
	/// Результатом функции является целое число X.
	/// </summary>
	/// <param name="angle">Угол в радианах</param>
	/// <returns>1</returns>

	[Export(typeof(IFunction))]
	[ExportMetadata("FuncName", "tg(arctg(x))")]
	[ExportMetadata("EquivalentArithmeticExpr", "x")]
	public class CLOX_3_1_tg_arctg : IFunction
	{
		public double Body(IEnumerable<double> args)
		{
			double angle = args.ElementAt(0);
			double F = 1;
			double Y1;
			Y1 = Math.Atan(angle);
			F = Math.Tan(Y1);
			return F;
		}

		public string Function { get; private set; } = @"(angle) => 
        {
			double F = 1;
			double Y1;
			Y1 = Math.Atan(angle);
			F = Math.Tan(Y1);
			return F;
        }";

		public ArgumentDescription[] Arguments { get; private set; } =
			new ArgumentDescription[] { new ArgumentDescription(typeof(double), true, -Math.PI * 0.5, Math.PI * 0.5) };
	}

    /// <summary>
    /// Реализует 107.​ f(x) = a^x – e^(x*ln(a))
    /// </summary>
    /// <param name="arg">Аргумент X</param>
    /// <param name="param">Необязательный параметр А</param>
    /// <returns>0</returns>
    /// 
	[Export(typeof(IFunction))]
    [ExportMetadata("FuncName", "f(x) = a^x – e^(x*ln(a))")]
    [ExportMetadata("EquivalentArithmeticExpr", "0")]
    public class CL00_107_2_pow_exp : IFunction
    {
        public static double L00_107_2_pow_exp(double arg, double param = 10.0)
        {
            double X, aX, ln, exp;
            ln = Math.Log(param);
            exp = Math.Exp(arg * ln);
            aX = Math.Pow(param, arg);

            X = aX - exp;
            return X;
        }

        public string Function { get; private set; } = @"(arg, param)=>
        {
            double X, aX, ln, exp;
            ln = Math.Log(param);
            exp = Math.Exp(arg * ln);
            aX = Math.Pow(param, arg);

            X = aX - exp;
            return X;
        }";

        public ArgumentDescription[] Arguments { get; private set; } =
            new ArgumentDescription[] { new ArgumentDescription(typeof(double), false),
                                        new ArgumentDescription(typeof(double), false, 0, 1)};

    }
}
