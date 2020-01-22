using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicePlayer : MonoBehaviour
{

    private GameObject target;
    public GameObject play, stop;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var voice = GameObject.Find("VoiceController").GetComponent<DemoScript>();
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 클릭 누름
            target = GetClickedObject();

            if (target.Equals(play))
            {
                target.transform.localScale = new Vector3(0.4f, 0.15f, 3f);
            }

            else if (target.Equals(stop))
            {
                target.transform.localScale = new Vector3(0.4f, 0.15f, 3f);
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            //마우스 클릭 뗌
            /*if (target.Equals(kor))
            {
                target.transform.localScale = new Vector3(0.6f, 0.2f, 5f);
            }
            
            else if (target.Equals(eng))
            {
                target.transform.localScale = new Vector3(0.6f, 0.2f, 5f);
            }*/

            if (target.Equals(play))
            {
                target.transform.localScale = new Vector3(0.6f, 0.2f, 5f);
                voice.StartSpeaking();
            }
            else if (target.Equals(stop))
            {
                target.transform.localScale = new Vector3(0.6f, 0.2f, 5f);
                voice.StopSpeaking();
            }
        }


    }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 포인트 근처 좌표를 만든다.

        if ((Physics.Raycast(ray.origin, ray.direction * 10, out hit)) == true) //마우스 근처에 오브젝트가 있는지 확인
        {
            //있으면 오브젝트를 저장
            target = hit.collider.gameObject;
        }
        return target;
    }
}