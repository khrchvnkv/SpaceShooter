using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay
{
    public abstract class ObjectFactory<T> : IObjectFactory<T> where T : MonoBehaviour
    {
        protected readonly IAssetProvider AssetProvider;
        protected readonly IZenjectFactory ZenjectFactory;
        
        protected abstract string Path { get; }

        protected T Prefab;

        protected ObjectFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory)
        {
            AssetProvider = assetProvider;
            ZenjectFactory = zenjectFactory;
        }

        public T Create(in Transform parent)
        {
            Prefab ??= AssetProvider.Load<T>(Path);
            return CreateInstance(parent);
        }

        protected abstract T CreateInstance(in Transform parent);

        public abstract void Destroy(in T instance);
    }
}