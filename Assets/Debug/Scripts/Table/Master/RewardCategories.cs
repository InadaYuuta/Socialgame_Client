using System;
using System.Collections.Generic;

[Serializable]
public class RewardCategoryModel
{
    public int reward_category;  // 報酬カテゴリー
    public string category_name; // カテゴリーの名前
}

public class RewardCategories : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists reward_categories(reward_category tinyint,category_name varchar,primary key(reward_category))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(RewardCategoryModel[] mission_categories_list)
    {
        foreach (RewardCategoryModel mission_category in mission_categories_list)
        {
            setQuery = "insert or replace into reward_categories(reward_category,category_name) values(" + mission_category.reward_category + ",\"" + mission_category.category_name + "\")";
            RunQuery(setQuery);
        }
    }

    // 全てのガチャカテゴリーデータを取得
    public static RewardCategoryModel[] GetRewardCategoryDataAll()
    {
        List<RewardCategoryModel> weaponCategoryList = new();
        getQuery = "select * from reward_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            RewardCategoryModel rewardCategoryModel = new();
            rewardCategoryModel.reward_category = int.Parse(dr["reward_category"].ToString());
            rewardCategoryModel.category_name = dr["category_name"].ToString();
            weaponCategoryList.Add(rewardCategoryModel);
        }
        return weaponCategoryList.ToArray();
    }

    // 指定された武器カテゴリーのデータのみ取得
    public static RewardCategoryModel GetRewardCategoryData(int reward_category)
    {
        RewardCategoryModel rewardCategoryModel = new();
        getQuery = string.Format("select * from reward_categories where reward_category = {0}", reward_category);

        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            rewardCategoryModel.reward_category = int.Parse(dr["reward_category"].ToString());
            rewardCategoryModel.category_name = dr["category_name"].ToString();
        }
        return rewardCategoryModel;
    }
}