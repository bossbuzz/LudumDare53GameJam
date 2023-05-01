using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private Queue<AudioSource> _audioSources;
        private AudioSource _musicSource;
        [SerializeField] private AudioClip flapClip;
        [SerializeField] private AudioClip landClip;
        [SerializeField] private AudioClip bounceClip;
        [SerializeField] private AudioClip throwClip;
        [SerializeField] private AudioClip eggLandClip;
        [SerializeField] private AudioClip crackClip;
        [SerializeField] private AudioClip finishLevelClip;
        [SerializeField] private AudioClip levelMusic;
        [SerializeField] private AudioClip titleMusic;
        private static AudioManager currentManager;
        
        public static AudioClip FlapClip => AM.flapClip;
        public static AudioClip LandClip => AM.landClip;
        public static AudioClip BounceClip => AM.bounceClip;
        public static AudioClip ThrowClip => AM.throwClip;
        public static AudioClip EggLandClip => AM.eggLandClip;
        public static AudioClip CrackClip => AM.crackClip;
        public static AudioClip FinishLevelClip => AM.finishLevelClip;
        public static AudioClip LevelMusic => AM.levelMusic;
        private static AudioClip TitleMusic => AM.titleMusic;
        
        private void Awake()
        {
            AudioSource[] sources = GetComponentsInChildren<AudioSource>();
            _audioSources = new Queue<AudioSource>(sources);
            if (currentManager is null || currentManager)
            {
                currentManager = this;
            }
            else Destroy(this);
        }
        
        public static AudioManager AM
        {
            get
            {
                if (currentManager is null)
                {
                    currentManager = FindObjectOfType<AudioManager>();
                }
                return currentManager;
            }
        }

        private void OnDestroy()
        {
            currentManager = null;
        }

        public static void PlayClip(AudioClip clip, float volume = 1, float pitch = 1)
        {
            AM.PlayAudioClip(clip,volume,pitch);
        }

        private void PlayAudioClip(AudioClip clip,float volume = 1,float pitch = 1)
        {
            AudioSource nextSource = _audioSources.Dequeue();
            _audioSources.Enqueue(nextSource);
            nextSource.clip = clip;
            nextSource.volume = volume;
            nextSource.pitch = pitch;
            nextSource.Play();
        }
        
    }
}