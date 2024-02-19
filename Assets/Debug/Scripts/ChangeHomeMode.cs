using UnityEngine;

public class ChangeHomeMode : MonoBehaviour
{
    enum HomeMode { Option, Home, Bag, PictureBook, Shop }
    HomeMode currentMode = HomeMode.Home;

    [SerializeField] GameObject shopCanvas, bagCanvas;

    void Awake()
    {
        currentMode = HomeMode.Home;
        shopCanvas.SetActive(false);
        bagCanvas.SetActive(false);
    }

    // オプションが選択された時
    public void ChoiceOption()
    {
        currentMode = HomeMode.Option;
        StartCoroutine(ResultPanelController.DisplayResultPanel("今後実装予定!\n乞うご期待!"));
    }

    // ホームが選択された時
    public void ChoiceHome()
    {
        currentMode = HomeMode.Home;
        shopCanvas.SetActive(false);
        bagCanvas.SetActive(false);
    }

    // ショップが選択された時
    public void ChoiceShop()
    {
        currentMode = HomeMode.Shop;
        shopCanvas.SetActive(true);
        bagCanvas.SetActive(false);
    }

    // バッグが選択された時
    public void ChoiceBag()
    {
        currentMode = HomeMode.Bag;
        bagCanvas.SetActive(true);
        shopCanvas.SetActive(false);
    }
}
