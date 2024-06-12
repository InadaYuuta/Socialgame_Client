#define LOCAL_SERVER

using UnityEngine;


namespace GameUtil
{
    public class Const : MonoBehaviour
    {
#if LOCAL_SERVER
        /** DB情報 */
        public const string REGISTRATION_URL = "http://localhost/api/register";             // 登録URL
        public const string LOGIN_URL = "http://localhost/api/login";                       // ログインURL
        public const string HOME_URL = "http://localhost/api/home";                         // ホーム情報更新URL
        public const string MASTER_CHECK_URL = "http://localhost/api/masterCheck";          // マスターデータチェックURL
        public const string MASTER_GET_URL = "http://localhost/api/masterGet";              // マスターデータ取得URL
        public const string BUY_CURRENCY_URL = "http://localhost/api/buyCurrency";          // 通貨購入URL
        public const string STAMINA_RECOVERY_URL = "http://localhost/api/staminaRecovery";  // スタミナ回復URL
        public const string STAMINA_CONSUMPTION = "http://localhost/api/testConsumption";   // スタミナ消費URL(仮)
        public const string ITEM_REGISTRATION_URL = "http://localhost/api/itemRegist";      // アイテムの登録URL
        public const string BUY_EXCHANGE_SHOP_URL = "http://localhost/api/exchangeShop";    // 交換ショップでのアイテム購入URL
        public const string GACHA_URL = "http://localhost/api/gachaExecute";                // ガチャURL
        public const string GET_GACHA_LOG = "http://localhost/api/getGachaLog";             // ガチャログ入手
        public const string WEAPON_LEVEL_UP_URL = "http://localhost/api/levelUp";           // 武器強化
        public const string WEAPON_LIMIT_BREAK_URL = "http://localhost/api/limitBreak";     // 武器限界突破
        public const string WEAPON_EVOLUTION_URL = "http://localhost/api/evolution";        // 武器進化
#elif DEVELOP_SERVER
        /** DB情報 */
        public const string REGISTRATION_URL = "http://localhost/api/register";             // 登録URL
        public const string LOGIN_URL = "http://localhost/api/login";                       // ログインURL
        public const string HOME_URL = "http://localhost/api/home";                         // ホーム情報更新URL
        public const string MASTER_CHECK_URL = "http://localhost/api/masterCheck";          // マスターデータチェックURL
        public const string MASTER_GET_URL = "http://localhost/api/masterGet";              // マスターデータ取得URL
        public const string BUY_CURRENCY_URL = "http://localhost/api/buyCurrency";          // 通貨購入URL
        public const string STAMINA_RECOVERY_URL = "http://localhost/api/staminaRecovery";  // スタミナ回復URL
        public const string STAMINA_CONSUMPTION = "http://localhost/api/testConsumption";   // スタミナ消費URL(仮)
        public const string ITEM_REGISTRATION_URL = "http://localhost/api/itemRegist";      // アイテムの登録URL
        public const string ITEM_UPDATE_URL = "http://localhost/api/itemUpdate";            // アイテムの更新URL
        public const string BUY_EXCHANGE_SHOP_URL = "http://localhost/api/exchangeShop";    // 交換ショップでのアイテム購入URL
        public const string GACHA_URL = "http://localhost/api/gachaExcute";                 // ガチャURL
        public const string GET_GACHA_LOG = "http://localhost/api/getGachaLog";             // ガチャログ入手
        public const string WEAPON_LEVEL_UP_URL = "http://localhost/api/levelUp";           // 武器強化
        public const string WEAPON_LIMIT_BREAK_URL = "http://localhost/api/limitBreak";     // 武器限界突破
        public const string WEAPON_EVOLUTION_URL = "http://localhost/api/evolution";        // 武器進化
#endif


        public const string SQLITE_FILE_NAME = "SQLiteFile.db";

        /** エラーID */
        public const string ERROR_DB_UPDATE = "1";
        public const string ERROR_MASTER_DATA_UPDATE = "2";
    }
}

