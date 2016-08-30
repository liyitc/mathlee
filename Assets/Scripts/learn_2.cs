using UnityEngine;
using System.Collections;

using DG.Tweening;

public class learn_2 : MonoBehaviour {


	public Transform _target;
	public Transform _cam;


	public float Damping=2.5F;

	//about zoom
	private float zoomSpeed=50f;
	private float zoomMin=2.0f;
	private float zoomMax=10.0f;

	private float Distance = 5.0f;
	private float zoomDifference = 0;

	//about drag
	private float xMoveSpeed=100;
	private float yMoveSpeed=100;

	private float yMoveMinLimit=-10;
	private float yMoveMaxLimit=50;

	private float xMove;
	private float yMove;


	//about Quaternion
	private Quaternion q = Quaternion.identity;
	private Vector3 p = Vector3.zero;


	// Use this for initialization
	void Start () {

		Debug.Log ("触摸操作 Camrea Look At 一个物体的教程");
		_cam.LookAt (_target);

	}


	// Update is called once per frame
	void LateUpdate () {

		//------------------------------------------------------------------------------------------------

		switch (Input.touchCount) {

		case 1:
			{
				//rotate
				Touch touch = Input.GetTouch (0);

				switch (touch.phase) {
				case TouchPhase.Moved:
					{
						Vector2 touchDeltaPostion = touch.deltaPosition;

						xMove += touchDeltaPostion.x * xMoveSpeed * 0.005F;
						yMove -= touchDeltaPostion.y * yMoveSpeed * 0.005F;
						yMove = ClampAngle(yMove,yMoveMinLimit,yMoveMaxLimit);
					}
					break;
				}



				//四元数旋转 测试
				q = Quaternion.AngleAxis (xMove, Vector3.up) * Quaternion.AngleAxis (yMove, Vector3.right);
				p = q * new Vector3(0.0F, 0.0F, -Distance)  + _target.position;
				_cam.rotation = q;
				_cam.position = p; 
				_cam.LookAt(_target);

		
				return;

				//欧拉角 测试
				Quaternion mRotation = Quaternion.Euler(yMove,xMove,0);
				Vector3 mPosition = mRotation * new Vector3(0.0F, 0.0F, -Distance) + _target.position;

				_cam.transform.rotation = mRotation; 
				_cam.transform.position = mPosition;
				_cam.LookAt(_target);





			}
			break;

		case 2:
			{
				//zoom
				Touch touch0 = Input.GetTouch (0);
				Touch touch1 = Input.GetTouch (1);

				float zoomTempDifference;

				if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved) {

					zoomTempDifference = (touch0.position - touch1.position).magnitude;

					if(zoomDifference==0)
						zoomDifference = zoomTempDifference;


					Debug.Log (zoomDifference + "-" + zoomTempDifference);

					Distance = Distance + (zoomDifference - zoomTempDifference) / zoomSpeed;

					zoomDifference = zoomTempDifference;

					Distance = Mathf.Clamp (Distance, zoomMin, zoomMax);

					_cam.transform.position = _cam.transform.rotation * new Vector3 (0.0F, 0.0F, -Distance) + _target.position;
		
				}
					
				if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended) {
					zoomDifference = 0;
				}
			}
			break;
		}			
	}
		
	private float  ClampAngle (float angle,float min,float max) 
	{
		if (angle < -360) angle += 360;
		if (angle >  360) angle -= 360;	
		return Mathf.Clamp (angle, min, max);
	}


}
