using System;
using System.Collections.Generic;

[Serializable]
public class MissionCategoryModel
{
    public int mission_category; // ミッションカテゴリー
    public string category_name; // カテゴリーの名前
}

public class MissionCategories : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists mission_categories(mission_category tinyint,category_name varchar,primary key(mission_category))";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(MissionCategoryModel[] mission_categories_list)
    {
        foreach (MissionCategoryModel mission_category in mission_categories_list)
        {
            setQuery = "insert or replace into mission_categories(mission_category,category_name) values(" + mission_category.mission_category + ",\"" + mission_category.category_name + "\")";
            RunQuery(setQuery);
        }
    }

    // 全てのガチャカテゴリーデータを取得
    public static MissionCategoryModel[] GetMissionCategoryDataAll()
    {
        List<MissionCategoryModel> missionCategoryList = new();
        getQuery = "select * from mission_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            MissionCategoryModel missionCategoryModel = new();
            missionCategoryModel.mission_category = int.Parse(dr["mission_category"].ToString());
            missionCategoryModel.category_name = dr["category_name"].ToString();
            missionCategoryList.Add(missionCategoryModel);
        }
        return missionCategoryList.ToArray();
    }

    // 指定された武器カテゴリーのデータのみ取得
    public static MissionCategoryModel GetMissionCategoryData(int mission_category)
    {
        MissionCategoryModel missionCategoryModel = new();
        getQuery = string.Format("select * from mission_categories where mission_category = {0}", mission_category);

        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            missionCategoryModel.mission_category = int.Parse(dr["mission_category"].ToString());
            missionCategoryModel.category_name = dr["category_name"].ToString();
        }
        return missionCategoryModel;
    }
}