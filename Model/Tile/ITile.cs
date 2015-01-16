using System.Drawing;

namespace Model.Tile
{
    public interface ITile
    {
        int ID { get; }
        bool IsPlain();
        bool IsDesert();
        bool IsMountain();
        bool IsForest();
    }
}