using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DAL;
namespace StudentManager
{
    public partial class FrmScoreManage : Form
    {
        private ScoreListService objScoreService = new ScoreListService();
        public FrmScoreManage()
        {
            InitializeComponent();

            //�Ͽ��¼�
            this.cboClass.SelectedIndexChanged -= new EventHandler(this.cboClass_SelectedIndexChanged);
            //��ʼ���༶������
            this.cboClass.DataSource = new StudentClassService().GetAllClasses();
            this.cboClass.DisplayMember = "ClassName";//�������������ʾ�ı�
            this.cboClass.ValueMember = "ClassId";//������������ʾ�ı���Ӧ��value 
            this.cboClass.SelectedIndex = -1;
            //�ҽ��¼�
            this.cboClass.SelectedIndexChanged +=
                new EventHandler(this.cboClass_SelectedIndexChanged);

        }
        //���ݰ༶��ѯ      
        private void cboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboClass.SelectedIndex == -1)
            {
                MessageBox.Show("������ѡ��Ҫ��ѯ�İ༶", "��ѯ��ʾ");
                return;
            }
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objScoreService.GetScoreList(this.cboClass.Text.Trim());
            //ͬ����ʾ�༶������Ϣ
            this.gbStat.Text = "[" + this.cboClass.Text.Trim() + "]���Գɼ�ͳ��";
            Dictionary<string, string> dic =
                objScoreService.GetScoreInfoByClassId(this.cboClass.SelectedValue.ToString());
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCount.Text = dic["absentCount"];
            //��ʾȱ����Ա����
            List<string> list =
                objScoreService.GetAbsentListByClassId(this.cboClass.SelectedValue.ToString());
            this.lblList.Items.Clear();
            if (list.Count == 0) this.lblList.Items.Add("û��ȱ��");
            else lblList.Items.AddRange(list.ToArray());
        }
        //�ر�
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //ͳ��ȫУ���Գɼ�
        private void btnStat_Click(object sender, EventArgs e)
        {
            this.gbStat.Text = "ȫУ���Գɼ�ͳ��";
            //��ѯ�ɼ��б�
            this.dgvScoreList.AutoGenerateColumns = false;
            this.dgvScoreList.DataSource = objScoreService.GetScoreList("");
            //��ѯ����ʾ�ɼ�ͳ��
            Dictionary<string, string> dic = objScoreService.GetScoreInfo();
            this.lblAttendCount.Text = dic["stuCount"];
            this.lblDBAvg.Text = dic["avgDB"];
            this.lblCSharpAvg.Text = dic["avgCSharp"];
            this.lblCount.Text = dic["absentCount"];
            //��ʾȱ������Ա����
            List<string> list = objScoreService.GetAbsentList();
            this.lblList.Items.Clear();
            this.lblList.Items.AddRange(list.ToArray());
        }
    }
}