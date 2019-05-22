using AYam.Common.Controls.Interface;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;

namespace AYam.Common.Controls.Behaviors
{

    /// <summary>
    /// Window.Closing用ビヘイビア
    /// </summary>
    public class WindowClosing : Behavior<Window>
    {

        /// <summary>
        /// ビヘイビア登録
        /// </summary>
        protected override void OnAttached()
        {

            base.OnAttached();

            AssociatedObject.Closing += OnClosing;

        }

        /// <summary>
        /// ビヘイビア解除
        /// </summary>
        protected override void OnDetaching()
        {

            base.OnDetaching();

            AssociatedObject.Closing -= OnClosing;

        }

        /// <summary>
        /// Window.Closingイベント
        /// </summary>
        private void OnClosing(object sender, CancelEventArgs e)
        {

            if (sender is Window form && form.DataContext is IClosing viewModel)
            {

                if (!viewModel.OnClosing())
                {

                    if (!viewModel.ClosingTitle.Equals(string.Empty) && !viewModel.ClosingMessage.Equals(string.Empty))
                    {
                        e.Cancel = MessageBox.Show(viewModel.ClosingMessage, viewModel.ClosingTitle, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly).Equals(MessageBoxResult.No);
                    }

                }

            }

        }

    }

}
