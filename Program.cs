using System.Data;
using MyCompiler;
using MyCompiler.Analyzers;


Console.WriteLine("Enter roman expression:");
string expression = "C-LXXXVIII*(VIII+IX/(III+I))=";

DataTable table = new DataTable();

DataColumn[] columns = new DataColumn[]
{
    new DataColumn("[terminals]", typeof(string)),
    new DataColumn("S", typeof(Symbols)),
    new DataColumn("X", typeof(Symbols)),
    new DataColumn("E", typeof(Symbols)),
    new DataColumn("O", typeof(Symbols)),
    new DataColumn("T", typeof(Symbols)),
};
table.Columns.AddRange(columns);

var S = new Symbol(SymbolType.NonTerminal, "S");
var X = new Symbol(SymbolType.NonTerminal, "X");
var E = new Symbol(SymbolType.NonTerminal, "E");
var O = new Symbol(SymbolType.NonTerminal, "O");
var T = new Symbol(SymbolType.NonTerminal, "T");
var end = new Symbol(SymbolType.Terminal, "$");
var equal = new Symbol(SymbolType.Terminal, "=");
var plus = new Symbol(SymbolType.Terminal, "+");
var minus = new Symbol(SymbolType.Terminal, "-");
var multiply = new Symbol(SymbolType.Terminal, "*");
var divide = new Symbol(SymbolType.Terminal, "/");
var num = new Symbol(SymbolType.Terminal, "num");
var openBracket = new Symbol(SymbolType.Terminal, "(");
var closeBracket = new Symbol(SymbolType.Terminal, ")");
object[] rows = new object[]
{
    // "[terminals]",                   S,                      X,                      E,                  O,                              T },
    new object[]{"$",                   Symbols.Null,           Symbols.Null,           Symbols.Null,       Symbols.Null,                   Symbols.Null},
    new object[]{"=",                   Symbols.Null,           Symbols.Null,           Symbols.Null,       Symbols.Empty(),                Symbols.Null},
    new object[]{"+",                   Symbols.Null,           Symbols.Null,           Symbols.Null,       new Symbols(plus, T, O),        Symbols.Null},
    new object[]{"-",                   Symbols.Null,           Symbols.Null,           Symbols.Null,       new Symbols(minus, T, O),       Symbols.Null},
    new object[]{"*",                   Symbols.Null,           Symbols.Null,           Symbols.Null,       new Symbols(multiply, T, O),    Symbols.Null},
    new object[]{"/",                   Symbols.Null,           Symbols.Null,           Symbols.Null,       new Symbols(divide, T, O),      Symbols.Null},
    new object[]{TokenType.Constant,    new Symbols(X),    new Symbols(E, equal),  new Symbols(T, O),  Symbols.Null,                   new Symbols(num)},
    new object[]{"(",                   new Symbols(X),    new Symbols(E, equal),  new Symbols(T, O),  Symbols.Null,                   new Symbols(openBracket, E, closeBracket)},
    new object[]{")",                   Symbols.Null,           Symbols.Null,           Symbols.Empty(),    Symbols.Null,                   Symbols.Null},
};
foreach (object[] _row in rows)
    table.Rows.Add(_row);
table.PrimaryKey = new[] { table.Columns["[terminals]"] };

ParserTable parserTable = new ParserTable(table);

var tokenizers = new List<Tokenizer>()
{
    new Tokenizer(TokenType.Operator, @"([+\-*/])"),
    // FIXME verificar se essa constante está formatada corretamente
    new Tokenizer(TokenType.Constant, @"([IVXLC]+)"),
    new Tokenizer(TokenType.Puntuator, @"([\(\)=])"),
};
var compiler = new Compiler(tokenizers, S, end, parserTable);
compiler.Compile(expression);
Console.WriteLine($"IsScriptValid = {compiler.Success}");
