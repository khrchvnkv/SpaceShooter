using Common.UnityLogic.GamePlay.Contracts;
using UnityEngine;

namespace Common.UnityLogic.Character.Zones
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class CharacterHealthZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IHealthZoneTriggerable healthZoneTriggerable))
            {
                healthZoneTriggerable.OnHealthZoneEntered();
            }
        }
    }
}