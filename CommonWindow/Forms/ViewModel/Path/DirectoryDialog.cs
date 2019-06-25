using Model = AYam.Common.Window.Forms.Model.Path;
using View = AYam.Common.Window.Pages.View.Path;
using ViewModel = AYam.Common.Window.Pages.ViewModel.Path;
using AYam.Common.MVVM;
using System;

namespace AYam.Common.Window.Forms.ViewModel.Path
{

    /// <summary>
    /// フォルダ選択ダイアログ.ViewModel
    /// </summary>
    public class DirectoryDialog : ViewModelBase, IDisposable
    {

        #region Model

        /// <summary>
        /// フォルダ選択ダイアログ.Model
        /// </summary>
        private Model::DirectoryDialog _Model;

        #endregion

        #region Property

        /// <summary>
        /// 選択しているフォルダのフルパスを取得
        /// </summary>
        public string FullPath { get { return _Model.GetFullPath(); } }

        /// <summary>
        /// 選択しているフォルダの名称を取得
        /// </summary>
        public string DirectoryName { get { return _Model.GetDirectoryName(); } }

        /// <summary>
        /// 説明文
        /// </summary>
        public string Message { get { return _Model.Message; } }

        /// <summary>
        /// フォルダ選択ページ
        /// </summary>
        public View::SelectDirectory Page {  get { return _Model.Page; } }

        /// <summary>
        /// フォルダ編集コマンド
        /// </summary>
        public DelegateCommand<string> DirectoryCommand
        {
            get
            {

                return new DelegateCommand<string>(
                    (parameter) => 
                    {

                        if (Page.DataContext is ViewModel::SelectDirectory viewModel)
                        {

                            switch (parameter)
                            {

                                case "add":     // フォルダ作成
                                    CallPropertyChanged("CallNewDirectory");
                                    break;

                                case "rename":  // 名前の変更
                                    CallPropertyChanged("CallRenameDirectory");
                                    break;

                                case "delete":  // 削除
                                    CallPropertyChanged("CallDeleteDirectory");
                                    break;

                                default:
                                    break;

                            }

                        }

                    });
            }
        }

        /// <summary>
        /// 選択コマンド
        /// </summary>
        public DelegateCommand SelectCommand
        {
            get
            {

                return new DelegateCommand(
                    () => { CallPropertyChanged("CallSelect"); },
                    () => true);

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
        /// フォルダ選択ダイアログ.ViewModel
        /// </summary>
        public DirectoryDialog()
        {
            _Model = new Model::DirectoryDialog("", Properties.Path.Message, 300d, 400d);
        }

        /// <summary>
        /// フォルダ選択ダイアログ.ViewModel
        /// </summary>
        /// <param name="defaultPath">初期パス</param>
        /// <param name="message">説明文</param>
        /// <param name="pageWidth">フォルダ選択ページ幅</param>
        /// <param name="pageHeight">フォルダ選択ページ高さ</param>
        public DirectoryDialog(string defaultPath, string message, double pageWidth, double pageHeight)
        {

            _Model = new Model::DirectoryDialog(defaultPath, message, pageWidth, pageHeight);

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
