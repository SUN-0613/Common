using AYam.Common.MVVM;
using Model = AYam.Common.Window.Forms.Model.Path;
using System;

namespace AYam.Common.Window.Forms.ViewModel.Path
{

    /// <summary>
    /// フォルダ名入力.ViewModel
    /// </summary>
    public class InputDirectoryName : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// フォルダ名入力.Model
        /// </summary>
        private Model::InputDirectoryName _Model;

        #endregion

        #region Property

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
        /// 保存コマンド
        /// </summary>
        public DelegateCommand OkCommand
        {
            get
            {
                return new DelegateCommand(
                    () => { CallPropertyChanged("CallOK"); }
                    , () => true);
            }
        }

        /// <summary>
        /// 取消コマンド
        /// </summary>
        public DelegateCommand CancelCommand
        {
            get
            {
                return new DelegateCommand(
                    () => { CallPropertyChanged("CallCancel"); },
                    () => true);
            }
        }

        #endregion

        /// <summary>
        /// フォルダ名入力.ViewModel
        /// </summary>
        public InputDirectoryName()
        {
            _Model = new Model::InputDirectoryName();
        }

        /// <summary>
        /// フォルダ名入力.ViewModel
        /// </summary>
        public InputDirectoryName(string directoryName)
        {
            _Model = new Model::InputDirectoryName(directoryName);
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
