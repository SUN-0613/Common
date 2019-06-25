using View = AYam.Common.Window.Pages.View.Path;
using ViewModel = AYam.Common.Window.Pages.ViewModel.Path;
using System;

namespace AYam.Common.Window.Forms.Model.Path
{

    /// <summary>
    /// フォルダ選択ダイアログ.Model
    /// </summary>
    public class DirectoryDialog : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// 説明文
        /// </summary>
        public string Message;

        /// <summary>
        /// フォルダ選択ページ
        /// </summary>
        public View::SelectDirectory Page;

        #endregion

        /// <summary>
        /// フォルダ選択ダイアログ.Model
        /// </summary>
        /// <param name="defaultPath">初期パス</param>
        /// <param name="message">説明文</param>
        /// <param name="pageWidth">フォルダ選択ページ幅</param>
        /// <param name="pageHeight">フォルダ選択ページ高さ</param>
        public DirectoryDialog(string defaultPath, string message, double pageWidth, double pageHeight)
        {

            Message = message;
            Page = new View::SelectDirectory(defaultPath, pageWidth, pageHeight);

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

            if (Page != null)
            {

                Page.Dispose();
                Page = null;

            }

        }

        /// <summary>
        /// 選択しているフォルダのフルパスを取得
        /// </summary>
        /// <returns>
        /// フルパス
        /// 取得できない場合はブランク
        /// </returns>
        public string GetFullPath()
        {
            
            if (Page.DataContext is ViewModel::SelectDirectory viewModel)
            {
                return viewModel.SelectedPath;
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 選択しているフォルダの名前を取得
        /// </summary>
        /// <returns></returns>
        public string GetDirectoryName()
        {

            if (Page.DataContext is ViewModel::SelectDirectory viewModel)
            {
                return viewModel.GetSelectedDirectoryName();
            }
            else
            {
                return "";
            }

        }

    }

}
