using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour {

    public Text inputName;
    public Text inputLoca;
    public Text inputRent;
    public Text inputCate;
    public Text InputLoca2;
    public string bookName;
    string bookLoca;
    string rent;
    string category;
    string bookLoca2;

    // Use this for initialization
    void Start () {
        var infoName = GameObject.Find("BookName").GetComponent<SaveData>();
        var infoLoca = GameObject.Find("SearchObj").GetComponent<SearchDB>();
        var infoRent = GameObject.Find("DBobj").GetComponent<DBscript>();

        bookName = infoName.bookNameR;

        bookLoca = infoLoca.room_num + " - " + infoRent.book_sNum + " 번 책장 ";

        bookLoca2 = infoRent.shelf_dir + " " + infoLoca.col_num + " 열 " + infoRent.shelf_num + " 행";

        category = infoRent.cateName;

        if (infoRent.rent == "false")
        {
            rent = "대여 가능";
        }
        else if (infoRent.rent == "true")
        {
            rent = "대여 중";
        }
    }

     // Update is called once per frame
     void Update () {
        inputName.text = bookName;
        inputLoca.text = bookLoca;
        InputLoca2.text = bookLoca2;
        inputCate.text = category;
        inputRent.text = rent;
    }

}
