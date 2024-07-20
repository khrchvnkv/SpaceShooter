using Common.UnityLogic.Enemy;
using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay.Contracts
{
    public interface INearestEnemySeeker
    {
        EnemyConstructor GetTheNearestEnemy(in Vector3 fromPosition, in float maxDistance);
    }
}