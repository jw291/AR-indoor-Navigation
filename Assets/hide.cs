using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour {

    public GameObject library;
    public GameObject library2;

    // Use this for initialization
    void Start () {
        /*
        library2.SetActive(false);
        */
        /*
        library.SetActive(false);
        */
    }
	
    void OffObject()
    {
        library.SetActive(false);
        library2.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
