namespace MyCompiler.Analyzers
{
    public interface IResetable
    {
        void Reset();
    }
    public interface IResetable<T> : IResetable
    {
        void Reset(T value);
    }
}