using ViewModel = AYam.Common.Window.Pages.ViewModel.Path;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AYam.Common.Window.Pages.Model.Path
{

    /// <summary>
    /// フォルダ階層.Model
    /// </summary>
    public class PathTree : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// フルパス
        /// </summary>
        public string FullPath = "";

        /// <summary>
        /// フォルダ名
        /// </summary>
        public string DirectoryName = "";

        /// <summary>
        /// 子階層
        /// </summary>
        public ObservableCollection<ViewModel::PathTree> Children { get; set; }

        #endregion

        /// <summary>
        /// フォルダ階層.Model
        /// </summary>
        /// <param name="fullPath">フルパス</param>
        public PathTree(string fullPath)
        {

            FullPath = fullPath;

            MakeChildPath();

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Children != null)
            {

                for (int iLoop = 0; iLoop < Children.Count; iLoop++)
                {
                    Children[iLoop].Dispose();
                    Children[iLoop] = null;
                }

                Children.Clear();
                Children = null;

            }

        }

        /// <summary>
        /// 子階層のフォルダ一覧を取得
        /// </summary>
        private async void MakeChildPath()
        {

            // 初期化
            if (Children == null)
            {
                Children = new ObservableCollection<ViewModel::PathTree>();
            }

            Children.Clear();

            await Task.Run(() => 
            {

                try
                {

                    if (!string.IsNullOrEmpty(FullPath) && System.IO.Directory.Exists(FullPath))
                    {

                        foreach(var path in System.IO.Directory.EnumerateDirectories(FullPath, "*", System.IO.SearchOption.TopDirectoryOnly))
                        {
                            Children.Add(new ViewModel.Path.PathTree(path));
                        }

                    }

                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(nameof(MakeChildPath) + "():" + ex.Message);
#endif
                }

            });

        }

    }

}
