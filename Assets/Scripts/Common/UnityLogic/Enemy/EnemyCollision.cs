using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.UnityLogic.Enemy.Contracts;
using Common.UnityLogic.GamePlay.Contracts;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Enemy
{
    public sealed class EnemyCollision : MonoBehaviour, IHealthZoneTriggerable, IBulletColliding
    {
        [SerializeField] private EnemyConstructor _enemyConstructor;
        
        private IObjectFactory<EnemyConstructor> _enemiesFactory;
        
        [Inject]
        private void Construct(IObjectFactory<EnemyConstructor> charactersFactory)
        {
            _enemiesFactory = charactersFactory;
        }

        private void OnValidate()
        {
            _enemyConstructor = gameObject.GetComponent<EnemyConstructor>();
        }

        public void OnHealthZoneEntered()
        {
            DestroyInstance();
        }

        public void CollideWithBullet(in float damage)
        {
            DestroyInstance();
        }

        private void DestroyInstance()
        {
            _enemiesFactory.Destroy(_enemyConstructor);
        }
    }
}