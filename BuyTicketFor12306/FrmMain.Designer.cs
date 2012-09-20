namespace BuyTicketFor12306
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.用户列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.快速购票方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtValidate = new System.Windows.Forms.TextBox();
            this.picValidate = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.wbMain = new BuyTicketFor12306.SafeWebBrowserExt.SafeWebBrowser();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnBuyTicket = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picValidate)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户列表ToolStripMenuItem,
            this.快速购票方案ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(732, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 用户列表ToolStripMenuItem
            // 
            this.用户列表ToolStripMenuItem.Name = "用户列表ToolStripMenuItem";
            this.用户列表ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.用户列表ToolStripMenuItem.Text = "用户列表";
            // 
            // 快速购票方案ToolStripMenuItem
            // 
            this.快速购票方案ToolStripMenuItem.Name = "快速购票方案ToolStripMenuItem";
            this.快速购票方案ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.快速购票方案ToolStripMenuItem.Text = "快速购票方案";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(173, 33);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "登 录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtValidate
            // 
            this.txtValidate.Location = new System.Drawing.Point(12, 34);
            this.txtValidate.Name = "txtValidate";
            this.txtValidate.Size = new System.Drawing.Size(77, 21);
            this.txtValidate.TabIndex = 2;
            // 
            // picValidate
            // 
            this.picValidate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picValidate.Location = new System.Drawing.Point(100, 34);
            this.picValidate.Name = "picValidate";
            this.picValidate.Size = new System.Drawing.Size(60, 20);
            this.picValidate.TabIndex = 3;
            this.picValidate.TabStop = false;
            this.picValidate.Click += new System.EventHandler(this.picValidate_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 434);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(732, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(131, 17);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // wbMain
            // 
            this.wbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbMain.Location = new System.Drawing.Point(12, 62);
            this.wbMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbMain.Name = "wbMain";
            this.wbMain.Size = new System.Drawing.Size(708, 360);
            this.wbMain.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(260, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "查 询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnBuyTicket
            // 
            this.btnBuyTicket.Location = new System.Drawing.Point(350, 33);
            this.btnBuyTicket.Name = "btnBuyTicket";
            this.btnBuyTicket.Size = new System.Drawing.Size(75, 23);
            this.btnBuyTicket.TabIndex = 7;
            this.btnBuyTicket.Text = "购 买";
            this.btnBuyTicket.UseVisualStyleBackColor = true;
            this.btnBuyTicket.Click += new System.EventHandler(this.btnBuyTicket_Click);
            // 
            // FrmMain
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 456);
            this.Controls.Add(this.btnBuyTicket);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.wbMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.picValidate);
            this.Controls.Add(this.txtValidate);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "12306火车票购票系统";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picValidate)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 用户列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 快速购票方案ToolStripMenuItem;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtValidate;
        private System.Windows.Forms.PictureBox picValidate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private SafeWebBrowserExt.SafeWebBrowser wbMain;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnBuyTicket;

    }
}

