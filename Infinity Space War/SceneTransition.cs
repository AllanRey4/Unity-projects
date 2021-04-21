
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Text loadingPercentage;
    private static SceneTransition instance;
    private static bool ShoudlPlayOpeningOperation = false;
    private Animator animator;
    private AsyncOperation loadingSceneOperation;
    public static void  SwitchToScene(string sceneName)
    {
        instance.animator.SetTrigger(name:"SceneStart");
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        instance.loadingSceneOperation.allowSceneActivation = false;
    }
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
        if(ShoudlPlayOpeningOperation) animator.SetTrigger(name:"SceneEnd");

    }

   
    void Update()
    {
        if(loadingSceneOperation != null)
        {
            loadingPercentage.text = Mathf.RoundToInt(f:loadingSceneOperation.progress * 100)+"%";
        }
    }

    public void OnAmimationOver()
    {
        ShoudlPlayOpeningOperation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}
