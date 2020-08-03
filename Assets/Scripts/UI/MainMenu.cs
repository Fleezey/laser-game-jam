using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SomewhatMerged");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
