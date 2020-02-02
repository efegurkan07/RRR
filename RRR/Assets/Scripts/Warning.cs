using System;
using TMPro;
using UnityEngine;

public class Warning : MonoBehaviour
{
	public TextMeshPro[] texts;

	private void Update()
	{
		foreach (var t in texts)
		{
			t.outlineWidth = Mathf.Abs(Mathf.Sin(Mathf.Rad2Deg * Time.time / 10f)) * 0.3f;
			t.color = new Color(0.5f + ((Mathf.Sin(Mathf.Rad2Deg * Time.time / 5f) + 1f) / 2f) * 0.5f, 0, 0);
		}
	}
}