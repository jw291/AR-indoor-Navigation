using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextToSpeech : Singleton<TextToSpeech>
{




    public enum Locale
    {
        ENGLISH = 0,
        KOREAN,
        SYSTEM_DEFAULT
    }



    public enum ToastLength
    {
        LENGTH_LONG  = 0,
        LENGTH_SHORT = 1
    }



    private string toastString;
    private AndroidJavaObject currentActivity;
    private AndroidJavaObject ttsEngine = null;
    private AndroidJavaObject activityContext = null;
    private OnErrorCallbackHandler errorCallback;
    private string toastLength;

                                                    /// <summary> The current language of the text to speech engine. </summary>
    public Locale language { private set; get; }
                                                    /// <summary> The current pitch of the text to speech engine. </summary>
    public float pitch     { private set; get; }
                                                    /// <summary> The current speed of the text to speech engine. </summary>
    public float speechRate     { private set; get; }
                                                    /// <summary> The current volume of the text to speech engine. </summary>
    public float volume    { private set; get; }
                                                    /// <summary> The delegate template for error callbacks. </summary>
    public delegate void OnErrorCallbackHandler(string error);


    private TextToSpeech baseInstance;

    public static new TextToSpeech instance
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return Singleton<TextToSpeech>.instance;
            }

            else
            {
                Debug.Log("<b><color=#CD5C5CFF><size=13>TextToSpeech requires the application to be running on an android device. You tried to use this plugin on \"" + Application.platform + "\". You shouldn't use this plugin in a non-android platform this includes Unity Editor as well. Using it this way will cause NullReference and many other errors!.</size></color></b>");
                return null;
            }

        }

        private set { }
    }


    protected override void OnAwake()
    {
      
    }



    /// <summary>
    /// Performs speech synthesis on the text given. In case an error occurs the callback is called with the error description.
    /// </summary>
    /// <param name="toSpeak"> The text to speak.</param>
    /// <param name="errorCallback"> The method that will be called when an error occurs. The method will be passed a string argument which will contain the error description.</param>
    public void Speak(string toSpeak, OnErrorCallbackHandler errorCallback)
    {
        if (ttsEngine == null)
        {
            //Initialize();
            errorCallback(TTSUninitializedError());
            return;
        }

        this.errorCallback = errorCallback;


        ttsEngine.Call("SpeakWithCallback", toSpeak, gameObject.name, "OnError");
       
    }



    /// <summary>
    /// Performs speech synthesis on the text given.This method doesn't provide you with error callback.
    /// </summary>
    /// <param name="toSpeak">The text to speak.</param>
    public void Speak(string toSpeak)
    {
        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();           
            return;
        }

        ttsEngine.Call("Speak", toSpeak);

    }




    private void OnError(string error)
    {
        if (errorCallback != null)
        {
            if (!string.IsNullOrEmpty(error))
            {
                errorCallback.Invoke(error);
            }
        }
       
    }



    /// <summary>
    /// Set the language of the text to speech engine.
    /// </summary>
    /// <param name="language"> The language you want to set.</param>
    /// <returns> A short constant that shows the result of setting the language.</returns>
    /// 
    public string SetLanguage(Locale language)
    {
        this.language = language;

        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();
            return "ENGINE_NOT_INITIALIZED";
        }

        return ttsEngine.Call<string>("SetLanguage", System.Enum.GetName(typeof(Locale) , language));
    }


    /// <summary>
    /// Set the language of the text to speech engine.
    /// </summary>
    /// <param name="languageAbbreviation"> Provide an ISO 639 alpha-2 or alpha-3 language code.When a language has both an alpha-2 code and an alpha-3 code, the alpha-2 code must be used. You can find a full list of valid language codes in the IANA Language Subtag Registry (search for "Type: language").  For example "spa" is for spanish, "de" is for German</param>
    /// <param name="countryAbbreviation">Provide an ISO 3166 alpha-2 country code or UN M.49 numeric-3 area code. You can find a full list of valid country and region codes in the IANA Language Subtag Registry (search for "Type: region"). For example "ES" for Spain, "DE" for Germany</param>
    /// <returns></returns>
    public string SetLanguageFromCustomLocale(string languageAbbreviation , string countryAbbreviation)
    {

        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();
            return "ENGINE_NOT_INITIALIZED";
        }

        return ttsEngine.Call<string>("SetLanguageFromCustomLocale", languageAbbreviation, countryAbbreviation);

    }



    /// <summary>
    /// Is the given language represented by the locale available.
    /// </summary>
    /// <param name="locale"> The locale enum representing the language.</param>
    /// <returns> True if the language is available, false otherwise.</returns>
    public bool IsLanguageAvailable(Locale locale)
    {
        return ttsEngine.Call<bool>("IsLanguageAvailable", System.Enum.GetName(typeof(Locale), language));
    }



    /// <summary>
    /// Is the given custom language available.
    /// </summary>
    /// <param name="languageAbbreviation"> Provide a 2 letter or 3 letter language abbreviation. For example "spa" for spanish, "de" for German</param>
    /// <param name="countryAbbreviation">Provide a 2 letter or 3 letter country name abbreviation. For example "ES" for Spain, "DE" for Germany</param>
    /// <returns> True if the language is available, false otherwise. </returns>
    public bool IsCustomLanguageAvailable(string languageAbbreviation, string countryAbbreviation)
    {
        return ttsEngine.Call<bool>("IsCustomLanguageAvailable", languageAbbreviation, countryAbbreviation);
    }


    /// <summary>
    /// Gets the name of the default selected TTS Engine from android settings. If the TTS engine is not successfully initialized this will return "TTS ENGINE NOT INITIALIZED
    /// <para> Please note that this method doesn't return the TTS engine that the app has selected. This will only return the TTS engine selected as default in android settings</para>
    /// </summary>
    /// <returns> The name of the selected TTS engine in android settings.</returns>
    public string GetDefaultEngine()
    {
        return ttsEngine.Call<string>("GetDefaultEngine");
    }


    /// <summary>
    /// Gets the names of the installed TTS engines on this device. If any TTS engine is currently not initialized this will return null
    /// </summary>
    /// <returns> An array constaining names of the installed TTS engines.</returns>
    public string[] GetInstalledEngines()
    {
        string[] engines = null;

        AndroidJavaObject obj = ttsEngine.Call<AndroidJavaObject>("GetInstalledEngines");

        if (obj.GetRawObject().ToInt32() != 0)
        {
            engines = AndroidJNIHelper.ConvertFromJNIArray<string[]> (obj.GetRawObject());
        }

        return engines;
    }




    /// <summary>
    /// Checks to see if google TTS engine is installed on the users' device or not.
    /// </summary>
    /// <returns> A boolean value that tells whether Google TTS engine is installed or not.</returns>
    public bool IsGoogleTTSInstalled()
    {
        return ttsEngine.Call<bool>("IsGoogleTTSInstalled");
    }



    /// <summary>
    /// Change to a TTS engine by providing its name. The name should match exactly as the one shown in android settings. You can get the names of all installed TTS engines by calling GetInstalledEngines() method. 
    /// <para>Please note that this method doesn't immediately set the engine, the engine will be set when the next call to Speak method is made. Also this method doesn't change the default TTS engine in android settings. </para>
    /// <para>Take note that the plugin works fully with the Google TTS engine. Changing to some other engine cannot guarantee that all the functionality stays intact. </para>
    /// </summary>
    /// <param name="engineName"> The engine name to set</param>
    /// <param name="setToGoogleTTS"> If true then it will set to google TTS engine if available.</param>
    /// <returns> True if the engine with the name exists, false if it doesn't. In case if "setToGoogleTTS" is true then if google TTS is not installed then false will be returned.</returns>
    public bool SetTTSEngineByName(string engineName, bool setToGoogleTTS)
    {
        return ttsEngine.Call<bool>("SetTTSEngineByName", engineName, setToGoogleTTS);
    }



    /// <summary>
    /// Get the engine currently being used by the plugin for speech synthesis.
    /// </summary>
    /// <returns> The currently selected TTS engine.</returns>
    public string GetCurrentlySelectedEngine()
    {
        return ttsEngine.Call<string>("GetCurrentlySelectedEngine");
    }


    /// <summary>
    /// Set the speed with which the text to speech engine speaks.
    /// </summary>
    /// <param name="speechRate"> Speech rate. 1.0 is the normal speech rate, lower values slow down the speech (0.5 is half the normal speech rate), greater values accelerate it(2.0 is twice the normal speech rate).</param>
    public void SetSpeed(float speechRate)
    {
        this.speechRate = speechRate;

        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();
            return;
        }
        ttsEngine.Set("Speed", speechRate);
    }



    /// <summary>
    /// Set the pitch with which the text to speech engine speaks.
    /// </summary>
    /// <param name="pitch"> Speech pitch. 1.0 is the normal pitch, lower values lower the tone of the synthesized voice, greater values increase it.</param>
    public void SetPitch(float pitch)
    {
        this.pitch = pitch;

        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();
            return;
        }
        ttsEngine.Set("Pitch", pitch);
    }



    /// <summary>
    /// Set the volume of the text to speech engine. Please note that this changes the volume with which the text to speech engine speaks and doesn't change the android system volume.
    /// </summary>
    /// <param name="volume"> Speech volume. 1.0 is the highest volume, 0.5 represents half the highest volume.Lower values lower the volume of the spoken voice, greater values increase it. </param>
    public void SetVolume(float volume)
    {
        this.volume = volume;

        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();
            return;
        }

        ttsEngine.Set("volume", "" + volume);
    }



    /// <summary> Instantly stops the speech.</summary>

    public void StopSpeech()
    {
        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();
            return;
        }

        ttsEngine.Call("StopSpeech");
    }



    /// <summary> Returns true if the text to speech engine is currently busy speaking. Please note that this method doesn't return realtime results, for example if the TTS engine has just stopped speaking the method might return true for sometime and will return false after a few milli secs.</summary>

    public bool IsSpeaking()
    {
        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();
            return false;
        }

        return ttsEngine.Call<bool>("IsSpeaking");
    }



    /// <summary>
    /// Register callback methods for various events regarding speech synthesis. Please note that you can't register multiple methods to receive the same callback. Calling this function with new set of methods will cause previous registered methods to unregister against the callbacks.
    /// </summary>
    /// <param name="callbackObject">  The gameObject with the script containing the callback methods.</param>
    /// <param name="onUtteranceStartedListener"> This method will be called when the TTS engine has started speaking. The method must not take any parameter.</param>
    /// <param name="onUtteranceErrorListener"> This method will be called when the TTS engine has got an error on speaking. The method must not take any parameter.</param>
    /// <param name="onUtteranceCompletedListener"> This method will be called whenever the TTS engine has stopped speaking. The method must not take any parameter.</param>

    public void RegisterUtteranceListeners(GameObject callbackObject, string onUtteranceStartedListener, string onUtteranceErrorListener, string onUtteranceCompletedListener)
    {
        if (ttsEngine == null)
        {
            //Initialize();
            TTSUninitializedError();
            return;
        }

        ttsEngine.Call("RegisterUtteranceListeners", callbackObject.name, onUtteranceStartedListener, onUtteranceErrorListener, onUtteranceCompletedListener);

    }




    /// <summary>
    /// Shows an android Toast message on the screen.
    /// </summary>
    /// <param name="message"> The message to be shown.</param>
    /// <param name="length"> The length for which the message appears.</param>
    public void Toast(string message, ToastLength length)
    {
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        this.toastString = message;
        this.toastLength = Enum.GetName(typeof(ToastLength), (int)length);
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToast));
    }



    /// <summary> Initializes the TTS engine with default settings.
    /// 
    /// </summary>
    public void Initialize()
    {
        if (ttsEngine == null)
        {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }

            using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.xyaabla.texttospeech.TTS"))
            {
                if (pluginClass != null)
                {
                    ttsEngine = pluginClass.CallStatic<AndroidJavaObject>("instance");
                    ttsEngine.Call("setContext", activityContext);        

                }
            }
        }


    }



    /// <summary>
    /// Initializes the TTS engine with the given parameters.
    /// </summary>
    /// <param name="language"> The language with which the TTS engine initializes.</param>
    /// <param name="speed"> The speed to be set for the TTS engine.</param>
    /// <param name="pitch"> The pitch to be set for the TTS engine.</param>
    /// <param name="volume">The volume to be set for the TTS engine.</param>
    /// 
    public void Initialize(Locale language, float speed, float pitch, float volume)
    {
        Initialize();
        this.errorCallback = InitializationError;

        ttsEngine.Call("SpeakWithCallback", "", gameObject.name, "OnError");

        SetLanguage(language);
        SetSpeed(speed);
        SetVolume(volume);
    }




    private void InitializationError(string error)
    {
        Toast(error, ToastLength.LENGTH_LONG);
    }



    private void showToast()
    {
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>(toastLength));
        toast.Call("show");
    }



    private string TTSUninitializedError()
    {
        string message = "Please initialize the TTS engine prior to performing any actions or changing the settings.";
        Toast(message, ToastLength.LENGTH_LONG);
        return message;
    }
}
