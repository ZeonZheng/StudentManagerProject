﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //创建登录窗体
            FrmUserLogin objFrmLogin = new FrmUserLogin();
            DialogResult result = objFrmLogin.ShowDialog();

            //判断登录是否成功
            if (result == DialogResult.OK)
                Application.Run(new FrmMain());
            else
                Application.Exit();//退出整个应用程序
        }
        //定义一个全局变量保存用户对象
        public static Models.SysAdmin objCurrentAdmin = null;
    }
}
