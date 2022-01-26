using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Core.Util
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        public AudioSource sfxAudioSource;

        // Random pitch adjustment range.
        public float lowPitchRange = 0.95f;
        public float highPitchRange = 1.05f;

        public float Volume { get; set; } = 1.0f;
        private float musicBaseVolume = 0.3f;
        private AudioSource audioSource;

        private Coroutine musicCoroutine;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySound(AudioClip clip, float volume = 1.0f, AudioSource src = null, bool randomizePitch = false)
        {
            float pitch = randomizePitch ? Random.Range(lowPitchRange, highPitchRange) : 1.0f;
            PlaySoundWithPitch(clip, pitch, volume, src);
        }

        public void PlayRandomSound(AudioClip[] clips, float volume = 1.0f, AudioSource src = null)
        {
            if (clips.Length == 0)
                return;

            AudioSource source = src ?? sfxAudioSource;

            int randomIndex = Random.Range(0, clips.Length);

            source.pitch = Random.Range(lowPitchRange, highPitchRange);
            source.PlayOneShot(clips[randomIndex], volume);
        }

        public void PlaySoundAtLocation(AudioClip clip, Vector3 position, float volume = 1.0f)
        {
            if (clip == null) return;
            
            var tempAudioSource = new GameObject("TempAudio");
            tempAudioSource.transform.position = position;
            var src = tempAudioSource.AddComponent<AudioSource>();
            src.clip = clip;
            src.volume = volume;
            src.spatialBlend = 1.0f;
            src.rolloffMode = AudioRolloffMode.Linear;
            src.minDistance = 5.0f;
            src.maxDistance = 15.0f;
            src.dopplerLevel = 0;
            tempAudioSource.AddComponent<Disposable>().lifetime = clip.length;
            src.Play();
        }

        public void PlaySoundWithPitch(AudioClip clip, float pitch, float volume = 1.0f, AudioSource src = null)
        {
            if (clip == null) return;
            
            AudioSource source = src ?? sfxAudioSource;
            source.pitch = pitch;
            source.PlayOneShot(clip, volume);
        }
    }
}