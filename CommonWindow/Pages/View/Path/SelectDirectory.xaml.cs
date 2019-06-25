using ViewModel = AYam.Common.Window.Pages.ViewModel.Path;
using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace AYam.Common.Window.Pages.View.Path
{

    /// <summary>
    /// SelectDirectory.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectDirectory : Page, IDisposable
    {

        /// <summary>
        /// フォルダ選択.View
        /// </summary>
        /// <param name="defaultPath">初期パス</param>
        /// <param name="width">ページ幅</param>
        /// <param name="height">ページ高さ</param>
        public SelectDirectory(string defaultPath, double width, double height)
        {

            InitializeComponent();

            if (DataContext is ViewModel::SelectDirectory oldViewModel)
            {
                oldViewModel.Dispose();
                oldViewModel = null;
            }

            DataContext = new ViewModel::SelectDirectory(defaultPath, width, height);

            if (DataContext is ViewModel::SelectDirectory newViewModel)
            {
                newViewModel.PropertyChanged += OnPropertyChanged;
            }

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (DataContext is ViewModel::SelectDirectory viewModel)
            {

                viewModel.PropertyChanged -= OnPropertyChanged;

                viewModel.Dispose();
                viewModel = null;

            }

        }

        /// <summary>
        /// ViewModelプロパティ変更通知イベント
        /// </summary>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (DataContext is ViewModel::SelectDirectory viewModel)
            {

                switch (e.PropertyName)
                {

                    default:
                        break;

                }

            }

        }

    }

}
