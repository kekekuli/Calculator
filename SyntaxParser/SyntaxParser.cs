using AbstractSyntaxTree;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Syntax {
    public class SyntaxParser {
        private static char[] legalChar = new char[] { '+', '-', '*', '/', '(', ')' };
        private static char[] legalNum = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        private static char[] specialChar = new char[] { '(', ')' };

        public static BaseASTNode Parse(string expression) {
            return new SyntaxParser(expression).GenerateAST();
        }

        private BaseASTNode _root;
        private string _expression;
        private int _index = 0;
        public SyntaxParser(string expression) {
            _expression = expression;
            _expression = _expression.Trim().Replace(" ", "");
           
            foreach (char c in _expression) {
                if (!legalChar.Contains(c) && !legalNum.Contains(c)) {
                    throw new Exception("Illegal character");
                }
            }
        }
        public BaseASTNode GenerateAST() {
            ASTNodeType operatorType;
            BaseASTNode leftNode;
            BaseASTNode rightNode;

            if (_root == null)
                _root = ReadOneNode();

            try {
                operatorType = ReadOperatorType();
            } catch (ArgumentOutOfRangeException){
                // Reach end of expression
                return _root;
            }
            leftNode = _root;

            switch (operatorType) {
                default:
                case ASTNodeType.Add:
                    rightNode = new SyntaxParser(_expression[_index..]).GenerateAST() ;
                    _index = _expression.Length;
                    _root = new ASTAdd(leftNode, rightNode);
                    break;
                case ASTNodeType.Minus:
                    rightNode = new SyntaxParser(_expression[_index..]).GenerateAST() ;
                    _index = _expression.Length;
                    _root = new ASTMinus(leftNode, rightNode);
                    break;
                case ASTNodeType.Multiply:
                    rightNode = ReadOneNode();
                    _root =  new ASTMul(leftNode, rightNode);
                    break;
                case ASTNodeType.Divide:
                    rightNode = ReadOneNode();
                    _root = new ASTDivide(leftNode, rightNode);
                    break;
            }
            // Continue to read the rest of the expression
            GenerateAST();
            return _root;
        } 
        
        public BaseASTNode ReadOneNode() {
            switch (_expression[_index]) {
                case char c when specialChar.Contains(c):
                    if (c == '(') {
                        int leftCount = 0, rightCount = 0;

                        int i;
                        for (i = 0; _index + i < _expression.Length; i++) {
                            if (_expression[_index + i] == '(')
                                leftCount++;
                            if (_expression[_index + i] == ')')
                                rightCount++;

                            if (_expression[_index + i] == ')' && leftCount == rightCount)
                                break;
                        }
                        if (_index + i >= _expression.Length)
                            throw new Exception("No match ')'");
                        var result = new SyntaxParser(_expression[(_index + 1)..(_index + i)]).GenerateAST();
                        _index = _index + i + 1;
                        return result;
                    } else if (c == ')'){
                        throw new Exception("Unexcept ')'");
                    } else {
                        throw new Exception("No support for other special char");
                    }
                case char c when legalNum.Contains(c):
                    double num = 0;
                    while(_index < _expression.Length && legalNum.Contains(_expression[_index])) {
                        num = num * 10 + (_expression[_index] - '0');
                        _index++;
                    }
                    return new ASTNumberNode(num);
                default:
                    throw new Exception("Unknow character");
            }
        }
        public ASTNodeType ReadOperatorType() {
            if (_index >= _expression.Length)
                throw new ArgumentOutOfRangeException();
            switch (_expression[_index]) {
                case '+':
                    _index++;
                    return ASTNodeType.Add;
                case '-':
                    _index++;
                    return ASTNodeType.Minus;
                case '*':
                    _index++;
                    return ASTNodeType.Multiply;
                case '/':
                    _index++;
                    return ASTNodeType.Divide;
                default:
                    throw new Exception("Unknow operator");
            }
        }
    }
}
