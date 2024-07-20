using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.UnityLogic.GamePlay.Bullet;
using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay
{
    public sealed class BulletFactory : ObjectFactory<BulletConstructor>
    {
        private const string BulletPath = "GamePlay/Bullet";

        protected override string Path => BulletPath;

        public BulletFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory) : base(assetProvider, zenjectFactory)
        { }
        
        protected override BulletConstructor CreateInstance(in Transform parent)
        {
            return ZenjectFactory.Instantiate(Prefab, parent.position, parent.rotation);
        }

        public override void Destroy(in BulletConstructor instance)
        {
            // TODO: add object pooling
            Object.Destroy(instance.gameObject);
        }
    }
}