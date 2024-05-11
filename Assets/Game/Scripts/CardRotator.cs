using UnityEngine;
using UnityEngine.Events;

namespace PxlSq.Game
{
    /// <summary>
    /// Handles the card flipping animation.
    /// </summary>
    public class CardRotator : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Transform _root;

        public bool IsRotating => _isRotating;

        public event UnityAction OnRotationFinished;

        private float _timer = 0f;
        private Quaternion _startRotation;
        private Quaternion _targetRotation;

        private bool _toggleRotation = false;
        private bool _isRotating = false;

        private readonly Vector3 FrontFaceRotation = Vector3.up * -180f;
        private readonly Vector3 BackFaceRotation = Vector3.zero;

        /// <summary>
        /// Rotates the card with a toggle flag.
        /// Which flips the card front and back facing.
        /// </summary>
        public void Rotate()
        {
            if (!_toggleRotation)
            {
                StartRotating(_duration, FrontFaceRotation);
            }
            else
            {
                StartRotating(_duration, BackFaceRotation);
            }

            _toggleRotation = !_toggleRotation;
        }

        /// <summary>
        /// Start a linear rotation for the card.
        /// </summary>
        /// <param name="duration">Duration of the animation</param>
        /// <param name="targetRotation">Target rotation in euler angle</param>
        private void StartRotating(float duration, Vector3 targetRotation)
        {
            if (_isRotating)
            {
                _timer = 0f;
                _isRotating = false;
                OnRotationFinished?.Invoke();
            }

            _isRotating = true;
            _timer = duration;
            _duration = duration;
            _startRotation = _root.rotation;
            _targetRotation = Quaternion.Euler(targetRotation);
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                _root.rotation = Quaternion.Lerp(_startRotation, _targetRotation, 1f - (_timer / _duration));

                if  (_timer <= 0)
                {
                    _timer = 0f;
                    _isRotating = false;
                    OnRotationFinished?.Invoke();
                }
            }
        }
    }
}
