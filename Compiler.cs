using System.Text;
using MyCompiler.Analyzers;

namespace MyCompiler
{
    public class Compiler : IResetable
    {
        LexicalAnalyzer lexical;
        SyntaxAnalyzer syntax;
        public bool IsDone { get; protected set; }
        public bool Success { get; protected set; }
        public Compiler(List<Tokenizer> tokenizers, Symbol firstSymbol, Symbol endSymbol, ParserTable table)
        {
            lexical = new LexicalAnalyzer(tokenizers);
            syntax = new SyntaxAnalyzer(firstSymbol, endSymbol, table);
        }
        public void Compile(string expression)
        {
            Reset();
            lexical.Analyze(expression);
            foreach (Token token in lexical.Tokens)
                Console.WriteLine(token);
            SaveTokens();
            syntax.TryParse(lexical.Tokens);
            Success = syntax.IsValid;
            IsDone = true;
        }
        public void Reset()
        {
            lexical.Reset();
            syntax.Reset();
            IsDone = false;
            Success = false;
        }
        void SaveTokens()
        {
            if (!lexical.IsDone || lexical.Tokens.Count <= 0) return;

            string tokenJson = TokensCollection.Serialize(lexical.Tokens);
            string TokensPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Tokens.json");
            if (!Directory.Exists(Path.GetDirectoryName(TokensPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(TokensPath));
            File.WriteAllText(TokensPath, tokenJson, Encoding.UTF8);
        }
    }
}