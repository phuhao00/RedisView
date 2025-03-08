namespace RedisTools
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtHost = new TextBox();
            txtPort = new TextBox();
            txtPassword = new TextBox();
            txtDatabase = new TextBox();
            btnConnect = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            txtValue = new TextBox();
            txtKey = new TextBox();
            label6 = new Label();
            label5 = new Label();
            btnSet = new Button();
            btnGet = new Button();
            btnDelete = new Button();
            txtResult = new TextBox();
            label7 = new Label();
            // 修改窗体初始设置
            this.Text = "Redis Tools";
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            // 连接设置组
            groupBox1.Text = "连接设置";
            groupBox1.Location = new Point(12, 12);
            groupBox1.Size = new Size(776, 150);
            // 连接组内控件布局
            label1.Text = "主机:";
            label1.Location = new Point(20, 30);
            label1.AutoSize = true;
            txtHost.Text = "127.0.0.1";
            txtHost.Location = new Point(100, 27);
            txtHost.Size = new Size(200, 23);
            label2.Text = "端口:";
            label2.Location = new Point(320, 30);
            label2.AutoSize = true;
            txtPort.Text = "6379";
            txtPort.Location = new Point(400, 27);
            txtPort.Size = new Size(100, 23);
            
            label3.Text = "密码:";
            label3.Location = new Point(20, 60);
            label3.AutoSize = true;
            txtPassword.Location = new Point(100, 57);
            txtPassword.Size = new Size(200, 23);
            label4.Text = "数据库:";
            label4.Location = new Point(320, 60);
            label4.AutoSize = true;
            // 修改数据库输入为组合框
            txtDatabase = new TextBox();
            cmbDatabase = new ComboBox();
            cmbDatabase.Location = new Point(400, 57);
            cmbDatabase.Size = new Size(100, 23);
            cmbDatabase.DropDownStyle = ComboBoxStyle.DropDown;
            for (int i = 0; i <= 15; i++)
            {
                cmbDatabase.Items.Add(i.ToString());
            }
            cmbDatabase.SelectedIndex = 0;
            cmbDatabase.TextChanged += (s, e) =>
            {
                if (!int.TryParse(cmbDatabase.Text, out int db) || db < 0 || db > 15)
                {
                    MessageBox.Show("数据库索引必须是0-15之间的数字");
                    cmbDatabase.Text = "0";
                }
            };
            // 修改控件数组，将 txtDatabase 替换为 cmbDatabase
            groupBox1.Controls.AddRange(new Control[] { 
                label1, txtHost, 
                label2, txtPort, 
                label3, txtPassword, 
                label4, cmbDatabase, 
                btnConnect 
            });
            btnConnect.Text = "连接";
            btnConnect.Location = new Point(100, 100);
            btnConnect.Size = new Size(100, 30);
            // 键列表
            // 初始化 lstKeys
            lstKeys = new ListBox();
            lstKeys.Location = new Point(12, 170);
            lstKeys.Size = new Size(250, 300);
            lstKeys.SelectionMode = SelectionMode.One;
            lstKeys.SelectedIndexChanged += new EventHandler(LstKeys_SelectedIndexChanged);
            // 操作组
            groupBox2.Text = "操作";
            groupBox2.Location = new Point(270, 170);
            groupBox2.Size = new Size(518, 300);
            // 操作组内控件布局
            label5.Text = "键:";
            label5.Location = new Point(20, 30);
            label5.AutoSize = true;

            txtKey.Location = new Point(100, 27);
            txtKey.Size = new Size(380, 23);

            label6.Text = "值:";
            label6.Location = new Point(20, 60);
            label6.AutoSize = true;

            txtValue.Location = new Point(100, 57);
            txtValue.Size = new Size(380, 23);

            btnSet.Text = "设置";
            btnSet.Location = new Point(100, 100);
            btnSet.Size = new Size(80, 30);
            btnSet.Click += new EventHandler(BtnSet_Click);
            // 修改其他事件处理器的绑定方式
            btnConnect.Click += new EventHandler(BtnConnect_Click);
            btnSet.Click += new EventHandler(BtnSet_Click);
            btnGet.Click += new EventHandler(BtnGet_Click);
            btnDelete.Click += new EventHandler(BtnDelete_Click);
            // 结果区域
            label7.Text = "结果:";
            label7.Location = new Point(12, 480);
            label7.AutoSize = true;

            txtResult.Location = new Point(12, 500);
            txtResult.Size = new Size(776, 50);
            txtResult.Multiline = true;
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = ScrollBars.Vertical;
            // 添加控件到组
            groupBox1.Controls.AddRange(new Control[] { label1, txtHost, label2, txtPort, label3, txtPassword, label4, txtDatabase, btnConnect });
            groupBox2.Controls.AddRange(new Control[] { label5, txtKey, label6, txtValue, btnSet, btnGet, btnDelete });
            this.Controls.AddRange(new Control[] { groupBox1, groupBox2, label7, txtResult, lstKeys });
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtPort.Text, out int port))
                {
                    MessageBox.Show("端口必须是数字");
                    return;
                }

                if (!int.TryParse(cmbDatabase.Text, out int db) || db < 0 || db > 15)
                {
                    MessageBox.Show("数据库索引必须是0-15之间的数字");
                    return;
                }

                if (_redisManager.Connect(txtHost.Text, port, txtPassword.Text, db))
                {
                    txtResult.Text = "连接成功！";
                    RefreshKeyList();
                }
                else
                {
                    txtResult.Text = "连接失败！";
                }
            }
            catch (Exception ex)
            {
                txtResult.Text = $"错误：{ex.Message}";
            }
        }
        private void RefreshKeyList()
        {
            lstKeys.Items.Clear();
            var keys = _redisManager.GetAllKeys();
            foreach (var key in keys)
            {
                lstKeys.Items.Add(key);
            }
        }
        private void LstKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstKeys.SelectedItem != null)
            {
                txtKey.Text = lstKeys.SelectedItem.ToString();
                var value = _redisManager.Get(txtKey.Text);
                txtValue.Text = value ?? string.Empty;
            }
        }
        private void BtnSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (_redisManager.Set(txtKey.Text, txtValue.Text))
                {
                    txtResult.Text = "设置成功！";
                }
                else
                {
                    txtResult.Text = "设置失败！";
                }
            }
            catch (Exception ex)
            {
                txtResult.Text = $"错误：{ex.Message}";
            }
        }

        private void BtnGet_Click(object sender, EventArgs e)
        {
            try
            {
                var value = _redisManager.Get(txtKey.Text);
                txtResult.Text = value != null ? $"值：{value}" : "键不存在";
            }
            catch (Exception ex)
            {
                txtResult.Text = $"错误：{ex.Message}";
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_redisManager.Delete(txtKey.Text))
                {
                    txtResult.Text = "删除成功！";
                }
                else
                {
                    txtResult.Text = "删除失败！";
                }
            }
            catch (Exception ex)
            {
                txtResult.Text = $"错误：{ex.Message}";
            }
        }
        private ComboBox cmbDatabase;
        private TextBox txtHost;
        private TextBox txtPort;
        private TextBox txtPassword;
        private TextBox txtDatabase;
        private Button btnConnect;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox txtValue;
        private TextBox txtKey;
        private Label label6;
        private Label label5;
        private Button btnSet;
        private Button btnGet;
        private Button btnDelete;
        private TextBox txtResult;
        private Label label7;
        private ListBox lstKeys;  // 添加 ListBox 声明
    }
}