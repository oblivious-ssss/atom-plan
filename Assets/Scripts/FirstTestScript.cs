using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstTestScript : MonoBehaviour
{
    private static Font defaultFont;

    private Text messageText;
    private int clickCount;

    void Start()
    {
        CreateHelloWorldUI();
    }

    void CreateHelloWorldUI()
    {
        var canvasObject = new GameObject("HelloWorldCanvas");
        var canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObject.AddComponent<GraphicRaycaster>();

        if (FindObjectOfType<EventSystem>() == null)
        {
            var eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();
        }

        var panel = CreatePanel(canvasObject.transform);
        CreateText(panel.transform, "Title", "Hello, Atom Plan!", 52, new Vector2(0, 60), FontStyle.Bold);
        messageText = CreateText(panel.transform, "Message", "Welcome to your first Unity UI!", 30, new Vector2(0, 0));
        CreateButton(panel.transform, "Click Me", new Vector2(0, -70), OnClickMe);
    }

    static Font GetDefaultFont()
    {
        if (defaultFont == null)
            defaultFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        return defaultFont;
    }

    static RectTransform CreatePanel(Transform parent)
    {
        var panelObject = new GameObject("Panel");
        panelObject.transform.SetParent(parent, false);

        var rect = panelObject.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(520, 280);
        rect.anchoredPosition = Vector2.zero;

        var image = panelObject.AddComponent<Image>();
        image.color = new Color(0.12f, 0.14f, 0.2f, 0.92f);

        return rect;
    }

    Text CreateText(Transform parent, string name, string content, int fontSize, Vector2 position, FontStyle style = FontStyle.Normal)
    {
        var textObject = new GameObject(name);
        textObject.transform.SetParent(parent, false);

        var rect = textObject.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(480, 70);
        rect.anchoredPosition = position;

        var text = textObject.AddComponent<Text>();
        text.text = content;
        text.font = GetDefaultFont();
        text.fontSize = fontSize;
        text.fontStyle = style;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;

        return text;
    }

    void CreateButton(Transform parent, string label, Vector2 position, UnityEngine.Events.UnityAction onClick)
    {
        var buttonObject = new GameObject("ClickButton");
        buttonObject.transform.SetParent(parent, false);

        var rect = buttonObject.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(180, 48);
        rect.anchoredPosition = position;

        var image = buttonObject.AddComponent<Image>();
        image.color = new Color(0.25f, 0.55f, 0.95f);

        var button = buttonObject.AddComponent<Button>();
        button.targetGraphic = image;
        button.onClick.AddListener(onClick);

        var labelObject = new GameObject("Label");
        labelObject.transform.SetParent(buttonObject.transform, false);

        var labelRect = labelObject.AddComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        var text = labelObject.AddComponent<Text>();
        text.text = label;
        text.font = GetDefaultFont();
        text.fontSize = 24;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
    }

    void OnClickMe()
    {
        clickCount++;
        messageText.text = clickCount == 1
            ? "You clicked the button once!"
            : $"You clicked the button {clickCount} times!";
    }
}
