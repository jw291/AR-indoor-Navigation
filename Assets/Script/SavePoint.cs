using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SavePoint : MonoBehaviour {

    public Vector3 point2, reset;
    public static Vector3 returnPoint;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        
            var next = GameObject.Find("Destination").GetComponent<AgentDestination>();

            GameObject start = GameObject.Find("start");

            reset = start.transform.position;

            point2 = next.destination;
    }

    public Vector3 GetVector()
    {
        return returnPoint;
    }

    public void HomeButtonClick()
    {
        returnPoint = reset;
    }

    public void SearchButtonClick()
    {
        returnPoint = point2;
    }
}
