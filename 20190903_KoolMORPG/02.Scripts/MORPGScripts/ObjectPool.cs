using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Tooltip("Copied object")]
    public GameObject copiedObject;

    [Tooltip("Parent transform. ")]
    public Transform parentTransformOrNull;

    [Tooltip("Object count")]
    public int count = 1; // default count is 1

    [Tooltip("Life time. if lifeTime is 0, it is not turn off.")]
    public float lifeTime;    // life time. if lifeTime is 0, it is not turn off.

    private GameObject[] objects;
    private int currentIdx;


    private void Start()
    {
        InitObject();
    }

    /// <summary>
    /// Use Object
    /// </summary>
    /// <param name="position">Position to create.</param>
    public void UseObject(Vector3 position)
    {
        currentIdx %= count;
        if(!objects[currentIdx].activeSelf)
        {
            objects[currentIdx].SetActive(true);
            objects[currentIdx].transform.position = position;

            StartCoroutine(IStartLifeTime(objects[currentIdx]));
            ++currentIdx;
        }
    }

    /// <summary>
    /// Init Setting
    /// </summary>
    private void InitObject()
    {
        if(count <= 0)
        {
            count = 1; // init count
        }
        objects = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            if(copiedObject == null)
            {
                Debug.LogError("Copie object not set");
            }
            GameObject obj = Instantiate(copiedObject, transform.position, Quaternion.identity, parentTransformOrNull) as GameObject;
            if (obj)
            {
                objects[i] = obj;
                //Debug.Log("넣어졌다.");
            }
        }
        currentIdx = 0;
    }

    /// <summary>
    /// object life time
    /// </summary>
    /// <param name="obj">Target object to turn off</param>
    /// <returns></returns>
    private IEnumerator IStartLifeTime(GameObject obj)
    {
        if(lifeTime > 0)
        {
            yield return new WaitForSeconds(lifeTime);
            obj.SetActive(false);
        }
    }
}
