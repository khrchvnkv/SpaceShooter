using System.Collections.Generic;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.UnityLogic.Enemy;
using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay
{
    public sealed class EnemyFactory : ObjectFactory<EnemyConstructor>, INearestEnemySeeker
    {
        private const string EnemyPath = "Characters/Enemy";

        private readonly List<EnemyConstructor> _createdEnemies = new();

        protected override string Path => EnemyPath;

        public EnemyFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory) : base(assetProvider, zenjectFactory)
        { }
        
        protected override EnemyConstructor CreateInstance(in Transform parent)
        {
            var instance = ZenjectFactory.Instantiate(Prefab, parent);
            _createdEnemies.Add(instance);
            return instance;
        }

        public override void Destroy(in EnemyConstructor instance)
        {
            // TODO: add object pooling
            _createdEnemies.Remove(instance);
            Object.Destroy(instance.gameObject);
        }
        
        public EnemyConstructor GetTheNearestEnemy(in Vector3 fromPosition, in float maxDistance)
        {
            EnemyConstructor enemy = null;
            var minDistance = float.MaxValue;
            foreach (var createdEnemy in _createdEnemies)
            {
                var distance = Vector3.Distance(fromPosition, createdEnemy.Position);
                if (distance < minDistance && distance <= maxDistance)
                {
                    minDistance = distance;
                    enemy = createdEnemy;
                }
            }

            return enemy;
        }
    }
}