using Common.StaticData;

namespace Common.UnityLogic.Character.Data
{
    public struct CharacterModel
    {
        public readonly uint StartHp;
        public readonly float MovementSpeed;
        public readonly float AttackRange;
        public readonly float ShootRate;

        public CharacterModel(CharacterStaticData staticData)
        {
            StartHp = staticData.StartHP;
            MovementSpeed = staticData.MovementSpeed;
            AttackRange = staticData.AttackRange;
            ShootRate = staticData.ShootRate;
        }
    }
}