using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;  
using System.IO; 
using System.Net;

public class learn_4 : MonoBehaviour {


	private	string url="http://api.yease.cn/api/place?page=1&oid=1&cid=1&aid=1&cityid=1&glat=1&glng=1&ver=1";


	// Use this for initialization
	void Start () {

		Debug.Log ("请求url、断点续传、解析Json数据序列化的教程");




		//StartCoroutine(waitandprint());


		jsonFromFile();
	}




	public void jsonFromFile()
	{
		TextAsset filePath = Resources.Load("Config",typeof(TextAsset)) as TextAsset;

		PlaceListModel c = JsonUtility.FromJson<PlaceListModel> (filePath.text);

		Place_Info pi = c.Place_List [0];

		Debug.Log("request result :" + c.Place_List.Count);

		Debug.Log("request result :" + pi.Address);


	}





	IEnumerator waitandprint()
	{
		WWW www = new WWW(url);
		yield return www;


		if (www.error != null) 
		{
			Debug.Log("error is :"+ www.error);
		} 
		else
		{

			Debug.Log("resp is :"+ www.text);


			PlaceListModel c = JsonUtility.FromJson<PlaceListModel> (www.text);






			Place_Info pi = c.Place_List [0];

			Debug.Log("request result :" + c.Place_List.Count);

			Debug.Log("request result :" + pi.Address);

		}

	}


	// Update is called once per frame
	void Update () {
	
	}
}
