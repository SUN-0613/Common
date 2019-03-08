namespace AYam.Common.ViewModel.Custom
{

    /// <summary>
    /// ViewModel基幹
    /// 編集FLG付
    /// </summary>
    public class EditedBase : Common.ViewModel.VMBase
    {

        /// <summary>
        /// 編集FLG
        /// true:編集済
        /// false:未編集
        /// </summary>
        private bool _IsEdited = false;

        /// <summary>
        /// 編集FLGプロパティ
        /// True:編集済
        /// False:未編集
        /// </summary>
        public bool IsEdited
        {
            get { return _IsEdited; }
            set
            {
                _IsEdited = value;
                CallPropertyChanged(nameof(IsEdited));
            }
        }

        /// <summary>
        /// 編集FLGの更新対象外プロパティ名
        /// 初期値："Call"
        /// </summary>
        /// <remarks>先頭一致するプロパティ名の場合はIsEditedを更新しない</remarks>
        public string ThrowEditEventName { private get; set; } = "Call";

        /// <summary>
        /// PropertyChanged()呼び出し
        /// </summary>
        /// <param name="propertyName">Changedイベントを発生させたいプロパティ名</param>
        /// <param name="stackFrameIndex">呼び出し元StackFrame</param>
        protected override void CallPropertyChanged(string propertyName = "", int stackFrameIndex = 1)
        {

            base.CallPropertyChanged(propertyName, stackFrameIndex + 1);

            // 編集FLGの更新
            // 編集FLGプロパティ更新時は除外
            // ThrowEditEventNameにて指定されたプロパティ更新時は除外
            if (!propertyName.Equals(nameof(IsEdited))
                && !(propertyName.Length >= ThrowEditEventName.Length && propertyName.Substring(0, ThrowEditEventName.Length).ToUpper().Equals(ThrowEditEventName.ToUpper())))
                IsEdited = true;

        }

    }

}
