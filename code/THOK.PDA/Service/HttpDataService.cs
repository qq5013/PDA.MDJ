using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using THOK.PDA.Util;
using THOK.PDA.Model;

namespace THOK.PDA.Service
{
    public class HttpDataService
    {
        HttpUtil util = new HttpUtil();

        public DataTable SearchOutAbnormalTask(string methodName)
        {
            string msg = util.GetDataFromServer(methodName);
            Result r = JsonConvert.DeserializeObject<Result>(msg);

            DataTable table = BuildTaskTable();
            if (r.IsSuccess)
            {
                for (int i = 0; i < r.SelectionTasks.Length; i++)
                {
                    DataRow row = table.NewRow();
                    row["TaskID"] = r.SelectionTasks[i].TaskID;
                    row["CellName"] = r.SelectionTasks[i].CellName;
                    row["ProductName"] = r.SelectionTasks[i].ProductName;
                    row["OrderID"] = r.SelectionTasks[i].OrderID;
                    row["OrderType"] = r.SelectionTasks[i].OrderType;
                    row["Status"] = r.SelectionTasks[i].Status;
                    row["Quantity"] = r.SelectionTasks[i].Quantity;
                    row["TaskQuantity"] = r.SelectionTasks[i].TaskQuantity;
                    row["PieceQuantity"] = r.SelectionTasks[i].PieceQuantity;
                    row["BarQuantity"] = r.SelectionTasks[i].BarQuantity;
                    table.Rows.Add(row);
                }
                return table;
            }
            else
            {
                return table;
            }
        }

        public string FinishTask(string methodName)
        {
            string error = "完成成功！";
            string msg = util.GetDataFromServer(methodName);
            Result r = JsonConvert.DeserializeObject<Result>(msg);
            if (!r.IsSuccess)
            {
                error = r.Message;
            }
            return error;
        }

        DataTable BuildTaskTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TaskID");
            table.Columns.Add("OrderID");
            table.Columns.Add("OrderType");
            table.Columns.Add("Status");
            table.Columns.Add("CellName");
            table.Columns.Add("ProductName");
            table.Columns.Add("Quantity");
            table.Columns.Add("TaskQuantity");
            table.Columns.Add("PieceQuantity");
            table.Columns.Add("BarQuantity");
            return table;
        }
    }
}
