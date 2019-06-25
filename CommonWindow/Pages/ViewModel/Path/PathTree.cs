using Model = AYam.Common.Window.Pages.Model.Path;
using AYam.Common.MVVM;
using System;
using System.Collections.ObjectModel;

namespace AYam.Common.Window.Pages.ViewModel.Path
{

    /// <summary>
    /// フォルダ階層.ViewModel
    /// </summary>
    public class PathTree : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// フォルダ階層.Model
        /// </summary>
        private Model::PathTree _Model;

        #endregion

        #region Property

        /// <summary>
        /// フルパス
        /// </summary>
        public string FullPath
        {
            get { return _Model.FullPath; }
            set
            {
                if (!_Model.FullPath.Equals(value))
                {
                    _Model.FullPath = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// フォルダ名
        /// </summary>
        public string DirectoryName
        {
            get { return _Model.DirectoryName; }
            set
            {
                if (!_Model.DirectoryName.Equals(value))
                {
                    _Model.DirectoryName = value;
                    CallPropertyChanged();
                }
            }
        }

        /// <summary>
        /// 子階層
        /// </summary>
        public ObservableCollection<PathTree> Children
        {
            get { return _Model.Children; }
            set { _Model.Children = value; }
        }

        #endregion

        /// <summary>
        /// フォルダ階層.ViewModel
        /// </summary>
        /// <param name="fullPath">フルパス</param>
        public PathTree(string fullPath)
        {

            _Model = new Model.Path.PathTree(fullPath);

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

    }

}
