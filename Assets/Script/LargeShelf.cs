using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeShelf : MonoBehaviour
{
    public GameObject frontShelf;
    public GameObject backShelf;
    GameObject targetShelf;
    public GameObject buttonUp;
    public GameObject buttonDown;
    public GameObject shelf1, shelf2, shelf3, shelf4;

    Vector3 frontLoc, backLoc, targetPos, targetSca;
    Quaternion targetRot;

    Vector3 destination, agentLoc;
    public GameObject agent;
    public float distance;

    float time1 = 0;
    float time2 = 0;
    float speed = 2.0f;
    float speed2 = 4.0f;
    bool flag = false;

    int book_sNum;
    string shelf_dir;
    int col_num;
    int shelf_num;
    //int book_num;

    public GameObject row1, row2, row3, row4, row5, row6;

    // Use this for initialization
    void Start()
    {
        var data = GameObject.Find("DBobj").GetComponent<DBscript>();
        book_sNum = data.book_sNum;
        shelf_dir = data.shelf_dir;
        col_num = data.col_num;
        shelf_num = data.shelf_num;
        //book_num = data.bookNum;

        if(book_sNum == 1)
        {
            if(col_num == 1)
            {
                targetShelf = GameObject.Find("shelf_1_1");
            }
            else if(col_num == 2)
            {
                targetShelf = GameObject.Find("shelf_1_2");
            }
            else if(col_num == 3)
            {
                targetShelf = GameObject.Find("shelf_1_3");
            }
            else if(col_num == 4)
            {
                targetShelf = GameObject.Find("shelf_1_4");
            }
        }
        else if (book_sNum == 2)
        {
            if (col_num == 1)
            {
                targetShelf = GameObject.Find("shelf_2_1");
            }
            else if (col_num == 2)
            {
                targetShelf = GameObject.Find("shelf_2_2");
            }
            else if (col_num == 3)
            {
                targetShelf = GameObject.Find("shelf_2_3");
            }
            else if (col_num == 4)
            {
                targetShelf = GameObject.Find("shelf_2_4");
            }
        }
        else if (book_sNum == 3)
        {
            if (col_num == 1)
            {
                targetShelf = GameObject.Find("shelf_3_1");
            }
            else if (col_num == 2)
            {
                targetShelf = GameObject.Find("shelf_3_2");
            }
            else if (col_num == 3)
            {
                targetShelf = GameObject.Find("shelf_3_3");
            }
            else if (col_num == 4)
            {
                targetShelf = GameObject.Find("shelf_3_4");
            }
        }
        else if (book_sNum == 4)
        {
            if (col_num == 1)
            {
                targetShelf = GameObject.Find("shelf_4_1");
            }
            else if (col_num == 2)
            {
                targetShelf = GameObject.Find("shelf_4_2");
            }
            else if (col_num == 3)
            {
                targetShelf = GameObject.Find("shelf_4_3");
            }
            else if (col_num == 4)
            {
                targetShelf = GameObject.Find("shelf_4_4");
            }
        }

        targetPos = targetShelf.transform.position;
        targetRot = targetShelf.transform.rotation;
        targetSca = targetShelf.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        var dest = GameObject.Find("Destination").GetComponent<AgentDestination>();
        destination = dest.destination;
        agentLoc = agent.transform.position;
        distance = Vector3.Distance(destination, agentLoc);

        frontLoc = frontShelf.transform.position;
        backLoc = backShelf.transform.position;

        time1 += Time.deltaTime;

        if (distance <= 13.0f)
        {
            time2 += Time.deltaTime;

            if (flag == true)
            {
                buttonUp.SetActive(false);
                buttonDown.SetActive(true);
                if (shelf_dir == "front")
                {
                    if (time2 >= 0.2f)
                    {
                        targetShelf.transform.position = Vector3.Lerp(targetShelf.transform.position, frontLoc, speed * Time.deltaTime);
                    }
                    /*if (time2 >= 0.3f)
                    {
                        targetShelf.SetActive(true);
                    }*/
                    if (time2 >= 0.4f)
                    {
                        targetShelf.transform.rotation = Quaternion.Lerp(targetShelf.transform.rotation, Quaternion.Euler(0, 0, 270), speed2 * Time.deltaTime);
                    }
                    if (time2 >= 0.6f)
                    {
                        targetShelf.transform.localScale = Vector3.Lerp(targetShelf.transform.localScale, new Vector3(3, 3, 3), speed * Time.deltaTime);
                    }
                    if(time2 >= 2.0f)
                    {
                        if(shelf_num == 1)
                        {
                            row1.SetActive(true);
                        }
                        else if(shelf_num == 2)
                        {
                            row2.SetActive(true);
                        }
                        else if(shelf_num == 3)
                        {
                            row3.SetActive(true);
                        }
                        else if(shelf_num == 4)
                        {
                            row4.SetActive(true);
                        }
                        else if(shelf_num == 5)
                        {
                            row5.SetActive(true);
                        }
                        else if(shelf_num == 6)
                        {
                            row6.SetActive(true);
                        }
                    }
                }
                else if (shelf_dir == "back")
                {
                    if (time2 >= 0.2f)
                    {
                        targetShelf.transform.position = Vector3.Lerp(targetShelf.transform.position, backLoc, speed * Time.deltaTime);
                    }
                    /*if (time2 >= 0.3f)
                    {
                        targetShelf.SetActive(true);
                    }*/
                    if (time2 >= 0.4f)
                    {
                        targetShelf.transform.rotation = Quaternion.Lerp(targetShelf.transform.rotation, Quaternion.Euler(0, 0, 90), speed2 * Time.deltaTime);
                    }
                    if (time2 >= 0.6f)
                    {
                        targetShelf.transform.localScale = Vector3.Lerp(targetShelf.transform.localScale, new Vector3(3, 3, 3), speed * Time.deltaTime);
                    }
                    if (time2 >= 2.0f)
                    {
                        if (shelf_num == 1)
                        {
                            row1.SetActive(true);
                        }
                        else if (shelf_num == 2)
                        {
                            row2.SetActive(true);
                        }
                        else if (shelf_num == 3)
                        {
                            row3.SetActive(true);
                        }
                        else if (shelf_num == 4)
                        {
                            row4.SetActive(true);
                        }
                        else if (shelf_num == 5)
                        {
                            row5.SetActive(true);
                        }
                        else if (shelf_num == 6)
                        {
                            row6.SetActive(true);
                        }
                    }
                }
                
            }
            else
            {
                buttonDown.SetActive(false);
                buttonUp.SetActive(true);
                {
                    if (shelf_num == 1)
                    {
                        row1.SetActive(false);
                    }
                    else if (shelf_num == 2)
                    {
                        row2.SetActive(false);
                    }
                    else if (shelf_num == 3)
                    {
                        row3.SetActive(false);
                    }
                    else if (shelf_num == 4)
                    {
                        row4.SetActive(false);
                    }
                    else if (shelf_num == 5)
                    {
                        row5.SetActive(false);
                    }
                    else if (shelf_num == 6)
                    {
                        row6.SetActive(false);
                    }
                }
                if (time2 >= 0.2f)
                {
                    targetShelf.transform.localScale = Vector3.Lerp(targetShelf.transform.localScale, targetSca, speed * Time.deltaTime);
                }
                if (time2 >= 0.4f)
                {
                    targetShelf.transform.position = Vector3.Lerp(targetShelf.transform.position, targetPos, speed * Time.deltaTime);
                }
                if (time2 >= 0.6f)
                {
                    targetShelf.transform.rotation = Quaternion.Lerp(targetShelf.transform.rotation, targetRot, speed2 * Time.deltaTime);
                }
                /*if (time2 >= 1.5f)
                {
                    targetShelf.SetActive(false);
                }*/
                
            }
        }
    }

    public void SizeUp()
    {
        flag = true;
        time2 = 0;
        shelf1.SetActive(false);
        shelf2.SetActive(false);
        shelf3.SetActive(false);
        shelf4.SetActive(false);
    }

    public void SizeDown()
    {
        flag = false;
        time2 = 0;
        shelf1.SetActive(true);
        shelf2.SetActive(true);
        shelf3.SetActive(true);
        shelf4.SetActive(true);
    }
}