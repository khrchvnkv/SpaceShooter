using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.UnityLogic.Character;
using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay
{
    public sealed class CharacterFactory : SingleObjectFactory<CharacterConstructor>
    {
        private const string PlayerPath = "Characters/Player";
        
        protected override string Path => PlayerPath;
        public override CharacterConstructor Instance { get; protected set; }

        public CharacterFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory) : base(assetProvider, zenjectFactory)
        { }
        
        protected override CharacterConstructor CreateInstance(in Transform parent)
        {
            Instance = ZenjectFactory.Instantiate(Prefab, parent);
            return Instance;
        }

        public override void Destroy(in CharacterConstructor instance)
        {
            // TODO: add object pooling
            Instance = null;
            Object.Destroy(instance.gameObject);
        }
    }
}