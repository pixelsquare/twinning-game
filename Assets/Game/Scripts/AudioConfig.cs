using UnityEngine;

namespace PxlSq.Game
{
    [CreateAssetMenu(menuName = "PxlSq/Configs/AudioConfig", fileName = "AudioConfig")]
    public class AudioConfig : ScriptableObject
    {
        [Header("General")]
        [Range(0, 100)]
        [SerializeField] private uint _volume = 100;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip _cardFlipSfx;
        [SerializeField] private AudioClip _cardMatchSfx;
        [SerializeField] private AudioClip _cardMisMatchSfx;
        [SerializeField] private AudioClip _gameOverSfx;

        public float Volume => _volume / 100f;

        public AudioClip CardFlipSfx => _cardFlipSfx;
        public AudioClip CardMatchSfx => _cardMatchSfx;
        public AudioClip CardMisMatchSfx => _cardMisMatchSfx;
        public AudioClip GameOverSfx => _gameOverSfx;
    }
}
