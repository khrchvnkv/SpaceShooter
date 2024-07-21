using Common.StaticData.Range;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(menuName = nameof(LevelStaticData), fileName = nameof(LevelStaticData), order = 3)]
    public sealed class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public MinMaxIntRange NumberOfEnemiesPerLevel { get; private set; }

        private void OnValidate()
        {
            NumberOfEnemiesPerLevel.OnValidate();
        }
    }
}