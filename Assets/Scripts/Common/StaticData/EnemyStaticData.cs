using Common.StaticData.Range;
using NaughtyAttributes;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(menuName = nameof(EnemyStaticData), fileName = nameof(EnemyStaticData), order = 2)]
    public sealed class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField, MinValue(1)] public int StartHp { get; private set; }
        [field: SerializeField] public MinMaxFloatRange SpeedRange { get; private set; }
        [field: SerializeField] public MinMaxFloatRange SpawnDelayRange { get; private set; }

        private void OnValidate()
        {
            SpeedRange.OnValidate();
            SpawnDelayRange.OnValidate();
        }
    }
}