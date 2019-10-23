using System.Windows;
using Controls = System.Windows.Controls;

namespace AYam.Common.Controls.Custom
{

    /// <summary>
    /// TreeViewカスタマイズ
    /// </summary>
    /// <remarks>
    /// SelectedItemをBinding可
    /// </remarks>
    public class TreeView : Controls::TreeView
    {

        #region DependencyProperty

        /// <summary>
        /// SelectedItem依存プロパティ
        /// </summary>
        public static readonly DependencyProperty BindableSelectedItemProperty
                                        = DependencyProperty.Register(
                                        nameof(BindableSelectedItem)
                                        , typeof(object)
                                        , typeof(TreeView)
                                        , new UIPropertyMetadata(null));

        #endregion

        #region Property

        /// <summary>
        /// SelectedItemプロパティ
        /// </summary>
        public object BindableSelectedItem
        {
            get { return GetValue(BindableSelectedItemProperty); }
            set { SetValue(BindableSelectedItemProperty, value); }
        }

        #endregion

        #region Event

        /// <summary>
        /// 選択箇所変更イベント
        /// </summary>
        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {

            base.OnSelectedItemChanged(e);

            SetValue(BindableSelectedItemProperty, SelectedItem);

        }

        #endregion

    }

}
