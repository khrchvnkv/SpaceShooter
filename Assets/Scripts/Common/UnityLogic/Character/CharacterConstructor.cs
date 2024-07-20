using Common.UnityLogic.Character.Data;
using UnityEngine;

namespace Common.UnityLogic.Character
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(CharacterShooting))]
    public class CharacterConstructor : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private CharacterShooting _characterShooting;

        private void OnValidate()
        {
            _characterMovement ??= gameObject.GetComponent<CharacterMovement>();
            _characterShooting ??= gameObject.GetComponent<CharacterShooting>();
        }

        public void Initialize(CharacterModel model)
        {
            _characterMovement.SetupMovementSpeed(model.MovementSpeed);
            _characterShooting.SetModel(model);
        }
    }
}