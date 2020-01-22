using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingInfo2 : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public Text deffText;
    public Slider slider;
    bool IsDone = false;
    float fTime = 0f;
    AsyncOperation async_operation;

    void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        StartCoroutine(StartLoad("InfoScene"));
    }

    void Update()
    {
        var result = GameObject.Find("SearchObj").GetComponent<SearchDB>();
        bool result_book = result.match_book_name;
        bool result_room = result.match_room_num;
        string roomName = result.room_num;
        fTime += Time.deltaTime;
        slider.value = fTime;

        if (fTime >= 1.5f)
        {
            if (result_book == true && result_room == true)
            {
                async_operation.allowSceneActivation = true;
            }
            else
            {
                if (result_book == true && result_room == false)
                {
                    panel1.SetActive(true);
                    deffText.text = "해당 도서는 " + roomName + "에\n" + "비치되어 있습니다.";
                }
                else
                {
                    panel2.SetActive(true);
                }
            }
        }
    }

    public IEnumerator StartLoad(string strSceneName)
    {
        async_operation = SceneManager.LoadSceneAsync(strSceneName);
        async_operation.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while (async_operation.progress < 0.9f)
            {
                slider.value = async_operation.progress;

                yield return true;
            }
        }
    }
}