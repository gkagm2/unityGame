using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour{

    public GameObject bulletPrefab;

    public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;




        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }
    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        // 서버에 세상에 존재하는 오브젝트를 다른 클라이언트들에게도 소환시키게 함.
        NetworkServer.Spawn(bullet);
    }
}
