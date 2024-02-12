using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class LogCategoryModel
{
    public int log_category;
    public string category_name;
}

public class LogCategories : TableBase
{
    public static void CreateTable()
    {
        createQuery = "create table if not exists log_categories (log_category tinyint, category_name varchar,primary key(log_category))";
        RunQuery(createQuery);
    }

    public static void Set(LogCategoryModel[] log_category_model_list)
    {
        foreach (LogCategoryModel logCategoryModel in log_category_model_list)
        {
            setQuery = "insert or replace into log_categories (log_category,category_name) values(" + logCategoryModel.log_category + ", \"" + logCategoryModel.category_name + "\") ";
            RunQuery(setQuery);
        }
    }

    // 全てのカテゴリーを取得
    public static LogCategoryModel[] GetLogCategoryAll()
    {
        List<LogCategoryModel> log_category_model_list = new();
        getQuery = "select * from log_categories";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            LogCategoryModel logCategoryModel = new();
            logCategoryModel.log_category = int.Parse(dr["log_category"].ToString());
            logCategoryModel.category_name = dr["category_name"].ToString();
            log_category_model_list.Add(logCategoryModel);
        }
        return log_category_model_list.ToArray(); // Listを配列に変換して返す
    }

    // 指定したカテゴリーだけを取得
    public static LogCategoryModel GetLogCategory(string log_category)
    {
        LogCategoryModel logCategoryModel = new();
        getQuery = "select * from log_categories where log_category" + log_category;
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            logCategoryModel.log_category = int.Parse(dr["log_category"].ToString());
            logCategoryModel.category_name = dr["category_name"].ToString();
        }
        return logCategoryModel;
    }
}