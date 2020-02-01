using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairUIHandler : MonoBehaviour
{
	public void Show(Robot robot)
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
}
