using Common.Infrastructure.WindowsManagement;
using Common.UnityLogic.Character;
using Common.UnityLogic.Enemy;

namespace Common.UnityLogic.UI.Windows.GameHUD
{
    public struct GameHudWindowData : IWindowData
    {
        internal readonly CharacterHealth Health; 
        internal readonly EnemySpawner EnemySpawner;

        public string WindowName => "GameHUD";
        public bool DestroyOnClosing => false;
        
        public GameHudWindowData(CharacterHealth health, EnemySpawner enemySpawner)
        {
            Health = health;
            EnemySpawner = enemySpawner;
        }
    }
}