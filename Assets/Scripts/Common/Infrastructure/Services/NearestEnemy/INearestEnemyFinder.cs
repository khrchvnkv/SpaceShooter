using Common.UnityLogic.Enemy;
using UnityEngine;

namespace Common.Infrastructure.Services.NearestEnemy
{
    public interface INearestEnemyFinder
    {
        EnemyConstructor GetTheNearestEnemy(in Vector3 fromPosition, in float maxDistance);
    }
}