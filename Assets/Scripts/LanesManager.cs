using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanesManager : MonoBehaviour {

    public List<GameObject> Lanes = null;
    List<float> lanePositions = new List<float>();

    void Awake()
    {
        if (Lanes == null || Lanes.Count == 0)
        {
            Debug.Log("Warning - Lanes Empty");
            Application.Quit();
        }
        Lanes.Sort((a, b) => (a.GetComponent<Transform>().position.x.CompareTo(b.GetComponent<Transform>().position.x)));
        foreach(GameObject lane in Lanes)
        {
            Debug.Log(lane.name + " " + lane.transform.position.x);
            lanePositions.Add(lane.transform.position.x);
        }
        foreach(float lane in lanePositions)
        {
            Debug.Log(lane);
        }
        
        
    }

    public float GetLaneAtLeft(float currentPosition, float lineThickness)
    {
        currentPosition = TrimPositionToLane(currentPosition, lineThickness);
        Debug.Log("trimmed position : " + currentPosition);
        if (currentPosition <= lanePositions[0]) return lanePositions[0];
        return lanePositions.FindLast(x => x < currentPosition);
    }

    public float GetLaneAtRight(float currentPosition, float lineThickness)
    {
        currentPosition = TrimPositionToLane(currentPosition, lineThickness);
        Debug.Log("trimmed position : " + currentPosition);
        if (currentPosition >= lanePositions[lanePositions.Count - 1]) return (lanePositions[lanePositions.Count - 1]);
        return lanePositions.Find(x => x > currentPosition);
    }

    float TrimPositionToLane(float position, float threshold)
    {
        foreach (float lane in lanePositions)
        {
            if (Mathf.Abs(lane - position) < threshold)
                position = lane;
        }
        return position;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
