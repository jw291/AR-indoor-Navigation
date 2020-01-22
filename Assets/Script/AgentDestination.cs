using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDestination : MonoBehaviour {

    GameObject tempObj;
    public Vector3 destination;
    string dest;
	// Use this for initialization
	void Start ()
    {
        var location = GameObject.Find("DBobj").GetComponent<DBscript>();

        int bShelf_num = location.book_sNum;
        int col_num = location.col_num;
        string shelf_dir = location.shelf_dir;

        if(bShelf_num == 1)
        {
            if(shelf_dir == "back")
            {
                if(col_num == 1)
                {
                    dest = "1-1";
                }
                else if(col_num == 2)
                {
                    dest = "1-2";
                }
                else if(col_num == 3)
                {
                    dest = "1-3";
                }
                else if(col_num == 4)
                {
                    dest = "1-4";
                }
            }
            else if (shelf_dir == "front")
            {
                if (col_num == 1)
                {
                    dest = "2-1";
                }
                else if (col_num == 2)
                {
                    dest = "2-2";
                }
                else if (col_num == 3)
                {
                    dest = "2-3";
                }
                else if (col_num == 4)
                {
                    dest = "2-4";
                }
            }

        }

        else if (bShelf_num == 2)
        {
            if (shelf_dir == "back")
            {
                if (col_num == 1)
                {
                    dest = "2-1";
                }
                else if (col_num == 2)
                {
                    dest = "2-2";
                }
                else if (col_num == 3)
                {
                    dest = "2-3";
                }
                else if (col_num == 4)
                {
                    dest = "2-4";
                }
            }
            else if (shelf_dir == "front")
            {
                if (col_num == 1)
                {
                    dest = "3-1";
                }
                else if (col_num == 2)
                {
                    dest = "3-2";
                }
                else if (col_num == 3)
                {
                    dest = "3-3";
                }
                else if (col_num == 4)
                {
                    dest = "3-4";
                }
            }

        }

        if (bShelf_num ==3)
        {
            if (shelf_dir == "back")
            {
                if (col_num == 1)
                {
                    dest = "3-1";
                }
                else if (col_num == 2)
                {
                    dest = "3-2";
                }
                else if (col_num == 3)
                {
                    dest = "3-3";
                }
                else if (col_num == 4)
                {
                    dest = "3-4";
                }
            }
            else if (shelf_dir == "front")
            {
                if (col_num == 1)
                {
                    dest = "4-1";
                }
                else if (col_num == 2)
                {
                    dest = "4-2";
                }
                else if (col_num == 3)
                {
                    dest = "4-3";
                }
                else if (col_num == 4)
                {
                    dest = "4-4";
                }
            }

        }

        if (bShelf_num == 4)
        {
            if (shelf_dir == "back")
            {
                if (col_num == 1)
                {
                    dest = "4-1";
                }
                else if (col_num == 2)
                {
                    dest = "4-2";
                }
                else if (col_num == 3)
                {
                    dest = "4-3";
                }
                else if (col_num == 4)
                {
                    dest = "4-4";
                }
            }
            else if (shelf_dir == "front")
            {
                if (col_num == 1)
                {
                    dest = "5-1";
                }
                else if (col_num == 2)
                {
                    dest = "5-2";
                }
                else if (col_num == 3)
                {
                    dest = "5-3";
                }
                else if (col_num == 4)
                {
                    dest = "5-4";
                }
            }

        }
        tempObj = GameObject.Find("Destination"+dest);
        destination = tempObj.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
