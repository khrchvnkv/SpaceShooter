using NaughtyAttributes;
using UnityEngine;

namespace Common.UnityLogic.Character.Zones
{
    public class MovementZone : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        [SerializeField, MinValue(0.1f)] private float _xOffset;
        [SerializeField, MinValue(0.1f)] private float _yOffset;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            _transform ??= transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            var point1 = new Vector2(_transform.position.x - _xOffset, _transform.position.y - _yOffset);
            var point2 = new Vector2(_transform.position.x - _xOffset, _transform.position.y + _yOffset);
            var point3 = new Vector2(_transform.position.x + _xOffset, _transform.position.y + _yOffset);
            var point4 = new Vector2(_transform.position.x + _xOffset, _transform.position.y - _yOffset);
            
            Gizmos.DrawLine(point1, point2);
            Gizmos.DrawLine(point2, point3);
            Gizmos.DrawLine(point3, point4);
            Gizmos.DrawLine(point4, point1);
        }
#endif

        public Vector3 ClampMovement(Vector3 position, Vector2 delta)
        {
            Vector3 offset = delta;

            var newX = position.x + delta.x;
            var xPos = _transform.position.x;
            if (newX > xPos + _xOffset ||
                newX < xPos - _xOffset)
            {
                offset.x = 0.0f;
            }
            
            var newY = position.y + delta.y;
            var yPos = _transform.position.y;
            if (newY > yPos + _yOffset ||
                newY < yPos - _yOffset)
            {
                offset.y = 0.0f;
            }

            return offset;
        }
    }
}