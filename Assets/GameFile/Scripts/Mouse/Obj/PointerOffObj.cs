using UnityEngine;

public class PointerOffObj : MonoBehaviour
{
    MouseMove mouseMove;

    GameObject pointerOffObj;

    void Awake()
    {
        mouseMove = FindObjectOfType<MouseMove>();
    }

    // オブジェクトにカーソルが乗った時のメソッド
    public void PointerOffMove()
    {
        if (mouseMove != null)
        {
            pointerOffObj = mouseMove.PointerGameObjOld;
            if (pointerOffObj != null && pointerOffObj == this.gameObject)
            {
                PointerOffTagMove();
            }
        }
    }

    // ここにタグとカーソルが乗った時の処理(GameObject)を追加
    void PointerOffTagMove()
    {
        var pointerTag = pointerOffObj.tag;
        switch (pointerTag)
        {
            default:
                PointerOffLayerMove();
                break;
        }
    }

    // ここにレイヤー番号とカーソルが乗った時の処理(UI)を追加
    void PointerOffLayerMove()
    {
        var pointerLayer = pointerOffObj.layer;
        switch (pointerLayer)
        {
            case 0:
                break;
            default:
                break;
        }
    }
}
