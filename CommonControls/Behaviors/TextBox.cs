using System.Windows;
using System.Windows.Interactivity;

namespace AYam.Common.Controls.Behaviors
{

    /// <summary>
    /// TextBox用ビヘイビア
    /// </summary>
    public class TextBox : Behavior<System.Windows.Controls.TextBox>
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

            if (sender is System.Windows.Controls.TextBox textBox)
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

            if (sender is System.Windows.Controls.TextBox textBox)
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
