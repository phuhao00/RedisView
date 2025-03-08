using System.Text.RegularExpressions;

namespace RedisTools
{
    public partial class Form1 : Form
    {
        private readonly RedisManager _redisManager;

        public Form1()
        {
            InitializeComponent();
            _redisManager = new RedisManager();
            
            // 设置窗体最小尺寸
            this.MinimumSize = new Size(800, 600);
            
            // 设置窗体初始大小为屏幕大小的80%
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            this.Size = new Size(
                (int)(workingArea.Width * 0.8),
                (int)(workingArea.Height * 0.8)
            );
            
            // 设置窗体启动位置为屏幕中心
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // 允许调整窗体大小
            this.FormBorderStyle = FormBorderStyle.Sizable;
            
            // 添加窗体大小改变事件处理
            this.Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // 调整控件大小和位置
            AdjustControlsLayout();
        }

        private void AdjustControlsLayout()
        {
            // 设置连接组的大小
            groupBox1.Width = this.ClientSize.Width - 24;
            
            // 设置键列表的大小
            lstKeys.Width = this.ClientSize.Width / 3;
            lstKeys.Height = this.ClientSize.Height - lstKeys.Top - 100;
            
            // 设置操作组的位置和大小
            groupBox2.Left = lstKeys.Right + 10;
            groupBox2.Width = this.ClientSize.Width - groupBox2.Left - 12;
            
            // 设置结果文本框的位置和大小
            txtResult.Top = lstKeys.Bottom + 10;
            txtResult.Width = this.ClientSize.Width - 24;
            txtResult.Height = this.ClientSize.Height - txtResult.Top - 12;
            
            // 设置标签位置
            label7.Top = txtResult.Top - 20;
        }
    }
}