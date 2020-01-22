using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide_script2 : MonoBehaviour
{
    public GameObject library;
    public GameObject wall1, wall2, wall3, wall4;

    // Use this for initialization
    void Start()
    {
        Hide(wall1);
        Hide(wall2);
        Hide(wall3);
        Hide(wall4);
    }

    public void Hide(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
