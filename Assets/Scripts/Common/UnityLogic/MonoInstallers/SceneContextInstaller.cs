using Common.Infrastructure.Factories.GamePlay;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.NearestEnemy;
using Common.UnityLogic.Character;
using Common.UnityLogic.Character.Zones;
using Common.UnityLogic.Enemy;
using Common.UnityLogic.GamePlay;
using Common.UnityLogic.GamePlay.Bullet;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.MonoInstallers
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private MovementZone _movementZone;
        [SerializeField] private CharacterHealthZone _healthZone;
        
        [SerializeField] private EnemySpawner _enemySpawner;
        
        public override void InstallBindings()
        {
            BindFactories();
            BindGamePlay();
        }

        private void BindFactories()
        {
            Container.Bind<IZenjectFactory>().To<ZenjectFactory>().AsSingle();
            Container.Bind<SingleObjectFactory<CharacterConstructor>>().To<CharacterFactory>().AsSingle();
            Container.Bind<MultiObjectFactory<EnemyConstructor>>().To<EnemyFactory>().AsSingle();
            Container.Bind<MultiObjectFactory<BulletConstructor>>().To<BulletFactory>().AsSingle();
        }

        private void BindGamePlay()
        {
            Container.Bind<INearestEnemyFinder>().To<NearestEnemyFinder>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CharacterSpawner>().FromInstance(_characterSpawner).AsCached();
            Container.BindInterfacesAndSelfTo<MovementZone>().FromInstance(_movementZone).AsCached();
            Container.BindInterfacesAndSelfTo<CharacterHealthZone>().FromInstance(_healthZone).AsCached();
            
            Container.BindInterfacesAndSelfTo<EnemySpawner>().FromInstance(_enemySpawner).AsCached();

            Container.BindInterfacesAndSelfTo<GamePlayListener>().AsCached();
        }
    }
}