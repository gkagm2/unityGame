using UnityEngine;
using UnityEngine.SceneManagement;
public class MySceneManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * number 0 : main scene
     * number 1 : game scene
     */

    public void MoveScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

}
