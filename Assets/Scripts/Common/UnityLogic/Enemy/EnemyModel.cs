using UnityEngine;

namespace Common.UnityLogic.Enemy
{
    public struct EnemyModel
    {
        public readonly uint StartHp;
        public readonly float MovementSpeed;
        public readonly Vector2 MovementDirection;

        public EnemyModel(uint startHp, float movementSpeed, Vector2 movementDirection)
        {
            StartHp = startHp;
            MovementSpeed = movementSpeed;
            MovementDirection = movementDirection;
        }
    }
}