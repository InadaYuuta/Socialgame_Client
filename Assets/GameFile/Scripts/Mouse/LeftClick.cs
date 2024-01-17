using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class LeftClick : MonoBehaviour
{
   // MyGameInput mouseInput;

    public GameObject clickedGameObj { get; private set; }      // クリックされているオブジェクト、外部から参照可
    public GameObject clickedGameObjOld { get; private set; }   // ひとつ前にクリックされていたオブジェクト
    public GameObject clickUpGameObj { get; private set; }      // クリックが離された時のオブジェクト

    bool isLeftClick = false;
    public bool IsLeftClick { get { return isLeftClick; } }     // レフトクリックしているかどうか、trueがしている状態

    bool isLeftClickUp = false;
    public bool IsLeftClickUp { get { return isLeftClickUp; } } // レフトクリックが離されたかどうか

    bool dragStart = false;   // ドラッグを始めるかどうか
    public bool DragStart { get { return dragStart; } }

    [SerializeField] RectTransform rect;


    [SerializeField] Sprite[] mouseSprites;
    Image image;

    void OnEnable()
    {
        //mouseInput.Enable();
        //// コールバック登録
        //mouseInput.MouseInput.LeftClick.performed += OnLeftClick;
        //mouseInput.MouseInput.LeftClick.canceled += OnLeftClickUp;
    }

    void OnDisable()
    {
        //mouseInput.Disable();
        //// コールバック削除
        //mouseInput.MouseInput.LeftClick.performed -= OnLeftClick;
        //mouseInput.MouseInput.LeftClick.canceled -= OnLeftClickUp;
    }

    void OnDestroy()
    {
       // mouseInput.Dispose();
    }

    void Awake()
    {
        //mouseInput = new MyGameInput();
        //gameManager = FindObjectOfType<GameManager>();
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }


    // クリックされている間に呼ばれるメソッド
    void OnLeftClick(InputAction.CallbackContext context)
    {
        image.sprite = mouseSprites[0];
        isLeftClick = true;
        isLeftClickUp = false;
        Vector3 objPos = rect.transform.position;

        // マウスの位置にRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(objPos);

        // RayCastで当たったオブジェクトを取得
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            clickedGameObj = hit.collider.gameObject;

            Debug.Log("クリックされたオブジェクト:" + clickedGameObj);

            // クリックしたオブジェクトのクリックされた時の動きを呼び出し
            ClickedObj clickedObj = clickedGameObj.GetComponent<ClickedObj>();
            if (clickedObj != null)
            {
                clickedObj.ClickedMove();
            }

            dragStart = true;
            clickedGameObjOld = clickedGameObj;
        }
        else
        {
            OnLeftClickImage();
        }
    }

    // クリックが離された時のメソッド
    void OnLeftClickUp(InputAction.CallbackContext context)
    {
        image.sprite = mouseSprites[1];
        ClickUpSetFlag();
        Vector3 objPos = rect.transform.position;

        // マウスの位置にRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(objPos);

        // RayCastで当たったオブジェクトを取得
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            clickUpGameObj = hit.collider.gameObject;

            Debug.Log("クリックが離されたオブジェクト:" + clickUpGameObj);

            // クリックが離されたオブジェクトのクリックが離された時の動きを呼び出し
            ClickUpObj clickUpObj = clickUpGameObj.GetComponent<ClickUpObj>();
            if (clickUpObj != null)
            {
                clickUpObj.ClickUpMove();
            }
        }
        else
        {
            OnLeftClickUoImage();
        }
    }

    // 取りたいオブジェクトがUIだった場合(クリック時)
    void OnLeftClickImage()
    {
        // RayCastAllの引数(PointerEventData)作成
        PointerEventData pointData = new PointerEventData(EventSystem.current);

        // RayCastAllの結果格納用List
        List<RaycastResult> rayResult = new List<RaycastResult>();

        // PointerEventDataにマウスの位置をセット
        pointData.position = new Vector2(transform.position.x, transform.position.y);
        // RayCast(スクリーン座標)
        EventSystem.current.RaycastAll(pointData, rayResult);

        foreach (RaycastResult result in rayResult)
        {
            // 最初にレイが当たったオブジェクトを取得
            clickedGameObj = result.gameObject;
            Debug.Log("クリックされたUI:" + clickedGameObj);
            break;

        }

        if (clickedGameObj == null) { return; }
        // クリックしたオブジェクトのクリックされた時の動きを呼び出し
        ClickedObj clickedObj = clickedGameObj.GetComponent<ClickedObj>();
        if (clickedObj != null)
        {
            clickedObj.ClickedMove();
        }

        dragStart = true;
        clickedGameObjOld = clickedGameObj;
    }

    // 取りたいオブジェクトがUIだった場合(クリックが離された時)
    void OnLeftClickUoImage()
    {
        // RayCastAllの引数(PointerEventData)作成
        PointerEventData pointData = new PointerEventData(EventSystem.current);

        // RayCastAllの結果格納用List
        List<RaycastResult> rayResult = new List<RaycastResult>();

        // PointerEventDataにマウスの位置をセット
        pointData.position = new Vector2(transform.position.x, transform.position.y);
        // RayCast(スクリーン座標)
        EventSystem.current.RaycastAll(pointData, rayResult);

        foreach (RaycastResult result in rayResult)
        {
            // 最初にレイが当たったオブジェクトを取得
            clickUpGameObj = result.gameObject;
            Debug.Log("クリックが離されたUI:" + clickUpGameObj);
            break;

        }

        if (clickUpGameObj == null) { return; }
        // クリックが離されたオブジェクトのクリックが離された時の動きを呼び出し
        ClickUpObj clickUpObj = clickUpGameObj.GetComponent<ClickUpObj>();
        if (clickUpObj != null)
        {
            clickUpObj.ClickUpMove();
        }
    }

    // クリックが離された時にフラグを設定する
    void ClickUpSetFlag()
    {
        isLeftClickUp = true;
        isLeftClick = false;
        clickedGameObj = null;
        dragStart = false;
    }
}
