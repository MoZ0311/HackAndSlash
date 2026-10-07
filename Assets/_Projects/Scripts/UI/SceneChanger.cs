using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] UIDocument fadeUI;
    [SerializeField] float fadeDuration;
    VisualElement fade;
    SceneType nextScene = SceneType.None;

    void OnEnable()
    {
        var root = fadeUI.rootVisualElement;
        fade = root.Q<VisualElement>();
    }

    void Start()
    {
        Fade(fadeDuration, 0);
    }

    public void ChangeScene(SceneType targetScene)
    {
        nextScene = targetScene;
        Fade(fadeDuration, 1);
    }

    void Fade(float duration, float alpha)
    {
        Time.timeScale = 0;
        DOTween.To(() => fade.resolvedStyle.opacity,
            x => fade.style.opacity = new StyleFloat(x), alpha, duration)
        .SetEase(Ease.InQuad)
        .SetUpdate(true)
        .OnComplete(OnFadeCompleted);
    }

    void OnFadeCompleted()
    {
        if (fade.resolvedStyle.opacity == 0)
        {
            Time.timeScale = 1;
        }
        else if (nextScene != SceneType.None)
        {
            SceneManager.LoadScene((int)nextScene);
        }
    }
}

public enum SceneType
{
    Title,
    Game,
    Over,

    None,
}