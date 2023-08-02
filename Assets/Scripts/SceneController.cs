using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneName;
    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SwitchScenes(sceneName);
        }
    }
}
