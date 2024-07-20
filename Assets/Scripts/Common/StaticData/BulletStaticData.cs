using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(menuName = nameof(BulletStaticData), fileName = nameof(BulletStaticData), order = 4)]
    public sealed class BulletStaticData : ScriptableObject
    {
        [field: SerializeField, Range(0.1f, 100.0f)] public float Speed { get; private set; }
        [field: SerializeField, Range(1, 100)] public int Damage { get; private set; }
    }
}