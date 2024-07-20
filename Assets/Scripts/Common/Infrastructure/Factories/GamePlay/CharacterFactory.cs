using Common.Infrastructure.Factories.Zenject;
using Common.Infrastructure.Services.AssetsManagement;
using Common.UnityLogic.Character;
using UnityEngine;

namespace Common.Infrastructure.Factories.GamePlay
{
    public sealed class CharacterFactory : ObjectFactory<CharacterConstructor>
    {
        private const string PlayerPath = "Characters/Player";
        
        protected override string Path => PlayerPath;

        public CharacterFactory(IAssetProvider assetProvider, IZenjectFactory zenjectFactory) : base(assetProvider, zenjectFactory)
        { }
        
        protected override CharacterConstructor CreateInstance(in Transform parent)
        {
            return ZenjectFactory.Instantiate(Prefab, parent);
        }

        public override void Destroy(in CharacterConstructor instance)
        {
            // TODO: add object pooling
            Object.Destroy(instance.gameObject);
        }
    }

}