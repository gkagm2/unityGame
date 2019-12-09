using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
