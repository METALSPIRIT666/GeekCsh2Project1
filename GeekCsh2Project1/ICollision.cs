using System.Drawing;

namespace GeekCsh2Project1
{
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
}
