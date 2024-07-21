using System;
using Common.Infrastructure.Services.Input;
using Common.Infrastructure.Services.MonoUpdate;
using Common.UnityLogic.Character.Zones;
using UnityEngine;

namespace Common.UnityLogic.Character
{
    public sealed class CharacterMovement : IDisposable
    {
        private readonly Transform _transform;
        private readonly IMonoUpdateService _monoUpdateService;
        private readonly IInputService _inputService;
        private readonly MovementZone _movementZone;

        private float _movementSpeed;
        
        public CharacterMovement(Transform transform, IMonoUpdateService monoUpdateService, 
            IInputService inputService, MovementZone movementZone)
        {
            _transform = transform;
            _monoUpdateService = monoUpdateService;
            _inputService = inputService;
            _movementZone = movementZone;
            
            _monoUpdateService.OnUpdate += OnUpdate;
        }
        
        public void Dispose()
        {
            _monoUpdateService.OnUpdate -= OnUpdate;
        }

        public void SetupMovementSpeed(in float movementSpeed)
        {
            _movementSpeed = movementSpeed;
        }

        private void OnUpdate()
        {
            Move();
        }

        private void Move()
        {
            var delta = _inputService.Direction * _movementSpeed * Time.deltaTime;
            _transform.position += _movementZone.ClampMovement(_transform.position, delta);
        }
    }
}