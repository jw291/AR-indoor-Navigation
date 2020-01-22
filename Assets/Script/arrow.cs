using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour {

	// Use this for initialization
	void Start () {


        // 좌표값 저장 방법
        /* 
        GameObject book = GameObject.Find("bookshelf1");

        Vector3 pos;

        pos = book.transform.position;
        */


        // 로컬 좌표 변경 방법
        //transform.Rotate(0, 0, 90);

        // 월드 좌표 변경 방법
        // transform.rotation = Quaternion.Euler(0, 180, 180);



        // 스크립트 참조하는 방법
        /*
        var result = GameObject.Find("DBobject").GetComponent<DBscript>();

        if(result.number == 1)
        {
            transform.Rotate(0, 0, 90);
        }

        else { }

    */




    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
