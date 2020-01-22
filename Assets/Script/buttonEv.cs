using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonEv : MonoBehaviour {

    public InputField inputName;
    public string number;

    public void Save()
    {
        PlayerPrefs.SetString("Name", inputName.text);
        number = inputName.text;
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        PlayerPrefs.SetString("Name", inputName.text);
        number = inputName.text;
        */
    }
}
