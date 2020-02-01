using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
	private static Camera _mainCamera;
	private static LayerMask _clickMask;
	private static Nullable<RaycastHit> _clickStart;
	private static Nullable<RaycastHit> _clickEnd;

	private void Start()
	{
		_mainCamera = Camera.main;
		_clickMask = LayerMask.GetMask("clickable");
	}

	private void Update()
	{
		//collect click information 
		if (Input.GetMouseButtonDown(0))
		{
			_clickStart = CastRay();
			_clickEnd = null;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			_clickEnd = CastRay();
		}
		else if (!Input.GetMouseButton(0))
		{
			_clickStart = null;
			_clickEnd = null;
		}

		//check what is clicked and do awesome things:
		
		//drag robot to lane
		{
			Robot robot = _clickStart?.collider?.GetComponent<Robot>();
			Lane lane = _clickEnd?.collider?.GetComponent<Lane>();
			if (robot != null && lane != null)
			{
				robot.currentLane = lane;
			}
		}
		{
			Robot robot = _clickStart?.collider?.GetComponent<Robot>();
			Robot sameRobot = _clickEnd?.collider?.GetComponent<Robot>();
			if (robot != null && sameRobot != null && robot.Equals(sameRobot))
			{
				FindObjectOfType<GameUIHandler>().ShowRepairOverlay(robot);
			}
		}
	}

	private RaycastHit CastRay()
	{
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		Physics.Raycast(ray, out hitInfo, Mathf.Infinity, _clickMask);
		return hitInfo;
	}
}