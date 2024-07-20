using NaughtyAttributes;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "Static Data/GameStaticData")]
    public class GameStaticData : ScriptableObject
    {
        [field: Header("Level"), SerializeField, Expandable] public LevelStaticData LevelStaticData { get; private set; }
        [field: Header("UI"), SerializeField, Expandable] public WindowStaticData WindowStaticData { get; private set; }
        [field: Header("Character"), SerializeField, Expandable] public CharacterStaticData CharacterStaticData { get; private set; }
        [field: Header("Bullet"), SerializeField, Expandable] public BulletStaticData BulletStaticData { get; private set; }
        [field: Header("Enemy"), SerializeField, Expandable] public EnemyStaticData EnemyStaticData { get; private set; }
    }
}