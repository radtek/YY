﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xr.Common.Internal;

namespace Xr.Common
{
    /// <summary>
    /// 消息框
    /// </summary>
    public static class MessageBoxUtils
    {
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">需要显示在消息框中的文本</param>
        /// <param name="buttons">指定在消息框中显示哪些按钮</param>
        /// <returns></returns>
        public static DialogResult Show(string text, MessageBoxButtons buttons)
        {
            return Show(text, buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">需要显示在消息框中的文本</param>
        /// <param name="buttons">指定在消息框中显示哪些按钮</param>
        /// <param name="buttonTexts">按钮文本</param>
        /// <returns></returns>
        public static DialogResult Show(string text, MessageBoxButtons buttons, string[] buttonTexts)
        {
            return Show(text, buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, buttonTexts);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">需要显示在消息框中的文本</param>
        /// <param name="buttons">指定在消息框中显示哪些按钮</param>
        /// <param name="icon">指定在消息框中显示哪个图标</param>
        /// <param name="defaultButton">指定消息框中的默认按钮</param>
        /// <returns></returns>
        public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return Show(text, "信息", buttons, icon, defaultButton);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">需要显示在消息框中的文本</param>
        /// <param name="caption">需要在消息框标题中显示的文本</param>
        /// <param name="buttons">指定在消息框中显示哪些按钮</param>
        /// <param name="icon">指定在消息框中显示哪个图标</param>
        /// <param name="defaultButton">指定消息框中的默认按钮</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return Show(text, caption, buttons, icon, defaultButton, null);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">需要显示在消息框中的文本</param>
        /// <param name="buttons">指定在消息框中显示哪些按钮</param>
        /// <param name="icon">指定在消息框中显示哪个图标</param>
        /// <param name="defaultButton">指定消息框中的默认按钮</param>
        /// <param name="buttonTexts">按钮文本</param>
        /// <returns></returns>
        public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, string[] buttonTexts)
        {
            return Show(text, "信息", buttons, icon, defaultButton, buttonTexts);
        }

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="text">需要显示在消息框中的文本</param>
        /// <param name="caption">需要在消息框标题中显示的文本</param>
        /// <param name="buttons">指定在消息框中显示哪些按钮</param>
        /// <param name="icon">指定在消息框中显示哪个图标</param>
        /// <param name="defaultButton">指定消息框中的默认按钮</param>
        /// <param name="buttonTexts">按钮文本</param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, string[] buttonTexts)
        {
            var frmMessageBox = new MessageBoxForm();
            frmMessageBox.Message = text;
            frmMessageBox.Caption = caption;
            frmMessageBox.Buttons = buttons;
            frmMessageBox.MessageIcon = icon;
            frmMessageBox.DefaultButton = defaultButton;
            frmMessageBox.ButtonTexts = buttonTexts;
            frmMessageBox.Initialize();
            return frmMessageBox.ShowDialog(GetMessageBoxOwner());
        }


        /// <summary>
        /// 显示弱消息提示框，3秒后自动消失
        /// </summary>
        /// <param name="text">需要显示在提示框中的文字</param>
        public static void Hint(string text)
        {
            Hint(text, HintMessageBoxIcon.Success, 3, false, false);
        }

        /// <summary>
        /// 显示弱消息提示框，2秒后自动消失
        /// </summary>
        /// <param name="text">需要显示在提示框中的文字</param>
        /// <param name="aotuSize">根据消息自适应大小,根据/n进行换行</param>
        public static void Hint(string text, bool aotuSize)
        {
            Hint(text, HintMessageBoxIcon.Success, 5, false, aotuSize);
        }

        /// <summary>
        /// 显示弱消息提示框，2秒后自动消失
        /// </summary>
        /// <param name="text">需要显示在提示框中的文字</param>
        /// <param name="icon">需要显示在提示框中的图标</param>
        public static void Hint(string text, HintMessageBoxIcon icon)
        {
            Hint(text, icon, 3, false, false);
        }

        /// <summary>
        /// 显示弱消息提示框
        /// </summary>
        /// <param name="text">需要显示在提示框中的文字</param>
        /// <param name="durationSeconds">提示框显示的秒数，超过这个秒数后，提示框自动消失</param>
        /// <param name="keepAliveOnOuterClick">点击消息框外侧区域时，是否自动关闭消息框</param>
        public static void Hint(string text, int durationSeconds, bool keepAliveOnOuterClick)
        {
            Hint(text, HintMessageBoxIcon.Success, durationSeconds, keepAliveOnOuterClick, false);
        }

        /// <summary>
        /// 显示弱消息提示框
        /// </summary>
        /// <param name="text">需要显示在提示框中的文字</param>
        /// <param name="icon">需要显示在提示框中的图标</param>
        /// <param name="durationSeconds">提示框显示的秒数，超过这个秒数后，提示框自动消失</param>
        /// <param name="keepAliveOnOuterClick">点击消息框外侧区域时，是否自动关闭消息框</param>
        /// <param name="autoSize">根据消息自适应大小</param>
        public static void Hint(string text, HintMessageBoxIcon icon, int durationSeconds, bool keepAliveOnOuterClick, bool autoSize)
        {
            var frmMessageBox = new HintMessageBoxForm();
            frmMessageBox.flag = autoSize;
            frmMessageBox.Message = text;
            frmMessageBox.MessageBoxIcon = icon;
            frmMessageBox.DurationSeconds = durationSeconds;
            frmMessageBox.KeepAliveOnOuterClick = keepAliveOnOuterClick;
            frmMessageBox.Owner = Application.OpenForms.Count == 0 ? null : Application.OpenForms[0];
            frmMessageBox.Show();
            frmMessageBox.BringToFront();
        }

        /// <summary>
        /// 显示内容框，一小时后自动消失
        /// </summary>
        /// <param name="text">需要显示在提示框中的文字</param>
        public static void HintTextEdit(string text, int xWidht, int yHeight, int formWdith)
        {
            HintTextEdit(text, 3600, false, true, xWidht, yHeight, formWdith);
        }

        /// <summary>
        /// 显示内容框
        /// </summary>
        /// <param name="text">需要显示在提示框中的文字</param>
        /// <param name="durationSeconds">提示框显示的秒数，超过这个秒数后，提示框自动消失</param>
        /// <param name="keepAliveOnOuterClick">点击消息框外侧区域时，是否自动关闭消息框</param>
        /// <param name="autoSize">根据消息自适应大小</param>
        public static void HintTextEdit(string text, int durationSeconds, bool keepAliveOnOuterClick,
            bool autoSize, int xWidth, int yHeight, int formWdith)
        {
            var frmMessageBox = new HintTextEditForm(autoSize, formWdith);
            frmMessageBox.Message = text;
            frmMessageBox.DurationSeconds = durationSeconds;
            frmMessageBox.KeepAliveOnOuterClick = keepAliveOnOuterClick;
            //frmMessageBox.Owner = Application.OpenForms.Count == 0 ? null : Application.OpenForms[0];
            frmMessageBox.Location = new Point(xWidth, yHeight);
            frmMessageBox.Width = formWdith;
            frmMessageBox.Show();
            //frmMessageBox.BringToFront();
        }

        private static Form GetMessageBoxOwner()
        {
            for (var i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Visible) return Application.OpenForms[i];
            }

            return null;
        }
    }
}
