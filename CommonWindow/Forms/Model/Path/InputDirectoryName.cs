using System;

namespace AYam.Common.Window.Forms.Model.Path
{

    /// <summary>
    /// フォルダ名入力.Model
    /// </summary>
    public class InputDirectoryName : IDisposable
    {

        #region ViewModel.Property

        /// <summary>
        /// フォルダ名
        /// </summary>
        public string DirectoryName;

        #endregion

        /// <summary>
        /// フォルダ名入力.Model
        /// </summary>
        /// <param name="directoryName">フォルダ名</param>
        public InputDirectoryName(string directoryName = "")
        {
            DirectoryName = directoryName;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        { }

    }

}
