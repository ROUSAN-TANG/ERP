﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ERP.ComClass;
using ERP.DataClass;

namespace ERP.RP.FORM
{
    public partial class FormSelCollectReport : Form
    {
        DataBase db = new DataBase();
        CommonUse commUse = new CommonUse();

        public FormSelCollectReport()
        {
            InitializeComponent();
        }

        private void FormSelCollectReport_Load(object sender, EventArgs e)
        {
            //权限
            commUse.CortrolButtonEnabled(btnQuery, this);
            //ComboBox绑定到数据源
            commUse.BindComboBox(cbxCustomerCode, "CustomerCode", "CustomerName", "select CustomerCode,CustomerName from BSCustomer", "BSCustomer");
            commUse.BindComboBox(cbxInvenCode, "InvenCode", "InvenName", "select InvenCode,InvenName from BSInven", "BSInven");
            //
            cbxCustomerCode.SelectedIndex = -1;
            cbxInvenCode.SelectedIndex = -1;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string strCondition = null;

            if (dtpStartDate.Value.Date > dtpEndDate.Value.Date)
            {
                MessageBox.Show("开始日期不许大于结束日期", "软件提示");
                return;
            }

            strCondition = "Select * From SEOutStore Where IsFlag = '1' ";

            //起始日期
            if (dtpStartDate.ShowCheckBox == true)
            {
                if (dtpStartDate.Checked == true)
                {
                    strCondition += " and SEOutDate >= '" + dtpStartDate.Value.ToString("yyyy-MM-dd") + "' ";
                }
            }

            //截止日期
            if (dtpEndDate.ShowCheckBox == true)
            {
                if (dtpEndDate.Checked == true)
                {
                    strCondition += " and SEOutDate <= '" + dtpEndDate.Value.ToString("yyyy-MM-dd") + "' ";
                }
            }

            //客户
            if (cbxCustomerCode.SelectedValue != null)
            {
                strCondition += " and CustomerCode = '" + cbxCustomerCode.SelectedValue.ToString() + "' ";
            }
            
            //产品
            if (cbxInvenCode.SelectedValue != null)
            {
                strCondition += " and InvenCode = '" + cbxInvenCode.SelectedValue.ToString() + "' ";
            }

            crySelCollectReport.ReportSource = commUse.CrystalReports("CrySelCollectReport.rpt", strCondition, "SEOutStore");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
