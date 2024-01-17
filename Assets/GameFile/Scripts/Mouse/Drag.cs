using UnityEngine;

public class Drag : MonoBehaviour
{
    LeftClick leftClick;

    float pressTime = 0f;                        // ボタンが押されている時間
    [SerializeField] float dragTime = 0.2f;       // ドラッグ状態にする時間

    bool isDrag = false;
    public bool IsDrag { get { return isDrag; } }  // 読み取り専用 このフラグがtrueの時ドラッグされている状態


    void Awake()
    {
        leftClick = GetComponent<LeftClick>();
    }

    void Update()
    {
        if (!leftClick.DragStart)
        {
            pressTime = 0;
            isDrag = false;
        }
        else
        {
            pressTime += Time.deltaTime;
            if (pressTime > dragTime)
            {
                isDrag = true;
                Debug.Log("ドラッグ中");
            }
        }
    }

}
