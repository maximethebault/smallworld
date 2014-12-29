using ClassLibrary.Move.Fight;

namespace ClassLibrary.Move
{
    public interface IMove
    {
        bool Success { get; }
        bool Fight { get; }
    }
}
