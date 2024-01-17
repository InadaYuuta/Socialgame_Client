using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickUpObj : MonoBehaviour
{
    LeftClick leftClick;

    GameObject clickUpObj;

    void Awake()
    {
        leftClick = FindObjectOfType<LeftClick>();
    }


    // オブジェクトがクリックされた時に呼ばれるメソッド
    public void ClickUpMove()
    {
        if (leftClick != null)
        {
            clickUpObj = leftClick.clickUpGameObj;
            if (clickUpObj != null && clickUpObj == this.gameObject)
            {
                ClickUpTagMove();
            }
        }
    }

    // ここにタグとクリックされた時の処理(GameObject)を追加
    void ClickUpTagMove()
    {
        var clickUpTag = clickUpObj.tag;
        switch (clickUpTag)
        {
            default:
                ClickUpLayerMove();
                break;
        }
    }

    // ここにレイヤー番号とクリックされた時の処理(UI)を追加
    void ClickUpLayerMove()
    {
        var clickUpLayer = clickUpObj.layer;
        switch (clickUpLayer)
        {
            case 0:
                break;
            default:
                break;
        }
    }
}
