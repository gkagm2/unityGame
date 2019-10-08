using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowComponent : MonoBehaviour
{
    public float arrowSpeed = 20f;
    public float distance = 20f;
    public float arrowRotateSpeedRange;
    public float arrowRotateSpeedMinRange = 15f;
    public float arrowRotateSpeedMaxRange = 18f;
    public bool isChasing;

    public ArcherComponent ac;
    public Transform targetTr = null;
    public AudioSource strikeSound;
    private Transform tr;
    private Transform playerTr;

    public CameraPosition cp;

    void Awake()
    {
        ac = GameObject.FindWithTag("Player").GetComponent<ArcherComponent>();
        tr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        cp = GameObject.FindWithTag("MainCamera").GetComponent<CameraPosition>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameObject.activeSelf) return;
        tr.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);

        if(targetTr != null)
        {
            if(Vector3.Distance(tr.position, playerTr.position) >= 10f)
            {
                isChasing = true;
            }
        }

        if(isChasing)
        {
            if (targetTr == null) return;
            tr.rotation = Quaternion.Lerp(tr.rotation, Quaternion.LookRotation(targetTr.position - tr.position), arrowRotateSpeedRange * Time.deltaTime);
        }
        
        if (Vector3.Distance(tr.position, playerTr.position) >= distance)
        {
            RecycleArrow();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            strikeSound.Play();
            StartCoroutine(IRecycling());
            cp.isShaking = true;
        }
    }

    private void OnEnable()
    {
        arrowRotateSpeedRange = Random.Range(arrowRotateSpeedMinRange, arrowRotateSpeedMaxRange);
        
        int idx = 0;
        idx = Random.Range(0, ac.target.Count);

        if (ac.isLockOn)
        {
            if (ac.target.Count == 0) return;
            targetTr = ac.target[idx].GetComponent<Transform>();
        }
        else
        {
            targetTr = null;
        }
    }

    public void RecycleArrow()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator IRecycling()
    {
        yield return new WaitForSeconds(0.1f);
        isChasing = false;
        gameObject.SetActive(false);
    }
}
