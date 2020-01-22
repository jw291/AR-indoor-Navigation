using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationText : MonoBehaviour {

    public Text text1;
	// Use this for initialization
	void Start () {
        var data = GameObject.Find("DBobj").GetComponent<DBscript>();
        string bookName = data.bookName;
        string UIText = "검색 도서 : " + bookName;
        text1.text = UIText;
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
