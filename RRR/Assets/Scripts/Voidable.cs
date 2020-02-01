using System;
using UnityEngine;

public class Voidable : MonoBehaviour
{
	private LayerMask _mask;
	private Vector3 _oldPosition;

	private void Start()
	{
		_mask = LayerMask.GetMask("thevoid");
		_oldPosition = transform.position;
	}

	private void Update()
	{
		var direction = transform.position - _oldPosition;
		if (Physics.Raycast(_oldPosition, direction, direction.magnitude, _mask))
		{
			Destroy(gameObject);
		}
	}
}