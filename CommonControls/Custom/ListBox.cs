using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace AYam.Common.Controls.Custom
{

    /// <summary>
    /// ListBoxカスタマイズ
    /// </summary>
    /// <remarks>
    /// SelectedItemsをBinding可
    /// </remarks>
    public class ListBox : System.Windows.Controls.ListBox, IDisposable
    {

        #region DependencyProperty

        /// <summary>
        /// SelectedItems依存プロパティ
        /// </summary>
        public static readonly DependencyProperty BindableSelectedItemsProperty
                                        = DependencyProperty.Register(
                                        nameof(BindableSelectedItems)
                                        , typeof(IList)
                                        , typeof(AYam.Common.Controls.Custom.ListBox)
                                        , new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        #endregion

        #region Property

        /// <summary>
        /// 選択項目
        /// </summary>
        public IList BindableSelectedItems
        {
            get { return (IList)GetValue(BindableSelectedItemsProperty); }
            set { SetValue(BindableSelectedItemsProperty, value); }
        }

        #endregion

        #region Event

        /// <summary>
        /// 選択箇所変更イベント
        /// </summary>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {

            base.OnSelectionChanged(e);

            BindableSelectedItems = base.SelectedItems;

        }

        #endregion

        /// <summary>
        /// ListBoxカスタマイズ
        /// </summary>
        public ListBox()
        {

        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {

        }

    }

}
