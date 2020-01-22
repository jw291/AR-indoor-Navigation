using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour {

    Vector3 phoneLoc;
    Vector3 markerLoc;
    public GameObject phone;
    public GameObject marker;

    float speed = 100.0f;
    float time = 0;

    // Use this for initialization
    void Start () {
        phoneLoc = phone.transform.position;
        markerLoc = marker.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        phone.transform.position = Vector3.MoveTowards(phone.transform.position, markerLoc, speed * Time.deltaTime);
        if(phone.transform.position == markerLoc)
        {
            time += Time.deltaTime;
            if (time >= 0.5f)
            {
                phone.transform.position = phoneLoc;
                time = 0;
            }
        }


	}
}
