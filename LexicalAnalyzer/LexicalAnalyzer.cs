using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyCompiler.Analyzers.Lexical
{
    public class LexicalAnalyzer
    {
        public List<Tokenizer> Tokenizers { get; set; }
        public TokensCollection Tokens { get; protected set; } = new TokensCollection();
        public bool IsDone { get; internal set; }

        Scanner scanner = new Scanner();
        Parser parser = new Parser();
        public LexicalAnalyzer(List<Tokenizer> tokenizers)
        {
            Tokenizers = tokenizers;
            parser.Reset(Tokenizers);
        }
        public void Analyze(string expression)
        {
            Reset();
            scanner.Reset(expression);
            while (!scanner.IsDone)
            {
                scanner.ScanNext();
                if (!scanner.Failed)
                {
                    Token token = parser.ParseLexeme(scanner.CurrentLexeme);
                    // TODO adicinar verificação de espaços
                    if (!token.Equals(Token.Invalid))
                        Tokens.Add(token);
                }
                else
                {
                    Console.WriteLine($"Failed to scan remaining ={scanner.RemainingExpression}");
                    break;
                }
            }
        }
        void Reset() => Tokens.Clear();
    }
}