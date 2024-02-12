using UnityEngine;

public class ChangeHomeMode : MonoBehaviour
{
    enum HomeMode { Option, Home, Bag, PictureBook, Shop }
    HomeMode currentMode = HomeMode.Home;

    [SerializeField] GameObject shopCanvas;

    void Awake()
    {
        currentMode = HomeMode.Home;
        shopCanvas.SetActive(false);
    }

    // オプションが選択された時
    public void ChoiceOption()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
        currentMode = HomeMode.Option;
    }

    // ホームが選択された時
    public void ChoiceHome()
    {
        shopCanvas.SetActive(false);
        currentMode = HomeMode.Home;
    }

    // ショップが選択された時
    public void ChoiceShop()
    {
        shopCanvas.SetActive(true);
        currentMode = HomeMode.Shop;
    }

    // バッグが選択された時
    public void ChoiceBag()
    {
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
        currentMode = HomeMode.Bag;
    }
}
