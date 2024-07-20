using Common.UnityLogic.GamePlay.Contracts;
using UnityEngine;

namespace Common.UnityLogic.GamePlay
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public sealed class OutLevelZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IOutLevelZoneTriggerable outLevelZoneTriggerable))
            {
                outLevelZoneTriggerable.OnOutLevelZoneEntered();
            }
        }
    }
}