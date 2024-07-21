using System;
using Common.UnityLogic.Character.Zones;

namespace Common.UnityLogic.Character
{
    public sealed class CharacterHealth : IDisposable
    {
        private readonly CharacterHealthZone _healthZone;
        
        private int _hp;

        public bool IsAlive => Hp > 0;

        public int Hp
        {
            get => _hp;
            private set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _hp = value;
                HpChanged?.Invoke(_hp);
                if (!IsAlive)
                {
                    Died?.Invoke();
                }
            }
        }

        public event Action Died;
        public event Action<int> HpChanged; 

        public CharacterHealth(CharacterHealthZone healthZone)
        {
            _healthZone = healthZone;

            _healthZone.OnZoneEntered += OnZoneEntered;
        }

        public void Dispose()
        {
            _healthZone.OnZoneEntered -= OnZoneEntered;
        }

        public void SetHp(in int hp)
        {
            Hp = hp;
        }
        
        private void OnZoneEntered()
        {
            Hp--;
        }
    }
}