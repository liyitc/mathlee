using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

public class learn_3 : MonoBehaviour {


	private RectTransform rt;

	Vector2 v0,v1;


	Vector3 v2,v3;

	// Use this for initialization
	void Start () {

		Debug.Log ("uGUI,DoTween教程");


		EventTriggerListener.FindGameObjectWithName ("bt1").onPointerDown = onDown;
		EventTriggerListener.FindGameObjectWithName ("bt1").onPointerUp = onUp;


		EventTriggerListener.FindGameObjectWithName ("bt2").onPointerUp = onUp;

		rt = GameObject.Find ("Image").GetComponent <RectTransform>();

		v0 = rt.anchoredPosition;
		v1 = rt.anchoredPosition + new Vector2 (0, 500);
	
		v2 = rt.localScale/2;
		v3 = rt.localScale;

		rt.localScale = v2;



		for (int i = 0; i < 3; i++) {
			Image im0 =  GameObject.Find ("Image"+i).GetComponent<Image>();
			im0.material.color = new Color (1, 1, 1, 1);
		}


	} 


	IEnumerator ShowDialog()
	{
		while(true){
			Debug.Log ("aaaa");
			yield return null;
		}
	}



	private void onDown(GameObject go){
		//Debug.Log ("onDown:" + go.name);

	}

	private void onExit(GameObject go){
		//Debug.Log ("onExit:" + go.name);
	}

	private void onUp(GameObject go){
		Debug.Log ("onUp:" + go.name);


		for (int i = 0; i < 3; i++) {
			RectTransform rt0 =  GameObject.Find ("Image"+i).GetComponent <RectTransform>();
			rt0.DOScale (new Vector3 (2, 2, 1), .5f).SetDelay(i/6.0f);
		

			Image im0 =  GameObject.Find ("Image"+i).GetComponent<Image>();
			im0.material.color = new Color (1, 1, 1, 1);
		}
			
		//Tweener tweener = rt.DOScale (new Vector3 (5, 5, 1), 2).OnComplete(fuck);

		return;

		//rt.gameObject.SetActive (false);

		StopAllCoroutines ();

		if (go.name == "bt1") {

			StartCoroutine (move (true));

		} else {

			StartCoroutine (move (false));

		}
			

	}


	void fuck()
	{
		rt.DOScale (new Vector3 (1, 1, 1), 2);
	}


	private IEnumerator move(bool b)
	{
		if (b) {
			
			while (rt.anchoredPosition.y < v1.y) {
				rt.localScale = Vector3.Lerp (rt.localScale, v3, Time.deltaTime * 10);
				rt.anchoredPosition = Vector2.Lerp (rt.anchoredPosition, v1, Time.deltaTime*10);
				yield return new WaitForSeconds (Time.deltaTime);
			}
		} else {

			while (rt.anchoredPosition.y > v0.y)
			{
				rt.localScale = Vector3.Lerp (rt.localScale, v2, Time.deltaTime * 10);
				rt.anchoredPosition = Vector2.Lerp (rt.anchoredPosition, v0,Time.deltaTime*10);
				yield return new WaitForSeconds(Time.deltaTime);
			}
		}
			

	}


	private void onEnter(GameObject go){
		Debug.Log ("onEnter:" + go.name);

	}
		
	// Update is called once per frame
	void Update () {


	}



}
