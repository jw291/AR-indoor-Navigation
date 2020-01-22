using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour {
    private GameObject target;
    public GameObject window, button;
    public TextMesh fontColor1, fontColor2, fontColor3;
    float speed = 10.0f;
    bool flag = false;
    float time = 0;

    // Use this for initialization
    void Start()
    {
        flag = false;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 클릭 누름
            target = GetClickedObject();

            if (target.Equals(gameObject)) // 선택된게 나라면
            {
                //여기에 코드 작성
                target.transform.localScale = new Vector3(0.09f, 0.09f, 3f);
                time = 0;
                flag = false;
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            //마우스 클릭 뗌
            if (target.Equals(gameObject))
            {
                target.transform.localScale = new Vector3(0.1f, 0.1f, 5f);
                flag = true;
                time = 0;
                //window.SetActive(false);
            }
        }

        if (flag == true)
        {
            time += Time.deltaTime;
            if (time >= 0.2f)
            {
                window.transform.localScale = Vector3.Lerp(window.transform.localScale, new Vector3(0.4f, 0.1f, 0.2f), speed * Time.deltaTime);
                fontColor1.color = new Color(0, 0, 0, 255);
                fontColor2.color = new Color(0, 0, 0, 255);
                fontColor3.color = new Color(0, 0, 0, 255);
            }
            if (time >= 0.8f)
            {
                window.transform.position = Vector3.Lerp(window.transform.position, button.transform.position, speed * Time.deltaTime);
            }
            if (time >= 1.0f & time <= 1.1f)
            {
                window.SetActive(false);
                time = 0;
                flag = false;
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
