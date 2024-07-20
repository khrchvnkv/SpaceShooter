namespace Common.UnityLogic.Enemy.Contracts
{
    public interface IBulletColliding
    {
        void CollideWithBullet(in float damage);
    }
}