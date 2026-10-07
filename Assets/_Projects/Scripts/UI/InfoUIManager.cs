using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class InfoUIManager : MonoBehaviour
{
    [SerializeField] UIDocument infoUI;
    [SerializeField] float animationDuration;
    const float HiddenUILeft = 530.0f;
    const string E = "E: ";
    Label barrelLabel;
    Label moduleLabel;

    void OnEnable()
    {
        var root = infoUI.rootVisualElement;
        barrelLabel = root.Q<Label>("BarrelLabel");
        moduleLabel = root.Q<Label>("ModuleLabel");
    }

    public void OnBarrelChanged(string message)
    {
        var sequence = DOTween.Sequence();

        sequence
        .Append(
            DOTween.To(() => barrelLabel.resolvedStyle.left,
            x => barrelLabel.style.left = new StyleLength(x), HiddenUILeft, animationDuration))
            .SetEase(Ease.InQuad)
        .AppendCallback(() => UpdateMessage(barrelLabel, message))
        .Append(
            DOTween.To(() => barrelLabel.resolvedStyle.left,
            x => barrelLabel.style.left = new StyleLength(x), 0, animationDuration))
            .SetEase(Ease.OutQuad);
    }

    public void OnModuleChanged(string message)
    {
        var sequence = DOTween.Sequence();

        sequence
        .Append(
            DOTween.To(() => moduleLabel.resolvedStyle.left,
            x => moduleLabel.style.left = new StyleLength(x), HiddenUILeft, animationDuration))
            .SetEase(Ease.InQuad)
        .AppendCallback(() => UpdateMessage(moduleLabel, message))
        .Append(
            DOTween.To(() => moduleLabel.resolvedStyle.left,
            x => moduleLabel.style.left = new StyleLength(x), 0, animationDuration))
            .SetEase(Ease.OutQuad);
    }

    void UpdateMessage(Label target, string message)
    {
        target.text = E + message;
    }
}
