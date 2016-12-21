using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections;

namespace OpaqueFunctions
{
    class Program
    {
        InliningManager inline;
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }
                
        void Run()
        {
            inline = new InliningManager();
			inline.Compose();
            Test();

            Console.ReadKey();
        }
        public void Test()
        {
            Console.WriteLine("Result: ");
            //Console.WriteLine(inline.InlineOpaqueFunctions(@"1.0 + Math.Log(0.1) + Math.Cos(0)"));
            Console.WriteLine(inline.InlineOpaqueFunctions(@"0"));

            double res1 = 1.0 + Math.Log(1) + Math.Cos(0);

            double res2 = ((Func<System.Double, double>)((angle) =>
            {
                return Math.Tan(angle) * Math.Cos(angle) / Math.Sin(angle);
            }))(74.0233480809365) + ((Func<System.Double, System.Int32, double>)((x, count) =>
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
            }))(((Func<System.Double, double>)((angle) =>
            {
                double F, f1, f2;
                f1 = 1 / Math.Cos(angle);
                f2 = Math.Cos(angle);
                F = f1 * f2;
                return F;
            }))(60.5058942737551), 174) + Math.Cos(((Func<System.Double, double>)((angle) =>
            {
                double X, thX, th2X;
                thX = Math.Tanh(angle);
                th2X = Math.Tanh(2 * angle);
                X = th2X - (2 * thX) / (1 + thX * thX);
                return X;
            }))(-71.5263737233013));


            Console.WriteLine(string.Format("Initial result: {0}, new result: {1}", res1, res2));
            
        }

    }
}
