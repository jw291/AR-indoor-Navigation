using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.IO;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    public string bookName = null;
    public string[] booksName = null;
    public Button searchButton;
    public Text nameText;
    public GameObject uiRoot, scroll;
    int rimit = 0;
    bool flag = true;
    Vector3 position;
    float tum;
    public GameObject warning;
    public Text warningText;
    private string connection;
    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;

    public static string GetiPhoneDocumentsPath()
    {

        string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
        return path;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Search2Scene")
        {
            var inputData = GameObject.Find("BookName").GetComponent<SaveData>();
            //var inputData2 = GameObject.Find("RoomName").GetComponent<RoomName>();
            string input_BN = inputData.bookName;
            //string input_RM = inputData2.roomName;

            byte[] bytesForEncoding = Encoding.UTF8.GetBytes(input_BN);
            string encodedString = Convert.ToBase64String(bytesForEncoding);

            byte[] decodedBytes = Convert.FromBase64String(encodedString);
            string text = Encoding.UTF8.GetString(decodedBytes);
            //string qRM = "'" + input_RM + "'";
            string qtext = "'%" + text + "%'";

            // IDbConnection
            string p = "DBtest2.db";
            //Debug.Log("Call to OpenDB:" + p);
            // check if file exists in Application.persistentDataPath
            //string filepath = Application.dataPath + "/StreamingAssets/" + p;

            //var filepath = string.Format("{0}/{1}", Application.dataPath, p);
            var filepath = string.Format("{0}/{1}", Application.persistentDataPath, p);
            if (!File.Exists(filepath))
            {
                //WWW loadDB = new WWW("jar:file://" + filepath);
                //while (!loadDB.isDone) { }
                //File.WriteAllBytes(filepath, loadDB.bytes);
                /*Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                                 Application.dataPath + "!/assets/" + p);*/
                // if it doesn't ->
                // open StreamingAssets directory and load the db -> 

                var loadDb = Application.dataPath + "/Raw/" + p;
                // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
            
            }

            //open db connection
            connection = "URI=file:" + filepath;
            //Debug.Log("Stablishing connection to: " + connection);
            dbcon = new SqliteConnection(connection);
            dbcon.Open();


            // IDbCommand
            IDbCommand dbcmd = dbcon.CreateCommand();
            // sql문장 = "SELECT 조회할 컬럼 FROM 조회할 테이블";
            //string sqlQuery = "SELECT book_number, book_name, room_name, book_shelf_number,  shelf_direction, shelf_row_number, shelf_column_number FROM bookTable WHERE book_name = " + qtext;


            dbcmd.CommandText = "SELECT COUNT(*) FROM bookTable WHERE book_name LIKE " + qtext;// + "AND room_name = " + qRM;
            dbcmd.CommandType = CommandType.Text;
            int count = Convert.ToInt32(dbcmd.ExecuteScalar());


            if (flag == true)
            {

                if (count <= 6)
                {
                    scroll.SetActive(true);
                    warning.SetActive(false);
                    uiRoot.transform.localScale = new Vector3(1, 0.7f, 1);
                    //scale = new Vector3(7, 2.5f, 0);
                    position = new Vector3(-20, 760, 0);
                    tum = 270;
                    if (count == 0)
                    {
                        warning.SetActive(true);
                        warningText.text = "검색 결과가 \n 존재하지 않습니다";
                        scroll.SetActive(false);
                    }
                }
                else if (6 < count & count <= 12)
                {
                    scroll.SetActive(true);
                    warning.SetActive(false);
                    uiRoot.transform.localScale = new Vector3(1, 1.4f, 1);
                    //scale = new Vector3(7, 1.2f, 0);
                    //scale = new Vector3(7, 2.5f, 0);
                    position = new Vector3(-20, 760, 0);
                    tum = 140;

                }
                else if (12 < count & count <= 18)
                {
                    scroll.SetActive(true);
                    warning.SetActive(false);
                    uiRoot.transform.localScale = new Vector3(1, 0.7f + (0.17f * (count - 6)), 1);
                    //scale = new Vector3(7, 2.5f - (0.105f * (count - 6)), 0);
                    position = new Vector3(-20, 760 + (3.0f * (count - 6)), 0);
                    tum = 270 - (11.3f * (count - 6));
                }
                else if (18 < count & count <= 24)
                {
                    scroll.SetActive(true);
                    warning.SetActive(false);
                    uiRoot.transform.localScale = new Vector3(1, 0.7f + (0.17f * (count - 6)), 1);
                    //scale = new Vector3(7, 2.5f - (0.105f * (count - 6)), 0);
                    //scale = new Vector3(7, 2.5f, 0);
                    position = new Vector3(-20, 760 + (3.0f * (count - 6)), 0);
                    tum = 270 - (11.3f * (count - 6));
                }
                else if (24 < count & count <= 30)
                {
                    scroll.SetActive(true);
                    warning.SetActive(false);
                    uiRoot.transform.localScale = new Vector3(1, 0.7f + (0.17f * (count - 6)), 1);
                    //scale = new Vector3(7, 2.5f - (0.105f * (count - 6)), 0);
                    //scale = new Vector3(7, 2.5f, 0);
                    position = new Vector3(-20, 760 + (3.0f * (count - 6)), 0);
                    tum = 270 - (10.8f * (count - 6));
                }
                else // 사이즈 너무 크면 조금 더 자세히 입력 요구.
                {
                    /*
                    uiRoot.transform.localScale = new Vector3(1, 0.7f + (0.17f * (count - 6)), 1);
                    //scale = new Vector3(7, 2.5f - (0.105f * (count - 6)), 0);
                    position = new Vector3(-20, 760 - (1.0f * (count - 6)), 0);
                    tum = 270 - (8f * (count - 6));
                    */
                    warning.SetActive(true);
                    warningText.text = "도서명을 조금 더 \n 자세히 입력해주세요";
                    scroll.SetActive(false);
                }

                string sqlQuery;
                sqlQuery = "SELECT book_name FROM bookTable WHERE book_name Like " + qtext; //+ "AND room_name = " + qRM;
                dbcmd.CommandText = sqlQuery;

                // IDataReader
                reader = dbcmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    // 변수타입은 컬럼 데이터 타입에 맞추면 된다.
                    string books_name = reader.GetString(0);
                    //int weight = 110 * i;
                    //Vector3 Vweight = transform.position - new Vector3(0, weight, 0);
                    booksName = new string[count];

                    //Debug.Log(books_name);
                    //Debug.Log(count);
                    //bookName = books_name;
                    //for(int i = 0; i < count; i++)
                    {
                        //count = reader.GetInt32(1);
                        //string books_name = reader.GetString(0);
                        //booksName[i] = books_name;
                        //Debug.Log(books_name);
                    }
                    //Debug.Log(" book_number = " + book_number + " book_name = " + book_name + " room_name = " + room_name + " book_shelf_number = " + book_shelf_number + "shelf_direction = " + shelf_direction + "shelf_row_number = " + shelf_row_number + " shelf_column_number = " + shelf_column_number);
                    
                    booksName[rimit] = books_name;

                    Button Instance = Instantiate(searchButton);//, searchButton.transform.position - new Vector3(0,110,0), transform.rotation);
                    Text textInstance = Instantiate(nameText);//, nameText.transform.position, nameText.transform.rotation);
                    Instance.transform.SetParent(uiRoot.transform);
                    textInstance.transform.SetParent(Instance.transform);
                    Instance.transform.localPosition = position - new Vector3(0, rimit * tum, 0);//new Vector3(-20, 780 - (rimit * 95), 0);
                    textInstance.transform.localPosition = new Vector3(0, 0, 0);
                    //Instance.transform.localScale = scale;
                    textInstance.text = booksName[rimit];
                    Instance.name = "NameButton" + rimit;
                    textInstance.name = "NameText" + rimit;
                    Instance.onClick.AddListener(() => NameButtonClickListener(textInstance.text));
                    //Debug.Log(bookName);
                    //Debug.Log(booksName[rimit]);
                    rimit++;
                }

                flag = false;

                // 닫아주고 초기화 시켜주는 곳
                reader.Close(); // clean everything up
                reader = null;
                dbcmd.Dispose();
                dbcmd = null;
                dbcon.Close();
                dbcon = null;
            }
        }
        
    }
    public void NameButtonClickListener(string name)
    {
        bookName = name;
        SceneManager.LoadScene("LoadingScene");
    }
}