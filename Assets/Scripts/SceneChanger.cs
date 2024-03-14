using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    private int SceneToLoad;

    public void FadeToScene(int levelIndex)
    {
        SceneToLoad = levelIndex;
        animator.SetTrigger("Change");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
