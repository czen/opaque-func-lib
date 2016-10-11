using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncUnion
{
    public abstract class ArithmeticExprNode
    {
        public bool IsOpaque = false;
    }

    public abstract class BinaryFunctionNode : ArithmeticExprNode
    {
        public ArithmeticExprNode Left, Right;

        public BinaryFunctionNode(ArithmeticExprNode left = null, ArithmeticExprNode right = null, bool isOpaque = false)
        {
            Left = left;
            Right = right;
            IsOpaque = isOpaque;
        }
    }

    public class SumNode : BinaryFunctionNode
    {
        public SumNode(ArithmeticExprNode left = null, ArithmeticExprNode right = null, bool isOpaque = false)
            : base(left, right, isOpaque)
        { }
        
    }

    public class SubtractNode : BinaryFunctionNode
    {
        public SubtractNode(ArithmeticExprNode left = null, ArithmeticExprNode right = null, bool isOpaque = false)
            : base(left, right, isOpaque)
        { }

    }

    public class MulNode : BinaryFunctionNode
    {
        public MulNode(ArithmeticExprNode left = null, ArithmeticExprNode right = null, bool isOpaque = false)
            : base(left, right, isOpaque)
        { }

    }

    public class DivNode : BinaryFunctionNode
    {
        public DivNode(ArithmeticExprNode left = null, ArithmeticExprNode right = null, bool isOpaque = false)
            : base(left, right, isOpaque)
        { }

    }

    public abstract class UnaryFunctionNode : ArithmeticExprNode
    {
        public ArithmeticExprNode Argument;

        public UnaryFunctionNode(ArithmeticExprNode argument = null, bool isOpaque = false)
        {
            Argument = argument;
            IsOpaque = isOpaque;
        }
    }

    public class SinNode : UnaryFunctionNode
    {
        public SinNode(ArithmeticExprNode argument = null, bool isOpaque = false)
            : base(argument, isOpaque)
        { }
    }

    public class CosNode : UnaryFunctionNode
    {
        public CosNode(ArithmeticExprNode argument = null, bool isOpaque = false)
            : base(argument, isOpaque)
        { }
    }

    public class TanNode : UnaryFunctionNode
    {
        public TanNode(ArithmeticExprNode argument = null, bool isOpaque = false)
            : base(argument, isOpaque)
        { }
    }

    public class ConstantNode<T> : ArithmeticExprNode
    {
        public T Value;

        public ConstantNode(T value = default(T), bool isOpaque = false)
        {
            Value = value;
            IsOpaque = isOpaque;
        }
    }

    public class VariableNode : ArithmeticExprNode
    {
        public string Name;

        public VariableNode(string name = "", bool isOpaque = false)
        {
            Name = name;
            IsOpaque = isOpaque;
        }
    }
}
