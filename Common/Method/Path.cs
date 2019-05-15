using System;
using System.IO;
using System.Text;

namespace AYam.Common.Method
{
    
    /// <summary>
    /// パス管理共通メソッド
    /// </summary>
    public static class Path
    {

        /// <summary>
        /// フルパス作成
        /// </summary>
        /// <param name="directoryPath">フォルダパス</param>
        /// <param name="fileName">ファイル名</param>
        /// <returns>フルパス</returns>
        public static string GetFullPath(string directoryPath, string fileName)
        {

            // 相対パスを絶対パスに変換
            if (!IsAbsolutePath(directoryPath))
            {
                directoryPath = System.IO.Path.GetFullPath((directoryPath));
            }

            
            if (directoryPath.Substring(directoryPath.Length - 1, 1).Equals(@"\"))
            {

                // フォルダ末尾が"\"ならそのままファイル名を付与
                return directoryPath + fileName;

            }
            else
            {

                // フォルダ末尾に"\"がないなら"\"を挟んでファイル名を付与
                return directoryPath + @"\" + fileName;

            }

        }

        /// <summary>
        /// 指定パスが絶対パスか相対パスか判断
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns>
        /// True:絶対パス
        /// False:相対パス
        /// </returns>
        /// <remarks>
        /// 以下ルールのパスは絶対パスとする
        /// ・先頭「\\」で始まるパス
        /// ・先頭「*:\」で始まるパス *はワイルド
        /// </remarks>
        public static bool IsAbsolutePath(string path)
        {

            // 「\\」「:\」を定義
            string[] abs = new string[] 
            {
                new string(System.IO.Path.DirectorySeparatorChar, 2)
                , new string(System.IO.Path.AltDirectorySeparatorChar, 2)
                , new string(new char[] { System.IO.Path.VolumeSeparatorChar, System.IO.Path.DirectorySeparatorChar })
                , new string(new char[] { System.IO.Path.VolumeSeparatorChar, System.IO.Path.AltDirectorySeparatorChar })
            };

            if (path.Length >= 2 
                && (path.Substring(0, 2).Equals(abs[0])
                    || path.Substring(0,2).Equals(abs[1])))
            {
                // 先頭が「\\」
                return true;
            }
            else if (path.Length >= 3
                && (path.Substring(1, 2).Equals(abs[2])
                    || path.Substring(1, 2).Equals(abs[3])))
            {
                // 先頭が「*:\」
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// フォルダ作成
        /// </summary>
        /// <param name="path">作成したいフォルダパス</param>
        public static string MakeDirectories(string path)
        {

            var makePath = new StringBuilder(path.Length + 1);
            string returnPath = "";

            try
            {

                foreach (var name in path.Split(new string[] { @"\" }, StringSplitOptions.None))
                {

                    makePath.Append(name).Append(@"\");

                    if (!Directory.Exists(makePath.ToString()))
                    {
                        Directory.CreateDirectory(makePath.ToString());
                    }

                }

                returnPath = makePath.ToString();

            }
            catch { }
            finally
            {

                makePath.Clear();
                makePath = null;

            }

            return returnPath;

        }

    }

}
