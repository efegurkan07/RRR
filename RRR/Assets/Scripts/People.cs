using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
	public class People : MonoBehaviour
	{
		[SerializeField] AudioClip[] TriggerSounds;

		private void Start()
		{
			GetComponent<Animator>().SetTrigger("HeavensCalling");
			var audioPlayer = GetComponent<AudioSource>();

			int randomIndex = Random.Range(0, TriggerSounds.Length);
			audioPlayer.clip = TriggerSounds[randomIndex];
			audioPlayer.Play();
		}
	}
}