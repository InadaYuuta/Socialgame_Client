using System;
using System.Collections.Generic;

[Serializable]
public class LogMasterModel
{
    public int log_id;          // お知らせID
    public int log_category;    // お知らせカテゴリー
    public string log_content;  // お知らせの内容
}

public class LogMaster : TableBase
{
    // テーブル作成
    // TODO: 変数の型を決めた
    public static void CreateTable()
    {
        createQuery = "create table if not exists log_masters(log_id bigint,log_category tinyint,log_content text,primary key(log_id))";
        RunQuery(createQuery);
        // インデックス作成
        createQuery = "CREATE INDEX IF NOT EXISTS log_category_index ON log_masters(log_category);";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(LogMasterModel[] news_model)
    {
        foreach (LogMasterModel news in news_model)
        {
            setQuery = "insert or replace into log_masters(log_id ,log_category ,log_content) values(\"" + news.log_id + "," + news.log_category + "\"," + news.log_content + ")";
            RunQuery(setQuery);
        }
    }

    // 全てのミッションデータを取得
    public static LogMasterModel[] GetLogDataAll()
    {
        List<LogMasterModel> logMasterList = new();
        getQuery = "select * from log_masters";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            LogMasterModel logMasterModel = new();
            logMasterModel.log_id = int.Parse(dr["log_id"].ToString());
            logMasterModel.log_category = int.Parse(dr["log_category"].ToString());
            logMasterModel.log_content = dr["log_content"].ToString();
            logMasterList.Add(logMasterModel);
        }
        return logMasterList.ToArray();
    }

    // 指定されたミッションデータを取得
    public static LogMasterModel[] GetLogData(int log_id)
    {
        List<LogMasterModel> logMasterList = new();
        getQuery = string.Format("select * from log_masters where log_id={0}", log_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            LogMasterModel logMasterModel = new();
            logMasterModel.log_id = int.Parse(dr["log_id"].ToString());
            logMasterModel.log_category = int.Parse(dr["log_category"].ToString());
            logMasterModel.log_content = dr["log_content"].ToString();
            logMasterList.Add(logMasterModel);
        }
        return logMasterList.ToArray();
    }
}