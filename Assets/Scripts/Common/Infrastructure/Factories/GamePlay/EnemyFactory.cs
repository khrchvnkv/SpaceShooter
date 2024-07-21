using System.Collections.Generic;
using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.UnityLogic.Enemy;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Infrastructure.Factories.GamePlay
{
    public sealed class EnemyFactory : MultiObjectFactory<EnemyConstructor>
    {
        private const string EnemyPath = "Characters/Enemy";

        private readonly List<EnemyConstructor> _createdEnemies = new();

        protected override string Path => EnemyPath;
        public override IEnumerable<EnemyConstructor> Collection => _createdEnemies;

        public EnemyFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory) : base(assetProvider, zenjectFactory)
        { }
        
        protected override EnemyConstructor CreateInstance(in Transform parent)
        {
            var instance = ZenjectFactory.Instantiate(Prefab, parent);
            _createdEnemies.Add(instance);
            return instance;
        }

        public override void Destroy(in EnemyConstructor instance)
        {
            // TODO: add object pooling
            _createdEnemies.Remove(instance);
            Object.Destroy(instance.gameObject);
        }
        
        protected override void ClearCollection()
        {
            _createdEnemies.Clear();
        }
    }
}