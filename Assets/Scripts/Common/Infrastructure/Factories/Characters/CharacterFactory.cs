using Common.Infrastructure.Services.AssetsManagement;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure.Factories.Characters
{
    public sealed class CharacterFactory : ICharactersFactory
    {
        private const string PlayerPath = "Characters/Player";
        
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _diContainer;

        private GameObject _characterPrefab;

        public CharacterFactory(IAssetProvider assetProvider, DiContainer diContainer)
        {
            _assetProvider = assetProvider;
            _diContainer = diContainer;
        }

        public GameObject CreateCharacter(Transform parent)
        {
            _characterPrefab ??= _assetProvider.Load(PlayerPath);
            return _diContainer.InstantiatePrefab(_characterPrefab, parent);
        }
    }
}