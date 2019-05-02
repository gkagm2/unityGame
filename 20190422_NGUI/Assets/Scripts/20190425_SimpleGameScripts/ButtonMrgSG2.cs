using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMrgSG2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClickBackBtn()
    { 
        SceneManager.LoadScene(0);

    }
}
