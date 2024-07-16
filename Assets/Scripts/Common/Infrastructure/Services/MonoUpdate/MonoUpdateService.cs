using System;
using UnityEngine;

namespace Common.Infrastructure.Services.MonoUpdate
{
    public sealed class MonoUpdateService : MonoBehaviour, IMonoUpdateService
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
    }
}