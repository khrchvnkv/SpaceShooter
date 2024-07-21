using System.Collections.Generic;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.UnityLogic.Enemy;
using UnityEngine;

namespace Common.Infrastructure.Services.NearestEnemy
{
    public class NearestEnemyFinder : INearestEnemyFinder
    {
        private readonly MultiObjectFactory<EnemyConstructor> _enemyFactory;

        private IEnumerable<EnemyConstructor> CreatedEnemies => _enemyFactory.Collection;
        
        public NearestEnemyFinder(MultiObjectFactory<EnemyConstructor> enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public EnemyConstructor GetTheNearestEnemy(in Vector3 fromPosition, in float maxDistance)
        {
            EnemyConstructor enemy = null;
            var minDistance = float.MaxValue;
            foreach (var createdEnemy in CreatedEnemies)
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