using System.Drawing;

namespace GeekCsh2Project1
{
    public interface ICollision
    {
         bool Collision(ICollision obj);
         Rectangle Rect { get; }
    }
}
