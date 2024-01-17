using UnityEngine;

public class StokerImage : MonoBehaviour
{
    [SerializeField] GameObject target;

    RectTransform CanvasRect;
    [SerializeField] Canvas canvas;

    RectTransform UI_Element;

    void Awake()
    {
        CanvasRect = canvas.GetComponent<RectTransform>();
        UI_Element = GetComponent<RectTransform>();
    }

    void Update()
    {
        ChangePoint();
    }

    // ゲームオブジェクトの位置に指定のオブジェクトを移動、今回はマウスオブジェクトの位置にマウスイメージを移動
    void ChangePoint()
    {
        Vector2 ViewportPos = Camera.main.WorldToViewportPoint(target.transform.position);
        Vector2 WorldScreenPos = new Vector2(
        ((ViewportPos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        UI_Element.anchoredPosition = WorldScreenPos;
    }
}
