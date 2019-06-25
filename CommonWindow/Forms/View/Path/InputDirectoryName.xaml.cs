using ViewModel = AYam.Common.Window.Forms.ViewModel.Path;
using System;
using System.ComponentModel;

namespace AYam.Common.Window.Forms.View.Path
{
    /// <summary>
    /// InputDirectoryName.xaml の相互作用ロジック
    /// </summary>
    public partial class InputDirectoryName : System.Windows.Window, IDisposable
    {

        #region ReturnValues

        /// <summary>
        /// 入力したフォルダ名
        /// </summary>
        public string DirectoryName
        {
            get
            {

                if (DataContext is ViewModel::InputDirectoryName viewModel)
                {
                    return viewModel.DirectoryName;
                }
                else
                {
                    return "";
                }

            }
        }

        #endregion

        /// <summary>
        /// フォルダ名入力.View
        /// </summary>
        public InputDirectoryName()
        {

            InitializeComponent();

            if (DataContext is ViewModel::InputDirectoryName viewModel)
            {
                viewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// フォルダ名入力.View
        /// </summary>
        /// <param name="directoryName">既存フォルダ名</param>
        public InputDirectoryName(string directoryName)
        {

            InitializeComponent();

            if (DataContext is ViewModel::InputDirectoryName oldViewModel)
            {
                oldViewModel.Dispose();
                oldViewModel = null;
            }

            DataContext = new ViewModel::InputDirectoryName(directoryName);

            if (DataContext is ViewModel::InputDirectoryName newViewModel)
            {
                newViewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel::InputDirectoryName viewModel)
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

                case "CallOK":
                    DialogResult = true;
                    break;

                case "CallCancel":
                    DialogResult = false;
                    break;

                default:
                    break;

            }

        }

    }
}
