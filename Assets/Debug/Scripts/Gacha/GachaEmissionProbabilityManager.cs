using TMPro;
using UnityEngine;

public class GachaEmissionProbabilityManager : MonoBehaviour
{
    [SerializeField] GameObject ePPanel;
    [SerializeField] TextMeshProUGUI ePText;

    string[] weaponNames;
    int[] weaponIds, weights;

    int count = 0;
    string emissionProbabilityString = "提供割合\r\n\r\nSRARA:3%\r\nRARA:17%\r\nCOMON:80%\n\n\n\n";

    GachaWeaponModel[] gachaWeaponModel;

    void Start()
    {
        gachaWeaponModel = GachaWeapons.GetSortDataAll();
        weaponNames = new string[gachaWeaponModel.Length];
        weaponIds = new int[gachaWeaponModel.Length];
        weights = new int[gachaWeaponModel.Length];

        UpdateText();
        ePPanel.SetActive(false);
    }

    // 戻るボタンが押されたら
    public void PushBackButton()
    {
        ePPanel.SetActive(false);
    }

    // 情報の取得
    void GetData()
    {
        gachaWeaponModel = GachaWeapons.GetSortDataAll();
        foreach (GachaWeaponModel gachaWeaponData in gachaWeaponModel)
        {
            weaponIds[count] = gachaWeaponData.weapon_id;
            weaponNames[count] = WeaponMaster.GetWeaponMasterData(weaponIds[count]).weapon_name;
            weights[count] = gachaWeaponData.weight;
            emissionProbabilityString = string.Format("{0}{1}:{2}%\r\n", emissionProbabilityString, weaponNames[count], weights[count] / 1000);
            count++;
        }
        count = 0;
    }

    // テキストの更新
    void UpdateText()
    {
        GetData();
        ePText.text = emissionProbabilityString;
    }


}
