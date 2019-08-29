

using System.Windows;
using System.Windows.Input;

namespace AYam.Common.Controls.Custom
{

    /// <summary>
    /// DataGridTemplateColumnカスタマイズ
    /// </summary>
    /// <remarks>
    /// 内部コントロールにフォーカスを当てる
    /// </remarks>
    public class DataGridTemplateColumn : System.Windows.Controls.DataGridTemplateColumn
    {

        /// <summary>
        /// 列内のセルを編集モードに変更
        /// </summary>
        /// <param name="editingElement">列内に表示される要素</param>
        /// <param name="editingEventArgs">ユーザジェスチャ情報</param>
        /// <returns>セル編集前の値</returns>
        protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
        {

            // セル内部コントロールへフォーカス移動
            editingElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));

            return base.PrepareCellForEdit(editingElement, editingEventArgs);

        }

    }

}
