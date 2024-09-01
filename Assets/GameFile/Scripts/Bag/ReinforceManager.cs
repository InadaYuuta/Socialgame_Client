using UnityEngine;

public class ReinforceManager : MonoBehaviour
{
    [SerializeField] GameObject reinforcePanel, evolutionPanel;
    LimitBreakManager limitBreakManager;
    LevelUpManager levelUpManager;
    EvolutionManager evolutionManager;

    ChoiceWeaponDataManager weaponData;
    BagSortManager bagSortManager;

    private void Awake()
    {
        limitBreakManager = GetComponent<LimitBreakManager>();
        levelUpManager = GetComponent<LevelUpManager>();
        evolutionManager = FindObjectOfType<EvolutionManager>();
        weaponData = FindObjectOfType<ChoiceWeaponDataManager>();
        bagSortManager = FindObjectOfType<BagSortManager>();
    }

    // パネル表示非表示 -----
    public void OnClickOpenReinforcePanelButton()
    {
        if (reinforcePanel == null) { return; }
        reinforcePanel.SetActive(true);
        levelUpManager.SetLevelUpWeaponParameter(weaponData.WeaponId);
        limitBreakManager.SetLimitBreakWeaponParameter(weaponData.WeaponId);
    }

    public void OnClickOpenEvolutionPanelButton()
    {
        if (evolutionPanel == null) { return; }
        evolutionPanel.SetActive(true);
        evolutionManager.SetEvolutionWeaponParameter(weaponData.WeaponId);
    }

    public void OnClickClosePanelButton()
    {
        if (evolutionPanel == null || reinforcePanel == null) { return; }
        bagSortManager.UpdateBag();
        evolutionPanel.SetActive(false);
        reinforcePanel.SetActive(false);
    }
}
