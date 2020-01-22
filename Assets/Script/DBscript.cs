using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using System.IO;
using System.Text;
using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;

public class DBscript : MonoBehaviour {

    private static DBscript instance = null;
    public static DBscript Instance
    {
        get
        {
            return instance;
        }
    }
    public int col_num = 0;
    public int shelf_num = 0;
    public string shelf_dir = null;
    public string bookName = null;
    public int bookNum = 0;
    public string roomNum = null;
    public int book_sNum = 0;
    public string rent = null;
    public string cateName = null;
    public string cateNum = null;

    private string connection;
    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;

    void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LoadingScene")
        {
            var inputData = GameObject.Find("BookName").GetComponent<SaveData>();
            //var inputData2 = GameObject.Find("RoomName").GetComponent<RoomName>();
            string input_BN = inputData.bookNameR;
            //string input_RM = inputData2.roomName;

            byte[] bytesForEncoding = Encoding.UTF8.GetBytes(input_BN);
            string encodedString = Convert.ToBase64String(bytesForEncoding);

            byte[] decodedBytes = Convert.FromBase64String(encodedString);
            string text = Encoding.UTF8.GetString(decodedBytes);

            string qtext = "'" + text + "'";

            // IDbConnection
            string p = "DBtest2.db";
            //Debug.Log("Call to OpenDB:" + p);
            // check if file exists in Application.persistentDataPath
            //string filepath = Application.dataPath + "/StreamingAssets/" + p;

            var filepath = string.Format("{0}/{1}", Application.persistentDataPath, p);

            if (!File.Exists(filepath))
            {
               //WWW loadDB = new WWW("jar:file://" + filepath);
               //while (!loadDB.isDone) { }
               //File.WriteAllBytes(filepath, loadDB.bytes);
                //Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                //Application.dataPath + "!/assets/" + p);
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
            string sqlQuery;
            sqlQuery = "SELECT * FROM bookTable WHERE book_name Like " + qtext;
            dbcmd.CommandText = sqlQuery;

            // IDataReader
            reader = dbcmd.ExecuteReader();

            while (reader.Read())
            {
                // 변수타입은 컬럼 데이터 타입에 맞추면 된다.
                int book_number = reader.GetInt32(0);
                string book_name = reader.GetString(1);
                string room_name = reader.GetString(2);
                int book_shelf_number = reader.GetInt32(3);
                string shelf_direction = reader.GetString(4);
                string category_name = reader.GetString(5);
                string category_number = reader.GetString(6);
                int shelf_row_number = reader.GetInt16(7);
                int shelf_column_number = reader.GetInt16(8);
                string book_rent = reader.GetString(9);




                shelf_num = shelf_row_number;
                shelf_dir = shelf_direction;
                col_num = shelf_column_number;
                bookName = book_name;
                bookNum = book_number;
                roomNum = room_name;
                book_sNum = book_shelf_number;
                rent = book_rent;
                cateName = category_name;
                cateNum = category_number;


                //Debug.Log(" book_number = " + book_number + " book_name = " + book_name + " room_name = " + room_name + " book_shelf_number = " + book_shelf_number + "shelf_direction = " + shelf_direction + "shelf_row_number = " + shelf_row_number + " shelf_column_number = " + shelf_column_number);

            }

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
