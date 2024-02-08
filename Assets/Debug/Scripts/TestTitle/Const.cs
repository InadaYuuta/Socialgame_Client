using UnityEngine;

namespace GameUtil
{
    public class Const : MonoBehaviour
    {
        /** DB情報 */
        public const string RESISTRATION_URL = "http://localhost/api/register";             // 登録URL
        public const string LOGIN_URL = "http://localhost/api/login";                       // ログインURL
        public const string MASTER_CHECK_URL = "http://localhost/api/masterCheck";          // マスターデータチェックURL
        public const string MASTER_GET_URL = "http://localhost/api/masterGet";              // マスターデータ取得URL
        public const string BUY_CURRENCY_URL = "http://localhost/api/buyCurrency";          // 通貨購入URL
        public const string STAMINA_RECOVERY_URL = "http://localhost/api/staminaRecovery";  // スタミナ回復URL
        public const string STAMINA_CONSUMPTION = "http://localhost/api/testConsumption";   // スタミナ消費URL(仮)

        public const string SQLITE_FILE_NAME = "SQLiteFile.db";

        /** エラーID */
        public const string ERROR_DB_UPDATE = "1";
        public const string ERROR_MASTER_DATA_UPDATE = "2";
    }
}

