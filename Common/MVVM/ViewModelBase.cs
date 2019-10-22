using System;
using System.ComponentModel;
using System.Diagnostics;

namespace AYam.Common.MVVM
{

    /// <summary>
    /// ViewModel基幹
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        /// <summary>
        /// Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// PropertyChanged()呼び出し
        /// </summary>
        /// <param name="propertyName">Changedイベントを発生させたいプロパティ名</param>
        /// <param name="stackFrameIndex">呼び出し元StackFrame</param>
        protected virtual void CallPropertyChanged(string propertyName = "", Int32 stackFrameIndex = 1)
        {

            if (PropertyChanged == null) return;

            // プロパティ名が指定されていない場合は呼び出し元メソッド名とする
            if (propertyName.Length.Equals(0))
            {

                StackFrame caller = new StackFrame(stackFrameIndex);        // 呼び出し元メソッド情報
                string[] methodNames = caller.GetMethod().Name.Split('_');  // 呼び出し元メソッド名

                propertyName = methodNames[methodNames.Length - 1];

            }

            // イベント発生
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

    }

}
