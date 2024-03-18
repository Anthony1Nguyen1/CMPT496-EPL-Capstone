using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public string sceneName;

    // Changes the scene.
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

}
