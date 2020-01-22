using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour {
    public UnityEngine.UI.Image fade;
    float fades = 1.0f;
    float time = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        if (fades > 0.0f && time >= 1.0f && time < 2.0f)
        {
            fades -= 0.01f;
            fade.color = new Color(0, 0, 0, fades);
            time = 1.0f;
        }
        else if(fades < 255.0f && time >=2.0f)
        {
            fades += 0.01f;
            fade.color = new Color(0, 0, 0, fades);
            time = 2.0f;
        }

	}
}
