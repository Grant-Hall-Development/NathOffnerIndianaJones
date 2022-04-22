using Base.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Base.Audio
{

    public class AudioManager : Singleton<AudioManager>
    {
        public static event Action<bool> OnMutedChange;

        public AllAudioSO allAudio; 
        public AudioSourceController[] allMusicAudioSources;
        public AudioSourceController basicSFXSource;

        public AudioMixer mixer;

        #region Public

        

        private void Awake()
        {
            base.Awake();
            allMusicAudioSources.ToList().ForEach(s => s.source.Stop());
        }

        private void Start()
        {
            SetMuteValues();
        }

        public void PlaySFX(AudioClipTemplate audioToPlay)
        {
            basicSFXSource.source.PlayOneShot(audioToPlay.clip, audioToPlay.desiredVolume);
            float pitch = audioToPlay.randomisePitch == true ? 1 + Random.Range(-0.2f, 0.2f) : 1;
            basicSFXSource.source.pitch = pitch;
        }
        public void PlaySFX(string soundEffectName)
        {
            AudioClipTemplate audioClip = allAudio.GetAudioClipFromCollection(allAudio.allSFXAudio, soundEffectName);
            if(audioClip == null)
            {
                Pr.Error($"{soundEffectName} couldn't be found. Are you sure the name is correct?");
                return;
            }

            basicSFXSource.source.PlayOneShot(audioClip.clip, audioClip.desiredVolume);
        }

        public void PlayTrack(AudioClipTemplate trackToPlay)
        {
            AudioSourceController playingSource = Return_CurrentlyPlayingSource();

            AudioClip currentTrack = (playingSource != null) ? playingSource.source.clip : null;
            AudioClipTemplate nextTrack = trackToPlay;

            if (playTrack_Coroutine != null)
            {
                StopCoroutine(playTrack_Coroutine);
                playTrack_Coroutine = StartCoroutine(PlayTrackIE(currentTrack, nextTrack));
            }
            else
            {
                playTrack_Coroutine = StartCoroutine(PlayTrackIE(currentTrack, nextTrack));
            }

        }
        public void PlayTrack(string nameOfTrack)
        {

            AudioSourceController playingSource = Return_CurrentlyPlayingSource();

            AudioClip currentTrack = (playingSource != null) ? playingSource.source.clip : null;
            AudioClipTemplate nextTrack = allAudio.GetAudioClipFromCollection(allAudio.allMusicAudio, nameOfTrack);

            if (playTrack_Coroutine != null)
            {
                StopCoroutine(playTrack_Coroutine);
                playTrack_Coroutine = StartCoroutine(PlayTrackIE(currentTrack, nextTrack));
            }
            else
            {
                playTrack_Coroutine = StartCoroutine(PlayTrackIE(currentTrack, nextTrack));
            }

        }

        public void ToggleMusicReduction(bool isMusicQuiet)
        {
            float nextVolume = isMusicQuiet ? 0.1f : 0.16f;
            Return_CurrentlyPlayingSource().source.volume = nextVolume;
        }

        public void ToggleBGMPause(bool isPaused)
        {
            if (isPaused)
                Return_CurrentlyPlayingSource().source.Pause();
            else
                Return_CurrentlyPlayingSource().source.UnPause();

        }

        public void ToggleMute()
        {
            SaveData.IsMuted = !SaveData.IsMuted;
            OnMutedChange?.Invoke(SaveData.IsMuted);
            SetMuteValues();
        }

        public void SetMuteValues()
        {
            if(mixer)mixer.SetFloat("Volume", (SaveData.IsMuted) ? -80 : 0);
        }
        

        #endregion Public

        #region Private
        AudioSourceController Return_FreeAudioSource()
        {
            for (int i = 0; i < allMusicAudioSources.Length; i++)
            {
                if (!allMusicAudioSources[i].source.isPlaying)
                    return allMusicAudioSources[i];
            }
            Debug.LogError("Couldn't find a free music source");
            return null;
        }
        AudioSourceController Return_CurrentlyPlayingSource()
        {
            for (int i = 0; i < allMusicAudioSources.Length; i++)
            {
                if (allMusicAudioSources[i].source.isPlaying)
                    return allMusicAudioSources[i];
            }
            //Debug.LogError("Couldn't find a playing music source");
            return null;
        }

        Coroutine playTrack_Coroutine;
        IEnumerator PlayTrackIE(AudioClip trackToFadeOut = null, AudioClipTemplate trackToFadeIn = null)
        {
            AudioSourceController currentSource = Return_CurrentlyPlayingSource();
            AudioSourceController nextSource = Return_FreeAudioSource();

            StartCoroutine(FadeMusicTrackOutIE(trackToFadeOut, currentSource));
            StartCoroutine(FadeMusicTrackInIE(trackToFadeIn, nextSource));


            playTrack_Coroutine = null;
            yield return null;

        }
        IEnumerator FadeMusicTrackOutIE(AudioClip trackToFadeOut, AudioSourceController currentPlaying)
        {
            if (currentPlaying == null)
                yield break;

            float startVolume = currentPlaying.source.volume;
            float t = startVolume;

            while (t > 0)
            {
                t -= Time.deltaTime * startVolume;
                currentPlaying.source.volume = t;
                yield return null;
            }
            currentPlaying.source.Stop();
        }

        IEnumerator FadeMusicTrackInIE(AudioClipTemplate trackToFadeIn, AudioSourceController nextSource)
        {
            float desiredVolume = trackToFadeIn.desiredVolume;
            float t = 0;

            nextSource.source.volume = t;
            nextSource.source.clip = trackToFadeIn.clip;
            nextSource.source.loop = trackToFadeIn.isLooping;
            nextSource.source.Play();

            while (t < desiredVolume)
            {
                t += desiredVolume * Time.deltaTime;
                nextSource.source.volume = t;
                yield return null;
            }
        }
        #endregion Private
    }
}
