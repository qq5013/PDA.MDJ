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
        SelectionTask task = null;
        HttpDataService httpDataService = new HttpDataService();

        string billType = "";

        public int Index;

        public DetailForm(SelectionTask sTask, string billType)
        {
            InitializeComponent();
            this.task = sTask;
            this.billType = billType;
        }

        private void BillDetailForm_Load(object sender, EventArgs e)
        {
            if (SystemCache.ConnetionType == "NetWork")
            {
                this.lbID.Text = task.TaskID.ToString();
                this.lbOrderID.Text=task.OrderID;
                this.lbCellCode.Text = task.CellName;
                this.lbProductName.Text = task.ProductName;
                this.lbPieceQuantity.Text = task.PieceQuantity.ToString();
                this.lbBarQuantity.Text = task.BarQuantity.ToString();
                this.lbStatus.Text = task.Status;
                this.lbOrderType.Text = task.OrderType;

                WaitCursor.Restore();
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
                    SelectionTask st = new SelectionTask();
                    st.TaskID = Convert.ToInt32(lbID.Text);
                    result = httpDataService.FinishTask("OutFinishTask/?taskID=" + st.TaskID);
                }
                MessageBox.Show(result);
                TaskForm baseTaskForm = new TaskForm(this.billType);
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
                MessageBox.Show(result + " " + ex.Message);
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
            TaskForm task = new TaskForm(this.billType);
            task.Show();
            
        }
    }
}