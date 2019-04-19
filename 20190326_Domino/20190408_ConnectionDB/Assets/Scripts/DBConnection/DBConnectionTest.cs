using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System.Data;


public class DBConnectionTest : MonoBehaviour
{

    private void Start()
    {
    //DB연결 문자열 --- 된 곳은 실제 DB가 설치된 시스템의 아이피를 입력
    //DB 연결에 쓰이는 아이디와 패스워드, DB 이름은 DB 설치 시 설정한 값으로 입력
    //http://58.238.204.11 
        string connection_str = @"Server=58.238.204.11;uid=jmaster;pwd=Wkdgusaud1!;database=Test";

        SqlConnection sql_conn = new SqlConnection(connection_str);
        //DB에 연결.
        sql_conn.Open();

        Debug.Log("DB Connect sring = " + connection_str);
        if(sql_conn.State != ConnectionState.Open)
        {
            //Open 상태가 아니라면 실패
            Debug.Log("DB Connect Failed.");
        }
        else
        {
            Debug.Log("DB Connect Success.");
        }



    }

    
}