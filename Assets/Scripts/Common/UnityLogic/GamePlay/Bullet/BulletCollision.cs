using System;
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
        
        private MultiObjectFactory<BulletConstructor> _bulletFactory;
        
        private int _damage;

        [Inject]
        private void Construct(MultiObjectFactory<BulletConstructor> bulletFactory)
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IBulletTriggerable bulletColliding))
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