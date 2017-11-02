using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TookabriManager : MonoBehaviour {
    
    InputManager inputReader;
    LanesManager lanes;
    GameVariables gameVars;

    float targetLane = 0.0f;

    bool grounded = true;

    public float horizontalSpeed = 1.0f;

    public float TargetLane
    {
        get
        {
            return targetLane;
        }

        set
        {
            targetLane = value;
        }
    }

    void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        if (gameController == null)
        {
            Debug.Log("Warning gameController null");
            Application.Quit();
        }
        inputReader = gameController.GetComponent<InputManager>();        
        gameVars = gameController.GetComponent<GameVariables>();


        lanes = GameObject.FindGameObjectWithTag("LaneHolder").GetComponent<LanesManager>();
        if (lanes == null) {
            Debug.Log("Warning Lanes null");
            Application.Quit();
        }

        inputReader.leftSwipeEventTrigger += LeftSwipe;
        inputReader.rightSwipeEventTrigger += RightSwipe;
    }

    void OnEnable()
    {
   
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        HorizontalSlide();
	}

    
    void LeftSwipe()
    {
        TargetLane = lanes.GetLaneAtLeft(transform.position.x, gameVars.laneThickness);
    }

    void RightSwipe()
    {
        TargetLane = lanes.GetLaneAtRight(transform.position.x, gameVars.laneThickness);
    }

    void HorizontalSlide()
    {//slide horizontally if tookabri has not reached target vector destination
        if ((Mathf.Abs(transform.position.x - TargetLane) > gameVars.laneThickness / 10.0f))
        {
            transform.position = new Vector2(Mathf.LerpUnclamped(transform.position.x, TargetLane, horizontalSpeed * gameVars.gameSpeed * Time.deltaTime), transform.position.y);
        }        
    }

}
