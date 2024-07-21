using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Services.MonoUpdate;
using Common.UnityLogic.GamePlay;
using Common.UnityLogic.GamePlay.Contracts;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Enemy
{
    [RequireComponent(typeof(EnemyView))]
    [RequireComponent(typeof(EnemyCollision))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class EnemyConstructor : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private EnemyView _view;
        [SerializeField] private EnemyCollision _collision;

        private PhysicsMovement _movement;
        private EnemyHealth _health;

        private IMonoUpdateService _monoUpdateService;
        private MultiObjectFactory<EnemyConstructor> _enemyFactory;
        private IGamePlayListener _gamePlayListener;
        
        private EnemyModel? _model;

        public Vector3 Position => _transform.position;

        [Inject]
        private void Construct(IMonoUpdateService monoUpdateService,
            MultiObjectFactory<EnemyConstructor> enemyFactory,
            IGamePlayListener gamePlayListener)
        {
            _monoUpdateService = monoUpdateService;
            _enemyFactory = enemyFactory;
            _gamePlayListener = gamePlayListener;
        }
        
        private void OnValidate()
        {
            _transform ??= transform;
            _rigidbody ??= gameObject.GetComponent<Rigidbody2D>();
            _view ??= gameObject.GetComponent<EnemyView>();
            _collision ??= gameObject.GetComponent<EnemyCollision>();
        }

        public void Initialize(EnemyModel model)
        {
            _model = model;

            _movement = new PhysicsMovement(_rigidbody, _monoUpdateService);
            _movement.SetSpeed(_model.Value.MovementSpeed);
            _movement.SetDirection(_model.Value.MovementDirection);

            _health = new EnemyHealth(_collision, _view);
            _health.Died += DiedAction;
            _health.SetHp(_model.Value.StartHp);
        }

        private void OnDisable()
        {
            _model = null;
            
            _movement?.Dispose();
            _model = null;

            if (_health != null) _health.Died -= DiedAction;
            _health?.Dispose();
            _health = null;
        }

        private void DiedAction()
        {
            _gamePlayListener.InformEnemyDied();
            _enemyFactory.Destroy(this);
        }
    }
}