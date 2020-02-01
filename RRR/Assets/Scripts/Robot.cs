using System.Linq;
using UnityEngine;

public class Robot : MonoBehaviour
{
	public Lane currentLane;

	private void Start()
	{
		currentLane = FindObjectsOfType<Lane>().OrderBy(x => Mathf.Abs(x.transform.position.z - transform.position.z))
			.First();
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(
			transform.position,
			new Vector3(transform.position.x, transform.position.y, currentLane.transform.position.z),
			Time.deltaTime
		);
	}
}