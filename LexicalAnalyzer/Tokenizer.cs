using System.Text.RegularExpressions;

namespace MyCompiler.Analyzers.Lexical
{
    public class Tokenizer
    {
        public TokenType Type { get; }
        public string TokenPattern { get; protected set; }
        public Tokenizer(TokenType type, string tokenMatch)
        {
            Type = type;
            TokenPattern = tokenMatch;
        }
        public bool TryTokenize(string lexeme, out Token token)
        {
            // TODO verificar se ele usou todos os caracteres do lexema ou se ficou faltando algum (se converteu só parte do lexema pq uma parte tava formatada errada)
            Match match = Regex.Match(lexeme, TokenPattern);
            if (!match.Success)
            {
                token = Token.Invalid;
                return false;
            }
            token = new Token(Type, lexeme);
            return true;
        }
    }
}