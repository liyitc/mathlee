using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButton : UIView {

	private Button button_component;
	private Image button_image;
	private Sprite highlightedImage,normalImage;

	public Transform transform;

	private bool _highlighted;

	public UIButton(GameObject v)
	{

		view = v;

		obj = new GameObject ();
		obj.name = "No Name";

		button_image = obj.AddComponent <Image>();
		button_component = obj.AddComponent <Button>();
		obj.transform.SetParent(view.transform,false);
		obj_rt = obj.GetComponent<RectTransform> ();
		obj_rt.localPosition = Vector3.zero;
		obj_rt.localScale = new Vector3(1f,1f,1f);

		view_rt = view.GetComponent<RectTransform> ();
		view_w = view_rt.rect.width;
		view_h = view_rt.rect.height;

		transform = obj.GetComponent<Transform> ();

		_highlighted = false;

	}


	public void LoadHighlightedImageFromResources(string path)
	{
		highlightedImage = Resources.Load (path,typeof(Sprite)) as Sprite;

		if (highlightedImage == null) {
			Debug.LogError ("iButton LoadImageFromResources:failed");
		}
	}



	public bool highlighted
	{

		set{
			_highlighted = value;
			button_image.sprite = _highlighted? highlightedImage:normalImage;
		}

		get{
			return _highlighted;
		}
	}
		

	public UnityAction<UIButton>onClick
	{
		set{
		
			button_component.onClick.AddListener(delegate() {
				value(this);
			});
		}
	}

	public GameObject gameobject
	{
		get {
			return obj;
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

		
	public Vector2 center
	{
		set{
			float centerX = value.x - view_w / 2;
			float centerY = view_h / 2 - value.y;
			obj_rt.localPosition = new Vector3 (centerX, centerY, 0f);
		}

		get{

			float centerX = obj_rt.localPosition.x + view_rt.rect.width / 2;
			float centerY = view_rt.rect.height / 2 - obj_rt.localPosition.y;

			return new Vector2(centerX, centerY);
		}
	}


	public Vector2 postion
	{
		set{
			float centerX = value.x - view_w / 2;
			float centerY = view_h / 2 - value.y;

			float postionX = centerX + button_image.sprite.rect.width / 2;
			float postionY = centerY - button_image.sprite.rect.height / 2;

			obj_rt.localPosition = new Vector3 (postionX, postionY, 0f);
		}

		get{

			float centerX = obj_rt.localPosition.x;
			float centerY = obj_rt.localPosition.y + view_rt.rect.height;;

			return new Vector2(centerX,centerY);
		}

	}


	public void LoadImageFromResources(string path)
	{
		normalImage = Resources.Load (path,typeof(Sprite)) as Sprite;

		if (normalImage == null) {
			Debug.LogError ("iButton LoadImageFromResources:failed");
		}
			
		button_image.sprite = normalImage;
		obj_rt.sizeDelta =new  Vector2 (normalImage.rect.width, normalImage.rect.height);

	}
		

}
