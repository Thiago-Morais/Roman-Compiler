namespace MyCompiler.Analyzers
{
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
        public override string ToString() => $"{Value}:{Type}";
    }
}