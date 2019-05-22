using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace AYam.Common.Controls.Behaviors
{

    /// <summary>
    /// TextBox用ビヘイビア
    /// 数値のみ入力許可
    /// </summary>
    public class TextBoxNumberOnly : Behavior<TextBox>
    {

        /// <summary>
        /// ビヘイビア登録
        /// </summary>
        protected override void OnAttached()
        {

            base.OnAttached();

            // テキスト入力処理
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;

            // クリップボード処理
            AssociatedObject.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnExecutePaste));

            // IMEモードを無効化
            InputMethod.SetIsInputMethodEnabled(AssociatedObject, false);

            // コンテキストメニュー無効化
            AssociatedObject.ContextMenu = null;

            // PreviewTextInputにてスペースをスルーしないようにする
            AssociatedObject.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.Space, ModifierKeys.None));
            AssociatedObject.InputBindings.Add(new KeyBinding(ApplicationCommands.NotACommand, Key.Space, ModifierKeys.Shift));

        }

        /// <summary>
        /// ビヘイビア解除
        /// </summary>
        protected override void OnDetaching()
        {

            base.OnDetaching();

            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            AssociatedObject.CommandBindings.Clear();
            AssociatedObject.InputBindings.Clear();

        }

        /// <summary>
        /// テキスト取得時の処理イベント
        /// </summary>
        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            // 整数値以外を入力された時は処理をキャンセルする
            if (!int.TryParse(e.Text, out int resultValue))
            {
                e.Handled = true;
            }

        }

        /// <summary>
        /// クリップボード貼り付け時の処理イベント
        /// </summary>
        private void OnExecutePaste(object sender, ExecutedRoutedEventArgs e)
        {

            // 整数値を貼り付けされた時は処理を許可する
            if (int.TryParse(Clipboard.GetText(), out int resultValue))
            {
                AssociatedObject.Paste();
            }

        }

    }

}
