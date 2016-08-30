using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;


public class UIScrollView : UIView {

	//private GameObject scrollview;
	private GameObject viewport;
	private GameObject _content;

	private ScrollRect scrollview_sr;

	//private RectTransform scrollview_rt;
	private RectTransform viewport_rt;
	private RectTransform content_rt;

	private Image scrollview_image;

//	private readonly RectTransform view_rt;
//	private readonly float view_w, view_h;


	public Transform transform;

	public UIScrollView(GameObject view)
	{
		
		//view 
		view_rt = view.GetComponent<RectTransform> ();
		view_w = view_rt.rect.width;
		view_h = view_rt.rect.height;

		obj = new GameObject ();
		scrollview_sr= obj.AddComponent <ScrollRect>();
		obj.AddComponent <Image>();
		obj.transform.SetParent(view.transform,false);
		obj_rt = obj.GetComponent<RectTransform> ();
		scrollview_image = obj.GetComponent<Image> ();
		obj.name="No Name";
		obj_rt.localScale = new Vector3 (1f, 1f, 1f);

		//mask
		viewport = new GameObject ();
		viewport.AddComponent <Image>();
		Mask m = viewport.AddComponent <Mask>();
		m.showMaskGraphic = false;
		viewport.name = "Viewport";
		viewport.transform.SetParent(obj.transform,false);
		viewport_rt = viewport.GetComponent<RectTransform> ();


		_content = new GameObject ();
		_content.name = "Content";
		content_rt = _content.AddComponent<RectTransform> ();
		_content.transform.SetParent(viewport.transform,false);
		content_rt.anchorMax = new Vector2 (0, 1);
		content_rt.anchorMin = new Vector2 (0, 1);

	

		scrollview_sr.content = content_rt;
		scrollview_sr.viewport = viewport.GetComponent<RectTransform> ();


		transform = obj.GetComponent<Transform> ();

	}

	public float backgroundAlpha
	{
		set{
			scrollview_image.color = new Color (scrollview_image.color.r, scrollview_image.color.g, scrollview_image.color.b, value);
		}

		get{
			return scrollview_image.color.a;
		}
	}

	public GameObject gameobject
	{
		get {
			return obj;
		}
	}

	public void LoadBackgroundImageFromResources(string path)
	{
		Sprite sprite = Resources.Load (path,typeof(Sprite)) as Sprite;

		if (sprite == null) {
			Debug.LogError ("iScrollView LoadBackgroundImageFromResources:failed");
		}

		scrollview_image.sprite = sprite;
		obj_rt.sizeDelta =new  Vector2 (sprite.rect.width, sprite.rect.height);


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
		
	public GameObject content
	{
		get{
			return _content;
		}
	}
		
	public Vector2 contentSize
	{
		set{
			content_rt.sizeDelta =new Vector2 (value.x,value.y);
			content_rt.localPosition = new Vector3(value.x/2,-value.y/2,0f);
		}

		get{
			return content_rt.sizeDelta;
		}

	}


	public Vector2 center
	{

		set{
			float newX = value.x - view_w / 2 ;
			float newY = view_h / 2 - value.y ;

			obj_rt.localPosition = new Vector3 (newX, newY, 0f);
		}
			
		get{
			
			float centerX = obj_rt.localPosition.x + view_w / 2;
			float centerY = view_h / 2 - obj_rt.localPosition.y;

			return new Vector2(centerX, centerY);
		}
	}



	public Rect frame
	{
		set{

			float w = value.width;
			float h = value.height;

			float newX = value.x - view_w / 2 + value.width / 2;
			float newY = view_h / 2 - value.y - value.height / 2;


			obj_rt.sizeDelta =new  Vector2 (value.width, value.height);
			viewport_rt.sizeDelta =new Vector2 (value.width,value.height);
			obj_rt.localPosition = new Vector3 (newX, newY, 0f);
		}

		get {

			float centerX = obj_rt.localPosition.x;
			float centerY = obj_rt.localPosition.y + view_rt.rect.height;

			return new Rect(centerX,centerY,obj_rt.sizeDelta.x,obj_rt.sizeDelta.y);
		}
	}
		
}
