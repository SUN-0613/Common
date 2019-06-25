using ViewModel = AYam.Common.Window.Pages.ViewModel.Path;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace AYam.Common.Window.Pages.Model.Path
{

    /// <summary>
    /// フォルダ選択.Model
    /// </summary>
    public class SelectDirectory : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// フォルダ階層
        /// </summary>
        public ObservableCollection<ViewModel::PathTree> Paths { get; set; }

        /// <summary>
        /// 選択フォルダ
        /// </summary>
        public ViewModel::PathTree SelectedDirectory = null;

        /// <summary>
        /// ページ：高さ
        /// </summary>
        public double PageHeight = 300d;

        /// <summary>
        /// ページ：幅
        /// </summary>
        public double PageWidth = 200d;

        #endregion

        /// <summary>
        /// フォルダ選択.Model
        /// </summary>
        /// <param name="defaultPath">初期パス</param>
        public SelectDirectory(string defaultPath)
        {

            MakeDriveList();
            SelectedDirectory = SelectPath(defaultPath, Paths);

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            InitializePaths();
        }

        /// <summary>
        /// Paths初期化
        /// </summary>
        private void InitializePaths()
        {

            if (Paths != null)
            {

                for (int iLoop = 0; iLoop < Paths.Count; iLoop++)
                {
                    Paths[iLoop].Dispose();
                    Paths[iLoop] = null;
                }

                Paths.Clear();
                Paths = null;

            }

        }

        /// <summary>
        /// ドライブ一覧取得
        /// </summary>
        private async void MakeDriveList()
        {

            // 初期化
            InitializePaths();
            Paths = new ObservableCollection<ViewModel::PathTree>();

            // ドライブ取得
            await Task.Run(() => 
            {

                try
                {

                    foreach (string drive in Environment.GetLogicalDrives())
                    {
                        Paths.Add(new ViewModel::PathTree(drive));
                    }

                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(nameof(MakeDriveList) + "():" + ex.Message);
#endif
                }

            });

        }

        /// <summary>
        /// 入力されたパスの検索
        /// </summary>
        /// <param name="inputPath">入力パス</param>
        /// <param name="paths">フォルダ階層</param>
        /// <param name="lastIndex">再帰用：前回検索パス開始位置</param>
        /// <returns>
        /// 検索結果パス情報
        /// パスが存在しない場合はnull
        /// </returns>
        public ViewModel::PathTree SelectPath(string inputPath, ObservableCollection<ViewModel::PathTree> paths, int lastIndex = 0)
        {

            // 入力値は存在するか
            if (Directory.Exists(inputPath))
            {

                // 上位フォルダから検索
                int index = -1;

                if (inputPath.Length < lastIndex + 1)
                {
                    index = inputPath.IndexOf(@"\", lastIndex + 1);
                }

                string str = index.Equals(-1) ? inputPath : inputPath.Substring(0, index + 1);

                for (int iLoop = 0; iLoop < paths.Count; iLoop++)
                {

                    // 該当フォルダ発見
                    if (paths[iLoop].FullPath.Equals(str))
                    {

                        if (str.Equals(inputPath))
                        {

                            // 入力値と完全一致すればループ完了
                            return Paths[iLoop];

                        }
                        else
                        {

                            // 引き続き下位フォルダを検索する
                            // 該当Itemを発見するまで再帰
                            return SelectPath(inputPath, paths[iLoop].Children, index);

                        }

                    }

                }

            }

            // 見つからなかった
            return null;

        }

    }

}
