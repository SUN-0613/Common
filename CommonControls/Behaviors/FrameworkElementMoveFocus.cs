using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Input;

namespace AYam.Common.Controls.Behaviors
{

    /// <summary>
    /// TextBox用ビヘイビア
    /// Enterで次フォーカス移動
    /// </summary>
    public class FrameworkElementMoveFocus : Behavior<FrameworkElement>
    {

        /// <summary>
        /// ビヘイビア登録
        /// </summary>
        protected override void OnAttached()
        {

            base.OnAttached();

            AssociatedObject.KeyDown += OnKeyDown;
            
        }

        /// <summary>
        /// ビヘイビア解除
        /// </summary>
        protected override void OnDetaching()
        {

            base.OnDetaching();

            AssociatedObject.KeyDown -= OnKeyDown;

        }

        /// <summary>
        /// キー押下イベント
        /// </summary>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            if (sender is FrameworkElement element)
            {

                switch (e.Key)
                {

                    case Key.Enter:

                        var request = new TraversalRequest(Keyboard.Modifiers.Equals(ModifierKeys.Shift) 
                                            ? FocusNavigationDirection.Previous 
                                            : FocusNavigationDirection.Next);

                        element.MoveFocus(request);
                        break;

                    default:
                        break;

                }

            }

        }

    }

}
