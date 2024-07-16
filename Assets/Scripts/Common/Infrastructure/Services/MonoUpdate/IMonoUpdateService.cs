using System;

namespace Common.Infrastructure.Services.MonoUpdate
{
    public interface IMonoUpdateService
    {
        event Action OnUpdate;
        event Action OnFixedUpdate;
        event Action OnLateUpdate;
    }
}