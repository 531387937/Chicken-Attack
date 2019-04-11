using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScale : MonoBehaviour {
	/*
	public Camera camera;
	public int ManualWidth = 1920;
	public int ManualHeight = 1080;

	void Awake () {
		UIRoot uiRoot = gameObject.GetComponent<UIRoot>();
		//camera.orthographicSize *= 16.0f / 9 / ((float)Screen.width / Screen.height);

		if (uiRoot != null)
		{
			if (System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
				uiRoot.minimumHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
			else
				uiRoot.minimumHeight = ManualHeight;
		}

	}
}


	public Camera Camera;
	public Camera uiCamera;

public float standard_width = 1920f;        //初始宽度
public float standard_height = 1080f;       //初始高度
float device_width = 0f;                //当前设备宽度
float device_height = 0f;               //当前设备高度
public float adjustor = 0f;         //屏幕矫正比例

void Awake ()
	{

		//获取设备宽高
		device_width = Screen.width;
		device_height = Screen.height;
		//计算宽高比例
		float standard_aspect = standard_width / standard_height;
		float device_aspect = device_width / device_height;
		//计算矫正比例
		if (device_aspect < standard_aspect) {
			adjustor = standard_aspect / device_aspect;
			//Debug.Log(standard_aspect);
		}
		Debug.Log ("屏幕的比例" + adjustor);
		if (adjustor < 2 && adjustor > 0) {
			uiCamera.orthographicSize = adjustor;
			Camera.orthographicSize *= adjustor;
		}

	}
}

*/

		public Camera mainCamera;


		void Awake()
		{
			AdaptCamera();
		}

		public void AdaptCamera()
		{
			

		float screenAspect = Screen.width / (float)Screen.height;
			float designAspect = 1920 / (float)1080;



		if (designAspect < screenAspect) //屏幕分辨率过大，宽度过长,则屏幕横向留出黑边,高度不变
			{
				float tarWidth = Screen.height * designAspect;//求出实际要显示的宽度
				float tarWidthRadio = tarWidth / Screen.width;//求出宽度百分比
				float posW = (1 - tarWidthRadio) / 2;//宽的起点
                                                     //camera.rect = new Rect(posW, 0, tarWidthRadio, 1);
            mainCamera.orthographicSize *= (designAspect / screenAspect);

			}
			else if (designAspect > screenAspect)//屏幕分辨率过小，高度过高，纵向留黑边,宽度不变
			{
				float tarHeight = Screen.width  / designAspect;
				float tarHeightRadio = tarHeight / Screen.height;
				float posH = (1 - tarHeightRadio) / 2;
            //camera.rect = new Rect(0, posH, 1, tarHeightRadio);
            mainCamera.orthographicSize *= (designAspect / screenAspect);

			}
			else
			{
			mainCamera.rect = new Rect(0, 0, 1, 1);
			}
		//Maincamera.rect = camera.rect;
		mainCamera.orthographicSize *= designAspect / screenAspect;
		}
	}