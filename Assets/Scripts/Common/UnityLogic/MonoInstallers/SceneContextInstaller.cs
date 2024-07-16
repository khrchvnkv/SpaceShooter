using Common.Infrastructure.Factories.Characters;
using Common.Infrastructure.Factories.Zenject;
using Common.UnityLogic.Character;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.MonoInstallers
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private MovementZone _movementZone;
        
        public override void InstallBindings()
        {
            Container.Bind<IZenjectFactory>().To<ZenjectFactory>().AsSingle();
            Container.Bind<ICharactersFactory>().To<CharacterFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<CharacterSpawner>().FromInstance(_characterSpawner);
            Container.BindInterfacesAndSelfTo<MovementZone>().FromInstance(_movementZone);
        }
    }
}