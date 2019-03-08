using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AYam.Common.DB
{

    /// <summary>
    /// SQL Server操作管理
    /// </summary>
    public class SqlServer : IDisposable
    {

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ExceptionMessage { get; private set; }

        /// <summary>
        /// エラー発生
        /// </summary>
        public bool IsError { get { return !ExceptionMessage.Length.Equals(0); } }

        #region SQL Server 接続情報

        /// <summary>
        /// 接続するサーバ名
        /// </summary>
        private string _ServerName;

        /// <summary>
        /// DB名
        /// </summary>
        private string _DbName;

        /// <summary>
        /// ユーザ名
        /// </summary>
        private string _UserName;

        /// <summary>
        /// パスワード
        /// </summary>
        private string _Password;

        /// <summary>
        /// 接続インスタンス
        /// </summary>
        private SqlConnection _SqlConnection;

        /// <summary>
        /// 接続FLG
        /// </summary>
        private bool _IsConnect = false;

        /// <summary>
        /// トランザクション
        /// </summary>
        private SqlTransaction _SqlTransaction;

        #endregion

        /// <summary>
        /// SQL Server操作管理
        /// SQL Server認証によるDB接続
        /// </summary>
        /// <param name="serverName">接続するサーバ名</param>
        /// <param name="dbName">DB名</param>
        /// <param name="userName">ユーザ名</param>
        /// <param name="password">パスワード</param>
        /// <param name="timeOut">タイムアウト時間(秒)</param>
        public SqlServer(string serverName, string dbName, string userName, string password, int timeOut = 30)
        {

            // 接続情報の保存
            _ServerName = serverName;
            _DbName = dbName;
            _UserName = userName;
            _Password = password;

            // 接続開始
            Open(false, timeOut);

        }

        /// <summary>
        /// SQL Server操作管理
        /// Windows認証によるDB接続
        /// </summary>
        /// <param name="serverName">接続するサーバ名</param>
        /// <param name="dbName">DB名</param>
        /// <param name="timeOut">タイムアウト時間(秒)</param>
        public SqlServer(string serverName, string dbName, int timeOut = 30)
        {

            // 接続情報の保存
            _ServerName = serverName;
            _DbName = dbName;
            _UserName = "";
            _Password = "";

            // 接続開始
            Open(true, timeOut);

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            // トランザクションの途中ならロールバックする
            Rollback();

            // 切断
            Close();

        }

        /// <summary>
        /// DB接続
        /// </summary>
        /// <param name="integratedSecurity">Windows認証</param>
        /// <param name="timeOut">タイムアウト時間(秒)</param>
        private void Open(bool integratedSecurity, int timeOut)
        {

            StringBuilder connectionString = new StringBuilder(256);

            try
            {

                // 接続文字列
                if (integratedSecurity)
                {

                    // Windows 認証
                    connectionString.Append("Data Source = ").Append(_ServerName).Append(";")
                                    .Append("Initial Catalog = ").Append(_DbName).Append(";")
                                    .Append("Integrated Security = True;")
                                    .Append("MultipleActiveResultSets = True;")
                                    .Append("Connection Timeout = ").Append(timeOut.ToString());

                }
                else
                {

                    // SQL Server 認証
                    connectionString.Append("Data Source = ").Append(_ServerName).Append(";")
                                    .Append("Initial Catalog = ").Append(_DbName).Append(";")
                                    .Append("User ID = ").Append(_UserName).Append(";")
                                    .Append("Password = ").Append(_Password).Append(";")
                                    .Append("MultipleActiveResultSets = True")
                                    .Append("Connection Timeout = ").Append(timeOut.ToString());

                }

                // 接続済の場合、接続解除
                if (_SqlConnection != null)
                {
                    Close();
                }

                // インスタンス生成
                _SqlConnection = new SqlConnection(connectionString.ToString());

                // DB接続
                _SqlConnection.Open();
                _IsConnect = true;

            }
            catch (Exception ex)
            {

                ExceptionMessage = ex.Message;

            }
            finally
            {

                connectionString.Clear();
                connectionString = null;

            }

        }

        /// <summary>
        /// DB切断
        /// </summary>
        private void Close()
        {

            try
            {

                // 切断
                if (_IsConnect)
                {
                    _SqlConnection.Close();
                    _IsConnect = false;
                }

                // メモリ解放
                if (_SqlConnection != null)
                {
                    _SqlConnection.Dispose();
                    _SqlConnection = null;
                }

            }
            catch (Exception ex)
            {

                ExceptionMessage = ex.Message;

            }

        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                _SqlTransaction = _SqlConnection.BeginTransaction();
            }
            catch (Exception ex)
            {

                ExceptionMessage = ex.Message;

            }
            
        }

        /// <summary>
        /// コミット
        /// </summary>
        public void Commit()
        {

            try
            {

                if (_SqlTransaction.Connection != null)
                {
                    _SqlTransaction.Commit();
                    _SqlTransaction.Dispose();
                }

            }
            catch (Exception ex)
            {

                ExceptionMessage = ex.Message;

            }

        }

        /// <summary>
        /// ロールバック
        /// </summary>
        public void Rollback()
        {

            try
            {

                if (_SqlTransaction.Connection != null)
                {
                    _SqlTransaction.Rollback();
                    _SqlTransaction.Dispose();
                }

            }
            catch (Exception ex)
            {

                ExceptionMessage = ex.Message;

            }

        }

        /// <summary>
        /// クエリ実行
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <param name="parameters">SQLパラメータ</param>
        /// <param name="queryTimeout">クエリ実行時間(秒)</param>
        /// <returns>クエリ実行結果</returns>
        public SqlDataReader ExecuteQuery(string query, Dictionary<string, object> parameters, int queryTimeout = 30)
        {

            try
            {

                using (var sqlCommand = new SqlCommand(query, _SqlConnection, _SqlTransaction))
                {

                    // タイムアウトの設定
                    sqlCommand.CommandTimeout = queryTimeout;

                    // パラメータの設定
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                    }

                    // 実行
                    return sqlCommand.ExecuteReader();

                }

            }
            catch (Exception ex)
            {

                ExceptionMessage = ex.Message;
                return null;

            }

        }

        /// <summary>
        /// クエリ実行
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <param name="queryTimeout">クエリ実行時間(秒)</param>
        /// <returns>クエリ実行結果</returns>
        public SqlDataReader ExecuteQuery(string query, int queryTimeout = 30)
        {
            return ExecuteQuery(query, new Dictionary<string, object>(), queryTimeout);
        }

        /// <summary>
        /// クエリ実行
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <param name="parameters">SQLパラメータ</param>
        /// <param name="queryTimeout">クエリ実行時間(秒)</param>
        public void ExecuteNonQuery(string query, Dictionary<string, object> parameters, int queryTimeout = 30)
        {

            try
            {

                using (var sqlCommand = new SqlCommand(query, _SqlConnection, _SqlTransaction))
                {

                    // タイムアウトの設定
                    sqlCommand.CommandTimeout = queryTimeout;

                    // パラメータの設定
                    foreach (KeyValuePair<string, object> parameter in parameters)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                    }

                    // 実行
                    sqlCommand.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {

                ExceptionMessage = ex.Message;

            }

        }

        /// <summary>
        /// クエリ実行後、DataTableにて返す
        /// </summary>
        /// <param name="query">SQL文</param>
        /// <param name="queryTimeout">クエリ実行時間(秒)</param>
        /// <returns>クエリ実行結果</returns>
        public DataTable ExecuteNonQuery(string query, int queryTimeout = 30)
        {

            try
            {

                using (var sqlCommand = new SqlCommand(query, _SqlConnection, _SqlTransaction))
                {

                    // タイムアウトの設定
                    sqlCommand.CommandTimeout = queryTimeout;

                    // 実行
                    sqlCommand.ExecuteNonQuery();

                    // 戻り値作成
                    var readData = new DataTable();
                    var dataAdapter = new SqlDataAdapter(sqlCommand);

                    dataAdapter.Fill(readData);

                    return readData;

                }

            }
            catch (Exception ex)
            {

                ExceptionMessage = ex.Message;
                return null;

            }

        }

    }

}
