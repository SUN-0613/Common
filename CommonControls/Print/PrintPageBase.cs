using System;
using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents; // PresentationFramework

namespace AYam.Common.Controls.Print
{

    /// <summary>
    /// 印刷実行クラス
    /// </summary>
    /// <remarks>
    /// 参照の追加
    /// ReachFramework
    /// System.Printing
    /// </remarks>
    public abstract class PrintPageBase : IDisposable
    {

        /// <summary>
        /// 印刷用紙指定
        /// </summary>
        protected PageMediaSizeName PageSizeName = PageMediaSizeName.ISOA4;

        /// <summary>
        /// 印刷向き指定
        /// </summary>
        protected PageOrientation PageOrientation = PageOrientation.Portrait;

        /// <summary>
        /// 印刷サイズ指定
        /// </summary>
        /// <remarks>
        /// 初期値はA4サイズ縦方向
        /// </remarks>
        protected Size PageSize = new Size(794d, 1123d);

        /// <summary>
        /// 印刷ページ一覧
        /// </summary>
        protected List<object> PrintPages = new List<object>();

        /// <summary>
        /// 印刷実行クラス
        /// </summary>
        public PrintPageBase()
        { }

        /// <summary>
        /// 終了処理
        /// </summary>
        public virtual void Dispose()
        {
            InitializePages();
        }

        /// <summary>
        /// 印刷ページ一覧の初期化
        /// </summary>
        private void InitializePages()
        {

            if (PrintPages != null)
            {

                PrintPages.ForEach(page =>
                {
                    if (page is IDisposable dispose)
                    {
                        dispose.Dispose();
                    }
                });

                PrintPages.Clear();
                PrintPages = null;

            }

        }

        /// <summary>
        /// 印刷実行
        /// </summary>
        /// <remarks>エラーメッセージ</remarks>
        protected string Print()
        {

            try
            {

                if (PrintPages.Count.Equals(0))
                {
                    throw new NullReferenceException("Pages is blank");
                }

                using (var printServer = new LocalPrintServer())
                {

                    using (var queue = printServer.DefaultPrintQueue)
                    {

                        var writer = PrintQueue.CreateXpsDocumentWriter(queue);

                        // 印刷設定
                        var ticket = queue.DefaultPrintTicket;
                        ticket.PageMediaSize = new PageMediaSize(PageSizeName);
                        ticket.PageOrientation = PageOrientation;

                        // 印刷内容作成
                        var document = new FixedDocument();

                        PrintPages.ForEach(page => 
                        {
                            document.Pages.Add(CreatePageContent(page, PageSize));
                        });

                        // 印刷実行
                        writer.Write(document, ticket);

                        // 初期化
                        InitializePages();
                        PrintPages = new List<object>();

                    }

                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;

        }

        /// <summary>
        /// PageからFixedPageを作成し、PageContentに追加
        /// </summary>
        /// <param name="sender">Page</param>
        /// <param name="size">縦横サイズ</param>
        /// <returns>PageContent</returns>
        private PageContent CreatePageContent(object sender, Size size)
        {

            var pageContent = new PageContent();
            var fixedPage = new FixedPage();

            if (sender is Page page)
            {

                var frame = new Frame { Content = page };

                FixedPage.SetLeft(frame, 0d);
                FixedPage.SetTop(frame, 0d);

                fixedPage.Children.Add(frame);
                fixedPage.Width = size.Width;
                fixedPage.Height = size.Height;
                fixedPage.Measure(size);
                fixedPage.Arrange(new Rect(new Point(), size));
                fixedPage.UpdateLayout();

                pageContent.Child = fixedPage;

            }

            return pageContent;

        }

    }

}
