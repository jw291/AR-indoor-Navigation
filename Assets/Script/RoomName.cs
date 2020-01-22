using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomName : MonoBehaviour {

    public string roomName;
    private static RoomName instance = null;
    public static RoomName Instance
    {
        get
        {
            return instance;
        }
    }

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

    // Use this for initialization
    void Start () {
        var roomobj = GameObject.Find("SaveRoom").GetComponent<SaveRoom>();
        roomName = roomobj.GetName();
    }
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            var roomobj = GameObject.Find("SaveRoom").GetComponent<SaveRoom>();
            roomName = roomobj.GetName();
        }
        
	}
}
