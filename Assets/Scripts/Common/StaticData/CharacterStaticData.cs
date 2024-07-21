using NaughtyAttributes;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(menuName = nameof(CharacterStaticData), fileName = nameof(CharacterStaticData), order = 1)]
    public sealed class CharacterStaticData : ScriptableObject
    {
        [field: SerializeField, MinValue(1)] public int StartHP { get; private set; }
        [field: SerializeField, Range(0.1f, 100.0f)] public float MovementSpeed { get; private set; }
        [field: SerializeField, Range(0.1f, 100.0f)] public float AttackRange { get; private set; }
        [field: SerializeField, Range(0.1f, 10.0f)] public float ShootRate { get; private set; }
    }
}