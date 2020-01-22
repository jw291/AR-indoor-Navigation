using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour {

    private static SaveData instance = null;
    public static SaveData Instance
    {
        get
        {
            return instance;
        }
    }

    public string bookName;
    public string roomName;
    public string bookNameR;
    // Use this for initialization
    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        var inputData = GameObject.Find("RoomName").GetComponent<RoomName>();
        var inputName = GameObject.Find("NameSave").GetComponent<Test>();
        roomName = inputData.roomName;
        bookName = inputName.GetName();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SearchScene")
        {
            var inputData = GameObject.Find("RoomName").GetComponent<RoomName>();
            var inputName = GameObject.Find("NameSave").GetComponent<Test>();
            roomName = inputData.roomName;
            //PlayerPrefs.SetString("BookName", inputName.text);
            //bookName = PlayerPrefs.GetString("BookName");
            bookName = inputName.GetName();
        }
        else if(SceneManager.GetActiveScene().name == "Search2Scene")
        {
            var selectBook = GameObject.Find("Main_Camera").GetComponent<CreateButton>();
            bookNameR = selectBook.bookName;
        }
    }
    // Update is called once per frame
    /*public void Load ()
    {
        bookName = PlayerPrefs.GetString("BookName");
	}*/
}
