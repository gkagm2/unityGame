using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public const int maxHealth = 100;

    // network를 넘나들면서 수치를 자동으로 동기화 시켜줌.
    [SyncVar]
    public int currentHealth = maxHealth;

    public Slider healthSlider;

    public void TakeDamage()
    {
        // 서버가 아닌 클리이언트면
        if (!isServer)
        {
            return;
        }

        //currentHealth -= amount;
    }

    void OnChangeHealth(int health)
    {
        //healthSilder.value = health;
    }

}
