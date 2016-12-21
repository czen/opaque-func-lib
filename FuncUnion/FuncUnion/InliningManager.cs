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

        Dictionary<string, ExpressionSyntax> funcNodes = new Dictionary<string, ExpressionSyntax>();
        const double eps = 0.00001;


        Random rnd = new Random();
        // Max value when generating
        public int MaxGeneratorValue  = 100;
        // Min value when generating
        public int MinGeneratorValue = -100;

        public void SetRandomSeed(int seed)
        {
            rnd = new Random(seed);
        }

        public void ResetSeed()
        {
            rnd = new Random();
        }

        public void Compose()
        {
            AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);
			container.SatisfyImportsOnce(this);
        }
        
        public string InlineOpaqueFunctions(string program, SourceCodeKind kind = SourceCodeKind.Script)
        {
            return this.Visit(CSharpSyntaxTree.ParseText(program, new CSharpParseOptions(LanguageVersion.CSharp6, DocumentationMode.Parse, kind)).GetRoot()).ToFullString();
        }


        IFunction FindEquivalentFunction(LiteralExpressionSyntax node)
		{

            double nodeValue = 0;
            if (!double.TryParse(node.Token.Text.Replace('.', ','), out nodeValue))
                return null;

            double metaValue = 0;
            List<IFunction> funcArr = 
                opaqueFunctions.Where(func => 
                    double.TryParse(func.Metadata.EquivalentArithmeticExpr.Replace('.', ','), out metaValue)
                && Math.Abs(metaValue - nodeValue) < eps).Select(func => func.Value).ToList();
            
			return funcArr.Count == 0 ? null : funcArr[rnd.Next(0, funcArr.Count - 1)];
        }

        IFunction FindEquivalentFunction(InvocationExpressionSyntax node, ArgumentListSyntax args)
        {
            string nodeStr = getAnalyzedFunction(node.ToString());
            List<IFunction> funcArr = opaqueFunctions
                .Where(func => getAnalyzedFunction(func.Metadata.EquivalentArithmeticExpr) == nodeStr)
                .Select(func => func.Value).ToList();

            funcArr = funcArr.Where(f => checkArgs(f, args)).ToList();


            return funcArr.Count == 0 ? null : funcArr[rnd.Next(0, funcArr.Count - 1)];
        }

        bool checkArgs(IFunction func, ArgumentListSyntax args)
        {
            List<ArgumentSyntax> argsList = args.Arguments.ToList();
            List<ArgumentDescription> inputArgs = func.Arguments.Where(a => a.IsInput).ToList();
            if (inputArgs.Count != argsList.Count)
                return false;
            double val;
            for(int i = 0; i < inputArgs.Count; ++i)
                if (inputArgs[i].MinValue == null && inputArgs[i].MaxValue == null)
                    continue;
                else if (!double.TryParse(argsList[i].ToString().Replace('.', ','), out val) ||
                    (inputArgs[i].MinValue != null && val < inputArgs[i].MinValue) ||
                    (inputArgs[i].MaxValue != null && val > inputArgs[i].MaxValue))
                    return false;
            
            return true;
        }

		string getAnalyzedFunction(string func)
		{
			return string.Concat(func.TakeWhile(c => c != '('));
		}

        private string prepareFunction(IFunction func)
        {
            return string.Format("((Func<{0}, double>)({1}))",
                string.Join(",", func.Arguments.Select(arg => arg.ArgType.ToString())),
                func.Function);
        }

        private ExpressionSyntax getFunctionNode(IFunction func)
        {
            if (!funcNodes.ContainsKey(func.Function))
                funcNodes[func.Function] = SyntaxFactory.ParseExpression(prepareFunction(func));
            return funcNodes[func.Function];
        }
        
        private string generateArgumentsString(IFunction func)
        {
            List<string> arguments = new List<string>();
            foreach (var arg in func.Arguments)
                if (!arg.IsInput)
                {
                    double min = arg.MinValue ?? MinGeneratorValue;
                    double val = min + rnd.NextDouble() * (arg.MaxValue ?? MaxGeneratorValue - min);
                    arguments.Add(Convert.ChangeType(val, arg.ArgType).ToString().Replace(',', '.'));
                }

            return string.Join(",", arguments);
        }

        private ArgumentListSyntax generateArguments(IFunction func, ArgumentListSyntax args = null)
        {
            List<ArgumentSyntax> inputArgs = args?.Arguments.ToList();
            List<ArgumentSyntax> genArguments = SyntaxFactory.ParseArgumentList(generateArgumentsString(func)).Arguments.ToList();
            List<ArgumentSyntax> result = new List<ArgumentSyntax>();
            int inputIndex = 0;
            int genIndex = 0;
            foreach (var arg in func.Arguments)
                if (arg.IsInput)
                    result.Add(inputArgs[inputIndex++]);
                else
                    result.Add(genArguments[genIndex++]);
            return SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(result));
        }
        
        private InvocationExpressionSyntax getFunctionInvocationNode(IFunction func, ArgumentListSyntax args = null)
        {
            return SyntaxFactory.InvocationExpression(getFunctionNode(func), generateArguments(func, args));
        }
        
        #region VisitMethods
        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			IFunction resFunc = FindEquivalentFunction(node, node.ArgumentList);
            
            return resFunc != null
				? getFunctionInvocationNode(resFunc, (base.VisitArgumentList(node.ArgumentList) as ArgumentListSyntax)) // base.VisitInvocationExpression(node) 
				: base.VisitInvocationExpression(node);
        }

        public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
        {
			IFunction resFunc = FindEquivalentFunction(node);
            
            return resFunc != null
                ? getFunctionInvocationNode(resFunc) // base.VisitInvocationExpression(node) 
                : base.VisitLiteralExpression(node);
        }

        public override SyntaxNode Visit(SyntaxNode node)
        {
            return base.Visit(node);
        }
        #endregion
    }

}
