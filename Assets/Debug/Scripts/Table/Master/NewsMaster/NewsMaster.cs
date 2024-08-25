using System;
using System.Collections.Generic;

[Serializable]
public class NewsMasterModel
{
    public int news_id;          // お知らせID
    public int news_category;    // お知らせカテゴリー
    public string news_name;     // お知らせ名
    public string news_content;  // お知らせの内容
    public int display_priority; // 表示優先度
    public string created;       // 作成日
    public string period_end;    // 終了日時
}

public class NewsMaster : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists news_masters(news_id bigint,news_category tinyint,news_name text,news_content text,display_priority smallint,created varcher,period_end varcher,primary key(news_id))";
        RunQuery(createQuery);
        // インデックス作成
        createQuery = "CREATE INDEX IF NOT EXISTS news_category_index ON news_masters(news_category);";
        RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(NewsMasterModel[] news_model)
    {
        foreach (NewsMasterModel news in news_model)
        {
            // エスケープ処理を行う
            string escapedNewsName = EscapeString(news.news_name);
            string escapedNewsContent = EscapeString(news.news_content);
            string escapedNewsCreated = EscapeString(news.created);
            string escapedNewsEnd = EscapeString(news.period_end);

            setQuery = "insert or replace into news_masters(news_id, news_category, news_name, news_content, display_priority, created, period_end) " +
                   "values(" + news.news_id + ", " + news.news_category + ", \"" + escapedNewsName + "\", \"" + escapedNewsContent + "\", " +
                   news.display_priority + ", \"" + escapedNewsCreated + "\", \"" + escapedNewsEnd + "\")";
            RunQuery(setQuery);
        }
    }

    // 全てのミッションデータを取得
    public static NewsMasterModel[] GetNewsDataAll()
    {
        List<NewsMasterModel> newsMasterList = new();
        getQuery = "select * from news_masters";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            NewsMasterModel newsMasterModel = new();
            newsMasterModel.news_id = int.Parse(dr["news_id"].ToString());
            newsMasterModel.news_category = int.Parse(dr["news_category"].ToString());
            newsMasterModel.news_name = dr["news_name"].ToString();
            newsMasterModel.news_content = dr["news_content"].ToString();
            newsMasterModel.display_priority = int.Parse(dr["display_priority"].ToString());
            newsMasterModel.created = dr["created"].ToString(); // TODO: 今後お知らせ関連を作るときに日時で取得できるメソッドを追加する
            newsMasterModel.period_end = dr["period_end"].ToString(); // TODO: 今後お知らせ関連を作るときに日時で取得できるメソッドを追加する
            newsMasterList.Add(newsMasterModel);
        }
        return newsMasterList.ToArray();
    }

    // 指定されたミッションデータを取得
    public static NewsMasterModel GetNewsData(int news_id)
    {
        NewsMasterModel newsMasterModel = new();
        getQuery = string.Format("select * from news_masters where news_id={0}", news_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            newsMasterModel.news_id = int.Parse(dr["news_id"].ToString());
            newsMasterModel.news_category = int.Parse(dr["news_category"].ToString());
            newsMasterModel.news_name = dr["news_name"].ToString();
            newsMasterModel.news_content = dr["news_content"].ToString();
            newsMasterModel.display_priority = int.Parse(dr["display_priority"].ToString());
            newsMasterModel.created = dr["created"].ToString(); // TODO: 今後お知らせ関連を作るときに日時で取得できるメソッドを追加する
            newsMasterModel.period_end = dr["period_end"].ToString(); // TODO: 今後お知らせ関連を作るときに日時で取得できるメソッドを追加する
        }
        return newsMasterModel;
    }

    // 指定されたカテゴリーのミッションデータを取得
    public static NewsMasterModel[] GetNewsCategory(int category)
    {
        List<NewsMasterModel> newsMasterList = new();
        getQuery = string.Format("select * from news_masters where news_category={0}", category);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            NewsMasterModel newsMasterModel = new();
            newsMasterModel.news_id = int.Parse(dr["news_id"].ToString());
            newsMasterModel.news_category = int.Parse(dr["news_category"].ToString());
            newsMasterModel.news_name = dr["news_name"].ToString();
            newsMasterModel.news_content = dr["news_content"].ToString();
            newsMasterModel.display_priority = int.Parse(dr["display_priority"].ToString());
            newsMasterModel.created = dr["created"].ToString(); // TODO: 今後お知らせ関連を作るときに日時で取得できるメソッドを追加する
            newsMasterModel.period_end = dr["period_end"].ToString(); // TODO: 今後お知らせ関連を作るときに日時で取得できるメソッドを追加する
            newsMasterList.Add(newsMasterModel);
        }
        return newsMasterList.ToArray();
    }
}