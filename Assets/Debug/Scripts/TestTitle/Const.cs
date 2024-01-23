using UnityEngine;

namespace GameUtil
{
    public class Const : MonoBehaviour
    {
        /** DB情報 */
        public const string RESISTRATION_URL = "http://localhost/api/register"; // サーバーURL
        public const string LOGIN_URL = "http://localhost/api/login";           // サーバーURL
        public const string SQLITE_FILE_NAME = "SQLiteFile.db";

        /** エラーID */
        public const string ERROR_DB_UPDATE = "1";
    }
}

