using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay.Contracts
{
    public abstract class SingleObjectFactory<T> : BaseObjectFactory<T> where T : MonoBehaviour
    {
        public abstract T Instance { get; protected set; }

        protected SingleObjectFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory) : base(assetProvider, zenjectFactory)
        { }

        public override void ClearAll()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
        }
    }
}