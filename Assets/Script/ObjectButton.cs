using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    private GameObject target;
    public GameObject newWindow;
    //public GameObject button, close;
    Animation anim;
    public TextMesh fontColor1, fontColor2, fontColor3;

    //float speed = 4.0f;
    float time = 0;
    bool flag = false;
    //Vector3 position, scale;
    // Use this for initialization
    void Start()
    {
        newWindow.SetActive(false);
        anim = gameObject.GetComponent<Animation>();
        //scale = target.transform.localScale;
        //position = newWindow.transform.position;
        //scale = newWindow.transform.localScale;
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
                target.transform.localScale = new Vector3(0.4f, 0.08f, 30f);
                //newWindow.transform.localScale = new Vector3(0.4f, 0.1f, 0.2f);
                //newWindow.transform.position = button.transform.position;

            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            //마우스 클릭 뗌
            if (target.Equals(gameObject))
            {
                target.transform.localScale = new Vector3(0.7f, 0.15f, 50f);
                time = 0;
                flag = true;
            }
            /*else
            {
                newWindow.SetActive(false);
            }*/
        }

        if (flag == true)
        {
            time += Time.deltaTime;
            if (time >= 0.2f)
            {
                //newWindow.transform.position = Vector3.Lerp(newWindow.transform.position, position, speed * Time.deltaTime);
                if (time <= 0.3f)
                {
                    newWindow.SetActive(true);
                    anim.Play();
                    //close.SetActive(false);
                }
                if (time >= 1.0f)
                {
                    //newWindow.transform.localScale = Vector3.Lerp(newWindow.transform.localScale, scale, speed * Time.deltaTime);
                    // new Vector3(14.28f, 3.33f, 0.2f)

                    fontColor1.color = new Color(0, 0, 0, 0);
                    fontColor2.color = new Color(0, 0, 0, 0);
                    fontColor3.color = new Color(0, 0, 0, 0);
                    if (time >= 1.4f & time <= 1.5f)
                    {
                        //close.SetActive(true);
                        time = 0;
                        flag = false;
                    }
                }
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