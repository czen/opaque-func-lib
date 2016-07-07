﻿#define TEST_TAYLOR

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

//
namespace TestingSystem
{
    delegate double TaylorFuncTestDelegate(double x, int N, TaylorTestingEntry tst);
    delegate double TaylorFuncDelegate(double x, int N);
    delegate double TaylorFuncParamDelegate(double x, double a, int N);

    class Utility
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
        public static void SwapArr<T>(ref System.Collections.Generic.List<T> array, int ind1, int ind2)
        {
            T t = array[ind1];
            array[ind1] = array[ind2];
            array[ind2] = t;
        }
    }

    class MainClass
    {
        static string[] testingMethodsNames = new string[] { "Body", "Sin_1_in" };

        static void Main(string[] args)
        {/*
            Heap h = new Heap();
            int sgn = 1;
            double s = 0;
            for(int i=1; i<=10000000; ++i)
            {
                h.AddElem(sgn*1.0 / i);
                s += sgn*1.0 / i;
                sgn = -sgn;
            }
            Console.WriteLine(h.Sum() + " " + s);
            Console.ReadKey();
            */
            //TaylorTestingEntry t = new TaylorTestingEntry();
            //TaylorFunc f = TaylorFuncTest.Sin;
            //Console.WriteLine(t.TestFunction(f, 1000000, 1e-10, -1, 1, 20));

            var path = "C:\\git_reps\\summer_practice\\opaque-func-lib\\Muradyan\\TestingSystem\\TestingSystem\\OpaqueFunctions.cs";
            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(path));
            var rt = tree.GetRoot();
            var newrt = rt;
            var nodes = newrt.DescendantNodes().OfType<MethodDeclarationSyntax>();
            int i = -1;
            while (nodes.Count() > (++i))
            {
                var method = nodes.ElementAt(i);
                var methodName = method.Identifier.ValueText;
                Console.WriteLine("-" + methodName + "-");
                //if (!testingMethodsNames.Contains(methodName)) continue;
                //Console.WriteLine(" ==================== ");

                /* Adding last param */
                //Console.WriteLine(i.GetText());
                var parlist = method.ChildNodes().OfType<ParameterListSyntax>().First();
                //Console.WriteLine(parlist.GetType()+ " " + parlist.GetText()+ " " + parlist.ChildTokens().Count());
                var newparlist = parlist.AddParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier("tst")).WithType(SyntaxFactory.ParseTypeName("TaylorTestingEntry ")));
                //Console.WriteLine(newparlist.GetText());
                var newmethod = method.ReplaceNode(parlist, newparlist);

                /* Adding tst.AddElement(i); */
                foreach (var s in newmethod.Body.DescendantNodes())
                {
                    SyntaxTrivia st = SyntaxFactory.SyntaxTrivia(SyntaxKind.WhitespaceTrivia, " ");
                    bool fl = false;
                    bool before = true;
                    var lt = s.GetLeadingTrivia();

                    foreach (var triviaEntry in lt)
                    {
                        if (triviaEntry.Kind() == SyntaxKind.SingleLineDocumentationCommentTrivia)
                        {
                            fl = true;
                            st = triviaEntry;
                            break;
                        }
                    }

                    if (!fl)
                    {
                        lt = s.GetTrailingTrivia();
                        before = false;
                        foreach (var triviaEntry in lt)
                        {
                            if (triviaEntry.Kind() == SyntaxKind.SingleLineDocumentationCommentTrivia)
                            {
                                fl = true;
                                st = triviaEntry;
                                break;
                            }
                        }
                        if (!fl) continue;
                    }

                    //Console.WriteLine("PAMPARAM");

                    var commentContents = st.ToString();
                    char[] delim = { ' ', '\n', '\t', '\r' };
                    var ar = commentContents.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                    //Console.WriteLine(ar.Length);
                    if (ar.Length != 2 || ar[0] != "add") continue;

                    var lineToAdd = "tst.AddElement(" + ar[1] + ")";
                    //newmethod = newmethod.//ReplaceNode(s, newparlist);
                    var linelist = new List<ExpressionStatementSyntax>();
                    linelist.Add(SyntaxFactory.ExpressionStatement(SyntaxFactory.ParseExpression(lineToAdd)));

                    var childlist = s.Parent.ChildNodes();

                    Console.WriteLine("trtt");
                    foreach (var si in childlist)
                    {
                        if (s != si) continue;
                        if (before) newmethod = newmethod.InsertNodesBefore(si, linelist);
                        else newmethod = newmethod.InsertNodesAfter(si, linelist);
                        break;
                    }

                    //Console.WriteLine(s.Kind() + " " + s.ToString());
                    break;
                }


                /* Nodes changing */
                newrt = newrt.ReplaceNode(method, newmethod);
                nodes = newrt.DescendantNodes().OfType<MethodDeclarationSyntax>();
            }

            //Console.WriteLine(newrt.GetText());

            Console.WriteLine("Max epsilon: " + UniversalTesting.getMaxEpsilon(path, "Sin_1", 100000, 10));

            Console.ReadKey();
        }
    }

    class Heap
    {
        private int elnum = 0;
        private List<double> ar = new List<double>();

        public static bool less(double a, double b)
        {
            return (Math.Abs(a) < Math.Abs(b));
        }
        public static bool more(double a, double b)
        {
            return (Math.Abs(a) > Math.Abs(b));
        }
        public static bool lesseq(double a, double b)
        {
            return (Math.Abs(a) <= Math.Abs(b));
        }
        public static bool moreeq(double a, double b)
        {
            return (Math.Abs(a) >= Math.Abs(b));
        }

        private void Heapify(int index = 1)
        {
            int l = index << 1;
            int r = l + 1;

            if (index > elnum || l > elnum)
            {
                return;
            }
            else
            {
                if (r > elnum)
                {
                    if (less(ar[l - 1], ar[index - 1]))
                        Utility.SwapArr(ref ar, l - 1, index - 1);
                }
                else
                {
                    if (lesseq(ar[index - 1], ar[r - 1]) && lesseq(ar[index - 1], ar[l - 1]))
                        return;
                    if (less(ar[r - 1], ar[l - 1]))
                    {
                        Utility.SwapArr(ref ar, r - 1, index - 1);
                        Heapify(r);
                    }
                    else
                    {
                        Utility.SwapArr(ref ar, l - 1, index - 1);
                        Heapify(l);
                    }
                }
            }
        }

        private double ExtractMin()
        {
            double m = ar[0];
            ar[0] = ar[--elnum];
            ar.RemoveAt(elnum);
            Heapify();
            return m;
        }

        private void AddTwoMin()
        {
            double m = ExtractMin();
            m += ExtractMin();
            //Console.WriteLine(m);
            elnum++;
            ar.Add(m);
            int cn = elnum;
            int p = cn >> 1;
            while (p > 0 && more(ar[p - 1], ar[cn - 1]))
            {
                Utility.SwapArr<double>(ref ar, p - 1, cn - 1);
                cn = p;
                p = p >> 1;
            }
        }

        public void Clear()
        {
            ar.Clear();
            elnum = 0;
        }

        public double Sum()
        {
            for (int i = elnum >> 1; i >= 1; --i)
            {
                Heapify(i);
            }
            while (elnum > 1) AddTwoMin();
            return ar[0];
        }

        public void ChangeArray(ref List<double> a)
        {
            ar = a;
            elnum = a.Count();
        }

        public void AddElem(double el)
        {
            //Console.WriteLine(elnum);
            ar.Add(el);
            ++elnum;
        }

    }

    class UniversalTesting
    {
        public static double getMaxEpsilon(string path, string f, int N, int pointsNumber)
        {

            var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(path));
            var rt = tree.GetRoot();
            var nodes = rt.DescendantNodes().OfType<MethodDeclarationSyntax>();
            var usings = "using System; \n";
            string defsStr = usings;
            foreach (var meth in nodes)
            {
                if (meth.Identifier.ValueText == f)
                {
                    defsStr += meth.GetText().ToString();
                    /*foreach(var i in meth.AttributeLists)
                    {
                        Console.WriteLine(i.Attributes.First());
                    }*/
                    break;
                }
                //defsStr += classnode.Members..GetText().ToString();
            }

            string idealExpr = "return ";
            var attribs = rt.DescendantNodes().OfType<AttributeSyntax>();
            foreach (var attr in attribs)
            {
                Console.WriteLine(attr.GetText());
                if (attr.Name.GetText().ToString() == "FunctionName" &&
                    attr.ArgumentList.Arguments.ElementAt(0).GetFirstToken().ValueText == f)
                {
                    idealExpr += attr.ArgumentList.Arguments.ElementAt(1).GetFirstToken().ValueText;
                    break;
                }

            }

            idealExpr += ";";
            string evalStr;
            string fullStr;
            
            double res = 0;
            //TaylorFuncDelegate idealFun;
            double l = -1, r = 1;
            double h = (r - l) / (pointsNumber + 1);
            for (double arg = l + h; arg < r; arg += h)
            {
                var strarg = arg.ToString(CultureInfo.CreateSpecificCulture("en-GB"));
                evalStr = "\n" +
                    //"C" + f + "." + 
                    "return " + f + "( " + strarg + "," + N + "); ";
                fullStr = defsStr + evalStr;
                //Console.WriteLine(fullStr);
                double val = 0;
                val = CSharpScript.EvaluateAsync<double>(fullStr).Result;
                var fullIdealExpr = usings + "double x = " + strarg + ";" + idealExpr;
                //Console.WriteLine(fullIdealExpr);
                double idealVal = CSharpScript.EvaluateAsync<double>(fullIdealExpr, 
                    ScriptOptions.Default.WithImports("System.Math")).Result;
                Console.WriteLine("Val: "+ val +". Ideal val: " + idealVal);
                res = Math.Max(res, Math.Abs(val - idealVal));
            }
            return res;
        }
    }

    class TaylorTestingEntry
    {
        private Heap h = new Heap();
        //private double eps;

        public void AddElement(double el)
        {
            h.AddElem(el);
        }

        public String MatchResult(double res, double eps)
        {
            double sum = h.Sum();
            double current_eps = Math.Abs(res - sum);
            h.Clear();
            if (current_eps <= eps)
                return "OK";
            else return "Error, mistake is " + current_eps;
        }

        public string TestFunction(TaylorFuncTestDelegate f, int N, double eps, double l, double r, int pointsNumber)
        {

            Random rand = new Random();
            String res = "Testing...\n";
            for (int i = 0; i < pointsNumber; ++i)
            {
                double arg = l + rand.NextDouble() * (r - l);
                double val = f(arg, N, this);
                res += "f(" + arg + ") = " + val + " : " + MatchResult(val, eps) + "\n";

            }
            return res;
        }


    }

    /*
        class TaylorFuncTest
        {
            static public double Sin(double x, int N
    #if TEST_TAYLOR
                                    , TaylorTestingEntry tst
    #endif
                )
            {
                double s = 0.0;
                double k = x;
                x *= x;
                long t;
                int sgn = 1;
                for (int i = 1; i < N; ++i)
                {
                    s += k;
    #if TEST_TAYLOR
                    tst.AddElement(k);
    #endif
                    k *= x;
                    sgn = -sgn;
                    k *= sgn;
                    t = i << 1;
                    k /= t * (t + 1);
                }

                return s;
            }
        }
    */

    class TaylorTestingUnit
    {
        private List<TaylorTestingEntry> entries = new List<TaylorTestingEntry>();


    }
}

