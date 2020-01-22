using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class button : MonoBehaviour {

    public InputField inputName;
    public string number;

    // Use this for initialization
    void Start () {
        //number = inputName.text;

    }
	
	// Update is called once per frame
	void Update () {
        /*
        PlayerPrefs.SetString("Name", inputName.text);
        number = inputName.text;
        */
    }

    public void ChangeGameScene()
    {
      
        SceneManager.LoadScene("ARs");
    }
}
