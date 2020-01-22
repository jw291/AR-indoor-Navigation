using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Text;
using System.IO;


public class NaverAPI : MonoBehaviour
{

    AndroidJavaObject activity;
    public string Information;
    public string bookName;
    public Text inputInfo;

    void Awake()
    {
        
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //activity = new AndroidJavaObject("com.openbook.openbook.MainActivity", bookName);
        activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        
    }

    void Start()
    {
        var tos = GameObject.Find("BookName").GetComponent<SaveData>();
        bookName = tos.bookName;
        activity.Call("searchNaver", bookName);
    }

    public void searchNaver(string bookInfo)
    {
        Information = bookInfo;
    }
    
    void Update()
    {
        inputInfo.text = Information;
    }
}