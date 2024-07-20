using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.GamePlay.Bullet
{
    [RequireComponent(typeof(PhysicsMovement))]
    [RequireComponent(typeof(BulletCollision))]
    public sealed class BulletConstructor : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _physicsMovement;
        [SerializeField] private BulletCollision _bulletCollision;

        private IStaticDataService _staticDataService;

        private BulletStaticData BulletStaticData => _staticDataService.GameStaticData.BulletStaticData;
        
        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        private void OnValidate()
        {
            _physicsMovement ??= gameObject.GetComponent<PhysicsMovement>();
            _bulletCollision ??= gameObject.GetComponent<BulletCollision>();
        }

        public void Initialize(in Vector3 movementDirection)
        {
            _physicsMovement.SetDirection(movementDirection);
            _physicsMovement.SetSpeed(BulletStaticData.Speed);
            _bulletCollision.SetDamage(BulletStaticData.Damage);
        }
    }
}