using System;
using System.Collections.Generic;
using UnityEngine;

namespace PxlSq.Game
{
    public enum SfxType
    {
        CardFlip,
        CardMatch,
        CardMisMatchSfx,
        GameOver
    }

    /// <summary>
    /// Manages all audio related logic.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioConfig _audioConfig;

        private List<AudioSource> _audioSourcePool = new();
        private Dictionary<int, AudioClip> _sfxClipsMap = new();

        /// <summary>
        /// Plays and sfx based on sfxtype.
        /// </summary>
        /// <param name="sfxType">SfxType</param>
        /// <exception cref="ArgumentNullException">Throws an exception when it cannot find the sfx type.</exception>
        public void PlaySfx(SfxType sfxType)
        {
            var audioSource = GetPooledAudioSource();

            if (!_sfxClipsMap.TryGetValue((int)sfxType, out var audioClip))
            {
                throw new ArgumentNullException(nameof(sfxType), "Unable to find sfx type on Audio Config.");
            }

            audioSource.volume = _audioConfig.Volume;
            audioSource.PlayOneShot(audioClip);
        }

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            foreach (var audioSource in _audioSourcePool)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.enabled = false;
                }
            }
        }

        /// <summary>
        /// Gets an audio source from a pool of audio sources.
        /// Creates a new one if it cannot find one.
        /// </summary>
        /// <returns>Audio Source</returns>
        private AudioSource GetPooledAudioSource()
        {
            foreach (var audioSource in _audioSourcePool)
            {
                if (!audioSource.enabled)
                {
                    audioSource.enabled = true;
                    return audioSource;
                }
            }

            var audioSrc = gameObject.AddComponent<AudioSource>();
            _audioSourcePool.Add(audioSrc);
            return audioSrc;
        }

        /// <summary>
        /// Initializes sfx clip mapping.
        /// </summary>
        private void Initialize()
        {
            _sfxClipsMap[(int)SfxType.CardFlip] = _audioConfig.CardFlipSfx;
            _sfxClipsMap[(int)SfxType.CardMatch] = _audioConfig.CardMatchSfx;
            _sfxClipsMap[(int)SfxType.CardMisMatchSfx] = _audioConfig.CardMisMatchSfx;
            _sfxClipsMap[(int)SfxType.GameOver] = _audioConfig.GameOverSfx;
        }
    }
}
