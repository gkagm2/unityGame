using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable] //
public class MessageData {

    public string stringData = "";
    public float mousex = 0;
    public float mousey = 0;
    public int type = 0;

    public static MessageData FromByteArray(byte[] input)
    {
        //메모리 스트림과 직렬화 생성
        MemoryStream stream = new MemoryStream(input);
        //이진 포매터 생성
        BinaryFormatter formatter = new BinaryFormatter();

        MessageData data = new MessageData();
        data.stringData = (string)formatter.Deserialize(stream);
        data.mousex = (float)formatter.Deserialize(stream);
        data.mousey = (float)formatter.Deserialize(stream);
        data.type = (int)formatter.Deserialize(stream);

        return data;
    }

    public static byte[] ToByteArray(MessageData msg)
    {
        //메모리 스트림과 직렬화 생성
        MemoryStream stream = new MemoryStream();
        //이진 포매터 생성
        BinaryFormatter formatter = new BinaryFormatter();
        //직렬화
        formatter.Serialize(stream, msg.stringData);
        formatter.Serialize(stream, msg.mousex);
        formatter.Serialize(stream, msg.mousey);
        formatter.Serialize(stream, msg.type);

        //배열로 반환
        return stream.ToArray();
    }
}
