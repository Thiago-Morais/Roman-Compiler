using System.Text.RegularExpressions;

namespace MyCompiler.Analyzers
{
    public struct Scanner : IResetable<string>
    {
        public int NextIndex { get; private set; }
        public string OriginalExpression { get; private set; }
        public string RemainingExpression => OriginalExpression.Substring(NextIndex);
        public string CurrentLexeme { get; private set; }
        public bool IsDone => NextIndex >= OriginalExpression.Length;
        public bool Failed { get; private set; }
        public Scanner(string expression) : this() => OriginalExpression = expression;
        public void ScanNext()
        {
            Match match = Regex.Match(RemainingExpression, @"([+\-*/\(\)=]|[IVXLC]+)");
            if (!match.Success)
            {
                Failed = true;
                return;
            }
            CurrentLexeme = match.Value;
            // TODO Verificar quando o index é maior que zero (o regex encontrou um match depois do inicio da expressão então ele pulou alguma coisa)
            NextIndex += match.Index + match.Value.Length;
        }
        public void Reset(string expression)
        {
            Reset();
            OriginalExpression = expression;
        }
        public void Reset()
        {
            OriginalExpression = "";
            NextIndex = 0;
            Failed = false;
        }
    }
}