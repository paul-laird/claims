using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataSet ds;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
                ds= new DataSet();
                dt = new DataTable();
                ds.Tables.Add(dt);
                dt.Columns.Add("Employee ID");
                dt.Columns.Add("Name");
                dt.Columns.Add("Hours");
                dt.Columns.Add("Rate");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                DataRow dr=dt.NewRow();
                dr["Employee ID"] = textBoxID.Text;
                dr["Name"] = textBoxName.Text;
                dr["Hours"] = int.Parse(textBoxHours.Text);
                dr["Rate"] = float.Parse(textBoxRate.Text);
                dt.Rows.Add(dr);
                dt.AcceptChanges();
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        string filePath = saveFileDialog.FileName;
                        ds.WriteXml(filePath);
                    }
                }

            }
            else
            {
                MessageBox.Show("Invalid input");
            }
        }
        private bool validate()
        {
            Regex eid = new Regex(@"[A-Z]\d{6}");
            Regex n = new Regex("[A-Z][a-z]+ [A-Z][a-z]+");
            int maxHours = 48;
            double minRate = 9.55;

            Match eidMatch = eid.Match(textBoxID.Text);
            Match nMatch = n.Match(textBoxName.Text);
            int hrs = 0;
            bool valid=int.TryParse(textBoxHours.Text, out hrs);
            float rate = 0;
            valid &= float.TryParse(textBoxRate.Text, out rate);
            valid &= hrs <= 48;
            valid &= hrs > 0;
            valid &= eidMatch.Success;
            valid &= nMatch.Success;
            valid &= rate >= 9.55;
            return valid;
        }
    }
}
