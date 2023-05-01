using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private Queue<AudioSource> _audioSources;
        [SerializeField] private AudioClip flapClip;
        [SerializeField] private AudioClip landClip;
        [SerializeField] private AudioClip bounceClip;
        private static AudioManager currentManager;
        
        public static AudioClip FlapClip => currentManager.flapClip;
        public static AudioClip LandClip => currentManager.landClip;
        public static AudioClip BounceClip => currentManager.bounceClip;
        private void Awake()
        {
            AudioSource[] sources = GetComponentsInChildren<AudioSource>();
            _audioSources = new Queue<AudioSource>(sources);
            if (currentManager is null)
            {
                DontDestroyOnLoad(this);
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

        public static void PlayClip(AudioClip clip)
        {
            AM.PlayAudioClip(clip);
        }

        private void PlayAudioClip(AudioClip clip)
        {
            AudioSource nextSource = _audioSources.Dequeue();
            _audioSources.Enqueue(nextSource);
            nextSource.clip = clip;
            nextSource.Play();
        }
        
    }
}