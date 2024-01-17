using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickedObj : MonoBehaviour
{
    LeftClick leftClick;

    GameObject clickedObj;

    void Awake()
    {
        leftClick = FindObjectOfType<LeftClick>();
    }

    // オブジェクトがクリックされた時に呼ばれるメソッド
    public void ClickedMove()
    {
        if (leftClick != null)
        {
            clickedObj = leftClick.clickedGameObj;
            if (clickedObj != null && clickedObj == this.gameObject)
            {
                ClickedTagMove();
            }
        }
    }

    // ここにタグとクリックされた時の処理(GameObject)を追加
    void ClickedTagMove()
    {
        var clickedTag = clickedObj.tag;
        switch (clickedTag)
        {
            case "": // plusButtonのタグを作って入れる
                // ここにショップへの遷移処理を書く
                break;
            default:
                ClickedLayerMove();
                break;
        }
    }

    // ここにレイヤー番号とクリックされた時の処理(UI)を追加
    void ClickedLayerMove()
    {
        var clickedLayer = clickedObj.layer;
        switch (clickedLayer)
        {
            case 0:
                break;
            default:
                break;
        }
    }
}
