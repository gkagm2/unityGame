using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {
    private Animator anim;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float turnSpeed = 2.0f;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");


        float h = Input.GetAxis("Horizontal");

        Vector3 vec = new Vector3(v, 0, h);

        if (vec != Vector3.zero)
        {
            anim.SetFloat("speed", v);

            transform.Translate(0f, 0, v * speed * Time.deltaTime);
            transform.Rotate(0, h * speed * turnSpeed * Time.deltaTime, 0);
        }

    }
}
