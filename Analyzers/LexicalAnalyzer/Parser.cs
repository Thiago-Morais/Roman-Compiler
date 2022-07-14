namespace MyCompiler.Analyzers
{
    public class TokenizerCollection : IResetable<List<Tokenizer>>
    {
        public IList<Tokenizer> Tokenizers { get; set; }
        public TokenizerCollection() { }
        public TokenizerCollection(IList<Tokenizer> tokenizers) : this() => Tokenizers = tokenizers;
        public Token ParseLexeme(string lexeme)
        {
            foreach (Tokenizer tokenizer in Tokenizers)
                if (tokenizer.TryTokenize(lexeme, out Token token))
                    return token;
            return Token.Invalid;
        }
        public void Reset() { }
        public void Reset(List<Tokenizer> tokenizers) => Tokenizers = new List<Tokenizer>(tokenizers);
    }

}