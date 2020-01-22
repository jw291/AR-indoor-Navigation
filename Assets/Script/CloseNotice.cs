using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseNotice : MonoBehaviour
{
    private GameObject target;
    public GameObject window;

    // Use this for initialization
    void Start()
    {

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
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            //마우스 클릭 뗌
            if (target.Equals(gameObject))
            {
                target.transform.localScale = new Vector3(0.1f, 0.1f, 5f);

                window.SetActive(false);
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
