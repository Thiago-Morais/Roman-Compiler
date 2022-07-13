using System.Collections;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace MyCompiler.Analyzers.Lexical
{
    // TODO mudar para hashtable ou dicionario com o tipo como chave e uma lista dos tokens como valor
    [Serializable]
    public struct TokensCollection : ICollection<Token>
    {
        Collection<Token> tokens;
        public int Count => tokens.Count;
        public bool IsReadOnly => false;
        public TokensCollection()
        {
            tokens = new Collection<Token>();
        }
        public void Add(Token token) => tokens.Add(token);
        public void Clear() => tokens.Clear();
        public bool Contains(Token token) => tokens.Contains(token);
        public void CopyTo(Token[] array, int arrayIndex) => tokens.CopyTo(array, arrayIndex);
        public IEnumerator<Token> GetEnumerator() => tokens.GetEnumerator();
        public bool Remove(Token token) => tokens.Remove(token);
        IEnumerator IEnumerable.GetEnumerator() => tokens.GetEnumerator();
        public static string Serialize(TokensCollection tokens) => JsonSerializer.Serialize(tokens);
    }
}