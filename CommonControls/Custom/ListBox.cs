using System.Collections;
using System.Windows;
using Controls = System.Windows.Controls;

namespace AYam.Common.Controls.Custom
{

    /// <summary>
    /// ListBoxカスタマイズ
    /// </summary>
    /// <remarks>
    /// SelectedItemsをBinding可
    /// </remarks>
    public class ListBox : Controls::ListBox
    {

        #region DependencyProperty

        /// <summary>
        /// SelectedItems依存プロパティ
        /// </summary>
        public static readonly DependencyProperty BindableSelectedItemsProperty
                                        = DependencyProperty.Register(
                                        nameof(BindableSelectedItems)
                                        , typeof(IList)
                                        , typeof(ListBox)
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
        protected override void OnSelectionChanged(Controls::SelectionChangedEventArgs e)
        {

            base.OnSelectionChanged(e);

            SetValue(BindableSelectedItemsProperty, SelectedItems);

        }

        #endregion

    }

}
