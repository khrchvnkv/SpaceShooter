using System.Collections.Generic;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay.Contracts
{
    public abstract class MultiObjectFactory<T> : BaseObjectFactory<T> where T : MonoBehaviour
    {
        public abstract IEnumerable<T> Collection { get; }

        protected MultiObjectFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory) : base(assetProvider, zenjectFactory)
        { }

        public override void ClearAll()
        {
            var collection = new List<T>(Collection);
            foreach (var item in collection)
            {
                if (item)
                {
                    Destroy(item);
                }
            }
            
            ClearCollection();
        }

        protected abstract void ClearCollection();
    }
}