using System;
using UnityEngine;

namespace DefaultNamespace
{
	
	public class People : MonoBehaviour
	{
		private void Start()
		{
			GetComponent<Animator>().SetTrigger("HeavensCalling");
		}
	}
}