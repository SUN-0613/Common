using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AYam.Common.Controls.ResourceDictionary
{
    partial class DataGridSingleClickStyle : System.Windows.ResourceDictionary
    {

        /// <summary>
        /// DataGridCell.Single Click Editing
        /// </summary>
        private void DataGridCell_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (sender is DataGridCell cell)
            {

                // 編集中のセル、読取専用のセルは除外
                if (!cell.IsEditing && !cell.IsReadOnly)
                {

                    // 未フォーカスならフォーカスセット
                    if (!cell.IsFocused)
                    {
                        cell.Focus();
                    }

                    // 対象セルを保有するDataGridを取得
                    var dataGrid = FindVisualParent<DataGrid>(cell);

                    if (dataGrid != default(DataGrid))
                    {

                        // 行単位で選択する設定となっているか
                        if (dataGrid.SelectionUnit != DataGridSelectionUnit.FullRow)
                        {

                            // セル単位で選択
                            if (!cell.IsSelected)
                            {
                                cell.IsSelected = true;
                            }

                        }
                        else
                        {

                            // 対象セルを保有するDataGridRowを取得
                            var row = FindVisualParent<DataGridRow>(cell);

                            // 行単位で選択
                            if (row != default(DataGridRow) && !row.IsSelected)
                            {
                                row.IsSelected = true;
                            }


                        }

                    }

                }

            }

        }

        /// <summary>
        /// 指定要素より上位の部分で指定型の要素を検索
        /// </summary>
        /// <typeparam name="T">指定型</typeparam>
        /// <param name="element">指定要素</param>
        /// <returns>指定型の要素、取得できない場合はdefault値</returns>
        private static T FindVisualParent<T>(UIElement element)
        {

            var parent = element;

            while (parent != null)
            {

                if (parent is T correctlyTyped)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;

            }

            return default;

        }

    }

}
