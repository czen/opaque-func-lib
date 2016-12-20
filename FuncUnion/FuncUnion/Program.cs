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
            inline.Test();

            Console.ReadKey();
        }
        
    }
}
