using UnityEngine;

namespace Common.Infrastructure.Factories.Characters
{
    public interface ICharactersFactory
    {
        GameObject CreateCharacter(Transform parent);
    }
}