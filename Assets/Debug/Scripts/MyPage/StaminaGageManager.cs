using UnityEngine;

public class StaminaGageManager : UsersBase
{
    int displayArea;
    [SerializeField] int displayStamina;
    int maxStamina;
    RectTransform gage;

    void Awake()
    {
        base.Awake();
        gage = GetComponent<RectTransform>();
    }

    void Start()
    {
        maxStamina = usersModel.max_stamina;
        displayStamina = usersModel.last_stamina * 2;
    }

    void Update()
    {
        SetDisplayValue();
    }

    // •\Ž¦‚·‚é”ÍˆÍ‚ðŒˆ‚ß‚é
    void SetDisplayValue()
    {
        base.Update();
        maxStamina = usersModel.max_stamina;
        displayStamina = usersModel.last_stamina * 2;
        displayArea = maxStamina - displayStamina / 2;
        gage.sizeDelta = new Vector2(displayStamina, gage.sizeDelta.y);
        gage.anchoredPosition = new Vector2(-displayArea, -20);
    }
}
