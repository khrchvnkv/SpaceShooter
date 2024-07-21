using System;
using System.Linq;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Factories.UI;
using Common.Infrastructure.Services.Input;
using Common.UnityLogic.Character;
using Common.UnityLogic.Enemy;
using Common.UnityLogic.GamePlay.Bullet;
using Common.UnityLogic.GamePlay.Contracts;
using Common.UnityLogic.UI.Windows.GameHUD;
using Common.UnityLogic.UI.Windows.LevelCompleted;
using Common.UnityLogic.UI.Windows.LevelFailed;
using Zenject;

namespace Common.UnityLogic.GamePlay
{
    public sealed class GamePlayListener : IInitializable, IGamePlayListener, IDisposable
    {
        private readonly CharacterSpawner _characterSpawner;
        private readonly EnemySpawner _enemySpawner;
        private readonly IUIFactory _uiFactory;
        private readonly SingleObjectFactory<CharacterConstructor> _characterFactory;
        private readonly MultiObjectFactory<EnemyConstructor> _enemyFactory;
        private readonly MultiObjectFactory<BulletConstructor> _bulletFactory;
        private readonly IInputService _inputService;

        private bool IsPlayerCreated => _characterFactory.Instance != null;
        private CharacterHealth Health => IsPlayerCreated ? _characterFactory.Instance.Health : null;
        private bool IsLastEnemy => _enemyFactory.Collection.Count() == 1;
        private bool IsNoAnyEnemy => !_enemyFactory.Collection.Any();
        
        public GamePlayListener(CharacterSpawner characterSpawner, 
            EnemySpawner enemySpawner, IUIFactory uiFactory,
            SingleObjectFactory<CharacterConstructor> characterFactory, 
            MultiObjectFactory<EnemyConstructor> enemyFactory, 
            MultiObjectFactory<BulletConstructor> bulletFactory, 
            IInputService inputService)
        {
            _characterSpawner = characterSpawner;
            _enemySpawner = enemySpawner;
            _uiFactory = uiFactory;
            _characterFactory = characterFactory;
            _enemyFactory = enemyFactory;
            _bulletFactory = bulletFactory;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _characterSpawner.Initialize();
            _enemySpawner.Initialize();

            _uiFactory.ShowWindow(new GameHudWindowData(Health, _enemySpawner));
            _inputService.EnableInput();

            Health.HpChanged += CheckLevelCompleteOnHpChanged;
            Health.Died += OnLevelFailed;
        }

        public void Dispose()
        {
            if (IsPlayerCreated)
            {
                Health.HpChanged -= CheckLevelCompleteOnHpChanged;
                Health.Died -= OnLevelFailed;
            }
            HideGameHud();
        }
        
        public void InformEnemyDied()
        {
            if (_enemySpawner.AllSpawned && IsLastEnemy && Health.IsAlive)
            {
                OnLevelCompleted();
            }
        }

        private void HideGameHud()
        {
            _uiFactory.Hide(new GameHudWindowData());
        }

        private void CheckLevelCompleteOnHpChanged(int newHp)
        {
            if (newHp > 0 && _enemySpawner.AllSpawned && IsNoAnyEnemy)
            {
                OnLevelCompleted();
            }
        }
        
        private void OnLevelFailed()
        {
            PreparePauseState();
            _uiFactory.ShowWindow(new LevelFailedWindowData());
        }

        private void OnLevelCompleted()
        {
            PreparePauseState();
            _uiFactory.ShowWindow(new LevelCompletedData());
        }

        private void PreparePauseState()
        {
            _inputService.DisableInput();
            StopSpawner();
            
            ClearFactories();
            
            HideGameHud();
        }
        
        private void ClearFactories()
        {
            _enemyFactory.ClearAll();
            _bulletFactory.ClearAll();
        }
        
        private void StopSpawner()
        {
            _enemySpawner.StopSpawn();
        }
    }
}