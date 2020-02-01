using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreListItem : MonoBehaviour
{
    public void SetScore(long score)
    {
        GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}
