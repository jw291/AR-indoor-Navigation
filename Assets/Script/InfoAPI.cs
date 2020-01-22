using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;


public class InfoAPI : MonoBehaviour
{
    string clientId = "v4R0SjbwGqs0hK05VzaC";
    string clientSecret = "YiHUN2cBUn";
    public string origine_kor;
    public string origine_eng;
    public string korInfo;
    public string engInfo;
    int count = 0;

    private static InfoAPI instance = null;
    public static InfoAPI Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "InfoScene")
        {
            if (count == 0)
            {
                var infoName = GameObject.Find("BookName").GetComponent<SaveData>();
                string book_name = infoName.bookNameR;
                origine_kor = SearchNaver(book_name);
                KorStringData(origine_kor);
                origine_eng = Translate(origine_kor);
                EngStringData(origine_eng);
                count += 1;
            }
        }
        else
        {
            count = 0;
        }
    }
    
    public string SearchNaver(string bookName)
    {

        int display = 1;

        try
        {
            string query = bookName;
            string apiURL = "https://openapi.naver.com/v1/search/book?query=" + query + "&display=" + display + "&";
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Headers.Add("X-Naver-Client-Id", clientId); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", clientSecret);       // 클라이언트 시크릿
            //b = new System.Security.Cryptography.AesCryptoServiceProvider();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                
                

                StringBuilder sb = new StringBuilder();
                string line;
                
                while ((line = reader.ReadLine()) != null)
                {
                    sb.Append(line + "\n");
                    //Debug.Log(reader.ReadLine());
                }
                //Debug.Log(sb);
                //string text = reader.ReadToEnd();
                //Debug.Log(text);
                string data = sb.ToString();
                //Debug.Log(data);

                int point = data.IndexOf("description");
                int startPoint = point + 15;
                //Debug.Log("point = " + point);
                //Debug.Log("Start Point = " + startPoint);
                int lastPoint = data.LastIndexOf('"');
                //Debug.Log("Last Point = " + lastPoint);

                string descript = data.Substring(startPoint, lastPoint - startPoint);
                //Debug.Log("Description = " + descript);

                return descript;
            }
            else
            {
                Debug.Log("Error 발생=" + status);
                return "Null";
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return " ";
        }
    }

    public string Translate(string origine)
    {
        try
        {
            string url = "https://openapi.naver.com/v1/language/translate";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", clientId); // 개발자센터에서 발급받은 Client ID
            request.Headers.Add("X-Naver-Client-Secret", clientSecret); // 개발자센터에서 발급받은 Client Secret
            request.Method = "POST";
            string query = origine;
            byte[] byteDataParams = Encoding.UTF8.GetBytes("source=ko&target=en&text=" + query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            StringBuilder sb = new StringBuilder();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                sb.Append(line + "\n");
                //Debug.Log(reader.ReadLine());
            }
            //Debug.Log(sb);
            //string text = reader.ReadToEnd();
            //Debug.Log(text);
            string data = sb.ToString();
            //Debug.Log(data);

            int point = data.IndexOf("translatedText");
            int startPoint = point + 17;
            //Debug.Log("point = " + point);
            //Debug.Log("Start Point = " + startPoint);
            int point2 = data.LastIndexOf("srcLangType");
            int lastPoint = point2 - 3;
            //Debug.Log("Last Point = " + lastPoint);

            string translateString = data.Substring(startPoint, lastPoint - startPoint);
            //Debug.Log("Description = " + translateString);

            //string text = reader.ReadToEnd();
            stream.Close();
            response.Close();
            reader.Close();
            //Debug.Log(text);
            return translateString;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return " ";
        }
    }

    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain,
        // look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    continue;
                }
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                bool chainIsValid = chain.Build((X509Certificate2)certificate);
                if (!chainIsValid)
                {
                    isOk = false;
                    break;
                }
            }
        }
        return isOk;
    }

    public void KorStringData(string book_Info)
    {
        string origine;
        int length;
        int result;
        int mod;

        origine = book_Info;
        origine = origine.Replace("<b>", "");
        origine = origine.Replace("</b>", "");
        origine = origine.Replace("\n", ""); // 추가 수정부분. 문제 있으면 삭제
        length = origine.Length;
        result = length / 17;
        mod = length % 17;

        if (result != 0)
        {

            if (result > 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    korInfo += origine.Substring(0 + (i * 17), 17);
                    if (i == 9)
                    {
                        korInfo += "...";
                    }
                    else
                    {
                        korInfo += '\n';
                    }
                }

            }
            else
            {
                for (int i = 0; i < result; i++)
                {
                    korInfo += origine.Substring(0 + (i * 17), 17) + "\n";
                }
                korInfo += origine.Substring(result * 17, mod);
            }
        }
        else
        {
            korInfo += origine;
        }
    }

    public void EngStringData(string infoEng)
    {
        string engOrigine;
        int eng_length;
        int eng_result;
        int eng_mod;

        engOrigine = infoEng;
        engOrigine = engOrigine.Replace("\n", "");
        engOrigine = engOrigine.Replace("<b>", "");
        engOrigine = engOrigine.Replace("</ b>", "");
        eng_length = engOrigine.Length;
        eng_result = eng_length / 30;
        eng_mod = eng_length % 30;
        if (eng_result != 0)
        {

            if (eng_result > 10)
            {
                for (int j = 0; j < 10; j++)
                {
                    engInfo += engOrigine.Substring(0 + (j * 30), 30);
                    if (j == 9)
                    {
                        engInfo += "...";
                    }
                    else
                    {
                        engInfo += '\n';
                    }
                }
            }
            else
            {
                for (int j = 0; j < eng_result; j++)
                {
                    engInfo += engOrigine.Substring(0 + (j * 30), 30) + "\n";
                }
                engInfo += engOrigine.Substring(eng_result * 30, eng_mod);
            }
        }
        else
        {
            engInfo += engOrigine;
        }
    }

    public class JTestClass
    {
        public int i;
        public float f;
        public bool b;
        public string str;
        public int[] iArray;
        public List<int> iList = new List<int>();
        public Dictionary<string, float> fDictionary = new Dictionary<string, float>();


        public JTestClass() { }



        public JTestClass(bool isSet)
        {

            if (isSet)

            {
                i = 10;
                f = 99.9f;
                b = true;
                str = "JSON Test String";
                iArray = new int[] { 1, 1, 3, 5, 8, 13, 21, 34, 55 };

                for (int idx = 0; idx < 5; idx++)
                {
                    iList.Add(2 * idx);
                }



                fDictionary.Add("PIE", Mathf.PI);
                fDictionary.Add("Epsilon", Mathf.Epsilon);
                fDictionary.Add("Sqrt(2)", Mathf.Sqrt(2));

            }
        }


        public void Print()
        {
            Debug.Log("i = " + i);
            Debug.Log("f = " + f);
            Debug.Log("b = " + b);
            Debug.Log("str = " + str);

            for (int idx = 0; idx < iArray.Length; idx++)
            {
                Debug.Log(string.Format("iArray[{0}] = {1}", idx, iArray[idx]));
            }

            for (int idx = 0; idx < iList.Count; idx++)
            {
                Debug.Log(string.Format("iList[{0}] = {1}", idx, iList[idx]));
            }

            foreach (var data in fDictionary)
            {
                Debug.Log(string.Format("iDictionary[{0}] = {1}", data.Key, data.Value));
            }
        }
    }
    

    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}
