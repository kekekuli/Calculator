## 基于面向对象的使用AST(Abstract Syntax Tree)实现的简单计算器。

### 实现功能：支持对加减乘除二元运算符和括号的数学公式运算

AST树结构表示：
 
    例子：1 + 2 * 5
        +
       / \
      /   \
      1    *
          / \
          2  5



**AST的实现方式**：  

    BaseASTNode,抽象表示任意一个节点
    ASTNumberNode,表示一个数字节点
    ASTOperatorNode,表示一个运算符节点
    ASTAdd,运算符节点中的加号节点
    ASTMinus,减号节点
    ASTMul,乘法节点
    ASTDivide,除法节点
    
**对象关系树**

    关系树
                     BaseASTNode
                     /         \
                    /           \
                   /       ASTNumberNode
           ASTOperatorNode
                 |       
                 |   
    ASTAdd, ASTMinus, ASTMul, ASTDivide


**从字符数学公式建立AST树结构**
    
    具体实现位于SyntaxParser.GenerateAST(String)，只需要传入字符串，就能得到AST，其中包括可能的非法输入处理方法。

    采用**递归**生成树的方法。例如 1 + 2 * 5. 先读入 1，作为数字节点，再读入+作为运算符节点，剩下的字符不管是什么，再次调用SyntaxParset.GenerateAST(剩下的节点)。

    过程如下：

                    +
                  /   \
                 /     \
                1       \
                       SyntaxParser.GenerateAST(2 * 5) 
    
    再重复此过程直到结束。

**处理括号**

    主思路：括号里面的内容就是一颗待生成的AST树。
    具体做法：当读取到第一个左括号时, 找到与之匹配的右括号而暂时不管括号里内容是什么。将这对括号里面的内容(而不包括左右括号)再次调用SyntaxParser.GenerateAST(括号里的内容)，并返回得到的AST。这样即使有多层括号嵌套也能按层次处理。

**获取AST树的运算值**

    任意一颗AST树，都能调用evalute()来计算当前树的值。当前值 = 左子树的值 (+, -, *, /) 右子数的值(运算符根据当前节点的类型决定)。当根节点是一个ASTNumberNode即数字节点时，直接返回节点内保存的值

**项目管理**

    使用了Nunit进行测试
    ASTNode实现作为单独一个项目，不依赖语法解析器SyntaxParser。SyntaxParser作为用户接口也作为单独一个项目。