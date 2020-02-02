using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Emoji : MonoBehaviour
{
    public TextMeshPro[] texts;

    private void Update()
    {
        foreach (var t in texts)
        {
            t.outlineWidth = Mathf.Abs(Mathf.Sin(Mathf.Rad2Deg * Time.time / 20f)) * 0.2f;
        }
    }
}
