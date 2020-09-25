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
            try
            {
                string ip = NetworkUtil.GetLocalIP();
                string macAddr = HardwareInfoHelper.GetMacAddress();

                string identity = BLLFactory<TIAS.Security.BLL.User>.Instance.VerifyUserNew(loginName, password, "TiasSys", ip, macAddr);
                if (identity == "VerifyOK")
                {
                    UserInfo info = BLLFactory<TIAS.Security.BLL.User>.Instance.GetUserByName(loginName);
                    if (info != null)
                    {
                        //メインウインドウ起動
                        Bootstrapper mainStarter = new Bootstrapper();
                        #region ログインユーザーの機能リスト取得

                        List<FunctionInfo> list = BLLFactory<TIAS.Security.BLL.Function>.Instance.GetFunctionsByUser(info.ID, Bootstrapper.gc.SystemType);
                        if (list != null && list.Count > 0)
                        {
                            foreach (FunctionInfo functionInfo in list)
                            {
                                if (!Bootstrapper.gc.FunctionDict.ContainsKey(functionInfo.ControlID))
                                {
                                    Bootstrapper.gc.FunctionDict.Add(functionInfo.ControlID, functionInfo.Name);
                                }
                            }
                        }

                        #endregion
                        //部門情報
                        List<OUInfo> OUInfoList = BLLFactory<TIAS.Security.BLL.OU>.Instance.GetOUsByUser(info.ID);

                        bLogin = true;
                        Bootstrapper.gc.UserInfo = info;
                        Bootstrapper.gc.LoginUserInfo = Bootstrapper.gc.ConvertToLoginUser(info);
                        if (OUInfoList.Count > 0)
                        {
                            OUInfo OuInfo = OUInfoList[0];
                            Bootstrapper.gc.LoginUserInfo.OuID = OuInfo.ID;
                            Bootstrapper.gc.LoginUserInfo.OuName = OuInfo.Name;
                        }
                        Cache.Instance.Add("LoginUserInfo", Bootstrapper.gc.LoginUserInfo);//後続の処理のためにユーザー情報をキャッシュに入れる
                        Cache.Instance.Add("FunctionDict", Bootstrapper.gc.FunctionDict);//後で使用するために機能情報をキャッシュする

                        Cache.Instance.Add("ModuleAccepModify", "none"); //ModuleAccepModify

                        var dic = new Dictionary<long, long>();
                        using (var context = new TiasContext())
                        {
                            string sql = "select User_ID,OU_ID from T_ACL_OU_User where 1=1;";
                            List<T_ACL_OU_User> UserOu = context.Database.SqlQuery<T_ACL_OU_User>(sql).ToList();
                            foreach (var item in UserOu)
                            {
                                if (dic.ContainsKey(item.User_ID))
                                {
                                    if (dic[item.User_ID] > item.OU_ID)
                                    {
                                        dic[item.User_ID] = item.OU_ID;
                                    }
                                }
                                else
                                {
                                    dic[item.User_ID] = item.OU_ID;
                                }
                            }
                        }
                        Cache.Instance.Add("UserOUMapping", dic);//後で使用するために機能情報をキャッシュする

                        //ログ出力
                        AccessLog.AddAccessLog("TIASシステム登録");

                        //観覧中受付削除します。
                        using (TiasContext dcontex = new TiasContext())
                        {
                            try
                            {
                                string sql = "DELETE T_ReserveLocks " +
                                             "WHERE LoginName = '" + ((LoginUserInfo)Cache.Instance["LoginUserInfo"]).Name + "'"
                                        + " and IPAddress='" + NetworkUtil.GetIPAddress() + "'; ";

                                dcontex.Database.ExecuteSqlCommand(sql);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        //メインウインドウを起動する
                        if (!DXSplashScreen.IsActive)
                            DXSplashScreen.Show<SplashScreenView>();
                        mainStarter.Run();
                        if (DXSplashScreen.IsActive)
                            DXSplashScreen.Close();
                        Window.GetWindow(this).Close();
                    }
                }
                else if (identity == "PasswordLock")
                {
                    MessageDxUtil.ShowTips(MainMessage.MAIN_MSG_013);
                    PasswordTextBox.Clear();
                    PasswordTextBox.Focus();
                }
                else if (identity == "UserExpireDate")
                {
                    MessageDxUtil.ShowTips(MainMessage.MAIN_MSG_020);
                    PasswordTextBox.Clear();
                    PasswordTextBox.Focus();
                }
                else if (identity == "PasswordExpireDate")
                {
                    MessageDxUtil.ShowTips(MainMessage.MAIN_MSG_021);
                    PasswordTextBox.Clear();
                    PasswordTextBox.Focus();
                }
                else
                {
                    MessageDxUtil.ShowTips(MainMessage.MAIN_MSG_006);
                    PasswordTextBox.Clear();
                    PasswordTextBox.Focus();
                }
            }
            catch (Exception err)
            {
                MessageDxUtil.ShowError(err.Message);
            }
            LogHelper.Info("OnEntry YesButton_Click out");
        }
    }
}
