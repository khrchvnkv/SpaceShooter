using Common.Infrastructure.Services.MonoUpdate;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class PhysicsMovement : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;

        private IMonoUpdateService _monoUpdateService;

        private Vector2? _movementDirection;
        private float _movementSpeed;

        public Vector3 Position => _transform.position;
        
        private void OnValidate()
        {
            _transform ??= transform;
            _rigidbody ??= gameObject.GetComponent<Rigidbody2D>();
        }

        [Inject]
        private void Construct(IMonoUpdateService monoUpdateService)
        {
            _monoUpdateService = monoUpdateService;
        }
        
        public void SetDirection(Vector2 direction)
        {
            _movementDirection = direction;
        }

        public void SetSpeed(in float movementSpeed)
        {
            _movementSpeed = movementSpeed;
        }

        private void OnEnable()
        {
            _monoUpdateService.OnFixedUpdate += OnFixedUpdate;
        }

        private void OnDisable()
        {
            _monoUpdateService.OnFixedUpdate -= OnFixedUpdate;
        }

        private void OnFixedUpdate()
        {
            if (_movementDirection.HasValue)
            {
                var position = _rigidbody.position;
                var offset = _movementDirection.Value * _movementSpeed * Time.fixedDeltaTime;
                _rigidbody.MovePosition(position + offset);
            }
        }
    }
}