using Common.UnityLogic.GamePlay;
using UnityEngine;

namespace Common.UnityLogic.Enemy
{
    [RequireComponent(typeof(PhysicsMovement))]
    [RequireComponent(typeof(EnemyCollision))]
    public sealed class EnemyConstructor : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _movement;
        
        private EnemyModel? _model;

        public Vector3 Position => _movement.Position;
        
        private void OnValidate()
        {
            _movement ??= gameObject.GetComponent<PhysicsMovement>();
        }

        public void Initialize(EnemyModel model)
        {
            _model = model;
            _movement.SetSpeed(_model.Value.MovementSpeed);
            _movement.SetDirection(_model.Value.MovementDirection);
        }

        private void OnDisable()
        {
            _model = null;
        }
    }
}