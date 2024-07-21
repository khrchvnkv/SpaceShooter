using UnityEngine;

namespace Common.Infrastructure.Services.Input
{
    public interface IInputService
    {
        bool IsEnabled { get; }
        Vector2 Direction { get; }

        void EnableInput();
        void DisableInput();
    }
}