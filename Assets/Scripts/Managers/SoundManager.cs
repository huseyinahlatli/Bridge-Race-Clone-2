using Singleton;
using UnityEngine;

namespace Managers
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private GameObject player;
        [Space]
        [Header("Audio Clips")] 
        public AudioClip backgroundMusic;
        public AudioClip addStackSound;
        public AudioClip removeStackSound;
        public AudioClip winSound;
        public AudioClip loseSound;

        private AudioSource _audioSource;
        private const float MediumVolume = .6f;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip targetAudio)
        {
            _audioSource.PlayOneShot(targetAudio, MediumVolume);
        }
    }
}
