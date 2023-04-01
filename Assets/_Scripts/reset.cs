using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{
    public void resetIT()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

