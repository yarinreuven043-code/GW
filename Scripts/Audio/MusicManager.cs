﻿using UnityEngine;
using System.Collections;

namespace Audio
{
	public class MusicManager : MonoBehaviour
	{
		private AudioSource audioSource;
		private Coroutine currentRoutine;

		[Header("Settings")]
		[Tooltip("Default fade duration if none is specified in PlayMusic()")]
		public float fadeDuration = 3f;

		[Header("Default Tracks")]
		public AudioClip menuTheme;

		void Awake()
		{		
			audioSource = GetComponent<AudioSource>();
			
			if (audioSource == null)
				audioSource = gameObject.AddComponent<AudioSource>();

			audioSource.loop = true;
			audioSource.playOnAwake = false;
			audioSource.clip = null;
		}

		public void PlayMusic(AudioClip clip, float customFadeIn = -1f)
		{
			if (clip == null) 
				return;

			float fadeInTime = (customFadeIn > 0) ? customFadeIn : fadeDuration;

			if (audioSource.isPlaying && audioSource.clip != null)
			{
				SwitchMusic(clip, fadeInTime);
			}
			else
			{
				if (currentRoutine != null)
					StopCoroutine(currentRoutine);

				currentRoutine = StartCoroutine(FadeInMusic(clip, fadeInTime));
			}
		}

		public void SwitchMusic(AudioClip newClip, float customFadeIn = -1f)
		{
			if (newClip == null) return;

			float fadeInTime = (customFadeIn > 0) ? customFadeIn : fadeDuration;

			if (currentRoutine != null)
				StopCoroutine(currentRoutine);

			currentRoutine = StartCoroutine(FadeOutIn(newClip, fadeInTime));
		}

		IEnumerator FadeInMusic(AudioClip clip, float fadeTime)
		{
			audioSource.clip = clip;
			audioSource.volume = 0f;
			audioSource.Play();

			float t = 0f;
			while (t < fadeTime)
			{
				t += Time.deltaTime;
				audioSource.volume = Mathf.Lerp(0f, 1f, t / fadeTime);
				yield return null;
			}

			audioSource.volume = 1f;
		}

		IEnumerator FadeOutIn(AudioClip newClip, float fadeTime)
		{
			float t = 0f;
			float startVolume = audioSource.volume;

			while (t < fadeTime)
			{
				t += Time.deltaTime;
				audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
				yield return null;
			}

			audioSource.Stop();
			audioSource.volume = 0f;

			yield return StartCoroutine(FadeInMusic(newClip, fadeTime));
		}
	}
}