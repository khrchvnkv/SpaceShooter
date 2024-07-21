using Common.Infrastructure.Factories.UI;
using Common.Infrastructure.StateMachine;
using Common.Infrastructure.StateMachine.States;
using Common.Infrastructure.WindowsManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.UnityLogic.UI.Windows.LevelFinish
{
    public abstract class LevelFinishedWindow<TData> : WindowBase<TData> where TData : struct, IWindowData
    {
        [SerializeField] private Button _restartButton;

        private GameStateMachine _gameStateMachine;
        private IUIFactory _uiFactory;
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine,
            IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }
        
        protected override void PrepareForShowing()
        {
            base.PrepareForShowing();
            _restartButton.onClick.AddListener(Restart);
        }

        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();
            _restartButton.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
            _uiFactory.Hide(new TData());
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}