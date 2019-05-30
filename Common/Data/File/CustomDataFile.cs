using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AYam.Common.Data.File
{

    /// <summary>
    /// データ管理ファイル
    /// ベースクラス
    /// ヘッダ1行と詳細1行で構成
    /// </summary>
    /// <remarks>
    /// ファイルはTAB区切、改行で次レコード
    /// ヘッダ開始は H + {TAB} + [ヘッダ名]
    /// 詳細は D + {TAB} + [詳細値1] + {TAB} + [詳細値2] + {TAB} + ...
    /// </remarks>
    public abstract class CustomDataFile : IDisposable
    {

        /// <summary>
        /// データテーブル
        /// Key : ヘッダ名
        /// Value : 詳細一覧
        /// </summary>
        private Dictionary<string, List<string>> DataTable;

        /// <summary>
        /// ファイルパス
        /// </summary>
        private readonly string _FilePath;

        /// <summary>
        /// ファイル読込
        /// </summary>
        public abstract void Read();

        /// <summary>
        /// ファイル保存
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// データファイル
        /// ベースクラス
        /// </summary>
        /// <param name="path">ファイルパス</param>
        public CustomDataFile(string path)
        {

            DataTable = new Dictionary<string, List<string>>();

            _FilePath = path;
            ReadFile();

            Read();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public virtual void Dispose()
        {

            if (DataTable != null)
            {

                foreach (var item in DataTable.Values)
                {
                    item.Clear();
                }
                DataTable.Clear();
                DataTable = null;

            }

        }

        /// <summary>
        /// ファイル読込
        /// </summary>
        private async void ReadFile()
        {

            if (System.IO.File.Exists(_FilePath))
            {

                var header = new StringBuilder();

                try
                {

                    using (var reader = new StreamReader(_FilePath, Encoding.Unicode))
                    {

                        while (!reader.EndOfStream)
                        {

                            var line = await reader.ReadLineAsync();
                            var values = line.Split(new string[] { "\t" }, StringSplitOptions.None);

                            switch (values[0].ToUpper())
                            {
                                case "H":   // ヘッダ : テーブル登録

                                    header.Clear().Append(values[1]);

                                    if (!DataTable.ContainsKey(header.ToString()))
                                    {
                                        DataTable.Add(header.ToString(), new List<string>());
                                    }

                                    break;

                                case "D":   // 詳細 : テーブル更新

                                    if (!header.Length.Equals(0))
                                    {

                                        for (int iLoop = 1; iLoop < values.Length; iLoop++)
                                        {
                                            DataTable[header.ToString()].Add(values[iLoop]);
                                        }

                                    }

                                    break;

                                default:
                                    break;

                            }

                        }

                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(nameof(ReadFile) + " : " + ex.Message);
                }
                finally
                {
                    header.Clear();
                    header = null;
                }

            }

        }

        /// <summary>
        /// 指定ヘッダの詳細値取得
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        /// <returns>
        /// 詳細値
        /// 取得できない場合はブランクを返す
        /// </returns>
        protected string GetValue(string key)
        {
            return GetValue(key, -1, "");
        }

        /// <summary>
        /// 指定ヘッダの詳細値取得
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        /// <param name="valueIndex">
        /// 詳細一覧のIndex
        /// -1の場合は改行コードにて連結
        /// </param>
        /// <returns>
        /// 詳細値
        /// 取得できない場合はブランクを返す
        /// </returns>
        protected string GetValue(string key, int valueIndex)
        {
            return GetValue(key, valueIndex, "");
        }

        /// <summary>
        /// 指定ヘッダの詳細値取得
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        /// <param name="returnValue">指定ヘッダがテーブルに存在しない時にreturnする初期値</param>
        /// <returns>詳細値</returns>
        protected string GetValue(string key, string returnValue)
        {
            return GetValue(key, -1, returnValue);
        }

        /// <summary>
        /// 指定ヘッダの詳細値取得
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        /// <param name="valueIndex">
        /// 詳細一覧のIndex
        /// -1の場合は改行コードにて連結
        /// </param>
        /// <param name="returnValue">指定ヘッダがテーブルに存在しない時にreturnする初期値</param>
        /// <returns>詳細値</returns>
        protected string GetValue(string key, int valueIndex, string returnValue)
        {

            if (DataTable.ContainsKey(key))
            {

                if (valueIndex > -1)
                {

                    if (valueIndex < DataTable[key].Count)
                    {
                        returnValue = DataTable[key][valueIndex];
                    }

                }
                else
                {

                    var value = new StringBuilder();

                    try
                    {

                        for (int iLoop = 0; iLoop < DataTable[key].Count; iLoop++)
                        {

                            if (iLoop.Equals(DataTable[key].Count - 1))
                            {
                                value.Append(DataTable[key][iLoop]);
                            }
                            else
                            {
                                value.AppendLine(DataTable[key][iLoop]);
                            }

                        }

                        returnValue = value.ToString();

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(nameof(GetValue) + " : " + ex.Message);
                    }
                    finally
                    {

                        value.Clear();
                        value = null;

                    }

                }

            }

            return returnValue;

        }

        /// <summary>
        /// 指定ヘッダの詳細値一覧のシャローコピーを取得
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        /// <returns>
        /// 詳細値一覧
        /// 取得できない場合はnullを返す
        /// </returns>
        protected List<string> GetValues(string key)
        {

            if (DataTable.ContainsKey(key))
            {
                return DataTable[key];
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 指定ヘッダの詳細数を取得
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        /// <returns>詳細一覧.Count</returns>
        protected int GetValueCount(string key)
        {
            return DataTable.ContainsKey(key) ? DataTable[key].Count : -1;
        }

        /// <summary>
        /// データテーブル更新
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        /// <param name="value">
        /// 詳細
        /// 改行で区切りList化する
        /// </param>
        protected void Update(string key, string value)
        {

            if (!DataTable.ContainsKey(key))
            {
                DataTable.Add(key, new List<string>());
            }
            else
            {
                DataTable[key].Clear();
            }

            var values = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (int iLoop = 0; iLoop < values.Length; iLoop++)
            {
                DataTable[key].Add(values[iLoop]);
            }

        }

        /// <summary>
        /// データテーブル更新
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        /// <param name="values">詳細一覧</param>
        protected void Update(string key, List<string> values)
        {

            if (!DataTable.ContainsKey(key))
            {
                DataTable.Add(key, new List<string>());
            }
            else
            {
                DataTable[key].Clear();
            }

            for (int iLoop = 0; iLoop < values.Count; iLoop++)
            {
                DataTable[key].Add(values[iLoop]);
            }

        }

        /// <summary>
        /// データテーブルより指定キーのデータを削除
        /// </summary>
        /// <param name="key">ヘッダ名</param>
        protected void Delete(string key)
        {

            if (DataTable.ContainsKey(key))
            {
                DataTable.Remove(key);
            }

        }

        /// <summary>
        /// ファイル書込
        /// </summary>
        protected async void WriteFile()
        {

            var detail = new StringBuilder();

            try
            {

                using (var writer = new StreamWriter(_FilePath, false, Encoding.Unicode))
                {

                    foreach (var item in DataTable)
                    {

                        await writer.WriteLineAsync("H" + "\t" + item.Key);

                        await Task.Run(() =>
                        {

                            detail.Clear().Append("D");
                            item.Value.ForEach(Data =>
                            {
                                detail.Append("\t").Append(Data);
                            });

                        });

                        await writer.WriteLineAsync(detail.ToString());

                    }

                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(nameof(WriteFile) + " : " + ex.Message);
            }
            finally
            {
                detail.Clear();
                detail = null;
            }

        }

        /// <summary>
        /// ファイル削除
        /// </summary>
        public void DeleteItself()
        {

            if (System.IO.File.Exists(_FilePath))
            {

                Dispose();

                System.IO.File.Delete(_FilePath);

            }

        }

    }

}
