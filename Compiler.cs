using System.Text;
using System.Text.Json;
using MyCompiler.Analyzers.Lexical;

namespace MyCompiler
{
    public class Compiler
    {
        LexicalAnalyzer lexical;
        public Compiler(List<Tokenizer> tokenizers) => lexical = new LexicalAnalyzer(tokenizers);
        public void Compile(string expression)
        {
            lexical.Analyze(expression);
            foreach (Token token in lexical.Tokens)
                Console.WriteLine(token);
            SaveTokens();
        }
        private void SaveTokens()
        {
            // if (!lexical.IsDone || lexical.Tokens.Count <= 0) return;

            string tokenJson = TokensCollection.Serialize(lexical.Tokens);
            string TokensPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Tokens.json");
            if (!Directory.Exists(Path.GetDirectoryName(TokensPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(TokensPath));
            File.WriteAllText(TokensPath, tokenJson, Encoding.UTF8);
        }
    }
    struct ParserTable
    {
        string lexeme;
        Token token;
    }
    struct SymbolTable
    {
        Dictionary<string, SymbolEntry> entries;
    }
    struct SymbolEntry
    {
        public string name;
        public Types type;
        public int size;
        public int dimention;
        public int lineOfDeclaration;
        public int address;
    }
    enum Types { Int, Float, String, Char }
}