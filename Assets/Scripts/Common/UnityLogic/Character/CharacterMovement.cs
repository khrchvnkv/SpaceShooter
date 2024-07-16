using Common.Infrastructure.Services.InputServices;
using Common.Infrastructure.Services.MonoUpdate;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Character
{
    public sealed class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        
        private IMonoUpdateService _monoUpdateService;
        private IInputService _inputService;
        private MovementZone _movementZone;

        [Inject]
        private void Construct(IMonoUpdateService monoUpdateService, 
            IInputService inputService, MovementZone movementZone)
        {
            _monoUpdateService = monoUpdateService;
            _inputService = inputService;
            _movementZone = movementZone;
        }

        private void OnValidate()
        {
            _transform ??= transform;
        }

        private void OnEnable()
        {
            _monoUpdateService.OnUpdate += OnUpdate;
        }

        private void OnDisable()
        {
            _monoUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            Move();
        }

        private void Move()
        {
            var delta = _inputService.Direction * Time.deltaTime;
            _transform.position += _movementZone.ClampMovement(_transform.position, delta);
        }
    }
}