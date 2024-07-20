using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.UnityLogic.Enemy.Contracts;
using Common.UnityLogic.GamePlay.Contracts;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.GamePlay.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class BulletCollision : MonoBehaviour, IOutLevelZoneTriggerable
    {
        [SerializeField] private BulletConstructor _bulletConstructor;
        
        private IObjectFactory<BulletConstructor> _bulletFactory;
        
        private int _damage;

        [Inject]
        private void Construct(IObjectFactory<BulletConstructor> bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        private void OnValidate()
        {
            _bulletConstructor ??= gameObject.GetComponent<BulletConstructor>();
        }

        public void SetDamage(in int damage)
        {
            _damage = damage;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IBulletColliding bulletColliding))
            {
                bulletColliding.CollideWithBullet(_damage);
                DestroyBullet();
            }
        }

        public void OnOutLevelZoneEntered()
        {
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            _bulletFactory.Destroy(_bulletConstructor);
        }
    }
}