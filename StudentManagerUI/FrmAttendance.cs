using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Models;
using DAL;

namespace StudentManager
{
    public partial class FrmAttendance : Form
    {     
        public FrmAttendance()
        {
            InitializeComponent();

            timer1_Tick(null, null);//避免时间显示的延迟
        }

        //显示当前时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblYear.Text = DateTime.Now.Year.ToString();
            this.lblMonth.Text = DateTime.Now.Month.ToString();
            this.lblDay.Text = DateTime.Now.Day.ToString();
            this.lblTime.Text = DateTime.Now.ToLongTimeString();
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Tuesday:
                    this.lblWeek.Text = "二";
                    break;
                case DayOfWeek.Wednesday:
                    this.lblWeek.Text = "三";
                    break;
                case DayOfWeek.Thursday:
                    this.lblWeek.Text = "四";
                    break;
                case DayOfWeek.Monday:
                    this.lblWeek.Text = "一";
                    break;
                case DayOfWeek.Saturday:
                    this.lblWeek.Text = "六";
                    break;
                case DayOfWeek.Friday:
                    this.lblWeek.Text = "五";
                    break;
                case DayOfWeek.Sunday:
                    this.lblWeek.Text = "日";
                    break;
            }
        }
        //学员打卡
        private AttendanceService objAttendanceService = new AttendanceService();
        private void txtStuCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txtStuCardNo.Text.Trim().Length != 0 && e.KeyValue == 13)
            {
                //显示学员信息
                StudentExt objStu = new StudentService().GetStudentByCardNo(this.txtStuCardNo.Text.Trim());
                if (objStu == null)
                {
                    MessageBox.Show("卡号不正确！", "信息提示");
                    this.txtStuCardNo.SelectAll();
                    return;
                }
                this.lblStuName.Text = objStu.StudentName;
                this.lblStuClass.Text = objStu.ClassName;
                this.lblStuId.Text = objStu.StudentId.ToString();
                //添加打卡信息
                string result = objAttendanceService.AddRecord(this.txtStuCardNo.Text.Trim());
                if (result != "success")
                {
                    this.lblInfo.Text = "打卡失败！";
                    MessageBox.Show(result, "错误提示");
                }
                else
                    this.lblInfo.Text = "打卡成功！";
                this.txtStuCardNo.Text = ""; //等待下一个打卡
                this.txtStuCardNo.Focus();
            }
        }
        //结束打卡
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAttendance_FormClosed(object sender, FormClosedEventArgs e)
        {
            FrmMain.objFrmAttendance = null;
        }
    }
}
