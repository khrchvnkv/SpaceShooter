using UnityEngine;

namespace Common.Infrastructure.Services.InputServices
{
    public class StandaloneInputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        public bool IsEnabled { get; private set; }
        public Vector2 Direction => GetInputDirection();
        
        public void EnableInput()
        {
            IsEnabled = true;
        }

        public void DisableInput()
        {
            IsEnabled = false;
        }

        private Vector2 GetInputDirection()
        {
            if (!IsEnabled)
            {
                return Vector2.zero;
            }
            
            var horizontal = Input.GetAxis(HorizontalAxis);
            var vertical = Input.GetAxis(VerticalAxis);

            return new Vector2(horizontal, vertical);
        }
    }
}