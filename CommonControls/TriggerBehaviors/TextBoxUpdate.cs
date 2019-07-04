using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace AYam.Common.Controls.TriggerBehaviors
{

    /// <summary>
    /// TextBox用ビヘイビア
    /// イベント発生時にTextPropertyを更新するトリガ
    /// </summary>
    public class TextBoxUpdate : TriggerAction<TextBox>
    {

        /// <summary>
        /// TextProperty.Binding
        /// </summary>
        private BindingExpression _Binding;

        /// <summary>
        /// ビヘイビア登録
        /// </summary>
        protected override void OnAttached()
        {

            base.OnAttached();
            _Binding = AssociatedObject.GetBindingExpression(TextBox.TextProperty);

        }

        /// <summary>
        /// ビヘイビア解除
        /// </summary>
        protected override void OnDetaching()
        {

            _Binding = null;
            base.OnDetaching();

        }

        /// <summary>
        /// TextProperty更新
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            
            if (_Binding != null)
            {
                _Binding.UpdateSource();
            }

        }

    }
}
