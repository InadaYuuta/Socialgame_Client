using UnityEngine;
using UnityEngine.UI;

public class ShopChangeManager : MonoBehaviour
{
    enum ShopState { Payment, Stamina, Fragment }
    ShopState currentState = ShopState.Payment;

    [SerializeField] GameObject paymentShopPanel;
    [SerializeField] GameObject staminaShopPanel;
    [SerializeField] GameObject fragmentShopPanel;

    [SerializeField] Image paymentButton, staminaButton, fragmentButton;

    Color choiceColor = new Color(255 / 255f, 165 / 255f, 0 / 255f);     // オレンジ
    Color unChoiceColor = new Color(135 / 255f, 206 / 255f, 250 / 255f); // 水色

    void Start()
    {
        ActivePanel(false, false, false);
        ChangeImageColor(unChoiceColor, unChoiceColor, unChoiceColor);
    }

    void Update()
    {
        DisplayPanel();
    }

    // パネルのSetActiveを変える
    void ActivePanel(bool paymentFlag, bool staminaFlag, bool fragmentFlag)
    {
        if (paymentShopPanel == null || staminaShopPanel == null || fragmentShopPanel == null) { return; }
        paymentShopPanel.SetActive(paymentFlag);
        staminaShopPanel.SetActive(staminaFlag);
        fragmentShopPanel.SetActive(fragmentFlag);
    }

    // パネルイメージの色を変える
    void ChangeImageColor(Color paymentColor, Color staminaColor, Color fragmentColor)
    {
        paymentButton.color = paymentColor;
        staminaButton.color = staminaColor;
        fragmentButton.color = fragmentColor;
    }

    // 表示するパネルを変える
    void DisplayPanel()
    {
        switch (currentState)
        {
            case ShopState.Payment:
                ActivePanel(true, false, false);
                ChangeImageColor(choiceColor, unChoiceColor, unChoiceColor);
                break;
            case ShopState.Stamina:
                ActivePanel(false, true, false);
                ChangeImageColor(unChoiceColor, choiceColor, unChoiceColor);
                break;
            case ShopState.Fragment:
                ActivePanel(false, false, true);
                ChangeImageColor(unChoiceColor, unChoiceColor, choiceColor);
                break;
        }
    }

    // 通貨ショップのボタンが選択された時
    public void ChoicePaymentShop() => currentState = ShopState.Payment;
    // スタミナショップ(回復画面)が選択された時
    public void ChoiceStaminaShop() => currentState = ShopState.Stamina;
    // かけらショップが選択された時
    public void ChoiceFragmentShop() => currentState = ShopState.Fragment;
}
