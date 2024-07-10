using System;
using System.Collections.Generic;

[Serializable]
public class PresentBoxModel
{
    public int present_id;             // プレゼントID
    public int whole_present_id;       // 全体プレゼントID
    public int reward_category;        // 報酬のカテゴリー
    public string present_box_reward;  // 報酬の内容
    public string receive_reason;      // 受け取った理由
    public int receipt;                // 受取
    public string receipt_date;           // 受取日
    public string display;             // 表示期限
}

public class PresentBoxes : TableBase
{
    // テーブル作成
    public static void CreateTable()
    {
        createQuery = "create table if not exists present_boxes(user_id varchar,present_id bigint,whole_present_id bigint,reward_category tinyint,present_box_reward text,receive_reason text,receipt tinyint,receipt_date varchar,display varchar,primary key(user_id,present_id))";
        RunQuery(createQuery);
        //// インデックス作成 TODO:今後サーバー側でリワードカテゴリーをインデックスにする場合はコメントアウト解除
        //createQuery = "CREATE INDEX IF NOT EXISTS reward_category_index ON present_boxes(reward_category);";
        //RunQuery(createQuery);
    }

    // レコード登録処理
    public static void Set(PresentBoxModel[] present_box_model, string user_id)
    {
        foreach (PresentBoxModel present_box in present_box_model)
        {
            // エスケープ処理を行う
            string escapedPresentBoxReward = EscapeString(present_box.present_box_reward);
            string escapedReceiveReason = EscapeString(present_box.receive_reason);
            string escapedDisplay = EscapeString(present_box.display);
            string escapedReceipted = EscapeString(present_box.receipt_date);

            setQuery = "insert or replace into present_boxes(user_id, present_id, whole_present_id, reward_category, present_box_reward, receive_reason, receipt, receipt_date, display) values(" +
                       "\"" + user_id + "\", " +
                       present_box.present_id + ", " +
                       present_box.whole_present_id + ", " +
                       present_box.reward_category + ", " +
                       "\"" + escapedPresentBoxReward + "\", " +
                       "\"" + escapedReceiveReason + "\", " +
                       present_box.receipt + ", " +
                       "\"" + escapedReceipted + "\", " +
                       "\"" + escapedDisplay + "\")";
            RunQuery(setQuery);
        }
    }

    // 全てのプレゼントボックスデータを取得
    public static PresentBoxModel[] GetPresentBoxDataAll()
    {
        List<PresentBoxModel> PresentBoxList = new();
        getQuery = "select * from present_boxes";
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PresentBoxModel PresentBoxModel = new();
            PresentBoxModel.present_id = int.Parse(dr["present_id"].ToString());
            PresentBoxModel.whole_present_id = int.Parse(dr["whole_present_id"].ToString());
            PresentBoxModel.reward_category = int.Parse(dr["reward_category"].ToString());
            PresentBoxModel.present_box_reward = dr["present_box_reward"].ToString();
            PresentBoxModel.receive_reason = dr["receive_reason"].ToString();
            PresentBoxModel.receipt = int.Parse(dr["receipt"].ToString());
            PresentBoxModel.receipt_date = dr["receipt_date"].ToString();
            PresentBoxModel.display = dr["display"].ToString();
            PresentBoxList.Add(PresentBoxModel);
        }
        return PresentBoxList.ToArray();
    }

    // 指定されたプレゼントデータを取得
    public static PresentBoxModel GetPresentBoxData(int present_id)
    {
        PresentBoxModel PresentBoxModel = new();
        getQuery = string.Format("select * from present_boxes where present_id={0}", present_id);
        DataTable dataTable = RunQuery(getQuery);
        foreach (DataRow dr in dataTable.Rows)
        {
            PresentBoxModel.present_id = int.Parse(dr["present_id"].ToString());
            PresentBoxModel.whole_present_id = int.Parse(dr["whole_present_id"].ToString());
            PresentBoxModel.reward_category = int.Parse(dr["reward_category"].ToString());
            PresentBoxModel.present_box_reward = dr["present_box_reward"].ToString();
            PresentBoxModel.receive_reason = dr["receive_reason"].ToString();
            PresentBoxModel.receipt = int.Parse(dr["receipt"].ToString());
            PresentBoxModel.receipt_date = dr["receipt_date"].ToString();
            PresentBoxModel.display = dr["display"].ToString();
        }
        return PresentBoxModel;
    }


    // エスケープ処理を行う(返ってくる文字列の中に/があると文字列にしたときにおかしくなるからエスケープ処理を行う)
    public static string EscapeString(string value)
    {
        return value.Replace("\"", "\"\"");
    }
}