using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Services.Input;
using Common.Infrastructure.Services.MonoUpdate;
using Common.Infrastructure.Services.NearestEnemy;
using Common.Infrastructure.Services.StaticData;
using Common.UnityLogic.Character.Data;
using Common.UnityLogic.Character.Zones;
using Common.UnityLogic.GamePlay.Bullet;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Character
{
    public class CharacterConstructor : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        private IMonoUpdateService _monoUpdateService;
        private IInputService _inputService;
        private INearestEnemyFinder _nearestEnemyFinder;
        private MultiObjectFactory<BulletConstructor> _bulletFactory;
        private MovementZone _movementZone;
        private CharacterHealthZone _healthZone;

        private CharacterMovement _movement;
        private CharacterShooting _shooting;
        
        public CharacterHealth Health { get; private set; }
        
        [Inject]
        private void Construct(IMonoUpdateService monoUpdateService, 
            IInputService inputService, IStaticDataService staticDataService,
            INearestEnemyFinder nearestEnemyFinder, MultiObjectFactory<BulletConstructor> bulletFactory,
            MovementZone movementZone, CharacterHealthZone healthZone)
        {
            _monoUpdateService = monoUpdateService;
            _inputService = inputService;
            _nearestEnemyFinder = nearestEnemyFinder;
            _bulletFactory = bulletFactory;
            _movementZone = movementZone;
            _healthZone = healthZone;
        }
        
        private void OnValidate()
        {
            _transform ??= transform;
        }

        public void Initialize(CharacterModel model)
        {
            Health = new CharacterHealth(_healthZone);
            Health.SetHp(model.StartHp);
            
            _movement = new CharacterMovement(_transform, _monoUpdateService, _inputService, _movementZone);
            _movement.SetupMovementSpeed(model.MovementSpeed);

            _shooting =
                new CharacterShooting(_transform, _monoUpdateService, _nearestEnemyFinder, _bulletFactory);
            _shooting.SetModel(model);
        }

        private void OnDisable()
        {
            Health?.Dispose();
            Health = null;
            
            _movement?.Dispose();
            _movement = null;
            
            _shooting?.Dispose();
            _shooting = null;
        }
    }
}