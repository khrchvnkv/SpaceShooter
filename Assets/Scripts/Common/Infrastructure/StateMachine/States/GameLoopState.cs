using Common.Infrastructure.Services.Input;

namespace Common.Infrastructure.StateMachine.States
{
    public class GameLoopState : State, IState
    {
        private readonly IInputService _inputService;

        public GameLoopState(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Enter()
        { }

        public override void Exit()
        {
            _inputService.DisableInput();
        }
    }
}