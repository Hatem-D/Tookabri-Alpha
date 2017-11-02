using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour {

    public float scrollSpeed = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float offset = Time.time * scrollSpeed;        
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);
    }

    void Stop()
    {
        scrollSpeed = 0.0f;
    }
}
