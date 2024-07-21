using System;
using Common.Infrastructure.Services.MonoUpdate;
using UnityEngine;

namespace Common.UnityLogic.GamePlay
{
    public sealed class PhysicsMovement : IDisposable
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly IMonoUpdateService _monoUpdateService;

        private Vector2? _movementDirection;
        private float _movementSpeed;

        public PhysicsMovement(Rigidbody2D rigidbody, 
            IMonoUpdateService monoUpdateService)
        {
            _rigidbody = rigidbody;
            _monoUpdateService = monoUpdateService;
            
            _monoUpdateService.OnFixedUpdate += OnFixedUpdate;
        }

        public void Dispose()
        {
            _monoUpdateService.OnFixedUpdate -= OnFixedUpdate;
        }
        public void SetDirection(Vector2 direction)
        {
            _movementDirection = direction;
        }

        public void SetSpeed(in float movementSpeed)
        {
            _movementSpeed = movementSpeed;
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