using AIO;

using System.Collections.Generic;

public partial class Utils
{
    /// <summary>
    /// 运算符工具类
    /// </summary>
    public static class Operator
    {
        static Operator()
        {
            unaryOperatorHandlers.Add(UnaryOperator.LogicalNegation, logicalNegationHandler);
            unaryOperatorHandlers.Add(UnaryOperator.NumericNegation, numericNegationHandler);
            unaryOperatorHandlers.Add(UnaryOperator.Increment, incrementHandler);
            unaryOperatorHandlers.Add(UnaryOperator.Decrement, decrementHandler);
            unaryOperatorHandlers.Add(UnaryOperator.Plus, plusHandler);

            binaryOperatorHandlers.Add(BinaryOperator.Addition, additionHandler);
            binaryOperatorHandlers.Add(BinaryOperator.Subtraction, subtractionHandler);
            binaryOperatorHandlers.Add(BinaryOperator.Multiplication, multiplicationHandler);
            binaryOperatorHandlers.Add(BinaryOperator.Division, divisionHandler);
            binaryOperatorHandlers.Add(BinaryOperator.Modulo, moduloHandler);
            binaryOperatorHandlers.Add(BinaryOperator.And, andHandler);
            binaryOperatorHandlers.Add(BinaryOperator.Or, orHandler);
            binaryOperatorHandlers.Add(BinaryOperator.ExclusiveOr, exclusiveOrHandler);
            binaryOperatorHandlers.Add(BinaryOperator.Equality, equalityHandler);
            binaryOperatorHandlers.Add(BinaryOperator.Inequality, inequalityHandler);
            binaryOperatorHandlers.Add(BinaryOperator.GreaterThan, greaterThanHandler);
            binaryOperatorHandlers.Add(BinaryOperator.LessThan, lessThanHandler);
            binaryOperatorHandlers.Add(BinaryOperator.GreaterThanOrEqual, greaterThanOrEqualHandler);
            binaryOperatorHandlers.Add(BinaryOperator.LessThanOrEqual, lessThanOrEqualHandler);
            binaryOperatorHandlers.Add(BinaryOperator.LeftShift, leftShiftHandler);
            binaryOperatorHandlers.Add(BinaryOperator.RightShift, rightShiftHandler);
        }

        // https://msdn.microsoft.com/en-us/library/2sk3x8a7(v=vs.71).aspx
        /// <summary>
        /// 运算符名称
        /// </summary>
        public static readonly Dictionary<string, string> operatorNames = new Dictionary<string, string>
            {
                { "op_Addition", "+" },
                { "op_Subtraction", "-" },
                { "op_Multiply", "*" },
                { "op_Division", "/" },
                { "op_Modulus", "%" },
                { "op_ExclusiveOr", "^" },
                { "op_BitwiseAnd", "&" },
                { "op_BitwiseOr", "|" },
                { "op_LogicalAnd", "&&" },
                { "op_LogicalOr", "||" },
                { "op_Assign", "=" },
                { "op_LeftShift", "<<" },
                { "op_RightShift", ">>" },
                { "op_Equality", "==" },
                { "op_GreaterThan", ">" },
                { "op_LessThan", "<" },
                { "op_Inequality", "!=" },
                { "op_GreaterThanOrEqual", ">=" },
                { "op_LessThanOrEqual", "<=" },
                { "op_MultiplicationAssignment", "*=" },
                { "op_SubtractionAssignment", "-=" },
                { "op_ExclusiveOrAssignment", "^=" },
                { "op_LeftShiftAssignment", "<<=" },
                { "op_ModulusAssignment", "%=" },
                { "op_AdditionAssignment", "+=" },
                { "op_BitwiseAndAssignment", "&=" },
                { "op_BitwiseOrAssignment", "|=" },
                { "op_Comma", "," },
                { "op_DivisionAssignment", "/=" },
                { "op_Decrement", "--" },
                { "op_Increment", "++" },
                { "op_UnaryNegation", "-" },
                { "op_UnaryPlus", "+" },
                { "op_OnesComplement", "~" },
            };

        /// <summary>
        /// 运算符优先级
        /// </summary>
        public static readonly Dictionary<string, int> operatorRanks = new Dictionary<string, int>
            {
                { "op_Addition", 2 },
                { "op_Subtraction", 2 },
                { "op_Multiply", 2 },
                { "op_Division", 2 },
                { "op_Modulus", 2 },
                { "op_ExclusiveOr", 2 },
                { "op_BitwiseAnd", 2 },
                { "op_BitwiseOr", 2 },
                { "op_LogicalAnd", 2 },
                { "op_LogicalOr", 2 },
                { "op_Assign", 2 },
                { "op_LeftShift", 2 },
                { "op_RightShift", 2 },
                { "op_Equality", 2 },
                { "op_GreaterThan", 2 },
                { "op_LessThan", 2 },
                { "op_Inequality", 2 },
                { "op_GreaterThanOrEqual", 2 },
                { "op_LessThanOrEqual", 2 },
                { "op_MultiplicationAssignment", 2 },
                { "op_SubtractionAssignment", 2 },
                { "op_ExclusiveOrAssignment", 2 },
                { "op_LeftShiftAssignment", 2 },
                { "op_ModulusAssignment", 2 },
                { "op_AdditionAssignment", 2 },
                { "op_BitwiseAndAssignment", 2 },
                { "op_BitwiseOrAssignment", 2 },
                { "op_Comma", 2 },
                { "op_DivisionAssignment", 2 },
                { "op_Decrement", 1 },
                { "op_Increment", 1 },
                { "op_UnaryNegation", 1 },
                { "op_UnaryPlus", 1 },
                { "op_OnesComplement", 1 },
            };


        private static readonly Dictionary<UnaryOperator, UnaryOperatorHandler> unaryOperatorHandlers
            = new Dictionary<UnaryOperator, UnaryOperatorHandler>();

        private static readonly Dictionary<BinaryOperator, BinaryOperatorHandler> binaryOperatorHandlers
            = new Dictionary<BinaryOperator, BinaryOperatorHandler>();

        private static readonly LogicalNegationHandler logicalNegationHandler
            = new LogicalNegationHandler();

        private static readonly NumericNegationHandler numericNegationHandler
            = new NumericNegationHandler();

        private static readonly IncrementHandler incrementHandler
            = new IncrementHandler();

        private static readonly DecrementHandler decrementHandler
            = new DecrementHandler();

        private static readonly PlusHandler plusHandler
            = new PlusHandler();

        private static readonly AdditionHandler additionHandler
            = new AdditionHandler();

        private static readonly SubtractionHandler subtractionHandler
            = new SubtractionHandler();

        private static readonly MultiplicationHandler multiplicationHandler
            = new MultiplicationHandler();

        private static readonly DivisionHandler divisionHandler
            = new DivisionHandler();

        private static readonly ModuloHandler moduloHandler
            = new ModuloHandler();

        private static readonly AndHandler andHandler
            = new AndHandler();

        private static readonly OrHandler orHandler
            = new OrHandler();

        private static readonly ExclusiveOrHandler exclusiveOrHandler
            = new ExclusiveOrHandler();

        private static readonly EqualityHandler equalityHandler
            = new EqualityHandler();

        private static readonly InequalityHandler inequalityHandler
            = new InequalityHandler();

        private static readonly GreaterThanHandler greaterThanHandler
            = new GreaterThanHandler();

        private static readonly LessThanHandler lessThanHandler
            = new LessThanHandler();

        private static readonly GreaterThanOrEqualHandler greaterThanOrEqualHandler
            = new GreaterThanOrEqualHandler();

        private static readonly LessThanOrEqualHandler lessThanOrEqualHandler
            = new LessThanOrEqualHandler();

        private static readonly LeftShiftHandler leftShiftHandler
            = new LeftShiftHandler();

        private static readonly RightShiftHandler rightShiftHandler
            = new RightShiftHandler();
    }
}
