using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    float scrollSpeed = 0.05f;

    Material BGmaterial;

    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        BGmaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        BGmaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
