using System;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Services.MonoUpdate;
using Common.Infrastructure.Services.NearestEnemy;
using Common.UnityLogic.Character.Data;
using Common.UnityLogic.Enemy;
using Common.UnityLogic.GamePlay.Bullet;
using UnityEngine;

namespace Common.UnityLogic.Character
{
    public sealed class CharacterShooting : IDisposable
    {
        private const float DelayBtwNearestSeek = 0.1f;
        private const float RotationSpeed = 75.0f;
        
        private readonly Transform _transform;
        private readonly IMonoUpdateService _monoUpdateService;
        private readonly INearestEnemyFinder _nearestEnemyFinder;
        private readonly MultiObjectFactory<BulletConstructor> _bulletFactory;

        private CharacterModel? _model;

        private float _shootRate;
        private float? _nearestSeekDelay;

        private EnemyConstructor _enemy;

        private bool IsTimerExpired => _shootRate <= 0.0f;
        
        public CharacterShooting(Transform transform, IMonoUpdateService monoUpdateService,
            INearestEnemyFinder nearestEnemyFinder, 
            MultiObjectFactory<BulletConstructor> bulletFactory)
        {
            _transform = transform;
            _monoUpdateService = monoUpdateService;
            _nearestEnemyFinder = nearestEnemyFinder;
            _bulletFactory = bulletFactory;
            
            _monoUpdateService.OnUpdate += OnUpdate;
        }
        
        public void Dispose()
        {
            _model = null;
            _monoUpdateService.OnUpdate -= OnUpdate;
        }

        public void SetModel(CharacterModel model)
        {
            _model = model;
            ResetShootRate();
        }

        private void OnUpdate()
        {
            if (!_model.HasValue)
            {
                return;
            }
            
            LookRotation();
            if (IsTimerExpired)
            {
                if (_nearestSeekDelay.HasValue)
                {
                    _nearestSeekDelay -= Time.deltaTime;
                    if (_nearestSeekDelay > 0.0f)
                    {
                        return;
                    }

                    _nearestSeekDelay = null;
                }
                
                _enemy = _nearestEnemyFinder.GetTheNearestEnemy(_transform.position, _model.Value.AttackRange);
                if (_enemy)
                {
                    var direction = (_enemy.Position - _transform.position).normalized;
                    Shoot(direction);
                    ResetShootRate();
                }
                else
                {
                    _nearestSeekDelay = DelayBtwNearestSeek;
                }
            }
            else
            {
                _shootRate -= Time.deltaTime;
            }
        }

        private void LookRotation()
        {
            if (!_enemy)
            {
                return;
            }
            
            var look = _transform.InverseTransformPoint(_enemy.Position);
            var angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            var newRotation = Quaternion.Euler(0, 0, angle);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, newRotation, RotationSpeed * Time.deltaTime);
        }

        private void ResetShootRate()
        {
            if (_model.HasValue)
            {
                _shootRate = _model.Value.ShootRate;
            }
        }
        
        private void Shoot(in Vector3 direction)
        {
            var instance = _bulletFactory.Create(_transform);
            instance.Initialize(direction);
        }
    }
}