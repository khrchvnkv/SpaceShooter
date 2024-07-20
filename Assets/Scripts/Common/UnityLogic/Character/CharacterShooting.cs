using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Services.MonoUpdate;
using Common.UnityLogic.Character.Data;
using Common.UnityLogic.GamePlay.Bullet;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Character
{
    public sealed class CharacterShooting : MonoBehaviour
    {
        private const float DelayBtwNearestSeek = 0.1f;
        
        [SerializeField] private Transform _transform;
        
        private IMonoUpdateService _monoUpdateService;
        private INearestEnemySeeker _nearestEnemySeeker;
        private IObjectFactory<BulletConstructor> _bulletFactory;

        private CharacterModel? _model;

        private float _shootRate;
        private float? _nearestSeekDelay;

        private bool IsTimerExpired => _shootRate <= 0.0f;
        
        [Inject]
        private void Construct(IMonoUpdateService monoUpdateService,
            INearestEnemySeeker nearestEnemySeeker, IObjectFactory<BulletConstructor> bulletFactory)
        {
            _monoUpdateService = monoUpdateService;
            _nearestEnemySeeker = nearestEnemySeeker;
            _bulletFactory = bulletFactory;
        }

        private void OnValidate()
        {
            _transform ??= transform;
        }

        public void SetModel(CharacterModel model)
        {
            _model = model;
            ResetShootRate();
        }

        private void OnEnable()
        {
            _monoUpdateService.OnUpdate += OnUpdate;
        }

        private void OnDisable()
        {
            _model = null;
            
            _monoUpdateService.OnUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            if (!_model.HasValue)
            {
                return;
            }
            
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
                
                var enemy = _nearestEnemySeeker.GetTheNearestEnemy(_transform.position, _model.Value.AttackRange);
                if (enemy)
                {
                    var direction = (enemy.Position - _transform.position).normalized;
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