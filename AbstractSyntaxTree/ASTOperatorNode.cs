namespace AbstractSyntaxTree {
    // Only have support for two factor operation
    public abstract class ASTOperatorNode : BaseASTNode{
        protected BaseASTNode _leftNode;
        protected BaseASTNode _rightNode;

        protected ASTOperatorNode(BaseASTNode leftNode, BaseASTNode rightNode) {
            _leftNode = leftNode;
            _rightNode = rightNode;

            if (_leftNode == null || _rightNode == null) {
                throw new ArgumentException("Operator Node: Left or right node is null");
            }
        }
    }

    public class ASTAdd : ASTOperatorNode {
        public ASTAdd(BaseASTNode leftNode, BaseASTNode rightNode) : base(leftNode, rightNode) {}

        public override double Evaluate() {
            return _leftNode.Evaluate() + _rightNode.Evaluate();
        }

        protected override void Init() {
            NodeType = ASTNodeType.Add;
        }
    }
    public class ASTMul : ASTOperatorNode {
        public ASTMul(BaseASTNode leftNode, BaseASTNode rightNode) : base(leftNode, rightNode) {}

        public override double Evaluate() {
            return _leftNode.Evaluate() * _rightNode.Evaluate();
        }

        protected override void Init() {
            NodeType = ASTNodeType.Multiply;
        }
    }
    public class ASTDivide : ASTOperatorNode {
        public ASTDivide(BaseASTNode leftNode, BaseASTNode rightNode) : base(leftNode, rightNode) {}

        public override double Evaluate() {
            try {
                return _leftNode.Evaluate() / _rightNode.Evaluate();
            }
            catch (DivideByZeroException) {
                Console.Error.WriteLine("Divide by zero error");
                return 0;
            }
        }

        protected override void Init() {
            NodeType = ASTNodeType.Divide;
        }
    }
    public class ASTMinus : ASTOperatorNode {
        public ASTMinus(BaseASTNode leftNode, BaseASTNode rightNode) : base(leftNode, rightNode) {}

        public override double Evaluate() {
            return _leftNode.Evaluate() - _rightNode.Evaluate();
        }

        protected override void Init() {
            NodeType = ASTNodeType.Minus;
        }
    }
}
