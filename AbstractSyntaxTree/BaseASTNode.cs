namespace AbstractSyntaxTree {
    public abstract class BaseASTNode {
        // Init() function is auto be called
        protected abstract void Init();
        public ASTNodeType NodeType { get; protected set; }
        public abstract double Evaluate();

        public BaseASTNode() {
            Init();
        }
        public override string ToString() {
            return Evaluate().ToString();
        }


    }
}
