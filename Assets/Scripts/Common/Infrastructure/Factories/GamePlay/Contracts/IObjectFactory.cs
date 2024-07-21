using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay.Contracts
{
    public interface IObjectFactory<T> where T : MonoBehaviour
    {
        T Create(in Transform parent);
        void Destroy(in T instance);
        void Destroy(in GameObject instance);
        void ClearAll();
    }
}