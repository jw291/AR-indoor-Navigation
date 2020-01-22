using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveRoom : MonoBehaviour
{
    
    public static string roomName;

    

    public string GetName()
    {
        return roomName;
    }

    public void GetName1()
    {
        roomName = "1인문과학실";
    }

    public void GetName2()
    {
        roomName = "2인문과학실";
    }

    public void GetName3()
    {
        roomName = "자연과학실";
    }

    public void GetName4()
    {
        roomName = "학위논문실";
    }

    public void GetName5()
    {
        roomName = "1사회과학실";
    }

    public void GetName6()
    {
        roomName = "2사회과학실";
    }

    public void GetName7()
    {
        roomName = "예체능실";
    }

    public void GetName8()
    {
        roomName = "고문헌실";
    }
}
