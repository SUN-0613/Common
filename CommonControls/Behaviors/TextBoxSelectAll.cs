using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AYam.Common.Controls.Behaviors
{

    /// <summary>
    /// TextBox用ビヘイビア
    /// フォーカスセット時に全選択
    /// </summary>
    public class TextBoxSelectAll : Behavior<TextBox>
    {

        /// <summary>
        /// ビヘイビア登録
        /// </summary>
        protected override void OnAttached()
        {

            base.OnAttached();

            AssociatedObject.GotFocus += OnGotFocus;
            AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;

        }

        /// <summary>
        /// ビヘイビア解除
        /// </summary>
        protected override void OnDetaching()
        {

            base.OnDetaching();

            AssociatedObject.GotFocus -= OnGotFocus;
            AssociatedObject.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;

        }

        /// <summary>
        /// フォーカス取得
        /// </summary>
        private void OnGotFocus(object sender, RoutedEventArgs e)
        {

            if (sender is TextBox textBox)
            {
                textBox.SelectAll();
                e.Handled = true;
            }

        }

        /// <summary>
        /// マウス操作
        /// </summary>
        private void OnPreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (sender is TextBox textBox)
            {

                if (!textBox.IsFocused)
                {

                    textBox.Focus();
                    e.Handled = true;

                }

            }

        }

    }

}
