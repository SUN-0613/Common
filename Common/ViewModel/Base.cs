using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Common.ViewModel
{

    /// <summary>
    /// ViewModel基幹
    /// </summary>
    public class Base : INotifyPropertyChanged
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

            //プロパティ名が指定されていない場合は呼び出し元メソッド名とする
            if (propertyName.Length.Equals(0))
            {
                StackFrame Caller = new StackFrame(stackFrameIndex);        //呼び出し元メソッド情報
                string[] MethodName = Caller.GetMethod().Name.Split('_');   //呼び出し元メソッド名
                propertyName = MethodName[MethodName.Length - 1];
            }

            //イベント発生
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

    }

}
