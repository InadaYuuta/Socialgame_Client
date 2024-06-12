using UnityEngine;
using UnityEngine.UI;

public class WeaponBase : MonoBehaviour
{
    Color comonPlusColor = new(0.509804f, 0.6666667f, 1);
    Color rarePlusColor = new(1, 0.509804f, 0.509804f);
    Color srarePlusColor = new(1, 0.9758152f, 0.9758152f);

    protected void WeaponSetting(GameObject weapon, int weaponId)
    {
        Image weaponImage = weapon.transform.GetChild(0).GetComponent<Image>();
        Image weaponBack = weapon.GetComponent<Image>();
        weaponImage.sprite = Resources.Load<Sprite>(string.Format("WeaponImage/w{0}", weaponId.ToString())); // Resources�t�H���_�̒��̓���̉摜���擾���ē����
        Outline outline = weapon.GetComponent<Outline>();
        int rarity = WeaponMaster.GetWeaponMasterData(weaponId).rarity_id;
        switch (rarity)
        {
            case 1: // Comon
                outline.effectColor = Color.blue;
                weaponBack.color = Color.white;
                break;
            case 2: // Rare 
                outline.effectColor = Color.red;
                weaponBack.color = Color.white;
                break;
            case 3: // SRare
                outline.effectColor = Color.yellow;
                weaponBack.color = Color.white;
                break;
            case 4: // Comon+
                outline.effectColor = Color.blue;
                weaponBack.color = comonPlusColor;
                break;
            case 5: // Rare+
                outline.effectColor = Color.red;
                weaponBack.color = rarePlusColor;
                break;
            case 6: // SRare+
                outline.effectColor = Color.yellow;
                weaponBack.color = srarePlusColor;
                break;
            default:
                break;
        }
    }

    // ����̃C���[�W������ς���
    protected void ChangeOnlyWeaponImage(Image weaponImage, int weaponId) => weaponImage.sprite = Resources.Load<Sprite>(string.Format("WeaponImage/w{0}", weaponId.ToString())); // Resources�t�H���_�̒��̓���̉摜���擾���ē����


    // �w�肵�������̎w��̌��̐��l��Ԃ� �Q�l�T�C�ghttps://santerabyte.com/c-sharp-get-nth-digit-num/
    protected int GetNthDigitNum(int num, int digit)
    {
        int currentDigitNum = 1;
        num = System.Math.Abs(num);
        while (num > 0)
        {
            if (currentDigitNum == digit) return num % 10;
            num /= 10;
            currentDigitNum++;
        }
        return 0;
    }

    // �i���ɕK�v�ȃ|�C���g��Ԃ�(����̎����Ńe�[�u�����ł����肵����ύX����
    protected int GetTheReinforcePointsNeededForEvolution(int weaponId)
    {
        int neededReinforcePoint = 0;

        int rarityId = Weapons.GetWeaponData(weaponId).rarity_id;

        switch (rarityId)
        {
            case 1:
                neededReinforcePoint = 5000;
                break;
            case 2:
                neededReinforcePoint = 10000;
                break;
            case 3:
                neededReinforcePoint = 15000;
                break;
            default:
                Debug.Log("�w��O�̃��A���e�B");
                neededReinforcePoint = 0;
                break;
        }


        return neededReinforcePoint;
    }
}
