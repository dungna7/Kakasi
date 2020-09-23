using DevExpress.Skins;
using DevExpress.Skins.XtraForm;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kakashi.Common
{
    public static class MessageDxUtil
    {
        static MessageDxUtil()
        {
            //14F
            MessageBoxForm.MessageBoxFont = new Font("Meiryo UI", 9F); //定义字体类型
        }

        static readonly Icon MessageBoxIcon = null;

        /**
         * 一定時間でメッセージのdialogを表示
         * during: ミリ秒
         * */
        public static void ShowFloatTips(string message, int during)
        {
            XtraMessageBoxArgs args = new XtraMessageBoxArgs();
            args.AutoCloseOptions.Delay = during;
            args.Caption = "情報";
            args.Text = message;
            args.Buttons = new DialogResult[] { DialogResult.OK};
            XtraMessageBox.Show(args).ToString();
        }

        public static DialogResult ShowTips(string message)
        {
            return ShowInternal(null, message, "情報", SystemIcons.Information, DialogResult.OK);
        }
        public static DialogResult ShowWarning(string message)
        {
            return ShowInternal(null, message, "アラート情報", SystemIcons.Warning, DialogResult.OK);
        }
        public static DialogResult ShowWarning(string message,string Title)
        {
            return ShowInternal(null, message, Title, SystemIcons.Warning, DialogResult.OK);
        }
        public static DialogResult ShowError(string message)
        {
            return ShowInternal(null, message, "エラー情報", SystemIcons.Error, DialogResult.OK);
        }
        public static DialogResult ShowYesNoAndError(string message)
        {
            return ShowInternal(null, message, "エラー情報", SystemIcons.Error, DialogResult.Yes, DialogResult.No);
        }
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return ShowInternal(null, message, "確認情報", SystemIcons.Information, DialogResult.Yes, DialogResult.No);
        }
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return ShowInternal(null, message, "アラート情報", SystemIcons.Warning, DialogResult.Yes, DialogResult.No);
        }

        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            return ShowInternal(null, message, "提示情報", SystemIcons.Information, DialogResult.Yes, DialogResult.No, DialogResult.Cancel);
        }

        public static DialogResult ShowYesNoAndTips(string message, string Title)
        {
            return ShowInternal(null, message, Title, SystemIcons.Information, DialogResult.Yes, DialogResult.No);
        }

        public static DialogResult ShowOkCancelAndTips(string message, string Title)
        {
            return ShowInternal(null, message, Title, SystemIcons.Information, DialogResult.OK, DialogResult.Cancel);
        }

        public static void Show(string message)
        {
            ShowInternal(null, message, "Notice", SystemIcons.Information, DialogResult.OK);
        }

        public static void Show(Control owner, string message)
        {
            ShowInternal(owner, message, "Notice", SystemIcons.Information, DialogResult.OK);
        }

        private static DialogResult ShowInternal(Control owner, string message, string caption, Icon messageIcon, params DialogResult[] dialogResults)
        {
            MessageBoxForm form = new MessageBoxForm();
            form.Icon = MessageBoxIcon;
            XtraMessageBoxArgs args = new XtraMessageBoxArgs(owner, message, caption, dialogResults, messageIcon, 0);
            args.Showing += Args_Showing;
            return form.ShowMessageBoxDialog(args);
        }

        private static void Args_Showing(object sender, XtraMessageShowingArgs e)
        {
            MessageButtonCollection buttons = e.Buttons as MessageButtonCollection;
            SimpleButton btn = null;
            foreach (var dialog in (DialogResult[])Enum.GetValues(typeof(DialogResult)))
            {
                btn = buttons[dialog] as SimpleButton;
                if (btn != null)
                {
                    btn.Size = new Size(Convert.ToInt32(btn.Width * 1.2), Convert.ToInt32(btn.Height * 1.2));
                    btn.Font = e.Form.Font;
                    btn.KeyPress += OperatorButton_Click;
                }
            }
        }
        private static void OperatorButton_Click(object sender, KeyPressEventArgs e)
        {
            if ("　".Equals(e.KeyChar.ToString()) || " ".Equals(e.KeyChar.ToString()))
            {
                // Do what you want to do.
                var operatorButton = sender as SimpleButton;
                if (operatorButton != null)
                    operatorButton.PerformClick();
            }
        }
    }

    internal class MessageBoxForm : XtraMessageBoxForm
    {
        internal static Font MessageBoxFont = new Font("Meiryo UI", 9F);

        public MessageBoxForm()
        {
            Appearance.Font = MessageBoxFont;
        }

        protected override FormPainter CreateFormBorderPainter()
        {
            return new MessageBoxFormPainter(this, LookAndFeel);
        }
    }

    internal class MessageBoxFormPainter : FormPainter
    {
        internal MessageBoxFormPainter(Control owner, ISkinProvider provider) : base(owner, provider) { }

        protected override void DrawText(GraphicsCache cache)
        {
            string text = Text;
            if (text == null || text.Length == 0 || TextBounds.IsEmpty)
                return;
            AppearanceObject appearance = new AppearanceObject(GetDefaultAppearance());
            appearance.Font = Owner.Font;
            appearance.TextOptions.Trimming = Trimming.EllipsisCharacter;
            Rectangle r = RectangleHelper.GetCenterBounds(TextBounds, new Size(TextBounds.Width, appearance.CalcDefaultTextSize(cache.Graphics).Height));
            DrawTextShadow(cache, appearance, r);
            cache.DrawString(text, appearance.Font, appearance.GetForeBrush(cache), r, appearance.GetStringFormat());
        }

        //protected override int CalcTextHeight(Graphics graphics, AppearanceObject appearance)
        //{
        //    return (int)(graphics.MeasureString(Text, Owner.Font).Height); 
        //}
    }
}
