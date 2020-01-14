using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/// <summary>
/// 저장 불러오기 매니저 클래스
/// </summary>
public class SaveLoadManager : MonoBehaviour
{
    public enum EPathOfFile
    {
        dataPath,
        persistentDataPath
    };
    
    public static SaveLoadManager instance = null;
    public string path = "";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        SetPath(EPathOfFile.persistentDataPath); // 초기화
    }
    
    /// <summary>
    /// 저장 할 경로를 설정한다.
    /// </summary>
    /// <param name="_ePathOfFile">파일 경로</param>
    public void SetPath(EPathOfFile _ePathOfFile)
    {
        switch (_ePathOfFile)
        {
            case EPathOfFile.dataPath:
                path = Application.dataPath;
                break;
            case EPathOfFile.persistentDataPath:
                path = Application.persistentDataPath;
                break;
        }
    }
    
    /// <summary>
    /// 디렉토리를 생성한다.
    /// </summary>
    /// <param name="directoryName">생할 할 디렉토리 이름</param>
    /// <returns></returns>
    public bool CreateDirectory(string directoryName)
    {
        DirectoryInfo diPath = new DirectoryInfo(path + "/" + directoryName);
        if(diPath.Exists == false)
        {
            diPath.Create();
            Debug.Log("존재하지 않아서 생성함 : " + path + "/" + directoryName);
            return true;
        }
        else
        {
            Debug.Log("존재함: " + path + "/" + directoryName);
            return false; // 존재하면 false를 리턴
        }
    }

    /// <summary>
    /// 데이터를 불러온다.
    /// </summary>
    /// <param name="directoryName">디렉토리 이름</param>
    /// <param name="fileName">파일 이름</param>
    public void Load(string directoryName, string fileName)
    {
        string loadPath = path + "/" + directoryName + "/" + fileName;
        // 파일이 존재하는지 확인
        if (File.Exists(loadPath)) // 파일이 존재하면
        {
            BinaryReader reader = new BinaryReader(File.Open(loadPath, FileMode.Open));
            string a = reader.ReadString();
            string b = reader.ReadString();
            bool c = reader.ReadBoolean();
            Debug.Log("a : " + a + ", b : " + b + ", c : " + c);
        }
    }

    // TODO (장현명) : 테스트 후 쓸모없는 데이터 지우기

    /// <summary>
    /// 데이터를 불러온다.
    /// </summary>
    /// <param name="directoryName">디렉토리 이름</param>
    /// <param name="fileName">파일 이름</param>
    public void Save(string directoryName, string fileName)
    {
        string savePath = path + "/" + directoryName + "/" + fileName;
        // 파일이 존재하지 않으면
        if (!File.Exists(savePath))
        {
            FileStream file = File.Open(savePath, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(file); // 두번째 인자 defualt : UTF8
            writer.Write("꿀꿀");
            writer.Write("11");
            writer.Write(true);
            file.Close();
        }
        else // 존재하면
        {
            FileStream file = File.Open(savePath, FileMode.Append, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(file); // 두번째 인자 defualt : UTF8
            writer.Write("멍멍");
            writer.Write("22");
            writer.Write(false);
            file.Close();
        }
    }
}
