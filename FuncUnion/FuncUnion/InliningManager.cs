using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.CodeAnalysis;
using OpaqueFunctions;

namespace OpaqueFunctions
{
    public class InliningManager : CSharpSyntaxRewriter
    {
        [ImportMany(typeof(IFunction))]
        IEnumerable<Lazy<IFunction, IMetaData>> opaqueFunctions;
        CSharpParseOptions parseOptions = new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.Parse, SourceCodeKind.Script);

        // Random argument generation //
        // Random generator
        // TODO: Enable custom seeds
        Random rnd = new Random();
        // Max value when generating
        // TODO: Make it a property, enable to change
        int maxValue = 100;
        // Min value when generating
        // TODO: Make it a property, enable to change
        int minValue = -100;
		////////////////////////////////

        public void Compose()
        {
            AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);
			container.SatisfyImportsOnce(this);
        }

        IFunction FindEquivalentFunction(LiteralExpressionSyntax node)
		{
			const double eps = 0.00001;
			ArrayList funcArr = new ArrayList();

			foreach (var func in opaqueFunctions)
			{
				double nodeValue = 0;
				double metaValue = 0;
				double.TryParse(node.Token.Text.Replace('.', ','), out nodeValue);
				double.TryParse(func.Metadata.EquivalentArithmeticExpr.Replace('.', ','), out metaValue);

				if (Math.Abs(metaValue - nodeValue) < eps)
					funcArr.Add(func.Value);
			}

			if (funcArr.Count == 0)
				return null;

			Random rand = new Random();
			int funcIndex = rand.Next(0, funcArr.Count);

			return (IFunction)funcArr[funcIndex];
        }

		IFunction FindEquivalentFunction(InvocationExpressionSyntax node)
		{
			ArrayList funcArr = new ArrayList();
			foreach (var func in opaqueFunctions)
			{
				// Here we should do something with argument
				string funcStr = getAnalizedFunction(func.Metadata.EquivalentArithmeticExpr);
				string nodeStr = getAnalizedFunction(node.ToString());

				if (funcStr == nodeStr)
				{
					funcArr.Add(func.Value);
				}
			}

			if (funcArr.Count == 0)
				return null;

			Random rand = new Random();
			int funcIndex = rand.Next(0, funcArr.Count);

			return (IFunction)funcArr[funcIndex];
		}

		// Simplest realization, we suggest that argument is only single number or variable
		string getAnalizedFunction(string func)
		{
			if (func == "1" || func == "0") return null;
			int cutFirstIndex = -1;
			int cutLastIndex = -1;

			for (var i = 0; i < func.Length; ++i)
			{
				if (func[i] == '(')
				{
					cutFirstIndex = i;
				}

				if (func[i] == ')')
				{
					cutLastIndex = i;
				}
			}

			return func.Substring(0, cutFirstIndex + 1) + func.Substring(cutLastIndex, 1);
		}

        public void Test()
        {
			SyntaxTree n = CSharpSyntaxTree.ParseText(@"1.0 + Math.Log(1) + Math.Cos(1)", parseOptions);
            SyntaxNode result = this.Visit(n.GetRoot());

            Console.WriteLine("Result: ");
            Console.WriteLine(result.ToString());

            double res1 = 1.0 + Math.Cos(2);

            double res2 = (((Func<System.Double, System.Int32, double>)((angle, count) =>
             {
                 double X = 1;
                 for (int i = 0; i < count; i++)
                     X *= Math.Sin(angle) * Math.Sin(angle) + Math.Cos(angle) * Math.Cos(angle);
                 return X;
             }))(56.2745029834446, 55)) + Math.Cos(2);

            Console.WriteLine(string.Format("Initial result: {0}, new result: {1}", res1, res2));
        }

        private string prepareFunction(IFunction func, params string[] args)
        {
            List<string> arguments = new List<string>();
            List<string> types = new List<string>();
            int usedArg = 0;
            foreach (var arg in func.Arguments)
            {
                types.Add(arg.ArgType.ToString());
				if (arg.IsInput)
				{
					arguments.Add(args[usedArg++]);
				}
                else
                {
                    int min = arg.MinValue ?? minValue;
                    double val = min + rnd.NextDouble() * (arg.MaxValue ?? maxValue - min);
                    arguments.Add(Convert.ChangeType(val, arg.ArgType).ToString().Replace(',','.'));
                }
            }

            return string.Format("(((Func<{0}, double>)({1}))({2}))", 
                string.Join(",", types), 
                func.Function, 
                string.Join(",", arguments));
        }

        private SyntaxNode getFunctionNode(IFunction func, params string[] args)
        {
            return CSharpSyntaxTree.ParseText(prepareFunction(func, args), parseOptions).GetRoot().DescendantNodes(s => !(s is ExpressionSyntax)).LastOrDefault();
        }

        public SyntaxNode ReplaceWithOpaque(SyntaxNode node)
        {
            return this.Visit(node);
        }
		/*
        public override SyntaxNode VisitBaseExpression(BaseExpressionSyntax node)
        {
            Console.WriteLine("Base expression");
            Console.WriteLine(node.ToString());
            return base.VisitBaseExpression(node);
        }
        
        public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
        {
            Console.WriteLine("Binary expression");
            Console.WriteLine(node.ToString());
            return base.VisitBinaryExpression(node);
        }

        public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            Console.WriteLine("Expression");
            Console.WriteLine(node.ToString());
            return base.VisitExpressionStatement(node);
        }*/

		public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			//Console.WriteLine("Invocation");
			//Console.WriteLine(node.ToString());
			//string methodName = (node.Expression as MemberAccessExpressionSyntax)?.ToString();

			string argument = "" + node.ArgumentList.ToString().ElementAt(1);
			string[] resArr = { argument };
			IFunction resFunc = FindEquivalentFunction(node);

			return resFunc != null
				? getFunctionNode(resFunc, resArr) // base.VisitInvocationExpression(node) 
				: node; //base.VisitInvocationExpression(node);
        }

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
			IFunction resFunc = FindEquivalentFunction(node);
			return resFunc != null ? getFunctionNode(resFunc) : (SyntaxNode)node;
        }
               

        public override SyntaxNode Visit(SyntaxNode node)
        {
            return base.Visit(node);
        }
    }
    
}
