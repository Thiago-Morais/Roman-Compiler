using System.Data;

namespace MyCompiler.Analyzers
{
    public class SyntaxAnalyzer : IResetable
    {
        Stack<Token> remainingTokens = new Stack<Token>();
        Stack<Symbol> symbolStack = new Stack<Symbol>();
        Symbols currentSymbols;
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
                Console.WriteLine(e);
                IsValid = false;
            }
        }
        void Parse()
        {
            while (remainingTokens.Count > 0 || symbolStack.Count > 0)
            {
                Symbol symbol = symbolStack.Pop();
                if (symbol.IsNull || symbol.IsEmpty) continue;
                if (!symbol.IsTerminal)
                {
                    currentSymbols = GetProductionFor(symbol);

                    for (int i = currentSymbols.Values.Count - 1; i >= 0; i--)
                        symbolStack.Push(currentSymbols.Values[i]);
                }
                else
                    remainingTokens.Pop();
            }
            IsDone = true;
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
    public struct Symbols
    {
        public List<Symbol> Values { get; } = new List<Symbol>();
        public SymbolType DefaultType { get; }
        public Symbols(params Symbol[] symbols) : this(defaultType: default) => Values.AddRange(symbols);
        public Symbols(SymbolType defaultType) => DefaultType = defaultType;
        public Symbols Append(params string[] names)
        {
            SymbolType type = DefaultType;
            Symbol[] symbols = Array.ConvertAll(names, n => new Symbol(type, n));
            return Append(symbols);
        }
        public Symbols Append(params Symbol[] symbols)
        {
            Values.AddRange(symbols);
            return this;
        }
        public Symbols Append(string name) => And(new Symbol(DefaultType, name));
        public Symbols And(Symbol symbol)
        {
            Values.Add(symbol);
            return this;
        }
        public static Symbols Null => new Symbols(Symbol.Null);
        public static Symbols Empty() => new Symbols(Symbol.Empty);
    }
    public struct Symbol
    {
        public SymbolType Type { get; private set; }
        public string Value { get; private set; }
        public bool IsNull => Type == SymbolType.Null;
        public bool IsEmpty => Type == SymbolType.Empty;
        public bool IsTerminal => Type == SymbolType.Terminal;
        public bool IsNonTerminal => Type == SymbolType.NonTerminal;
        public Symbol(SymbolType type, string value) : this()
        {
            Type = type;
            Value = value;
        }
        public static Symbol Null => new Symbol(SymbolType.Null, null);
        public static Symbol Empty => new Symbol(SymbolType.Empty, "");
    }

    public enum SymbolType { Null, Empty, Terminal, NonTerminal }
}