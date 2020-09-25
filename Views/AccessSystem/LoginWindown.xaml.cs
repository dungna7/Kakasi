using DevExpress.Xpf.Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using kakashi.Common;
using kakashi.Properties;

namespace kakashi.Views
{
    /// <summary>
    /// Interaction logic for LoginWindown.xaml
    /// </summary>
    public partial class LoginWindown : ThemedWindow
    {
        public LoginWindown()
        {
            InitializeComponent();
            InitLoginCobobox();
        }
        public void InitLoginCobobox()
        {
            UserComboBox.ItemsSource = Settings.Default.LastLoginUser;
            if (Settings.Default.LastLoginUser != null)
            {
                UserComboBox.SelectedIndex = 0;
            }
        }

        private void YesBtnClick(object sender, RoutedEventArgs e)
        {
            LogHelper.Info("OnEntry YesButton_Click in");
            var loginName = UserComboBox.Text.Trim();
            var password = PasswordTextBox.Password.Trim();
            if (string.IsNullOrEmpty(loginName))
            {
                MessageDxUtil.ShowError(MainMessage.MAIN_MSG_005);
                UserComboBox.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageDxUtil.ShowError(MainMessage.MAIN_MSG_006);
                PasswordTextBox.Focus();
                return;
            }

            //登録ユーザーを設定に保存する(次回登録時、cobobox最初行に表示する)
            int userConut = 0;
            if (Settings.Default.LastLoginUser != null)
            {
                userConut = Settings.Default.LastLoginUser.Length;
            }
            List<System.String> userList = new List<System.String>();
            userList.Add(loginName);

            for (int i = 0; i < userConut; i++)
            {
                userList.Add(Properties.Settings.Default.LastLoginUser[i].ToString());
            }

            userList = userList.Distinct().ToList();

            Settings.Default.LastLoginUser = userList.ToArray();
            Settings.Default.Save();
        }
    }
}
