using UnityEngine;

public class UpdateConditionOfAchievement : MonoBehaviour
{
    [SerializeField] int pullGachaCount = 0;  // ガチャを引いた回数
    [SerializeField] int loginCount = 0;      // ログイン日数
    [SerializeField] int getWeaponCount = 0;  // 合計武器取得数
    [SerializeField] int totalLevelCount = 0; // 合計武器レベル
    [SerializeField] int evolutionCount = 0;  // 合計武器進化数

    public int PullGachaCount { get { return pullGachaCount; } }
    public int LoginCount { get { return loginCount; } }
    public int GetWeaponCount { get { return getWeaponCount; } }
    public int TotalLevelCount { get { return totalLevelCount; } }
    public int EvolutionCount { get { return evolutionCount; } }

    void Start() => GetAchievementCount();

    void Update() => GetAchievementCount();

    // 所持している武器の合計レベルを取得
    int GetTotalLevel()
    {
        int totalLevel = 0;

        var targets = Weapons.GetWeaponDataAll();

        foreach (var target in targets)
        {
            totalLevel += target.level;
        }

        return totalLevel;
    }

    // 所持している武器の合計進化数(現在進化されているもの)を取得
    int GetTotalEvolution()
    {
        int totalEvolution = 0;

        var targets = Weapons.GetWeaponDataAll();

        foreach (var target in targets)
        {
            var evolution = target.evolution;
            if (evolution > 0)
            {
                totalEvolution++;
            }
        }

        return totalEvolution;
    }

    // 現在の達成状況を取得
    void GetAchievementCount()
    {
        pullGachaCount = GachaLogs.GetGacaLogDataAll().Length;
        loginCount = Users.Get().login_days;
        getWeaponCount = pullGachaCount;      // TODO: 現状の武器の入手方法がガチャのみなので代入、今後クエストやプレゼントで受け取るようにするなら処理を変更
        totalLevelCount = GetTotalLevel();
        evolutionCount = GetTotalEvolution();
    }
}
