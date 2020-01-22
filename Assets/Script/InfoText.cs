using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoText : MonoBehaviour {

    public TextMesh inputInfo;
	// Use this for initialization
	void Start () {
        inputInfo = GameObject.Find("textInfo").GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        var language = GameObject.Find("FunctionController").GetComponent<LanguageSelect>();
        var information = GameObject.Find("InfoAPI").GetComponent<InfoAPI>();
        bool english = language.language;
        string engInfo = information.engInfo;
        string korInfo = information.korInfo;

        if (english == true)
        {
            inputInfo.text = engInfo;

        }
        else
        {
            inputInfo.text = korInfo;
        }
    }
}
