using UnityEngine;

public class ScoreManager : MonoBehaviour {
    //싱글톤 객체
    static ScoreManager _instance = null;

    int _bestScore = 0;
    int _myScore = 0;

    public static ScoreManager Instance()
    {
        return _instance;
    }
	// Use this for initialization
	void Start () {
        if (_instance == null)
            _instance = this;

        LoadBestScore();
	}
	
    public int myScore
    {
        get
        {
            return _myScore;
        }
        set
        {
            _myScore = value;
            if(_myScore > _bestScore)
            {
                _bestScore = _myScore;

                SaveBestScore();
            }
        }
    }

    //최고 점수기록 남기기
    void SaveBestScore()
    {
        //PlayerPrefs 의 정보는 Windows 레지스트리에 저장이 된다. (해킹의 위험이 있다.)
        PlayerPrefs.SetInt("Beset Score", _bestScore);
    }
    void LoadBestScore()
    {
        _bestScore = PlayerPrefs.GetInt("Best Score", 0);

    }
}
