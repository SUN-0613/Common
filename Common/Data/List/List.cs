using System;

namespace AYam.Common.Data.List
{

    /// <summary>
    /// Listにリソース解放処理を追加
    /// </summary>
    /// <typeparam name="T">型指定</typeparam>
    public class List<T> : System.Collections.Generic.List<T>, IDisposable
    {

        #region IDisposable Support

        /// <summary>
        /// 重複する呼び出しを検出
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// 終了処理
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)。
                    ForEach(value =>
                    {

                        if (value is IDisposable dispoable)
                        {
                            dispoable.Dispose();
                        }

                    });

                    Clear();

                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        ~List()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(false);
        }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        void IDisposable.Dispose()
        {

            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);

            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            GC.SuppressFinalize(this);

        }
        #endregion

    }

}
