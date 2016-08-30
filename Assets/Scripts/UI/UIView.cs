using UnityEngine;
using System.Collections;

public abstract class UIView  {

	//parent
	protected GameObject view;
	protected float view_w;
	protected float view_h;
	protected RectTransform view_rt;

	//baseobj
	protected GameObject obj;
	protected RectTransform obj_rt;


	public Vector2 CenterToUnity(Vector2 center)
	{
		float view_w = view_rt.rect.width;
		float view_h = view_rt.rect.height;

		float x = center.x - view_w / 2;
		float y = view_h / 2 - center.y;

		return new Vector2 (x, y);
	}


	public Vector2 PostionToUnity(Vector2 postion)
	{
		float view_w = view_rt.rect.width;
		float view_h = view_rt.rect.height;

		float x = postion.x - view_w / 2 + obj_rt.rect.width / 2;
		float y = view_h / 2 - postion.y - obj_rt.rect.height / 2;

		return new Vector2 (x, y);
	}

}
