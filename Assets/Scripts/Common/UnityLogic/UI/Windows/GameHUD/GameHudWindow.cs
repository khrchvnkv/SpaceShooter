using TMPro;
using UnityEngine;

namespace Common.UnityLogic.UI.Windows.GameHUD
{
    public sealed class GameHudWindow : WindowBase<GameHudWindowData>
    {
        private const string HpText = "Health: {0}";
        private const string EnemiesLeftText = "Enemies Left: {0}";
        
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _enemiesLeftText;
        
        protected override void PrepareForShowing()
        {
            base.PrepareForShowing();

            WindowData.Health.HpChanged += UpdateHealthText;
            UpdateHealthText(WindowData.Health.Hp);
            
            WindowData.EnemySpawner.OnLeftValueChanged += UpdateEnemiesCountText;
            UpdateEnemiesCountText(WindowData.EnemySpawner.EnemiesLeft);
        }
        
        protected override void PrepareForHiding()
        {
            base.PrepareForHiding();

            if (WindowData.Health != null) WindowData.Health.HpChanged -= UpdateHealthText;
            if (WindowData.EnemySpawner != null) WindowData.EnemySpawner.OnLeftValueChanged -= UpdateEnemiesCountText;
        }

        private void UpdateHealthText(int newValue)
        {
            _healthText.text = string.Format(HpText, newValue);
        }

        private void UpdateEnemiesCountText(int newValue)
        {
            _enemiesLeftText.text = string.Format(EnemiesLeftText, newValue);
        }
    }
}