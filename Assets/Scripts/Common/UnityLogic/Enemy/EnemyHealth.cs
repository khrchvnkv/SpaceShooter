using System;
using Common.Infrastructure.Factories.GamePlay.Contracts;

namespace Common.UnityLogic.Enemy
{
    public sealed class EnemyHealth : IDisposable
    {
        private readonly EnemyCollision _collision;
        private readonly EnemyView _view;
        
        private int _hp;

        private int Hp
        {
            get => _hp;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _hp = value;
                _view.SetHp(_hp);
            }
        }

        private bool IsAlive => Hp > 0;

        public event Action Died;
        
        public EnemyHealth(EnemyCollision collision, EnemyView view)
        {
            _collision = collision;
            _view = view;

            _collision.Damaged += TakeDamage;
        }
        
        public void Dispose()
        {
            _collision.Damaged -= TakeDamage;
        }
        
        public void SetHp(in int hp)
        {
            Hp = hp;
        }

        private void TakeDamage(int damage)
        {
            Hp -= damage;

            if (!IsAlive)
            {
                Died?.Invoke();
            }
        }
    }
}