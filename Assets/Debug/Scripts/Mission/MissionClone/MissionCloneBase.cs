using UnityEngine;

public class MissionCloneBase : MonoBehaviour
{
    protected enum ConditionOfAchievement // �~�b�V�����B������
    {
        Gacha = 1,  �@// �K�`������������
        Login,      �@// ���O�C������
        GetWeapon,  �@// ����̊l����
        LevelUp,      // ��������̍��v���x��
        Evolution = 5 // ����̐i����
    }

    protected enum MissionCondition // �~�b�V�����̏��
    {
        CONSTANCY, // �P��
        DAILY,     // �f�C���[
        WEEKLY     // �E�B�[�N���[
    }

    protected ConditionOfAchievement thisCondition = ConditionOfAchievement.Gacha;

    protected UpdateConditionOfAchievement updateData;

    protected void Awake()
    {
        updateData = FindObjectOfType<UpdateConditionOfAchievement>();
    }

    /// <summary>
    /// �w�肵�������̎w��̌��̐��l��Ԃ� �Q�l�T�C�ghttps://santerabyte.com/c-sharp-get-nth-digit-num/
    /// </summary>
    /// <param name="num">�叫�̐��l</param>
    /// <param name="digit">�����ڂ̐��l��Ԃ�������(�E���琔����)</param>
    /// <returns></returns>
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

    // �����B���󋵂Ȃ̂��m�F
    protected void CheckCondition(int id)
    {
        int check = GetNthDigitNum(id, 5);
        switch (check)
        {
            case 1:
                thisCondition = ConditionOfAchievement.Gacha;
                break;
            case 2:
                thisCondition = ConditionOfAchievement.Login;
                break;
            case 3:
                thisCondition = ConditionOfAchievement.GetWeapon;
                break;
            case 4:
                thisCondition = ConditionOfAchievement.LevelUp;
                break;
            case 5:
                thisCondition = ConditionOfAchievement.Evolution;
                break;
            default:
                Debug.Log("�T�����s�A��O���o�Ă���F" + id + " check: " + check);
                break;
        }
    }


}
