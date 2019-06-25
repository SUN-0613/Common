using Model = AYam.Common.Window.Pages.Model.Path;
using AYam.Common.MVVM;
using System;
using System.Collections.ObjectModel;

namespace AYam.Common.Window.Pages.ViewModel.Path
{

    /// <summary>
    /// フォルダ選択.ViewModel
    /// </summary>
    public class SelectDirectory : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// フォルダ選択.Model
        /// </summary>
        private Model::SelectDirectory _Model;

        #endregion

        #region Property

        /// <summary>
        /// フォルダ階層
        /// </summary>
        public ObservableCollection<PathTree> Paths
        {
            get { return _Model.Paths; }
            set
            {
                _Model.Paths = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 選択フォルダ
        /// </summary>
        public PathTree SelectedDirectory
        {
            get { return _Model.SelectedDirectory; }
            set
            {
                _Model.SelectedDirectory = value;
                CallPropertyChanged();
            }
        }

        /// <summary>
        /// 選択パス
        /// </summary>
        public string SelectedPath
        {
            get
            {

                if (_Model.SelectedDirectory == null)
                {
                    return "";
                }
                else
                {

                    int lastIndex = _Model.SelectedDirectory.FullPath.LastIndexOf(@"\");
                    int length = _Model.SelectedDirectory.FullPath.Length;

                    // ドライブ名は"\"がデフォルトで付与されているが
                    // 下位フォルダは付与されていないので"\"を付与する
                    return _Model.SelectedDirectory.FullPath + (lastIndex.Equals(length - 1) ? "" : @"\");
                }

            }
            set
            {

                var returnValue = _Model.SelectPath(value, Paths);

                if (returnValue != null)
                {
                    _Model.SelectedDirectory = returnValue;
                }
                else
                {
                    CallPropertiesChanged();
                }

            }
        }

        /// <summary>
        /// ページ：幅
        /// </summary>
        public double PageWidth
        {
            get { return _Model.PageWidth; }
            set
            {
                _Model.PageWidth = value;
                CallPropertiesChanged();
            }
        }

        /// <summary>
        /// ページ：高さ
        /// </summary>
        public double PageHeight
        {
            get { return _Model.PageHeight; }
            set
            {
                _Model.PageHeight = value;
                CallPropertiesChanged();
            }
        }

        #endregion

        /// <summary>
        /// フォルダ選択.ViewModel
        /// </summary>
        public SelectDirectory()
        {

            _Model = new Model::SelectDirectory("");

        }

        /// <summary>
        /// フォルダ選択.ViewModel
        /// </summary>
        /// <param name="defaultPath">初期パス</param>
        public SelectDirectory(string defaultPath, double pageWidth, double pageHeight)
        {

            _Model = new Model::SelectDirectory(defaultPath);

            PageWidth = pageWidth;
            PageHeight = pageHeight;

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (_Model != null)
            {
                _Model.Dispose();
                _Model = null;
            }

        }

        /// <summary>
        /// 複数のプロパティ変更通知
        /// </summary>
        private void CallPropertiesChanged()
        {

            CallPropertyChanged(nameof(SelectedDirectory));
            CallPropertyChanged(nameof(SelectedPath));

        }

        /// <summary>
        /// 選択中のフォルダ名取得
        /// </summary>
        public string GetSelectedDirectoryName()
        {
            return _Model.SelectedDirectory.DirectoryName;
        }

    }

}
