using UnityEngine;
using System.Collections;

public class CameraSet : MonoBehaviour
{

    float devHeight = 6.4f;
    float devWidth = 9.6f;

    // Use this for initialization
    void Start()
    {

        float screenHeight = Screen.height;

        Debug.Log("screenHeight = " + screenHeight);

        this.GetComponent<Camera>().orthographicSize = screenHeight / 100.0f;

        float orthographicSize = this.GetComponent<Camera>().orthographicSize;

        float aspectRatio = Screen.width * 1.0f / Screen.height;

        float cameraWidth = orthographicSize * 2 * aspectRatio;

        Debug.Log("cameraWidth = " + cameraWidth);

        if (cameraWidth < devWidth)
        {
            orthographicSize = devWidth / (2 * aspectRatio);
            Debug.Log("new orthographicSize = " + orthographicSize);
            this.GetComponent<Camera>().orthographicSize = orthographicSize;
        }

    }
}
