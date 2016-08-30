using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;



public class UIImage : UIView {

	private Image image_component;
	public Transform transform;

	public UIImage(GameObject view)
	{
		obj = new GameObject ();
		obj.name = "No Name";
		image_component = obj.AddComponent <Image>();
		obj.transform.SetParent(view.transform,false);

		obj_rt = obj.GetComponent<RectTransform> ();
		obj_rt.localScale = new Vector3 (1f, 1f, 1f);
	
		view_rt = view.GetComponent<RectTransform> ();
		view_w = view_rt.rect.width;
		view_h = view_rt.rect.height;

		transform = obj.GetComponent<Transform> ();

	}

	public float width
	{
		get{
			return	obj_rt.sizeDelta.x;
		}
	}

	public float height
	{
		get{
			return	obj_rt.sizeDelta.y;
		}
	}
		
	public string name
	{
		get{
			return obj.name;
		}

		set{
			obj.name = value;
		}
	}
		
	public void LoadImageFromResources(string path)
	{
		Sprite sprite = Resources.Load (path,typeof(Sprite)) as Sprite;

		if (sprite == null) {
			Debug.LogError ("iImage LoadImageFromResources:failed");
		}

		image_component.sprite = sprite;
		obj_rt.sizeDelta =new  Vector2 (sprite.rect.width, sprite.rect.height);

	}

	public void LoadImageFromAssetBundle (string path)
	{
		
	}
		
	public GameObject gameobject
	{
		get {
			return obj;
		}
	}
		
	public Vector2 center
	{

		set{
			float centerX = value.x - view_w / 2;
			float centerY = view_h / 2 - value.y;
			obj_rt.localPosition = new Vector3 (centerX, centerY, 0f);
		}
			
		get{

			float centerX = obj_rt.localPosition.x + view_w / 2;
			float centerY = view_h / 2 - obj_rt.localPosition.y;

			return new Vector2(centerX, centerY);
		}

	}
		
	public Vector2 postion
	{
		set{

			float centerX = value.x - view_w / 2;
			float centerY = view_h / 2 - value.y;

			float postionX = centerX + image_component.sprite.rect.width / 2;
			float postionY = centerY - image_component.sprite.rect.height / 2;

			obj_rt.localPosition = new Vector3 (postionX, postionY, 0f);

		}

		get{
		
			float centerX = obj_rt.localPosition.x;
			float centerY = obj_rt.localPosition.y + view_rt.rect.height;


			return new Vector2(centerX,centerY);
		}

	}

}
