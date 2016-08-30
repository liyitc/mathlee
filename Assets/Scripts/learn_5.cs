using UnityEngine;
using System.Collections;

public class learn_5 : MonoBehaviour
{


	//about zoom
	private float zoomSpeed = 100f;
	private float zoomMin = 1.0f;
	private float zoomMax = 2.0f;

	private float Distance = 5.0f;
	private float zoomDifference = 0;

	//about drag
	private float xMoveSpeed = 100;
	private float yMoveSpeed = 100;

	private float yMoveMinLimit = -10;
	private float yMoveMaxLimit = 50;

	private float xMove;
	private float yMove;


	//about Quaternion
	private Quaternion q = Quaternion.identity;

	private Quaternion tq;

	private Vector3 p = Vector3.zero;


	private bool moved,selected;





	//test



	Vector3 _vec3TargetScreenSpace;
	Vector3 _vec3TargetWorldSpace;
	Vector3 _vec3Offset;
	Vector3 _vec3MouseScreenSpace;


	private Vector2 nowFingerPos, startFingerPos;
	private float xMoveDistance, yMoveDistance;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("触摸拖动、旋转与放大缩小一个物体的教程");

	}

	// Update is called once per frame
	void Update ()
	{

	}


	void LateUpdate ()
	{

		switch (Input.touchCount) {

		case 1:
			{

				//rotate
				Touch touch = Input.GetTouch (0);

				switch (touch.phase) {

				case TouchPhase.Began:
					{

						Debug.Log ("Began");

						Ray ray = Camera.main.ScreenPointToRay (touch.position);
						RaycastHit hit;

						if (Physics.Raycast (ray, out hit, 10000)) {
							
							_vec3TargetScreenSpace = Camera.main.WorldToScreenPoint(this.transform.position);  

							// 存储鼠标的屏幕空间坐标（Z值使用目标物体的屏幕空间坐标）   

							_vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);  

							// 计算目标物体与鼠标物体在世界空间中的偏移量   

							_vec3Offset = this.transform.position - Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace);  


							selected = true;
							moved = false;


						} else {
							selected = false;
							tq = this.transform.rotation;
							xMove = 0;
						}



					}
					break;

				case TouchPhase.Ended:
					{
						if(selected)
							Debug.Log (moved?"拖动了":"只是点击");
					}
					break;

				case TouchPhase.Moved:
					{

						if(selected)
						{
							moved = true;

							Debug.Log ("Moved");

							_vec3MouseScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _vec3TargetScreenSpace.z);  

							// 把鼠标的屏幕空间坐标转换到世界空间坐标（Z值使用目标物体的屏幕空间坐标），加上偏移量，以此作为目标物体的世界空间坐标  

							_vec3TargetWorldSpace = Camera.main.ScreenToWorldPoint(_vec3MouseScreenSpace) + _vec3Offset;                

							// 更新目标物体的世界空间坐标   

							this.transform.position = _vec3TargetWorldSpace;  
						}
						else
						{
							Vector2 touchDeltaPostion = touch.deltaPosition;
							xMove += touchDeltaPostion.x * xMoveSpeed * 0.005F;
							q = Quaternion.AngleAxis (-xMove, Vector3.up);
							this.transform.rotation = tq * q;
						}


					}
					break;
				}
					
			}
			break;

		case 2:
			{
				//zoom
				Touch touch0 = Input.GetTouch (0);
				Touch touch1 = Input.GetTouch (1);

				float zoomTempDifference;

				if (touch0.phase == TouchPhase.Began||touch1.phase == TouchPhase.Began) {
				
					Distance = this.transform.localScale.x;
				
					selected = false;

				}

				if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved) {

					zoomTempDifference = (touch0.position - touch1.position).magnitude;

					if (zoomDifference == 0)
						zoomDifference = zoomTempDifference;

					Distance = Distance - (zoomDifference - zoomTempDifference) / zoomSpeed;

					Debug.Log (zoomDifference + "-" + zoomTempDifference + "=" + Distance);

					zoomDifference = zoomTempDifference;

					Distance = Mathf.Clamp (Distance, zoomMin, zoomMax);


					this.transform.localScale = new Vector3 (Distance, Distance, Distance);

				}

				if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended) {
					zoomDifference = 0;
				}
			}
			break;
		}			
	}


	private float  ClampAngle (float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;	
		return Mathf.Clamp (angle, min, max);
	}

}
