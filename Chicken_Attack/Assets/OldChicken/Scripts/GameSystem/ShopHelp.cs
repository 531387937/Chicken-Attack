using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopHelp : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RawImage>().texture = image.texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
