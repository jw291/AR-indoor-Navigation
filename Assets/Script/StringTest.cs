using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringTest : MonoBehaviour {
    public TextMesh test;
    string origine;
    string after = null;
    int length, result, mod;
	// Use this for initialization
	void Start () {
        test = GameObject.Find("textInfo").GetComponent<TextMesh>();
        origine = "가나다라마바사아자차카타파하가가거겨고교구규기게";
        length = origine.Length;
        result = length / 16;
        mod = length % 16;
        if (result != 0)
        {
            for (int i = 0; i < result; i++)
            {
                after += origine.Substring(0 + (i * 16), 16) + "\n";
            }
            after += origine.Substring(result * 16, mod);
        }
        else
        {
            after += origine;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        test.text = after;
	}

}
