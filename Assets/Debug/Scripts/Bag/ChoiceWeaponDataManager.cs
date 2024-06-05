using TMPro;
using UnityEngine;

public class ChoiceWeaponDataManager : WeaponBase
{
    int weapon_id;
    public int WeaponId { get { return weapon_id; } }

    int weapon_level;
    public int weaponLevel { get { return weapon_level; } }

    [SerializeField] TextMeshProUGUI detailNameText, reinforceNameText;
    [SerializeField] GameObject detailWaponBack, reinforceWeaponBack;
    [SerializeField] GameObject detailBack;
    public GameObject DetailBack { get { return detailBack; } }

    private void Awake() => detailBack.SetActive(false);

    // ���킪�I�����ꂽ���A������i���̂��߂ɕ���̊�{�f�[�^��ۑ�����
    public void SetChoiceWeaponData(int id, int level)
    {
        weapon_id = id;
        weapon_level = level;
    }

    // �ڍ׉�ʂ̐ݒ�
    public void SetDetailData(string item_name)
    {
        detailNameText.text = item_name;
        reinforceNameText.text = item_name;
        WeaponSetting(detailWaponBack, weapon_id);     // �o�b�O��ʂ̕���ڍ׉�ʂ̕���摜�ύX
        WeaponSetting(reinforceWeaponBack, weapon_id); // ������ʂ̕���ڍ׉�ʂ̕���摜�ύX
    }

}
