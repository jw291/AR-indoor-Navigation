using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shiftScene : MonoBehaviour
{
    public void ChangeMainScene()
    {
        SceneManager.LoadScene("ButtonSyncronizationWorldScale");
    }

    public void ChangeNevigationScene()
    {
        SceneManager.LoadScene("navigationScene");
    }

    public void ChangeHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ChangeHelpScene()
    {
        SceneManager.LoadScene("HelpScene1");
    }

    public void ChangeSelectScene()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void ChangeSearchScene()
    {
        SceneManager.LoadScene("SearchScene");
    }

    public void ChangeInfoScene()
    {
        SceneManager.LoadScene("InfoScene");
    }

    public void ChangeLoadingScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void ChangeHelp2()
    {
        SceneManager.LoadScene("HelpScene2");
    }

    public void ChangeHelp3()
    {
        SceneManager.LoadScene("HelpScene3");
    }

    public void ChangeHelp4()
    {
        SceneManager.LoadScene("HelpScene4");
    }

    public void ChangeHelp5()
    {
        SceneManager.LoadScene("HelpScene5");
    }

    public void Search2()
    {
        SceneManager.LoadScene("Search2Scene");
    }
}