using UnityEngine;
using System.Collections;

public class CameraSet : MonoBehaviour
{

    float devWidth = 17.78f;

    // Use this for initialization
    void Start()
    {
        //this.GetComponent<Camera>().orthographicSize = screenHeight / 97.0f;

        float orthographicSize = this.GetComponent<Camera>().orthographicSize;
        float aspectRatio = Screen.width * 1.0f / Screen.height;
        float cameraWidth = orthographicSize * 2 * aspectRatio;
        if (cameraWidth > devWidth)
        {
            orthographicSize = devWidth / (2 * aspectRatio);
            Debug.Log("new orthographicSize = " + orthographicSize);
            this.GetComponent<Camera>().orthographicSize = orthographicSize;
        }
    }
}
