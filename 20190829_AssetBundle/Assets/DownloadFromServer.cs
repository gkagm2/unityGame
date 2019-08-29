using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
public class DownloadFromServer : MonoBehaviour
{
    private const string downloadURL = "https://downloadj.run.goorm.io";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadAssets());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DownloadAssets()
    {
        UnityWebRequest www = UnityWebRequest.Get(downloadURL);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "myasset"), www.downloadHandler.data);
            Debug.Log("파일이 Assets/StreamingAssets에 저장되었습니다.");
        }

        /*
        WWW www = new WWW(downloadURL);

        yield return www;

        if (www.isDone)
        {
            Debug.Log(www.text);
            File.WriteAllBytes(Path.Combine(Application.streamingAssetsPath, "myasset"), www.bytes);
            Debug.Log("파일이 Assets/StreamingAssets에 저장되었습니다.");
        }
        */
    }
}
