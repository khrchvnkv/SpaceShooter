using System;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.UnityLogic.Enemy.Contracts;
using Common.UnityLogic.GamePlay.Contracts;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Enemy
{
    public sealed class EnemyCollision : MonoBehaviour, IHealthZoneTriggerable, IBulletTriggerable
    {
        private MultiObjectFactory<EnemyConstructor> _enemiesFactory;

        public event Action<int> Damaged;
        
        [Inject]
        public void Construct(MultiObjectFactory<EnemyConstructor> enemiesFactory)
        {
            _enemiesFactory = enemiesFactory;
        }

        public void OnHealthZoneEntered()
        {
            DestroyInstance();
        }

        public void CollideWithBullet(in int damage)
        {
            Damaged?.Invoke(damage);
        }

        private void DestroyInstance()
        {
            _enemiesFactory.Destroy(gameObject);
        }
    }
}