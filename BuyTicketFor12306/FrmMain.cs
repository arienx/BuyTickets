using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Configuration;
using BuyTicketFor12306.Entites;
using mshtml;

namespace BuyTicketFor12306
{
    public partial class FrmMain : Form
    {
        #region 私有字段
        private static readonly string BASEURL = "https://dynamic.12306.cn/otsweb/";
        private static readonly string VALIDATEIMAGEURL = "passCodeAction.do?rand=sjrand";
        private static readonly string LOGINSUGGEST = "loginAction.do?method=loginAysnSuggest";
        private static readonly string LOGINURL = "loginAction.do?method=login";
        private static readonly string SEARCHURL = "order/querySingleAction.do?";
        private static readonly string ORDERACTIONURL = "order/myOrderAction.do?method=queryMyOrderNotComplete&leftmenu=N&fakeParent=true";
        private static readonly string ORDERACTION_TOKENKEY = "org.apache.struts.taglib.html.TOKEN";
        //登录参数
        private static readonly string LOGINPARAM = "loginRand={0}&refundLogin=N&refundFlag=Y&loginUser.user_name={1}&nameErrorFocus=&user.password={2}&passwordErrorFocus=&randCode={3}&randErrorFocus=";
        //记录是否点击过
        private static bool _isClicked = false;
        //线程锁
        private static object _aLock = new object();
        string loginParamString = string.Empty;
        #endregion

        #region 构造函数
        public FrmMain()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 获取验证码图片
        /// </summary>
        private void GetValidateImage()
        {
            Thread t = null;
            t = new Thread(() =>
            {
                picValidate.Image = HttpHelper.GetResponseImage(Path.Combine(BASEURL, VALIDATEIMAGEURL));
                Message = "验证码加载结束!";
                t.Abort();
            });
            t.Name = "test1";
            Message = "验证码加载中...";
            t.Start();
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void DoLogin(bool refreshValidateCode = true)
        {
            Thread th = null;
            th = new Thread(() =>
            {
                //获取随机数
                string rndNumber = DoLoginSuggest("loginRand");
                //if (refreshValidateCode || !string.IsNullOrEmpty(txtValidate.Text))
                //{
                //拼接登录参数
                loginParamString = string.Format(LOGINPARAM, rndNumber,
                   ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"], txtValidate.Text.Trim());
                //}
                //执行登录请求
                string filePath = HttpHelper.GetResponseHTML(Path.Combine(BASEURL, LOGINURL), false, loginParamString);
                wbMain.Url = new Uri(filePath, UriKind.RelativeOrAbsolute);
                wbMain.NewWindow += (sender, e) => { MessageBox.Show("Test"); };
                Message = "登录处理结束";
                //this.Invoke((Action)(() => { txtValidate.Text = string.Empty; }));
                th.Abort();
            });
            Message = "登录中...";
            th.Start();
        }

        /// <summary>
        /// 获取随机数，提供给登录
        /// </summary>
        /// <returns></returns>
        private string DoLoginSuggest(string key)
        {
            Stream stream = HttpHelper.GetResponseStream(Path.Combine(BASEURL, LOGINSUGGEST));
            string result = StreamHelper.ConvertStreamToString(stream);

            JObject obj = (JObject)JsonConvert.DeserializeObject(result);

            return obj[key].ToString();
        }

        /// <summary>
        /// 查询剩余
        /// </summary>
        private void DoSearch()
        {
            SearchTicketEntity entity = new SearchTicketEntity
            {
                //此处添加其它参数
            };
            string filePath = HttpHelper.GetResponseHTML(Path.Combine(BASEURL, SEARCHURL + entity));
            wbMain.Url = new Uri(filePath, UriKind.RelativeOrAbsolute);
            wbMain.Url = new Uri(HttpHelper.GetResponseHTML(Path.Combine(BASEURL, ORDERACTIONURL)), UriKind.RelativeOrAbsolute);
        }

        private string GetTokenData()
        {
            HTMLDocument html = wbMain.Document.DomDocument as HTMLDocument;
            IHTMLElementCollection collection = html.getElementsByName(ORDERACTION_TOKENKEY);
            var result = (collection.item(0) as HTMLInputElementClass).value;
            return result;
        }

        /// <summary>
        /// 状态栏消息
        /// </summary>
        private string Message
        {
            set
            {
                lock (_aLock)
                {
                    lblStatus.Text = value;
                }
            }
        }

        #region 私有事件
        private void picValidate_Click(object sender, EventArgs e)
        {
            GetValidateImage();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DoLogin(!_isClicked);
            if (!_isClicked)
                _isClicked = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            wbMain.WBDocHostShowUIShowMessage += new SafeWebBrowserExt.SafeWebBrowser.DocHostShowUIShowMessageEventHandler(wbMain_WBDocHostShowUIShowMessage);
            GetValidateImage();
        }
        int tempCount = 0;
        void wbMain_WBDocHostShowUIShowMessage(object sender, SafeWebBrowserExt.SafeWebBrowser.ExtendedBrowserMessageEventArgs e)
        {
            e.Cancel = true;
            //if (MessageBox.Show("是否要屏蔽Alert对话框", "提示", MessageBoxButtons.YesNo) == DialogResult.No)
            //{
            //    MessageBox.Show(e.Text, e.Caption);
            //}
            tempCount++;
            MessageBox.Show("正在进行第 " + (tempCount + 1) + " 次登录重试...\r\n原消息:" + e.Text);
            DoLogin();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            StreamHelper.DeleteAllTempFiles();
        }
        #endregion

        private void btnBuyTicket_Click(object sender, EventArgs e)
        {
            string token = GetTokenData();
        }
    }
}
