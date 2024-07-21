using Common.Infrastructure.Services.MonoUpdate;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.GamePlay.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BulletCollision))]
    public sealed class BulletConstructor : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private BulletCollision _bulletCollision;

        private IStaticDataService _staticDataService;
        private IMonoUpdateService _monoUpdateService;
        
        private PhysicsMovement _physicsMovement;

        private BulletStaticData BulletStaticData => _staticDataService.GameStaticData.BulletStaticData;
        
        [Inject]
        private void Construct(IStaticDataService staticDataService,
            IMonoUpdateService monoUpdateService)
        {
            _staticDataService = staticDataService;
            _monoUpdateService = monoUpdateService;
        }
        
        private void OnValidate()
        {
            _rigidbody ??= gameObject.GetComponent<Rigidbody2D>();
            _bulletCollision ??= gameObject.GetComponent<BulletCollision>();
        }

        public void Initialize(in Vector3 movementDirection)
        {
            _physicsMovement = new PhysicsMovement(_rigidbody, _monoUpdateService);
            _physicsMovement.SetDirection(movementDirection);
            _physicsMovement.SetSpeed(BulletStaticData.Speed);
            
            _bulletCollision.SetDamage(BulletStaticData.Damage);
        }

        private void OnDisable()
        {
            _physicsMovement?.Dispose();
            _physicsMovement = null;
        }
    }
}