namespace Common.UnityLogic.Enemy.Contracts
{
    public interface IBulletTriggerable
    {
        void CollideWithBullet(in int damage);
    }
}