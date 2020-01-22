using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SearchDB : MonoBehaviour {

    private static SearchDB instance = null;
    public static SearchDB Instance
    {
        get
        {
            return instance;
        }
    }

    public string book_name = null;
    public string room_num = null;
    public string shelf_dir = null;
    public int book_num = 0;
    public int bShelf_num = 0;
    public int col_num = 0;

    public bool match_book_name = false;
    public bool match_room_num = false;

    public string input_BN = null;
    public string input_RN = null;

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
        var inputData = GameObject.Find("BookName").GetComponent<SaveData>();
        var inputData2 = GameObject.Find("RoomName").GetComponent<RoomName>();
        var matchData = GameObject.Find("DBobj").GetComponent<DBscript>();

        input_BN = inputData.bookNameR;
        input_RN = inputData2.roomName;

        book_name = matchData.bookName;
        room_num = matchData.roomNum;
        shelf_dir = matchData.shelf_dir;
        book_num = matchData.bookNum;
        bShelf_num = matchData.book_sNum;
        col_num = matchData.col_num;

        if (input_BN == book_name)
        {
            match_book_name = true;
        }
        else
        {
            match_book_name = false;
        }

        if (input_RN == room_num)
        {
            match_room_num = true;
        }
        else
        {
            match_room_num = false;
        }
    }

	
	// Update is called once per frame
	void Update () {
        if(SceneManager.GetActiveScene().name == "LoadingScene")
        {
            var inputData = GameObject.Find("BookName").GetComponent<SaveData>();
            var inputData2 = GameObject.Find("RoomName").GetComponent<RoomName>();
            var matchData = GameObject.Find("DBobj").GetComponent<DBscript>();

            input_BN = inputData.bookNameR;
            input_RN = inputData2.roomName;

            book_name = matchData.bookName;
            room_num = matchData.roomNum;
            shelf_dir = matchData.shelf_dir;
            book_num = matchData.bookNum;
            bShelf_num = matchData.shelf_num;
            col_num = matchData.col_num;

            if (input_BN == book_name)
            {
                match_book_name = true;
            }
            else
            {
                match_book_name = false;
            }

            if (input_RN == room_num)
            {
                match_room_num = true;
            }
            else
            {
                match_room_num = false;
            }
        }
        

	}
}
