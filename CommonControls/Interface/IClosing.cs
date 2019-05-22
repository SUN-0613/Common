namespace AYam.Common.Controls.Interface
{

    /// <summary>
    /// Window.Closing用Interface
    /// </summary>
    public interface IClosing
    {

        /// <summary>
        /// Window.Closing処理実装
        /// </summary>
        /// <returns>
        /// True:閉じる処理続行
        /// False:閉じる処理中止
        /// </returns>
        bool OnClosing();

        /// <summary>
        /// Window.Closing確認メッセージボックス.タイトル
        /// </summary>
        string ClosingTitle { get; }

        /// <summary>
        /// Window.Closing確認メッセージボックス.文章
        /// </summary>
        string ClosingMessage { get; }

    }

}
