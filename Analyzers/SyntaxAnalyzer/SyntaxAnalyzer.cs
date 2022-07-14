using System.Data;

namespace MyCompiler.Analyzers
{
    public class SyntaxAnalyzer : IResetable
    {
        Stack<Token> remainingTokens = new Stack<Token>();
        Stack<Symbol> symbolStack = new Stack<Symbol>();
        Symbol currentSymbol;
        Symbols currentProduction;
        public Symbol EndSymbol { get; }
        public Symbol FirstSymbol { get; private set; }
        public ParserTable Parser { get; }
        public bool IsValid { get; set; }
        public bool IsDone { get; protected set; }
        Token CurrentToken => remainingTokens.Peek();

        public SyntaxAnalyzer(Symbol firstSymbol, Symbol endSymbol, ParserTable parser)
        {
            FirstSymbol = firstSymbol;
            EndSymbol = endSymbol;
            Parser = parser;
        }
        public SyntaxAnalyzer(Symbol firstSymbol, Symbol endSymbol, DataTable dataTable) : this(firstSymbol, endSymbol, new ParserTable(dataTable)) { }
        public void TryParse(TokensCollection tokens)
        {
            tokens.Add(new Token(TokenType.Puntuator, EndSymbol.Value));
            remainingTokens = new Stack<Token>(tokens.Reverse());
            Reset();
            try
            {
                Parse();
                IsValid = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}\nRemaining tokens:{string.Join(" ; ", remainingTokens)}");
                IsValid = false;
            }
            finally
            {
                IsDone = true;
            }
        }
        void Parse()
        {
            while (remainingTokens.Count > 0 || symbolStack.Count > 0)
            {
                if (symbolStack.Peek().IsNull)
                    throw new Exception($"Invalid symbol {currentSymbol}");
                currentSymbol = symbolStack.Pop();
                if (currentSymbol.IsEmpty)
                    continue;
                if (!currentSymbol.IsTerminal)
                {
                    currentProduction = GetProductionFor(currentSymbol);

                    for (int i = currentProduction.Values.Count - 1; i >= 0; i--)
                        symbolStack.Push(currentProduction.Values[i]);
                }
                else
                    remainingTokens.Pop();
            }
        }
        Symbols GetProductionFor(Symbol symbol)
        {
            Symbols? symbols = Parser.GetValue(CurrentToken, symbol);
            if (symbols != null) return (Symbols)symbols;
            // FIXME expecificar exception
            throw new Exception("No production found for this.");
        }
        public void Reset()
        {
            symbolStack.Clear();
            symbolStack.Push(EndSymbol);
            symbolStack.Push(FirstSymbol);
            IsDone = false;
        }
    }

    public enum SymbolType { Null, Empty, Terminal, NonTerminal }
}