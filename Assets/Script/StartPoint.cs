using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour {

    public Vector3 point;

    private static StartPoint instance = null;
    public static StartPoint Instance
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

        GameObject startPoint = GameObject.Find("StartPoint");

        GameObject start = GameObject.Find("start");

        startPoint.transform.position = start.transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (SceneManager.GetActiveScene().name == "navigationScene")
        {
            var next = GameObject.Find("start").GetComponent<SavePoint>();

            GameObject startPoint = GameObject.Find("StartPoint");

            point = next.GetVector();

            if (point != Vector3.zero)
            {
                startPoint.transform.position = point;
            }
        }
        else if(SceneManager.GetActiveScene().name == "HomeScene" || SceneManager.GetActiveScene().name == "SelectScene")
        {
            point = new Vector3(-36, 40, 36.8f);
        }
    }
}
