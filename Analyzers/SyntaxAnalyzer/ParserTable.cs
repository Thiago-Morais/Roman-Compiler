using System.Data;

namespace MyCompiler.Analyzers
{
    public class ParserTree
    {
        public Node root;
        public ParserTree() { }
        public ParserTree(Node root) : this() => this.root = root;
        public class Node
        {
            public List<TokenType> tokens = new List<TokenType>();
            public List<Node> nodes = new List<Node>();
            public Node() { }
            public Node(List<Node> nodes) : this() => this.nodes = nodes;
            public Node(params TokenType[] tokens) : this() => this.tokens = new List<TokenType>(tokens);
        }
    }
    public class ParserTable
    {
        public DataTable Table { get; }
        public ParserTable(DataTable table) => Table = table;
        public Symbols? GetValue(Token token, Symbol symbol)
        {
            string lexeme;
            if (token.Type == TokenType.Constant)
                lexeme = TokenType.Constant.ToString();
            else
                lexeme = token.Lexeme;

            DataRow row = Table.Rows.Find(lexeme);
            DataColumn column = Table.Columns[symbol.Value];
            return row[column] as Symbols?;
        }
    }
}