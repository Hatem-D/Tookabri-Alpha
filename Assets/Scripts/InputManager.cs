using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InputManager : MonoBehaviour {

    
    public delegate void stateDependantFunctionPointer();
    public stateDependantFunctionPointer monitorInput;

    public delegate void SwipeEventTriggerFctPtr();
    public SwipeEventTriggerFctPtr leftSwipeEventTrigger;
    public SwipeEventTriggerFctPtr rightSwipeEventTrigger;
    public SwipeEventTriggerFctPtr upSwipeEventTrigger;
    public SwipeEventTriggerFctPtr downSwipeEventTrigger;

    GameVariables gameVars;
    
    private Vector3 grabbedScreenPosition;

    float doubleClickTimer = 0.0f;
    public float doubleClickThreshold = 0.3f;
    
    public float swipeMinMagnitude = 0.5f;
    

    Vector2 swipeMagnitude = new Vector2(0, 0);
    Vector2 swipeStart = new Vector2 (0,0);
    bool swiping = false;
    float pullThreshold;
    
    void Awake()
    {
        gameVars = gameObject.GetComponent<GameVariables>();
    }

    void Start()
    {
        monitorInput += MonitorSwipe;
        //doubleClickTimer = 0.0f;
        //monitorInput += TemporizeDoubleClick;
    }

    // Update is called once per frame
    void Update () {
        if (monitorInput != null) {
            monitorInput();
        }
    }
    
    void SetDbleClickMonitor()
    {
        doubleClickTimer = 0.0f;
        monitorInput += MonitorDoubleClick;
    }
    

    #region Swipes methods
    void MonitorSwipe()//monitors horizontal swipes raises left-right SwipeEventTrigger
    {
        if (!swiping)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Swipe start");
                Vector2 mouseDown = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                //Debug.Log("input pos : " + mouseDown);
                if (gameVars.IsInGameScreen(mouseDown))
                {
                    swipeStart = mouseDown;
                    swiping = true;
                }
            }
        }
        else if (swiping)
        {
            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("Swipe end");
                Vector2 mouseUp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                //Debug.Log("input pos : " + mouseUp);
                swiping = false;
                if (gameVars.IsInGameScreen(mouseUp))
                {
                    swipeMagnitude = mouseUp - swipeStart;

                    if (Mathf.Abs(swipeMagnitude.x) >= Mathf.Abs(swipeMagnitude.y))//if horizontal swipe
                    {
                        if (swipeMagnitude.x > swipeMinMagnitude) SwipeRight();
                        else if (swipeMagnitude.x < -swipeMinMagnitude) SwipeLeft();
                    }
                    else//else vertical swipe
                    {
                        if (swipeMagnitude.y > swipeMinMagnitude) SwipeUp();
                        else if (swipeMagnitude.y < -swipeMinMagnitude) SwipeDown();
                    }

                    
                }
                
            }
        }

    }

    void SwipeLeft()
    {
        Debug.Log("Left");
        if (leftSwipeEventTrigger != null)
        {
            leftSwipeEventTrigger();
        }
    }

    void SwipeRight()
    {
        Debug.Log("Right");
        if (rightSwipeEventTrigger != null)
        {
            rightSwipeEventTrigger();
        }
    }

    void SwipeUp()
    {
        Debug.Log("Up");
        if (upSwipeEventTrigger != null)
        {
            rightSwipeEventTrigger();
        }
    }

    void SwipeDown()
    {
        Debug.Log("Down");
        if (downSwipeEventTrigger != null)
        {
            rightSwipeEventTrigger();
        }
    }

    #endregion

    #region Doubleclick methods

    void TemporizeDoubleClick() {
        //Debug.Log("Before IF tempo dbclicktimer : " + doubleClickTimer + " " + doPlayerStateStuff.GetInvocationList().Length);
        
        if (doubleClickTimer < doubleClickThreshold)
        {
            doubleClickTimer += Time.deltaTime;
        }
        else {
            //Debug.Log("into the else");
        }
    }

    void MonitorDoubleClick()
    {
        if (DoubleClick()) { 
			Debug.Log("Double click"); 
			doubleClickTimer = 0.0f;  
		}
    }

    bool DoubleClick() {
        if (Input.GetMouseButtonUp(0)) { 
            if ( (doubleClickTimer > 0.0f) && (doubleClickTimer < doubleClickThreshold) )
            {
                doubleClickTimer = 0.0f;
                return true;
            }else
            {
                if (doubleClickTimer == 0.0f) { doubleClickTimer += Time.deltaTime; return false; }
                if (doubleClickTimer > doubleClickThreshold) { doubleClickTimer = 0.0f; return false; }
            }
            if (doubleClickTimer > 0.0f) doubleClickTimer += Time.deltaTime;
        }else if (doubleClickTimer > 0.0f) { 
                
                if (doubleClickTimer <= doubleClickThreshold) doubleClickTimer += Time.deltaTime;
        }
        if (doubleClickTimer >= doubleClickThreshold) doubleClickTimer = 0.0f;
        return false;
    }
    #endregion
}