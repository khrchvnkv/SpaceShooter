using Common.Infrastructure.Factories.UI;
using Common.Infrastructure.Services.StaticData;

namespace Common.Infrastructure.StateMachine.States
{
    /// <summary>
    /// Data loading, UIRoot creating
    /// </summary>
    public class BootstrapState : State, IState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;

        public BootstrapState(IStaticDataService staticDataService, IUIFactory uiFactory)
        {
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }
        public void Enter()
        {
            LoadStaticData();
            CreateUIRootAndShowLoadingCurtain();
            
            StateMachine.Enter<LoadLevelState>();
        }
        public override void Exit()
        { }
        private void LoadStaticData() => _staticDataService.Load();
        private void CreateUIRootAndShowLoadingCurtain()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.ShowLoadingCurtain();
        }
    }
}