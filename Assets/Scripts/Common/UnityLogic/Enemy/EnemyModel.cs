using UnityEngine;

namespace Common.UnityLogic.Enemy
{
    public struct EnemyModel
    {
        public readonly int StartHp;
        public readonly float MovementSpeed;
        public readonly Vector2 MovementDirection;

        public EnemyModel(int startHp, float movementSpeed, Vector2 movementDirection)
        {
            StartHp = startHp;
            MovementSpeed = movementSpeed;
            MovementDirection = movementDirection;
        }
    }
}