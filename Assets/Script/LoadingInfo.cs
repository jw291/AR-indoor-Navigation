using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingInfo : MonoBehaviour {

    public GameObject camera1; //기존 카메라
    public GameObject camera2; //로딩 카메라
    //public Slider slider;            //로딩 진행도를 알려줄 슬라이더 UI
    public GameObject fadeoutobj;
    public bool loadingState;
    Button button;

    private void Start()
    {
        button = this.transform.GetComponent<Button>();
        button.onClick.AddListener(OnTriggerEnter);
    }

    void OnTriggerEnter()
    {
        StartCoroutine(SceneLoad()); // 버튼 클릭시 씬전환 코루틴 시작
    }

    IEnumerator SceneLoad()
    {
        camera1.SetActive(false); //기존 카메라
        camera2.SetActive(true); //로딩화면에 있는 카메라
        loadingState = true;
        yield return null;
        //씬 로딩 시작
        AsyncOperation async = SceneManager.LoadSceneAsync("InfoScene");
        while (!async.isDone)
        {
            //1프레임마다 검사
            yield return null;
            //로딩 진행도를 슬라이더 UI 값에 저장
            //slider.value = async.progress;
            //print(async.progress);
        }
    }
}

