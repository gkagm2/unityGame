using UnityEngine;

public class ItemControl : MonoBehaviour {
    public enum Item
    {
        hp,
        laser
    }
    public Item item;

    // Parameters
    public float maxRange = 2.5f; //반응 하기위한 최대 거리

    // Prefabs
    GameObject player; //플레이어 오브젝트
    public GameObject getItemEffect; //  아이템 획득시 나오는 Effect

	void Start () {
        player = GameObject.Find("Player");

        CheckMeWhichItem();
        
    
	}
	
	void Update () {
        CheckMeWhichItem();
        transform.Rotate(0f, 20f * Time.deltaTime, 0f); //아이템 회전

        float distance = Vector3.Distance(player.transform.position, transform.position);

        //거리가 가까워지면
        if (maxRange > distance)
        {
            UseItem(); //아이템을 사용한다.
            gameObject.SetActive(false); //물건 사라짐
        }
    }
    public void CheckMeWhichItem()
    {
        if (gameObject.activeSelf == true)
        {
            if (gameObject.name == "ItemHp")
            {
                item = Item.hp;
            }
            else if (gameObject.name == "ItemGun")
            {
                item = Item.laser;
            }
        }
    }
    
    public void UseItem()
    {
        switch (item)
        {
            case Item.hp: //hp면
                PlayerState playerState = GameObject.Find("Player").GetComponent<PlayerState>(); //플레이어의 상태를 찾아
                if (playerState) //찾았으면. 
                {
                    Instantiate(getItemEffect, transform.position, transform.rotation); // 아이템 획득 이펙트
                    playerState.GetHp(); //HP 가 차오름
                }
                break;
            case Item.laser: //laser면
                //다른 오브젝트의 자식 객체들 중에 컴포넌트를 찾음.
                FireManager fireManager = GameObject.Find("Player").GetComponentInChildren<FireManager>();
                if (fireManager)
                {
                    Instantiate(getItemEffect, transform.position, transform.rotation); // 아이템 획득 이펙트
                    fireManager.isLaser = true;
                }
                break;
        }
    }

}
