using System.ComponentModel;
using System.Text;

namespace MyCompiler.Analyzers
{
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
        public override string ToString() => string.Join("//", Values);
    }
}