using ViewModel = AYam.Common.Window.Forms.ViewModel.Path;
using System;
using System.ComponentModel;

namespace AYam.Common.Window.Forms.View.Path
{
    /// <summary>
    /// DirectoryDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class DirectoryDialog : System.Windows.Window, IDisposable
    {

        #region Const

        /// <summary>
        /// フォルダ選択ページ幅
        /// デフォルトサイズ
        /// </summary>
        private const double defaultPageWidth = 300d;

        /// <summary>
        /// フォルダ選択ページ高さ
        /// デフォルトサイズ
        /// </summary>
        private const double defaultPageHeight = 400d;

        #endregion

        #region ReturnValues

        /// <summary>
        /// 選択したフォルダのフルパス
        /// </summary>
        public string GetSelectPath
        {
            get
            {

                if (DataContext is ViewModel::DirectoryDialog viewModel)
                {
                    return viewModel.FullPath;
                }
                else
                {
                    return "";
                }

            }
        }

        #endregion

        /// <summary>
        /// フォルダ選択ダイアログ.View
        /// </summary>
        public DirectoryDialog()
        {

            InitializeComponent();

            if (DataContext is ViewModel::DirectoryDialog viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// フォルダ選択ダイアログ.View
        /// </summary>
        /// <param name="defaultPath">初期パス</param>
        public DirectoryDialog(string defaultPath)
        {

            if (DataContext is ViewModel::DirectoryDialog oldViewModel)
            {
                oldViewModel.Dispose();
                oldViewModel = null;
            }

            DataContext = new ViewModel::DirectoryDialog(defaultPath, Properties.Path.Message, defaultPageWidth, defaultPageHeight);

            if (DataContext is ViewModel::DirectoryDialog newViewModel)
            {
                newViewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// フォルダ選択ダイアログ.View
        /// </summary>
        /// <param name="defaultPath">初期パス</param>
        /// <param name="message">説明文</param>
        public DirectoryDialog(string defaultPath, string message)
        {

            if (DataContext is ViewModel::DirectoryDialog oldViewModel)
            {
                oldViewModel.Dispose();
                oldViewModel = null;
            }

            DataContext = new ViewModel::DirectoryDialog(defaultPath, message, defaultPageWidth, defaultPageHeight);

            if (DataContext is ViewModel::DirectoryDialog newViewModel)
            {
                newViewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// フォルダ選択ダイアログ.View
        /// </summary>
        /// <param name="defaultPath">初期パス</param>
        /// <param name="message">説明文</param>
        /// <param name="pageWidth">フォルダ選択ページ幅</param>
        /// <param name="pageHeight">フォルダ選択ページ高さ</param>
        public DirectoryDialog(string defaultPath, string message, double pageWidth, double pageHeight)
        {

            if (DataContext is ViewModel::DirectoryDialog oldViewModel)
            {
                oldViewModel.Dispose();
                oldViewModel = null;
            }

            DataContext = new ViewModel::DirectoryDialog(defaultPath, message, pageWidth, pageHeight);

            if (DataContext is ViewModel::DirectoryDialog newViewModel)
            {
                newViewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel::DirectoryDialog viewModel)
            {

                viewModel.PropertyChanged -= OnPropertyChanged;

                viewModel.Dispose();
                viewModel = null;

            }

        }

        /// <summary>
        /// ViewModelプロパティ変更イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            switch (e.PropertyName)
            {

                case "CallSelect":
                    DialogResult = true;
                    break;

                case "CallClose":
                    DialogResult = false;
                    break;

                case "CallNewDirectory":

                    var form = new InputDirectoryName()
                    {
                        Owner = this
                    };

                    form.ShowDialog();
                    if (form.DialogResult.HasValue
                        && form.DialogResult.Value.Equals(true))
                    {

                        // form.DirectoryName;

                    }

                    break;

                case "CallRenameDirectory":

                    if (DataContext is ViewModel::DirectoryDialog viewModel
                        && !viewModel.DirectoryName.Length.Equals(0))
                    {

                        form = new InputDirectoryName(viewModel.DirectoryName);

                        form.ShowDialog();
                        if (form.DialogResult.HasValue
                            && form.DialogResult.Value.Equals(true))
                        {

                            // form.DirectoryName;

                        }

                    }

                    break;

                case "CallDeleteDirectory":
                    /* 後で考える  */
                    break;

                default:
                    break;

            }

        }

    }
}
