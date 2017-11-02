using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariables : MonoBehaviour {
    [HideInInspector]
    public float UpScreenLimit;
    [HideInInspector]
    public float DownScreenLimit;
    [HideInInspector]
    public float LeftScreenLimit;
    [HideInInspector]
    public float RightScreenLimit;


    public float laneThickness = 0.05f;
    public float gameSpeed = 1.0f;

    // Use this for initialization
    void Awake () {
        UpScreenLimit = Screen.height;
        DownScreenLimit = 0;
        RightScreenLimit = Screen.width;
        LeftScreenLimit = 0;

        Debug.Log(UpScreenLimit +" "+ DownScreenLimit + " " + LeftScreenLimit + " " + RightScreenLimit);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsInGameScreen(Vector2 point)
    {
        if (point.y < DownScreenLimit) return false;
        if (point.y > UpScreenLimit) return false;
        if (point.x > RightScreenLimit) return false;
        if (point.x < LeftScreenLimit) return false;
        return true;
    }

}

