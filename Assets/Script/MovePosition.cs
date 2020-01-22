using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePosition : MonoBehaviour {
    public GameObject wall;
    public float time = 0;
	// Use this for initialization
	void Start () {
        time += Time.deltaTime;
        if(time >= 1.0f)
        {
            wall.transform.position = new Vector3(2000f, 2000f, -2000f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
