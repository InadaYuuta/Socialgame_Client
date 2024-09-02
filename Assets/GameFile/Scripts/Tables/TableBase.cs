using System;
using UnityEngine;

public class TableBase : MonoBehaviour
{
    protected static string createQuery = "";
    protected static string setQuery = "";
    protected static string getQuery = "select * from weapons";

    // TODO:メソッドとかも一括で継承すればできるようにする
    //// テーブル作成
    //protected static void CreateTable()=>RunQuery(createQuery);

    //// レコード登録処理
    //protected static void Set<T>(ref T models)
    //{
    //    RunQuery(setQuery);
    //}

    // レコード取得処理
    //protected static T Get<T>(Action<T> action) where T : TableBase, new()
    //{
    //    string query = getQuery;
    //    DataTable dataTable = RunQuery(query);

    //    var models = new T();
    //    foreach (DataRow dr in dataTable.Rows)
    //    {
    //        action(models);
    //    }
    //    return models;
    //}

    //// ここに取得の詳しい処理を入れる
    //public virtual void GetModelParts<T>(ref T models)
    //{

    //}

    /// <summary>
    /// クエリを走らせる奴
    /// queryには走らせたいsqlを書く
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    protected static DataTable RunQuery(string query)
    {
        SqliteDatabase sqlDB = new SqliteDatabase(GameUtil.Const.SQLITE_FILE_NAME);
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        return dataTable;
    }

    /// <summary>
    /// エスケープ処理を行う(返ってくる文字列の中に/があると文字列にしたときにおかしくなるからエスケープ処理を行う)
    /// </summary>
    /// <param name="target">エスケープ処理したい文字列</param>
    /// <returns></returns>
    protected static string EscapeString(string target)
    {
        return target.Replace("\"", "\"\"");
    }
}
