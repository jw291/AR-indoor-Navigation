using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour
{
    //public Dropdown languageSelection;

    public string[] installedEngines;
    string info_eng, info_kor;
    int option;
    string text;


    // Start is called before the first frame update
    void Start()
    {
        
        if(Application.platform == RuntimePlatform.Android)
        {
            TextToSpeech.instance.Initialize(TextToSpeech.Locale.KOREAN, 1 , 0.75f , 1);
            TextToSpeech.instance.RegisterUtteranceListeners(gameObject , "SpeechStarted", "SpeechError", "SpeechEnded");
        }

        /*
        List<Dropdown.OptionData> languageOptions = new List<Dropdown.OptionData>();

        foreach(var value in System.Enum.GetValues(typeof(TextToSpeech.Locale)))
        {
            Dropdown.OptionData option = new Dropdown.OptionData(System.Enum.GetName(typeof(TextToSpeech.Locale), value));

            languageOptions.Add(option);        
        }

        languageSelection.ClearOptions();
        languageSelection.AddOptions(languageOptions);


        List<Dropdown.OptionData> engineOptions = new List<Dropdown.OptionData>();

        installedEngines = TextToSpeech.instance.GetInstalledEngines();

        foreach (var value in installedEngines)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(value);

            engineOptions.Add(option);
        }
        */

    }

    void Update()
    {
        var language = GameObject.Find("FunctionController").GetComponent<LanguageSelect>();
        var information = GameObject.Find("InfoAPI").GetComponent<InfoAPI>();
        bool eng = language.language;
        info_kor = information.origine_kor;
        info_eng = information.origine_eng;
        
        if (eng == true)
        {
            option = 0;
            text = info_eng;
        }
        else
        {
            option = 1;
            text = info_kor;
        }
        TextToSpeech.instance.SetLanguage((TextToSpeech.Locale)option);
    }



    public void StartSpeaking()
    {            
        TextToSpeech.instance.Speak(text, (string error) =>
        {
            TextToSpeech.instance.Toast("An error occured while speaking  " + error, TextToSpeech.ToastLength.LENGTH_LONG);
        });


    }



    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeech();
    }






}
