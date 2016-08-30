using UnityEngine;
using System.Collections;

public class learn_1 : MonoBehaviour
{


	public Transform _target;
	public Transform _cam;

	//观察距离
	public float Distance = 5F;




	//about zoom
	private float zoomSpeed=50f;
	private float zoomMin=2.0f;
	private float zoomMax=10.0f;


	//about drag
	private float xMoveSpeed=100;
	private float yMoveSpeed=100;

	private float yMoveMinLimit=-10;
	private float yMoveMaxLimit=50;

	private float xMove;
	private float yMove;



	// Use this for initialization
	void Start ()
	{
		Debug.Log ("鼠标操作教程");
	}

	// Update is called once per frame
	void LateUpdate ()
	{

		if (Input.GetMouseButton (1)) {
	

			xMove += Input.GetAxis("Mouse X") * xMoveSpeed * 0.1F;
			yMove -= Input.GetAxis("Mouse Y") * yMoveSpeed * 0.1F;
			yMove = ClampAngle(yMove,yMoveMinLimit,yMoveMaxLimit);


			Quaternion mRotation = Quaternion.Euler(yMove,xMove,0);
			Vector3 mPosition = mRotation * new Vector3(0.0F, 0.0F, -Distance) + _target.position;

			_cam.transform.rotation = mRotation; 
			_cam.transform.position = mPosition;


		} else if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			
			Distance = Distance - Input.GetAxis ("Mouse ScrollWheel");
			Distance = Mathf.Clamp (Distance, zoomMin, zoomMax);
			_cam.transform.position = _cam.transform.rotation * new Vector3 (0.0F, 0.0F, -Distance) + _target.position;
		} 
			
		_cam.LookAt(_target);

	}


	private float ClampAngle (float angle , float min ,float  max) 
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
