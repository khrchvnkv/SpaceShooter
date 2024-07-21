using Common.Infrastructure.Factories.GamePlay.Contracts;
using Common.Infrastructure.Services.StaticData;
using Common.StaticData;
using Common.UnityLogic.Character.Data;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Character
{
    public sealed class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        
        private SingleObjectFactory<CharacterConstructor> _characterFactory;
        private IStaticDataService _staticDataService;

        private CharacterStaticData CharacterStaticData => _staticDataService.GameStaticData.CharacterStaticData;
        
        private void OnValidate()
        {
            _spawnPoint ??= transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_spawnPoint.position, 0.3f);
        }

        [Inject]
        private void Construct(SingleObjectFactory<CharacterConstructor> characterFactory,
            IStaticDataService staticDataService)
        {
            _characterFactory = characterFactory;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            var instance = _characterFactory.Create(_spawnPoint);
            instance.Initialize(CreatModel());
        }

        private CharacterModel CreatModel()
        {
            return new CharacterModel(CharacterStaticData);
        }
    }
}