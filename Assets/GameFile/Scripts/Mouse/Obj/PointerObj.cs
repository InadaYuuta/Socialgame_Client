using UnityEngine;

public class PointerObj : MonoBehaviour
{
    MouseMove mouseMove;

    GameObject pointerObj;

    void Awake()
    {
        mouseMove = FindObjectOfType<MouseMove>();
    }

    // オブジェクトにカーソルが乗った時のメソッド
    public void PointerMove()
    {
        if (mouseMove != null)
        {
            pointerObj = mouseMove.PointerGameObj;
            if (pointerObj != null && pointerObj == this.gameObject)
            {
                PointerTagMove();
            }
        }
    }

    // ここにタグとカーソルが乗った時の処理(GameObject)を追加
    void PointerTagMove()
    {
        var pointerTag = pointerObj.tag;
        switch (pointerTag)
        {
            default:
                PointerLayerMove();
                break;
        }
    }

    // ここにレイヤー番号とカーソルが乗った時の処理(UI)を追加
    void PointerLayerMove()
    {
        var pointerLayer = pointerObj.layer;
        switch (pointerLayer)
        {
            case 0:
                break;
            default:
                break;
        }
    }
}
