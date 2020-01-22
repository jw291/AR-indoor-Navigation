using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAnim : MonoBehaviour {
    Animation anim;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animation>();
        anim.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
