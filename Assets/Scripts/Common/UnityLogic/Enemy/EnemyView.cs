using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Common.UnityLogic.Enemy
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField, Required] private TMP_Text _hpText;

        public void SetHp(in int hp)
        {
            _hpText.text = hp.ToString();
        }
    }
}