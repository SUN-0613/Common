using System.ComponentModel;

namespace AYam.Common.Method
{

    /// <summary>
    /// 値変換管理共通メソッド
    /// </summary>
    public static class ValueConvert
    {

        /// <summary>
        /// 文字列を型Tに変換
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="value">値</param>
        /// <returns>変換値</returns>
        public static T FromString<T>(string value)
        {

            var conv = TypeDescriptor.GetConverter(typeof(T));

            if (conv != null)
            {
                return (T)conv.ConvertFromString(value);
            }
            else
            {
                return default(T);
            }

        }

    }
}
