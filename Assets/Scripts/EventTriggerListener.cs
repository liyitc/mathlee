using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


public class EventTriggerListener : EventTrigger {

	public delegate void VoidDelegate (GameObject go);


	public VoidDelegate onPointerDown;
	public VoidDelegate onPointerUp;
	public VoidDelegate onPointerClick;
	public VoidDelegate onPointerEnter;
	public VoidDelegate onPointerExit;

	static public EventTriggerListener FindGameObjectWithName (string name)
	{

		GameObject go = GameObject.Find (name);

		//if (go == null)
			

		EventTriggerListener listener =  go.GetComponent<EventTriggerListener>();


		if (listener == null)
			listener = go.AddComponent<EventTriggerListener>();
	
		return listener;
	}


	public override void OnPointerClick (PointerEventData eventData){
		if(onPointerClick != null) onPointerClick(gameObject);
	}

	public override void OnPointerDown (PointerEventData eventData){
		if(onPointerDown != null) onPointerDown(gameObject);
	}
		
	public override void OnPointerUp (PointerEventData eventData){
		if(onPointerUp != null) onPointerUp(gameObject);
	}

	public override void OnPointerEnter (PointerEventData eventData){
		if(onPointerEnter != null) onPointerEnter(gameObject);
	}

	public override void OnPointerExit (PointerEventData eventData){
		if(onPointerExit != null) onPointerExit(gameObject);
	}





}
