using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseMove : MonoBehaviour
{
   // MyGameInput mouseInput;

    [SerializeField, Header("マウスの感度")] float mouseSensitivity = 0.01f;

    public Vector2 ScreenMousePos { get; private set; }
    public Vector2 WorldMousePos { get; private set; }

    GameObject pointerGameObj;  // カーソルの下にあるオブジェクト
    GameObject pointerGameObjOld;
    public GameObject PointerGameObj { get { return pointerGameObj; } }       // カーソルの下にあるオブジェクト(外部参照)
    public GameObject PointerGameObjOld { get { return pointerGameObjOld; } } // カーソルの下にあったオブジェクト(外部参照)

   [SerializeField] RectTransform mouseImage;

    Vector2 rectPos; // UI座標系の自分の座標

    Vector3 rayHitPos; // レイの当たった位置を保存
    public Vector3 RayHitPos { get { return rayHitPos; } }


    void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

       // mouseInput = new MyGameInput();
        ScreenMousePos = new Vector2(Screen.width / 2, Screen.height / 2);
       // mouseInput.MouseInput.Move.performed += OnMouseMove;
    }

    void OnEnable()
    {
        //mouseInput.Enable();
        //mouseInput.MouseInput.Move.performed += OnMouseMove;
    }

    void OnDisable()
    {
        //mouseInput.Disable();
        //mouseInput.MouseInput.Move.performed -= OnMouseMove;
    }

    void OnDestroy()
    {
     //   mouseInput.Dispose();
    }

    void RayHitMove()
    {
        Vector3 objPos = rectPos;

        // マウスの位置にRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(objPos);

        // RayCastで当たったオブジェクトを取得
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            rayHitPos = hit.point;
            pointerGameObj = hit.collider.gameObject;
            if (pointerGameObj == null) { return; }
            // カーソルが乗ったオブジェクトのカーソルが乗った時の処理を呼び出す
            PointerObj pointerObj = pointerGameObj.GetComponent<PointerObj>();
            if (pointerObj != null)
            {
                pointerObj.PointerMove();
            }
        }
        else
        {
            OnPointerImage();
        }
    }

    // 取りたいオブジェクトがUIだった場合
    void OnPointerImage()
    {
        // RayCastAllの引数(PointerEventData)作成
        PointerEventData pointData = new PointerEventData(EventSystem.current);

        // RayCastAllの結果格納用List
        List<RaycastResult> rayResult = new List<RaycastResult>();

        // PointerEventDataにマウスの位置をセット
        pointData.position = new Vector2(mouseImage.transform.position.x, mouseImage.transform.position.y);

        // RayCast(スクリーン座標)
        EventSystem.current.RaycastAll(pointData, rayResult);

        foreach (RaycastResult result in rayResult)
        {
            // 最初にレイが当たったオブジェクトを取得
            pointerGameObj = result.gameObject;
            break;
        }

        if (pointerGameObj == null) { return; }
        // カーソルが乗ったオブジェクトのカーソルが乗った時の処理を呼び出す
        PointerObj pointerObj = pointerGameObj.GetComponent<PointerObj>();
        if (pointerObj != null)
        {
            pointerObj.PointerMove();
        }

        // カーソルが離れたとき(カーソルが乗ったオブジェクトが別のものになったら)
        if (pointerGameObjOld != null && pointerGameObjOld != pointerGameObj)
        {
            PointerOffObj pointerOffObj = pointerGameObjOld.GetComponent<PointerOffObj>();
            if (pointerOffObj != null)
            {
                pointerOffObj.PointerOffMove();
            }
            pointerGameObjOld = pointerGameObj;
        }
    }

    // マウスが動いたときに呼ばれるコールバック
    void OnMouseMove(InputAction.CallbackContext context)
    {
        //Vector2 mouseInputVal = mouseInput.MouseInput.Move.ReadValue<Vector2>();             // マウスの移動量
        //Vector3 worldMouseInputVal = new Vector3(mouseInputVal.x, mouseInputVal.y, 0f);      // 三次元空間のマウスの移動量

        //if (Mathf.Abs(worldMouseInputVal.x) > 0.1f || Mathf.Abs(worldMouseInputVal.y) > 0.1f)
        //{
        //    var rect = GetComponent<RectTransform>();

        //    rectPos = mouseImage.transform.position;

        //    Vector3 newPos = rect.transform.position + (Vector3)mouseInputVal * mouseSensitivity;

        //    // 画面の外に出ないように移動制限
        //    Vector2 screenPos = Camera.main.ScreenToViewportPoint(newPos);
        //    Vector2 viewClamp = new Vector2(screenPos.x, screenPos.y);
        //    if (0 <= viewClamp.x && viewClamp.x <= 1 && viewClamp.y <= 1 && 0 <= viewClamp.y)
        //    {
        //        rect.transform.position = newPos;

        //        WorldMousePos = transform.position;

        //        ScreenMousePos = rect.transform.position;
        //    }
        //    RayHitMove();

        //    pointerGameObjOld = pointerGameObj;
        //}
    }
}
