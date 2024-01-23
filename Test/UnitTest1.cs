using AbstractSyntaxTree;
using Syntax;

namespace Test {
    public class Tests {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void NumberNodeTest() {
            ASTNumberNode aSTNumberNode = new ASTNumberNode(1);
            Assert.That(aSTNumberNode.NodeType, Is.EqualTo(ASTNodeType.Number));
            Assert.That(aSTNumberNode.Evaluate(), Is.EqualTo(1));
        }
        [Test]
        public void EvaluateTest0() {
            ASTNumberNode aSTNumberNode = new ASTNumberNode(1);
            ASTNumberNode aSTNumberNode2 = new ASTNumberNode(2);
            ASTAdd aSTAdd = new ASTAdd(aSTNumberNode, aSTNumberNode2);
            // 1 + 2 = 3?
            Assert.That(aSTAdd.Evaluate(), Is.EqualTo(3));

            // 3 + 3 = 6?
            ASTAdd aSTAdd2 = new ASTAdd(aSTAdd, new ASTNumberNode(3));
            Assert.That(aSTAdd2.Evaluate(), Is.EqualTo(6));
            // 6 + 3 = 9?
            ASTAdd aSTAdd3 = new ASTAdd(new ASTNumberNode(3), aSTAdd2);
            Assert.That(aSTAdd3.Evaluate(), Is.EqualTo(9));
        }
        [Test]
        public void EvaluateTest1() {
            ASTNumberNode aSTNumberNode = new ASTNumberNode(1);
            ASTNumberNode aSTNumberNode2 = new ASTNumberNode(2);
            ASTMinus aSTMinus = new ASTMinus(aSTNumberNode, aSTNumberNode2);
            // 1 - 2 = -1?
            Assert.That(aSTMinus.Evaluate(), Is.EqualTo(-1));

            // -1 - 3 = -4?
            ASTMinus aSTMinus2 = new ASTMinus(aSTMinus, new ASTNumberNode(3));
            Assert.That(aSTMinus2.Evaluate(), Is.EqualTo(-4));
            // 3 - -4 = 7?
            ASTMinus aSTMinus3 = new ASTMinus(new ASTNumberNode(3), aSTMinus2);
            Assert.That(aSTMinus3.Evaluate(), Is.EqualTo(7));
        }
        [Test]
        public void EvaluateTest2() {
            ASTNumberNode aSTNumberNode = new ASTNumberNode(1);
            ASTNumberNode aSTNumberNode2 = new ASTNumberNode(2);
            ASTMul aSTMul = new ASTMul(aSTNumberNode, aSTNumberNode2);
            // 1 * 2 = 2?
            Assert.That(aSTMul.Evaluate(), Is.EqualTo(2));

            // 2 * 3 = 6?
            ASTMul aSTMul2 = new ASTMul(aSTMul, new ASTNumberNode(3));
            Assert.That(aSTMul2.Evaluate(), Is.EqualTo(6));
            // 6 * 3 = 18?
            ASTMul aSTMul3 = new ASTMul(new ASTNumberNode(3), aSTMul2);
            Assert.That(aSTMul3.Evaluate(), Is.EqualTo(18));
        }
        [Test]
        public void EvaluateTest3() {
            ASTNumberNode aSTNumberNode = new ASTNumberNode(1);
            ASTNumberNode aSTNumberNode2 = new ASTNumberNode(2);
            ASTDivide aSTDivide = new ASTDivide(aSTNumberNode, aSTNumberNode2);
            // 1 / 2 = 0.5?
            Assert.That(aSTDivide.Evaluate(), Is.EqualTo(0.5));

            // 0.5 / 3 = 0.16666666666666666?
            ASTDivide aSTDivide2 = new ASTDivide(aSTDivide, new ASTNumberNode(3));
            Assert.That(aSTDivide2.Evaluate(), Is.EqualTo(0.16666666666666666));
            // 3 / 0.16666666666666666 = 18?
            ASTDivide aSTDivide3 = new ASTDivide(new ASTNumberNode(3), aSTDivide2);
        }
        [Test]
        public void EvaluateTest4() {
            //(2 - 5 + 5 * 3) / 6 == 2?
            ASTDivide aSTDivide = new ASTDivide(
                new ASTAdd(
                    new ASTMinus(
                        new ASTNumberNode(2),
                        new ASTNumberNode(5)
                    ),
                    new ASTMul(
                        new ASTNumberNode(5),
                        new ASTNumberNode(3)
                    )
                ),
                new ASTNumberNode(6)
            );

            Assert.That(aSTDivide.Evaluate(), Is.EqualTo(2));
        }
        [Test]
        public void LittleTest() {
            SyntaxParser.Parse("1/0");

            Assert.That(SyntaxParser.Parse("1+2*(4+(1))").Evaluate(), Is.EqualTo(11));
            Assert.That(SyntaxParser.Parse("5 * 4 * 3 / 2").Evaluate(), Is.EqualTo(30));
            Assert.That(SyntaxParser.Parse("(5 + 4 / (5 + (1 - 2))) - 5 * (3 + 1)").Evaluate(), Is.EqualTo(-14));
            Assert.That(() => SyntaxParser.Parse("hahaha"), Throws.Exception);
            Assert.That(() => SyntaxParser.Parse("(5 + 4 / (5 + (1 - 2)) - 5 * (3 + 1)").Evaluate(), Throws.Exception);
        }
    }
}