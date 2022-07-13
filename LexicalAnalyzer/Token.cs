namespace MyCompiler.Analyzers.Lexical
{
    public enum TokenType { Identifier, Operator, Constant, Puntuator, Invalid }
    public struct Token
    {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public Token(TokenType type, string lexeme)
        {
            Type = type;
            Lexeme = lexeme;
        }
        public override string ToString() => $"'{Type}';'{Lexeme}'";
        public static Token Invalid => new Token(TokenType.Invalid, "");
    }
}