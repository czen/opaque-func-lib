using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Collections;

namespace FuncUnion
{
    class Program
    {
        [ImportMany(typeof(IFunction))]
        IEnumerable<Lazy<IFunction, IMetaData>> opaqueFunctions;
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        IEnumerable<string> zipToString<T1,T2>(IEnumerable<T1> a, IEnumerable<T2> b, string del = ", ")
        {
            return a.Zip(b, (a1, b1) => string.Format("{0}{1}{2}", a1, del, b1));
        }

        IEnumerable<string> zipToString3<T1, T2, T3>(IEnumerable<T1> a, IEnumerable<T2> b, IEnumerable<T3> c, string del = ", ")
        {
            return zipToString(zipToString(a, b, del), c, del);
        }
        IEnumerable<string> zipToString4<T1, T2, T3, T4>(IEnumerable<T1> a, IEnumerable<T2> b, IEnumerable<T3> c, IEnumerable<T4> d, string del = ", ")
        {
            return zipToString(zipToString3(a, b, c, del), d, del);
        }
        IEnumerable<string> zipToString(string del = ", ", params IEnumerable<object>[] l)
        {
            return l.Skip(1).Aggregate(l[0].Select(p => p.ToString()), (p, n) => zipToString(p, n, del));
        }
        
        void Run()
        {
            Compose();
            Console.WriteLine("Imports satisfied");
            Console.WriteLine(opaqueFunctions.Count());
            foreach (Lazy<IFunction, IMetaData> p in opaqueFunctions)
                Console.WriteLine(string.Format("Function: {0}, Equivalent expression: {1}, Args: {2} ",
                    p.Metadata.FuncName, p.Metadata.EquivalentArithmeticExpr,
                    string.Join("; ", zipToString4(p.Metadata.ArgType, p.Metadata.ArgIsInput, p.Metadata.ArgMinValue, p.Metadata.ArgMaxValue, "; "))));
            
            Console.ReadKey();
        }

        private void Compose()
        {
            AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
            //Console.WriteLine(container.GetExports<IFunction, IMetaData>().Count());
        }
    }
}
