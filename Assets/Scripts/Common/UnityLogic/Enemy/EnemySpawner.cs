using System;
using System.Collections;
using System.Linq;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using NaughtyAttributes;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Common.UnityLogic.Enemy
{
    public sealed class EnemySpawner : MonoBehaviour, IDisposable
    {
        [InfoBox("For correct operation, the number of spawn points must be greater than 0", EInfoBoxType.Warning)]
        [SerializeField] private Transform[] _spawnPoints;
        
        private IStaticDataService _staticDataService;
        private ICoroutineRunner _coroutineRunner;
        private MultiObjectFactory<EnemyConstructor> _enemiesFactory;

        private Coroutine _coroutine;

        private int _enemiesLeft;

        public int EnemiesLeft
        {
            get => _enemiesLeft;
            set
            {
                if (value < 0)
                {
                    Debug.LogWarning("Trying to set the number of remaining enemies to a negative value");
                    value = 0;
                }
                _enemiesLeft = value;
                OnLeftValueChanged?.Invoke(_enemiesLeft);
            }
        }

        public bool AllSpawned => EnemiesLeft == 0;

        public event Action<int> OnLeftValueChanged;

        private EnemyStaticData EnemyStaticData => _staticDataService.GameStaticData.EnemyStaticData;

        [Inject]
        private void Construct(IStaticDataService staticDataService, 
            ICoroutineRunner coroutineRunner, MultiObjectFactory<EnemyConstructor> enemyFactory)
        {
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
            _enemiesFactory = enemyFactory;
        }
        
        public void Initialize()
        {
            EnemiesLeft = _staticDataService.GameStaticData.LevelStaticData.NumberOfEnemiesPerLevel.GetRandom();
            _coroutine = _coroutineRunner.StartCoroutine(SpawnCoroutine());
        }

        public void Dispose()
        {
            StopSpawn();
        }

        public void StopSpawn()
        {
            _coroutineRunner.StopCoroutineSafe(_coroutine);
        }

        private IEnumerator SpawnCoroutine()
        {
            while (EnemiesLeft > 0)
            {
                var delay = EnemyStaticData.SpawnDelayRange.GetRandom();
                yield return new WaitForSeconds(delay);
                var spawnPoint = GetRandomSpawnPoint();
                var instance = _enemiesFactory.Create(spawnPoint);
                instance.Initialize(CreateModel(spawnPoint));
                EnemiesLeft--;
            }
        }

        private Transform GetRandomSpawnPoint()
        {
            if (!_spawnPoints.Any())
            {
                throw new Exception("Spawn points not set");
            }

            return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        }

        private EnemyModel CreateModel(Transform spawnPoint)
        {
            return new EnemyModel(EnemyStaticData.StartHp, 
                EnemyStaticData.SpeedRange.GetRandom(), spawnPoint.up);
        }
    }
}