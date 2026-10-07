using UnityEngine;
using UnityEngine.UIElements;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] UIDocument titleUI;
    [SerializeField] SceneChanger sceneChanger;
    Button startButton;
    Button exitButton;

    void OnEnable()
    {
        var root = titleUI.rootVisualElement;
        startButton = root.Q<Button>("StartButton");
        exitButton = root.Q<Button>("ExitButton");

        if (startButton == null || exitButton == null)
        {
            Debug.LogError("not found UI");
            return;
        }

        startButton.clicked += OnPressedStart;
        exitButton.clicked += OnPressedExit;

        UnityEngine.Cursor.visible = true;
    }

    void OnDisable()
    {
        startButton.clicked -= OnPressedStart;
        exitButton.clicked -= OnPressedExit;
    }

    void OnPressedStart()
    {
        AudioPlayer.Instance.PlaySE("Enter");
        sceneChanger.ChangeScene(SceneType.Game);
    }

    void OnPressedExit()
    {
        AudioPlayer.Instance.PlaySE("Enter");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
