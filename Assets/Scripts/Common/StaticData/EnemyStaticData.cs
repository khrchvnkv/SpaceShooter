using Common.StaticData.Range;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(menuName = nameof(EnemyStaticData), fileName = nameof(EnemyStaticData), order = 2)]
    public sealed class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField] public uint StartHp { get; private set; }
        [field: SerializeField] public MinMaxFloatRange SpeedRange { get; private set; }
        [field: SerializeField] public MinMaxFloatRange SpawnDelayRange { get; private set; }

        private void OnValidate()
        {
            SpeedRange.OnValidate();
            SpawnDelayRange.OnValidate();
        }
    }
}