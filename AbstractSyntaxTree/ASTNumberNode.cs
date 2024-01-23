namespace AbstractSyntaxTree {
    public class ASTNumberNode : BaseASTNode {
        private double number;
        protected override void Init() {
            NodeType = ASTNodeType.Number;
            number = 0;
        }
        public override double Evaluate() {
            return number;
        }
        public ASTNumberNode(double value)  {
            number = value;
        }
        public ASTNumberNode(){
        }
    }
}
