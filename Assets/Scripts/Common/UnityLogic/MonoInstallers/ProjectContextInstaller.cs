using Common.Infrastructure.Factories.UI;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.Infrastructure.Services.Coroutines;
using Common.Infrastructure.Services.DontDestroyOnLoadCreator;
using Common.Infrastructure.Services.Input;
using Common.Infrastructure.Services.MonoUpdate;
using Common.Infrastructure.Services.SceneLoading;
using Common.Infrastructure.Services.StaticData;
using Common.Infrastructure.StateMachine;
using Common.Infrastructure.StateMachine.States;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.MonoInstallers
{
    public sealed class ProjectContextInstaller : MonoInstaller
    {
        [SerializeField, Required] private MonoUpdateService _monoUpdateService;
        [SerializeField, Required] private DontDestroyOnLoadCreator _dontDestroyOnLoadCreator;
        [SerializeField, Required] private CoroutineRunner _coroutineRunner;

        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindServices();
            BindMonoServices();
            BindFactories();
        }
        private void BindMonoServices()
        {
            Container.Bind<IMonoUpdateService>().FromInstance(_monoUpdateService).AsSingle();
            Container.Bind<IDontDestroyOnLoadCreator>().FromInstance(_dontDestroyOnLoadCreator).AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
        }
        private void BindServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
        }
        private void BindGameStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }
        private void BindFactories()
        {
            Container.Bind<IZenjectFactory>().To<ZenjectFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }
    }
}
