using System;
using System.Collections.Generic;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.UnityLogic.GamePlay.Bullet;
using Common.UnityLogic.GamePlay.Contracts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Infrastructure.Factories.GamePlay
{
    public sealed class BulletFactory : MultiObjectFactory<BulletConstructor>
    {
        private const string BulletPath = "GamePlay/Bullet";

        private readonly IGamePlayListener _gamePlayListener;
        private readonly List<BulletConstructor> _createdBullets = new();
        
        protected override string Path => BulletPath;
        public override IEnumerable<BulletConstructor> Collection => _createdBullets;

        public BulletFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory) : base(assetProvider, zenjectFactory)
        { }
        
        protected override BulletConstructor CreateInstance(in Transform parent)
        {
            var instance = ZenjectFactory.Instantiate(Prefab, parent.position, parent.rotation);
            _createdBullets.Add(instance);
            return instance;
        }

        public override void Destroy(in BulletConstructor instance)
        {
            // TODO: add object pooling
            _createdBullets.Remove(instance);
            Object.Destroy(instance.gameObject);
        }

        protected override void ClearCollection()
        {
            _createdBullets.Clear();
        }
    }
}