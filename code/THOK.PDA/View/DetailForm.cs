using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Net;
using THOK.PDA.Service;
using THOK.PDA.Util;
using THOK.PDA.Model;

namespace THOK.PDA.View
{
    public partial class DetailForm : Form
    {
        RestTask task = null;
        HttpDataService httpDataService = new HttpDataService();
        string positionType = "";
        public int Index;

        public DetailForm(RestTask sTask, string positionType)
        {
            InitializeComponent();
            this.task = sTask;
            this.positionType = positionType;
        }

        private void BillDetailForm_Load(object sender, EventArgs e)
        {
            if (SystemCache.ConnetionType == "NetWork")
            {
                this.lbID.Text = task.TaskID.ToString();
                this.lbOrderID.Text = task.OrderID;
                this.lbCellCode.Text = task.CellName;
                this.lbProductName.Text = task.ProductName;
                this.lbPieceQuantity.Text = task.PieceQuantity.ToString();
                this.lbBarQuantity.Text = task.BarQuantity.ToString();
                this.lbStatus.Text = task.Status;
                this.lbOrderType.Text = task.OrderType;

                WaitCursor.Restore();
            }
            else
            {
                WaitCursor.Restore();
                MessageBox.Show("请检查配置！连接类型是否为：NetWork");
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            WaitCursor.Set();
            string result = string.Empty;
            try
            {
                if (SystemCache.ConnetionType == "NetWork")
                {
                    RestTask st = new RestTask();
                    st.TaskID = Convert.ToInt32(lbID.Text);
                    result = httpDataService.FinishTask("FinishOutTask/?taskID=" + st.TaskID);
                }
                else
                {
                    WaitCursor.Restore();
                    MessageBox.Show("请检查配置！连接类型是否为：NetWork");
                    return;
                }
                MessageBox.Show(result);
                TaskForm baseTaskForm = new TaskForm(this.positionType);
                if (this.Index > 0)
                {
                    baseTaskForm.index = this.Index;
                }
                baseTaskForm.Show();
                this.Close();
                WaitCursor.Restore();
            }
            catch (Exception ex)
            {
                WaitCursor.Restore();
                MessageBox.Show("报错："+result + "，系统错误：" + ex.Message);
                this.Close();
                SystemCache.MainFrom.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbPieceQuantity.Text = Convert.ToString(Convert.ToInt32(lbPieceQuantity.Text) + 1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            lbPieceQuantity.Text = Convert.ToString(Convert.ToInt32(lbPieceQuantity.Text) - 1);
            if (Convert.ToInt32(lbPieceQuantity.Text) < 0)
            {
                lbPieceQuantity.Text = "0";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            lbBarQuantity.Text = Convert.ToString(Convert.ToInt32(lbBarQuantity.Text) + 1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            lbBarQuantity.Text = Convert.ToString(Convert.ToInt32(lbBarQuantity.Text) - 1);
            if (Convert.ToInt32(lbBarQuantity.Text) < 0)
            {
                lbBarQuantity.Text = "0";
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            WaitCursor.Set();
            TaskForm task = new TaskForm(this.positionType);
            task.Show();
            this.Close();
        }
    }
}