using System;
using System.Windows;

namespace AYam.Common.Controls.Custom
{

    /// <summary>
    /// TreeViewカスタマイズ
    /// </summary>
    /// <remarks>
    /// SelectedItemをBinding可
    /// </remarks>
    public class TreeView : System.Windows.Controls.TreeView, IDisposable
    {

        #region DependencyProperty

        /// <summary>
        /// SelectedItem依存プロパティ
        /// </summary>
        public static readonly DependencyProperty BindableSelectedItemProperty
                                        = DependencyProperty.Register(
                                        nameof(BindableSelectedItem)
                                        , typeof(object)
                                        , typeof(AYam.Common.Controls.Custom.TreeView)
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
        protected virtual void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            if (SelectedItem != null)
            {
                SetValue(BindableSelectedItemProperty, SelectedItem);
            }

        }

        #endregion

        /// <summary>
        /// TreeViewカスタマイズ
        /// </summary>
        public TreeView()
        {
            SelectedItemChanged += OnSelectedItemChanged;
        }

        /// <summary>
        /// 終了処理
        /// </summary>
        public void Dispose()
        {
            SelectedItemChanged -= OnSelectedItemChanged;
        }

    }

}
