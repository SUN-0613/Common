using System;
using System.Windows.Input;

namespace AYam.Common.ViewModel
{

#pragma warning disable 0067    //警告非表示 C#0067

    /// <summary>
    /// Delegateを受け取るICommandの実装
    /// </summary>
    public class DelegateCommand : ICommand
    {


        /// <summary>
        /// CanExecute変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 実行メソッド
        /// </summary>
        private readonly Action _Execute;

        /// <summary>
        /// 実行メソッド処理許可
        /// </summary>
        private readonly Func<bool> _CanExecute;

        /// <summary>
        /// Delegateを受け取るICommandの実装
        /// </summary>
        /// <param name="execute">実行メソッド</param>
        public DelegateCommand(Action execute)
        {

            _Execute = execute ?? throw new ArgumentNullException(nameof(DelegateCommand) + ":" + nameof(execute));
            _CanExecute = null;

        }

        /// <summary>
        /// Delegateを受け取るICommandの実装
        /// </summary>
        /// <param name="execute">実行メソッド</param>
        /// <param name="canExecute">実行メソッドの処理許可</param>
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {

            _Execute = execute ?? throw new ArgumentNullException(nameof(DelegateCommand) + ":" + nameof(execute));
            _CanExecute = canExecute ?? throw new ArgumentNullException(nameof(DelegateCommand) + ":" + nameof(canExecute));

        }

        /// <summary>
        /// メソッド実行
        /// </summary>
        public void Execute()
        {
            _Execute();
        }

        /// <summary>
        /// メソッド実行
        /// </summary>
        /// <param name="parameter">
        /// パラメータ
        /// このクラスでは使用しない
        /// </param>
        public void Execute(object parameter)
        {
            _Execute();
        }

        /// <summary>
        /// メソッド実行許可の確認
        /// </summary>
        /// <returns>
        /// True:OK
        /// False:NG
        /// </returns>
        public bool CanExecute()
        {
            return _CanExecute == null ? true : _CanExecute();
        }

        /// <summary>
        /// メソッド実行許可の確認
        /// </summary>
        /// <param name="parameter">
        /// パラメータ
        /// このクラスでは使用しない
        /// </param>
        /// <returns>
        /// True:OK
        /// False:NG
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null ? true : _CanExecute();
        }

    }

    /// <summary>
    /// Delegateを受け取るICommandの実装
    /// パラメータ有
    /// </summary>
    /// <typeparam name="T">コマンドパラメータ</typeparam>
    public class DelegateCommand<T> : ICommand
    {


        /// <summary>
        /// CanExecute変更イベント
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 実行メソッド
        /// </summary>
        private readonly Action<T> _Execute;

        /// <summary>
        /// 実行メソッド処理許可
        /// </summary>
        private readonly Func<bool> _CanExecute;

        /// <summary>
        /// new
        /// </summary>
        /// <param name="execute">実行メソッド</param>
        public DelegateCommand(Action<T> execute)
        {
            _Execute = execute ?? throw new ArgumentNullException(nameof(DelegateCommand) + ":" + nameof(execute));
            _CanExecute = null;
        }

        /// <summary>
        /// new
        /// </summary>
        /// <param name="execute">実行メソッド</param>
        /// <param name="canExecute">実行メソッドの処理許可</param>
        public DelegateCommand(Action<T> execute, Func<bool> canExecute)
        {
            _Execute = execute ?? throw new ArgumentNullException(nameof(DelegateCommand) + ":" + nameof(execute));
            _CanExecute = canExecute ?? throw new ArgumentNullException(nameof(DelegateCommand) + ":" + nameof(canExecute));
        }

        /// <summary>
        /// メソッド実行
        /// </summary>
        /// <param name="parameter">
        /// パラメータ
        /// このクラスでは使用しない
        /// </param>
        public void Execute(object parameter)
        {
            _Execute((T)parameter);
        }

        /// <summary>
        /// メソッド実行許可の確認
        /// </summary>
        /// <param name="parameter">
        /// パラメータ
        /// このクラスでは使用しない
        /// </param>
        /// <returns>
        /// True:OK
        /// False:NG
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return _CanExecute == null ? true : _CanExecute();
        }

    }

}
