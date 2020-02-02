using System;
using DefaultNamespace;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
	private static Camera _mainCamera;
	private static LayerMask _clickStartMask;
	private static LayerMask _clickEndMask;
	private static Nullable<RaycastHit> _clickStart;
	private static Nullable<RaycastHit> _clickEnd;

	private void Start()
	{
		_mainCamera = Camera.main;
		_clickStartMask = LayerMask.GetMask("clickable");
		_clickEndMask = LayerMask.GetMask("lanes");
	}

	int clicked = 0;
	float clicktime = 0;
	float clickdelay = 0.5f;
	bool doubleClick = false;
	
	private void Update()
	{
		if (GameManager.Instance.CurrentState != GameState.DriveUnicorn)
		{
			return;
		}
		
		//collect click information 
		if (Input.GetMouseButtonDown(0))
		{
			_clickStart = CastRay(_clickStartMask);
			_clickEnd = null;
			
			clicked++;
			if (clicked == 1) clicktime = Time.time;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			_clickEnd = CastRay(_clickEndMask);
		}
		else if (!Input.GetMouseButton(0))
		{
			_clickStart = null;
			_clickEnd = null;
		}
		
		if (clicked > 1 && Time.time - clicktime < clickdelay)
		{
			clicked = 0;
			clicktime = 0;
			doubleClick = true;
		} 
		else if (clicked > 2 || Time.time - clicktime > 1)
		{
			clicked = 0;
			doubleClick = false;
		}

		//check what is clicked and do awesome things:

		//drag robot to lane
		{
			Robot robot = _clickStart?.collider?.GetComponent<Robot>();
			Lane lane = _clickEnd?.collider?.GetComponent<Lane>();
			if (robot != null && lane != null)
			{
				Vector3 clickStartPoint = _clickStart?.point ?? Vector3.zero;
				Vector3 clickEndPoint = _clickEnd?.point ?? Vector3.zero;
				
				var startWorldPoint = Camera.main.WorldToScreenPoint(clickStartPoint);
				var EndWorldPoint = Camera.main.WorldToScreenPoint(clickEndPoint);
				var swipeDistance = (EndWorldPoint - startWorldPoint).magnitude;

				if (swipeDistance > 10)
				{
					Debug.Log("magnitude " + swipeDistance);
					robot.currentLane = lane;

					_clickStart = null;
					_clickEnd = null;
				}
			}
		}
		{
			Robot robot = _clickStart?.collider?.GetComponent<Robot>();
			if (doubleClick && robot != null)
			{
				FindObjectOfType<GameUIHandler>().ShowRepairOverlay(robot);
				
				_clickStart = null;
				_clickEnd = null;
			}
		}
	}

	private RaycastHit CastRay(LayerMask mask)
	{
		Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask);
		return hitInfo;
	}
}