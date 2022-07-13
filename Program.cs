using MyCompiler;
using MyCompiler.Analyzers.Lexical;

Console.WriteLine("Enter roman expression:");
string expression = "C-LXXXVIII*(VIII+IX/(III+I))=";

var tokenizers = new List<Tokenizer>()
{
    new Tokenizer(TokenType.Operator, @"([+\-*/])"),
    // FIXME verificar se essa constante está formatada corretamente
    new Tokenizer(TokenType.Constant, @"([IVXLC]+)"),
    new Tokenizer(TokenType.Puntuator, @"([\(\)=])"),
};
Compiler compiler = new Compiler(tokenizers);
compiler.Compile(expression);
