using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {
    public InputField input;
    public static string bookName;
	// Use this for initialization
	void Start () {
        bookName = input.text;
	}
	
	// Update is called once per frame
	void Update () {
        bookName = input.text;
    }

    public string GetName()
    {
        return bookName;
    }
}
