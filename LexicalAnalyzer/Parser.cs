namespace MyCompiler.Analyzers.Lexical
{
    public class Parser
    {
        public IList<Tokenizer> Tokenizers { get; set; }
        public Parser() { }
        public Parser(IList<Tokenizer> tokenizers) : this() => Tokenizers = tokenizers;
        public Token ParseLexeme(string lexeme)
        {
            foreach (Tokenizer tokenizer in Tokenizers)
                if (tokenizer.TryTokenize(lexeme, out Token token))
                    return token;
            return Token.Invalid;
        }
        public void Reset(List<Tokenizer> tokenizers)
        {
            Tokenizers = new List<Tokenizer>(tokenizers);
        }
    }

}