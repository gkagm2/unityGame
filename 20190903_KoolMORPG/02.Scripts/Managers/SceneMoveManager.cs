using UnityEngine.SceneManagement;

public static class SceneMoveManager
{

    /// <summary>
    /// 씬을 하나만 연다
    /// </summary>
    /// <param name="sceneName">씬 이름</param>
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 씬을 두개 연다
    /// </summary>
    /// <param name="sceneName1">메인 이름</param>
    /// <param name="sceneName2">여하 씬 이름</param>
    public static void LoadScene(string sceneName1, string sceneName2)
    {
        SceneManager.LoadScene(sceneName1);
        SceneManager.LoadScene(sceneName2, LoadSceneMode.Additive);
    }
}
