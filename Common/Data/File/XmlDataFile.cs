using AYam.Common.Method;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AYam.Common.Data.File
{

    /// <summary>
    /// データファイル
    /// XML形式
    /// ベースクラス
    /// </summary>
    public abstract class XmlDataFile : IDisposable
    {

        /// <summary>
        /// ファイルパス
        /// </summary>
        private readonly string _FilePath;

        /// <summary>
        /// ルートパス名称
        /// </summary>
        private readonly string _RootTag;

        /// <summary>
        /// XMLオブジェクト
        /// </summary>
        private XDocument _Document;

        /// <summary>
        /// XML宣言
        /// </summary>
        private readonly XDeclaration _Declaration;

        /// <summary>
        /// XMLオブジェクト内のルート要素
        /// </summary>
        public XElement Element;

        /// <summary>
        /// ファイル読込
        /// </summary>
        public abstract void Read();

        /// <summary>
        /// ファイル保存
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// 終了処理
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// データファイル管理
        /// XMLファイル読込後、Read()実行
        /// using System.Xml.Linq; 推奨
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="root">XMLルート要素名</param>
        /// <param name="declaration">
        /// XML宣言
        /// nullの場合は初期値をセット
        /// </param>
        public XmlDataFile(string path, string root, XDeclaration declaration = null)
        {

            _FilePath = path;
            _RootTag = root;
            _Declaration = declaration ?? new XDeclaration("1.0", "utf-8", null);

            if (System.IO.File.Exists(_FilePath))
            {

                _Document = XDocument.Load(_FilePath);
                Element = _Document.Element(_RootTag);

            }

            Read();

        }

        /// <summary>
        /// 現在要素の子要素から値取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="tag">子要素タグ名</param>
        /// <returns>値</returns>
        protected T GetValue<T>(string tag)
        {
            return Element != null ? GetValue(Element.Element(tag), default(T)) : default(T);
        }

        /// <summary>
        /// 現在要素の子要素から値取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="tag">子要素タグ名</param>
        /// <param name="defaultValue">値取得失敗時の戻り値</param>
        /// <returns>値</returns>
        protected T GetValue<T>(string tag, T defaultValue)
        {
            return Element != null ? GetValue(Element.Element(tag), defaultValue) : defaultValue;
        }

        /// <summary>
        /// 指定要素から値取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="element">指定要素</param>
        /// <param name="tag">子要素タグ名</param>
        /// <returns>値</returns>
        protected T GetValue<T>(XElement element)
        {
            return element != null ? ValueConvert.FromString<T>(element.Value) : default(T);
        }

        /// <summary>
        /// 指定要素の子要素から値取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="element">指定要素</param>
        /// <param name="tag">子要素タグ名</param>
        /// <returns>値</returns>
        protected T GetValue<T>(XElement element, string tag)
        {
            return element != null ? GetValue(element.Element(tag), default(T)) : default(T);
        }

        /// <summary>
        /// 指定要素の子要素から値取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="element">指定要素</param>
        /// <param name="tag">子要素タグ名</param>
        /// <param name="defaultValue">値取得失敗時の戻り値</param>
        /// <returns>値</returns>
        protected T GetValue<T>(XElement element, string tag, T defaultValue)
        {
            return element != null ? GetValue(element.Element(tag), defaultValue) : defaultValue;
        }

        /// <summary>
        /// 指定要素から値取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="element">指定要素</param>
        /// <param name="tag">子要素タグ名</param>
        /// <param name="defaultValue">値取得失敗時の戻り値</param>
        /// <returns>値</returns>
        protected T GetValue<T>(XElement element, T defaultValue)
        {

            if (element != null)
            {

                var value = ValueConvert.FromString<T>(element.Value);

                return value.Equals(default(T)) ? defaultValue : value;

            }
            else
            {
                return defaultValue;
            }

        }

        /// <summary>
        /// 現在要素の指定属性値を取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="name">属性名称</param>
        /// <returns>値</returns>
        protected T GetAttribute<T>(string name)
        {
            return GetAttribute(Element, name, default(T));
        }

        /// <summary>
        /// 現在要素の指定属性値を取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">値取得失敗時の戻り値</param>
        /// <returns>値</returns>
        protected T GetAttribute<T>(string name, T defaultValue)
        {
            return GetAttribute(Element, name, defaultValue);
        }

        /// <summary>
        /// 指定要素から指定属性値を取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="element">指定要素</param>
        /// <param name="name">属性名称</param>
        /// <returns>値</returns>
        protected T GetAttribute<T>(XElement element, string name)
        {
            return (element != null && element.Attribute(name) != null) ? ValueConvert.FromString<T>(element.Attribute(name).Value) : default(T);
        }

        /// <summary>
        /// 指定要素から指定属性値を取得
        /// </summary>
        /// <typeparam name="T">型指定</typeparam>
        /// <param name="element">指定要素</param>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">値取得失敗時の戻り値</param>
        /// <returns>値</returns>
        protected T GetAttribute<T>(XElement element, string name, T defaultValue)
        {

            if (element != null && element.Attribute(name) != null)
            {

                var value = ValueConvert.FromString<T>(element.Attribute(name).Value);

                return value.Equals(default(T)) ? defaultValue : value;

            }
            else
            {
                return defaultValue;
            }

        }

        /// <summary>
        /// 新規要素作成
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="child">子要素</param>
        /// <returns>新規要素</returns>
        protected XElement NewElement(XName name, XElement child)
        {
            return new XElement(name, child);
        }

        /// <summary>
        /// 新規要素作成
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="children">子要素一覧</param>
        /// <returns>新規要素</returns>
        protected XElement NewElement(XName name, List<XElement> children)
        {

            var element = new XElement(name);

            children.ForEach(child => { element.Add(child); });

            return element;

        }

        /// <summary>
        /// 子要素を追加
        /// </summary>
        /// <param name="parent">子要素を追加する親要素</param>
        /// <param name="child">子要素</param>
        protected void AddElement(ref XElement parent, XElement child)
        {
            parent.Add(child);
        }

        /// <summary>
        /// 子要素を一括追加
        /// </summary>
        /// <param name="parent">親要素</param>
        /// <param name="children">子要素一覧</param>
        protected void AddElements(ref XElement parent, List<XElement> children)
        {

            for (int iLoop = 0; iLoop < children.Count; iLoop++)
            {
                parent.Add(children[iLoop]);
            }

        }

        /// <summary>
        /// 属性を追加
        /// </summary>
        /// <param name="element">要素</param>
        /// <param name="attribute">属性</param>
        protected void AddAttribute(ref XElement element, XAttribute attribute)
        {
            element.Add(attribute);
        }

        /// <summary>
        /// 属性を一括追加
        /// </summary>
        /// <param name="element">要素</param>
        /// <param name="attributes">属性一覧</param>
        protected void AddAttributes(ref XElement element, List<XAttribute> attributes)
        {

            for (int iLoop = 0; iLoop < attributes.Count; iLoop++)
            {
                element.Add(attributes[iLoop]);
            }

        }

        /// <summary>
        /// XMLファイル書込
        /// </summary>
        /// <param name="elements">ルート要素の下位に登録する要素一覧</param>
        protected void WriteFile(List<XElement> elements)
        {
            WriteFile(new XDocument(_Declaration, NewElement(_RootTag, elements)));
        }

        /// <summary>
        /// XMLファイル書込
        /// </summary>
        /// <param name="document">保存するXMLオブジェクト</param>
        private void WriteFile(XDocument document)
        {

            _Document = document;
            _Document.Save(_FilePath);

        }

        /// <summary>
        /// XMLファイル削除
        /// </summary>
        protected void DeleteXmlFile()
        {

            if (System.IO.File.Exists(_FilePath))
            {
                System.IO.File.Delete(_FilePath);
            }

        }

    }

}
