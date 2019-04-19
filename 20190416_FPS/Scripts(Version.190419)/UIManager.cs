using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {

    // player state
    PlayerState playerState;
    //HP
    public Text hpText;
    public RectTransform hpBar;

	// Use this for initialization
	void Start () {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        hpText.text = playerState.hp + " / " + playerState.maxhp;
        hpBar.localScale = Vector3.one;

    }
	
	// Update is called once per frame
	void Update () {
        UpdateHp();
    }
    void UpdateHp()
    {
        hpText.text = playerState.hp + " / " + playerState.maxhp;
        hpBar.localScale = new Vector3((float)playerState.hp / playerState.maxhp, 1, 1);
    }

}
