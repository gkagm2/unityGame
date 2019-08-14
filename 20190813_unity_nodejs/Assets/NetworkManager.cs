//#define _WWW
#define _UNITY_WEB_REQUEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
#if _UNITY_WEB_REQUEST
using UnityEngine.Networking;
#endif

public class NetworkManager : MonoBehaviour {
    public static NetworkManager instance;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private const string insertURL = "https://sagacityjang3.run.goorm.io/new";
    private const string selectURL= "https://sagacityjang3.run.goorm.io/select";
    private const string updateURL = "https://sagacityjang3.run.goorm.io/update";
    private const string deleteURL = "https://sagacityjang3.run.goorm.io/delete";
    private const string checkIdURL = "https://sagacityjang3.run.goorm.io/checkId";


    // 서버 데이터베이스에서 유저가 존재하는지 체크한다.
    public void CheckUserExisted(string userName)
    {
        StartCoroutine(ICheckUserExisted(userName));
    }
    IEnumerator ICheckUserExisted(string userName)
    {
        WWWForm form = new WWWForm();
        form.AddField("Name", userName); // Database에서 Name은 유니크함

#if _WWW
        WWW www = new WWW(checkIdURL, form);

        yield return www;

        if (www.isDone)
        {
            if(www.text == "NotExist") // 존재하지 않는 Name이면 
            {
                InsertNewUser(); // 데이터 베이스에 추가
            }
            else //존재하는 Name이면 
            {
                JSONArray jsonString = (JSONArray)JSON.Parse(www.text); // 이런식으로 쓸 수 있다.
                UserManager.userKey = (uint)jsonString[0]["Key"];
                PlayerPrefs.SetInt("Key", (int)UserManager.userKey);

                GetUserDataFromServer(); // 유저의 데이터를 가져온다.
            }
        }
#elif _UNITY_WEB_REQUEST
        UnityWebRequest www = UnityWebRequest.Post(checkIdURL, form);

        yield return www.SendWebRequest();
        if (www.isDone)
        {
            if (www.downloadHandler.text == "NotExist")
            {
                InsertNewUser(); // 데이터 베이스에 추가
            }
            else
            {
                JSONArray jsonString = (JSONArray)JSON.Parse(www.downloadHandler.text);
                UserManager.userKey = (uint)jsonString[0]["Key"];
                PlayerPrefs.SetInt("Key", (int)UserManager.userKey);

                GetUserDataFromServer(); // 유저의 데이터를 가져온다.
            }
        }
#endif
    }

    // Insert
    public void InsertNewUser()
    {
        StartCoroutine(IInsertNewUser());
    }
    IEnumerator IInsertNewUser()
    {
        WWWForm form = new WWWForm();
        //form.AddField("Key", PlayerPrefs.GetInt("Key"));
        form.AddField("Name", PlayerPrefs.GetString("UserName"));
        form.AddField("GId", Random.Range(0, 1000000).ToString());
        form.AddField("Gold", 0);
        form.AddField("Score", 0);
        form.AddField("Level", 1);

#if _WWW
        WWW www = new WWW(insertURL, form);

        yield return www;
        
        if (www.isDone)
        {
            UserManager.userKey = GetUserKeyFromServer(www.text);

            PlayerPrefs.SetInt("Key", (int)UserManager.userKey); // TODO : uint에서 int로 형 변환 시 값이 변형될 우려가 있음. Database의 intager 사이즈 값을 봐야 할 듯.
        }

#elif _UNITY_WEB_REQUEST
        UnityWebRequest www = UnityWebRequest.Post(insertURL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            UserManager.userKey = GetUserKeyFromServer(www.downloadHandler.text);
            PlayerPrefs.SetInt("Key", (int)UserManager.userKey); 
        }
#endif
    }

    private uint GetUserKeyFromServer(string text)
    {
        JSONObject userJson = (JSONObject)JSON.Parse(text);
        return (uint)userJson["LAST_INSERT_ID()"];
    }

    // Select
    public void GetUserDataFromServer()
    {
        StartCoroutine(IGetUserDataFromServer());
    }
    IEnumerator IGetUserDataFromServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Key", PlayerPrefs.GetInt("Key"));

#if _WWW
        WWW www = new WWW(selectURL, form);

        yield return www;

        if (www.isDone)
        {
            var userDataJson = JSON.Parse(www.text); // 이런식으로도 쓸 수 있다.
            UserManager.score = userDataJson[0]["Score"];
            UserManager.level = userDataJson[0]["Level"];
            UserManager.gold = userDataJson[0]["Gold"];
            UIManager.instance.UpdateUI();
        }

#elif _UNITY_WEB_REQUEST
        UnityWebRequest www = UnityWebRequest.Post(selectURL, form);

        yield return www.SendWebRequest();

        if (www.isDone)
        {
            var userDataJson = JSON.Parse(www.downloadHandler.text);
            UserManager.score = userDataJson[0]["Score"];
            UserManager.level = userDataJson[0]["Level"];
            UserManager.gold = userDataJson[0]["Gold"];
            UIManager.instance.UpdateUI();
        }
#endif
    }

    // Update
    public void UpdateDataToServer()
    {
        StartCoroutine(IUpdateDataToServer());
    }
    IEnumerator IUpdateDataToServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("Gold", UserManager.gold);
        form.AddField("Score", UserManager.score);
        form.AddField("Key", PlayerPrefs.GetInt("Key"));

#if _WWW
        WWW www = new WWW(updateURL, form);
        yield return www;
        if (www.isDone)
            Debug.Log("update www : " + www.text);

#elif _UNITY_WEB_REQUEST
        UnityWebRequest www = UnityWebRequest.Post(updateURL, form);
        yield return www.SendWebRequest();
        if (www.isDone)
            Debug.Log("update www : " + www.downloadHandler.text);
#endif
    }

    // Delete
}
