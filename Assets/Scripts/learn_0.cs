using UnityEngine;
using System.Collections;

public class learn_0 : MonoBehaviour {


	public Transform ob0;//被看物体

	public Transform player;
	public GameObject viewpoint;//视野前方的模型点

	public float speed=5.0f;


	// Use this for initialization
	void Start () {

		Debug.Log ("点积的学习");


	}
	
	// Update is called once per frame
	void Update () {

		//移动物体
		if (Input.GetKey(KeyCode.A)) {
			player.Translate (Vector3.left * speed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D)) {
			player.Translate (Vector3.left * -speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.W)) {
			
			player.Translate (Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.S)) {

			player.Translate (Vector3.forward * -speed * Time.deltaTime);
		}


		//移动视野

		if (Input.GetKey(KeyCode.LeftArrow)) {
			player.Rotate (Vector3.up * -speed * Time.deltaTime);
		}


		if (Input.GetKey(KeyCode.RightArrow)) {
			player.Rotate (Vector3.up * speed * Time.deltaTime);
		}

		//视野长度
		Vector3 v0 = new Vector3 (0, 0, 5);

		//四元数 * 向量 ＝ 向量（旋转并移动）
		v0 = player.rotation * v0;
			
		//视野前方的位置
		Vector3 v1 = v0 + player.position; 


		Vector3 lookat = v1 - player.position;//目光
		lookat.y = 0;
		lookat.Normalize();



		//画出第一个向量 
		//Vector3 forward = player.TransformDirection(Vector3.forward) * 5;
		//Debug.DrawRay (player.position,forward,Color.red);
		//Debug.DrawLine (player.position, v1 ,Color.red);

		//加入视野点
		//viewpoint.transform.position = v1;


		//画出第二个向量 
		Debug.DrawLine (player.position, ob0.position ,Color.red);
		Vector3 cangNormal = (ob0.position - player.position).normalized;

		//画个扇形视角
		Vector3 v2 = Quaternion.AngleAxis (30, Vector3.up) * v0 + player.position;
		Debug.DrawLine (player.position, v2 ,Color.yellow);

		Vector3 v3 = Quaternion.AngleAxis (-30, Vector3.up) * v0 + player.position;
		Debug.DrawLine (player.position, v3 ,Color.yellow);

		Debug.DrawLine (v3, v2 ,Color.yellow);

		//判断两个向量的夹角是否小于30度
		if(Vector3.Dot(cangNormal,lookat)<Mathf.Cos(Mathf.PI/6.0f))
		{
			ob0.GetComponent<Renderer> ().material.color = Color.white;
		}
		else
		{
			ob0.GetComponent<Renderer> ().material.color = Color.red;
		}


	}
}
