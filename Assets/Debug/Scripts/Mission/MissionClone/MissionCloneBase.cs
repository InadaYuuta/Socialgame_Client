using UnityEngine;

public class MissionCloneBase : MonoBehaviour
{
    protected enum ConditionOfAchievement // ミッション達成条件
    {
        Gacha = 1,  　// ガチャを引いた回数
        Login,      　// ログイン日数
        GetWeapon,  　// 武器の獲得数
        LevelUp,      // 所持武器の合計レベル
        Evolution = 5 // 武器の進化回数
    }

    protected enum MissionCondition // ミッションの状態
    {
        CONSTANCY, // 恒常
        DAILY,     // デイリー
        WEEKLY     // ウィークリー
    }

    protected ConditionOfAchievement thisCondition = ConditionOfAchievement.Gacha;

    protected UpdateConditionOfAchievement updateData;

    protected void Awake()
    {
        updateData = FindObjectOfType<UpdateConditionOfAchievement>();
    }

    /// <summary>
    /// 指定した数字の指定の桁の数値を返す 参考サイトhttps://santerabyte.com/c-sharp-get-nth-digit-num/
    /// </summary>
    /// <param name="num">大将の数値</param>
    /// <param name="digit">何桁目の数値を返したいか(右から数えて)</param>
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

    // 何が達成状況なのか確認
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
                Debug.Log("探査失敗、例外が出ている：" + id + " check: " + check);
                break;
        }
    }


}
