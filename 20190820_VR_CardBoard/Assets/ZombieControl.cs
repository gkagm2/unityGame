using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieControl : MonoBehaviour {
    Animator anim;
    public float distance =3f;
    Transform target;
    GameObject playerCamera;

    public float hp = 2f;

    public AudioSource audioSource;
    public AudioClip[] attackSound;
    public AudioClip[] walkSound;
    public AudioClip[] deadSound;

    bool isAttack = false;
    private bool isDead = false;

    public GameObject bloodSprayEffect;
    public GameObject bloodStreamEffect;
    //public NavMeshAgent agent;


    private void Start()
    {

        //agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
        target = GameObject.Find("Player/PlayerPosition").GetComponent<Transform>();
        transform.LookAt(target);
    }
    public void Dead()
    {
        audioSource.PlayOneShot(deadSound[Random.Range(0, deadSound.Length)]);
        anim.SetBool("isDead", true);
        Destroy(gameObject, 5f);
    }

    public void Damage()
    {
        GameObject.Find("Player/Main Camera").GetComponent<PlayerCamera>().GetHitPosition();

        if (isDead)
            return;
        hp -= 1f;

        
        if(hp <= 0)
        {
            Dead();
            isDead = true;
        }
        audioSource.PlayOneShot(walkSound[Random.Range(0, walkSound.Length)]);
    }

    public void FlashEffect(Vector3 hitPoint, Quaternion rot)
    {
        
        Instantiate(bloodSprayEffect, hitPoint, rot);
        Instantiate(bloodStreamEffect, hitPoint, rot);
    }

    private void Update()
    {
        if (isDead)
            return;

        if (Vector3.Distance(target.position, transform.position) <= distance)
        {
            anim.SetBool("isAttack", true);
            isAttack = true;
        }
        if (isAttack)
        {
            audioSource.PlayOneShot(attackSound[Random.Range(0, attackSound.Length)]);
            isAttack = false;
        }
        
    }
}
