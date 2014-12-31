using System.Drawing;

namespace Model.Tile
{
    public interface ITile
    {
        void SetBitmap(Bitmap bitmap);
        Bitmap GetBitmap();
        bool IsPlain();
        bool IsDesert();
        bool IsMountain();
        bool IsForest();
    }
}