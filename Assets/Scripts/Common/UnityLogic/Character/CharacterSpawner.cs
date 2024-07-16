using Common.Infrastructure.Factories.Characters;
using UnityEngine;
using Zenject;

namespace Common.UnityLogic.Character
{
    public sealed class CharacterSpawner : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform _spawnPoint;
        
        private ICharactersFactory _charactersFactory;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _spawnPoint ??= transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_spawnPoint.position, 0.3f);
        }
#endif

        [Inject]
        private void Construct(ICharactersFactory charactersFactory)
        {
            _charactersFactory = charactersFactory;
        }

        public void Initialize()
        {
            _charactersFactory.CreateCharacter(_spawnPoint);
        }
    }
}