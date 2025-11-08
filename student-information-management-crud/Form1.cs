using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StudentInformationManagementCRUD
{
    public partial class Form1 : Form
    {
            readonly string exePath = AppDomain.CurrentDomain.BaseDirectory;
            readonly string dataPath;
            readonly string supportPath;
            readonly string retrievePath;

            int t1, t2, t3, t4, t5;

        public Form1()
        {
            InitializeComponent();
            dataPath = Path.Combine(exePath, "Data.txt");
            supportPath = Path.Combine(exePath, "DataSupport.txt");
            retrievePath = Path.Combine(exePath, "Retrieve.txt");
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string line = sr.ReadLine();
                string[] lineContent;
                if (txtID.MaskCompleted == true)
                {
                    while (line != null)
                    {
                        lineContent = line.Split("|");
                        if (lineContent[0] == txtID.Text.Trim())
                        {
                            txtFN.Text = lineContent[1];
                            txtLN.Text = lineContent[2];
                            txtLec.Text = lineContent[3];
                            txtLab.Text = lineContent[4];
                            txtExam.Text = lineContent[5];
                            txtGrade.Text = lineContent[6];
                            txtID.Enabled = false;
                            txtFN.Enabled = true;
                            txtLN.Enabled = true;
                            txtLec.Enabled = true;
                            txtLab.Enabled = true;
                            txtExam.Enabled = true;
                            btnAdd.Enabled = false;
                            btnEdit.Enabled = true;
                            btnDelete.Enabled = true;
                            break;
                        }
                        else
                        {
                            txtFN.Enabled = true;
                            txtLN.Enabled = true;
                            txtLec.Enabled = true;
                            txtLab.Enabled = true;
                            txtExam.Enabled = true;
                            btnAdd.Enabled = true;
                            txtFN.Focus();
                        }
                        line = sr.ReadLine();
                    }
                }
                sr.Close();
                fs.Close();
            }
            catch (IOException io)
            {
                MessageBox.Show(io.Message);
            }
        }

        public void LoadForm()
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void btnTry_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string fileContent = "";
            try
            {
                FileStream fs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string line = sr.ReadLine();
                string[] lineContent;
                while (line != null)
                {
                    lineContent = line.Split("|");
                    fileContent += line + Environment.NewLine;
                    line = sr.ReadLine();
                }
                MessageBox.Show(fileContent, "Student Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sr.Close();
                fs.Close();
            }
            catch (IOException IO)
            {
                MessageBox.Show(IO.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            filter(txtID.Text, txtFN.Text, txtLN.Text, txtLec.Text, txtLab.Text, txtExam.Text);
            if ((txtID.MaskCompleted == true) && (txtFN.Text != "") && (txtLN.Text != "") && (txtLec.Text != "") && (txtLab.Text != "") && (txtExam.Text != "")
                && (!(int.TryParse(txtFN.Text, out t1))) && (!(int.TryParse(txtLN.Text, out t2))) && (int.TryParse(txtLec.Text, out t3)) && (int.TryParse(txtLab.Text, out t4)) && (int.TryParse(txtExam.Text, out t5))
                && ((Convert.ToInt32(txtLec.Text) >= 0) && ((Convert.ToInt32(txtLec.Text)) <= 100)) && ((Convert.ToInt32(txtLab.Text) >= 0) && ((Convert.ToInt32(txtLab.Text)) <= 100)) && ((Convert.ToInt32(txtExam.Text) >= 0) && ((Convert.ToInt32(txtExam.Text)) <= 100)))
            {
                try
                {
                    
                    File.Copy(dataPath, retrievePath, true);;
                    string line = null;
                    string[] lineContent;
                    string idEdit = txtID.Text.Trim();

                    StreamReader sr = new StreamReader(dataPath);
                    StreamWriter sw = new StreamWriter(supportPath);
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineContent = line.Split("|");
                        if (lineContent[0] == idEdit)
                        {
                            sw.WriteLine(txtID.Text.Trim() + "|" + txtFN.Text.Trim() + "|" + txtLN.Text.Trim() + "|" + txtLec.Text.Trim() + "|" + txtLab.Text.Trim() + "|" + txtExam.Text.Trim() + "|" + CalculateGrade(txtLec.Text, txtLab.Text, txtExam.Text));
                            continue;
                        }
                        sw.WriteLine(line);
                    }
                    sr.Close();
                    sw.Close();
                    File.Copy(supportPath, dataPath, true);
                    MessageBox.Show("Data edited successfully", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadForm();
                }
                catch (IOException io)
                {
                    MessageBox.Show(io.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            filter(txtID.Text, txtFN.Text, txtLN.Text, txtLec.Text, txtLab.Text, txtExam.Text);
            if ((txtID.MaskCompleted == true) && (txtFN.Text != "") && (txtLN.Text != "") && (txtLec.Text != "") && (txtLab.Text != "") && (txtExam.Text != "")
                && (!(int.TryParse(txtFN.Text, out t1))) && (!(int.TryParse(txtLN.Text, out t2))) && (int.TryParse(txtLec.Text, out t3)) && (int.TryParse(txtLab.Text, out t4)) && (int.TryParse(txtExam.Text, out t5))
                && ((Convert.ToInt32(txtLec.Text) >= 0) && ((Convert.ToInt32(txtLec.Text)) <= 100)) && ((Convert.ToInt32(txtLab.Text) >= 0) && ((Convert.ToInt32(txtLab.Text)) <= 100)) && ((Convert.ToInt32(txtExam.Text) >= 0) && ((Convert.ToInt32(txtExam.Text)) <= 100)))
            {
                try
                {
                    File.Copy(dataPath, retrievePath, true);;
                    string line = null;
                    string[] lineContent;
                    string idDelete = txtID.Text.Trim();

                    StreamReader sr = new StreamReader(dataPath);
                    StreamWriter sw = new StreamWriter(supportPath);
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineContent = line.Split("|");
                        if (lineContent[0] == idDelete)
                            continue;
                        sw.WriteLine(line);
                    }
                    sr.Close();
                    sw.Close();
                    File.Copy(supportPath, dataPath, true);
                    MessageBox.Show("Data deleted successfully", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadForm();
                }
                catch (IOException io)
                {
                    MessageBox.Show(io.Message);
                }
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(retrievePath, dataPath, true);
                MessageBox.Show("Data retrieved successfully", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException io)
            {
                MessageBox.Show(io.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtID.Focus();
        }

        public static double CalculateGrade(string x, string y, string z)
        {
            double grade = (Convert.ToDouble(x) * 0.3) + (Convert.ToDouble(y) * 0.3) + (Convert.ToDouble(z) * 0.4);
            return grade;
        }

        public void filter(string a, string b, string c, string d, string e, string f)
        {
            if ((txtID.MaskCompleted == false) || (txtFN.Text == "") || (txtLN.Text == "") || (txtLec.Text == "") || (txtLab.Text == "") || (txtExam.Text == ""))
            {
                if (txtID.MaskCompleted == false)
                {
                    MessageBox.Show("This field is required.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }
                if (b == "")
                {
                    MessageBox.Show("This field is required.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFN.Focus();
                    return;
                }
                if (c == "")
                {
                    MessageBox.Show("This field is required.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLN.Focus();
                    return;
                }
                if (d == "")
                {
                    MessageBox.Show("This field is required.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLec.Focus();
                    return;
                }
                if (e == "")
                {
                    MessageBox.Show("This field is required.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLab.Focus();
                    return;
                }
                if (f == "")
                {
                    MessageBox.Show("This field is required.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtExam.Focus();
                    return;
                }
            }
            else
            {
                if ((int.TryParse(txtFN.Text, out t1)) || (int.TryParse(txtLN.Text, out t2)))
                {
                    if (int.TryParse(txtFN.Text, out t1))
                    {
                        MessageBox.Show("Invalid data was input", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtFN.SelectionStart = 0;
                        txtFN.SelectionLength = txtFN.TextLength;
                        txtFN.Focus();
                        return;
                    }
                    if (int.TryParse(txtLN.Text, out t2))
                    {
                        MessageBox.Show("Invalid data was input", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtLN.SelectionStart = 0;
                        txtLN.SelectionLength = txtLN.TextLength;
                        txtLN.Focus();
                        return;
                    }
                }
                else
                {
                    if (!(int.TryParse(txtLec.Text, out t3)) || !(int.TryParse(txtLab.Text, out t4)) || !(int.TryParse(txtExam.Text, out t5)))
                    {
                        if (!(int.TryParse(txtLec.Text, out t3)))
                        {
                            MessageBox.Show("Invalid data was input", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtLec.SelectionStart = 0;
                            txtLec.SelectionLength = txtLec.TextLength;
                            txtLec.Focus();
                            return;
                        }
                        if (!(int.TryParse(txtLab.Text, out t4)))
                        {
                            MessageBox.Show("Invalid data was input", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtLab.SelectionStart = 0;
                            txtLab.SelectionLength = txtLab.TextLength;
                            txtLab.Focus();
                            return;
                        }
                        if (!(int.TryParse(txtExam.Text, out t5)))
                        {
                            MessageBox.Show("Invalid data was input", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtExam.SelectionStart = 0;
                            txtExam.SelectionLength = txtExam.TextLength;
                            txtExam.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (((Convert.ToInt32(txtLec.Text) < 0) || ((Convert.ToInt32(txtLec.Text)) > 100)) 
                            || ((Convert.ToInt32(txtLab.Text) < 0) || ((Convert.ToInt32(txtLab.Text)) > 100))
                            || ((Convert.ToInt32(txtExam.Text) < 0) || ((Convert.ToInt32(txtExam.Text)) > 100)))
                        {
                            if ((Convert.ToInt32(txtLec.Text) < 0) || ((Convert.ToInt32(txtLec.Text)) > 100))
                            {
                                MessageBox.Show("Invalid grade was input", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtLec.SelectionStart = 0;
                                txtLec.SelectionLength = txtLec.TextLength;
                                txtLec.Focus();
                                return;
                            }
                            if ((Convert.ToInt32(txtLab.Text) < 0) || ((Convert.ToInt32(txtLab.Text)) > 100))
                            {
                                MessageBox.Show("Invalid grade was input", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtLab.SelectionStart = 0;
                                txtLab.SelectionLength = txtLab.TextLength;
                                txtLab.Focus();
                                return;
                            }
                            if ((Convert.ToInt32(txtExam.Text) < 0) || ((Convert.ToInt32(txtExam.Text)) > 100))
                            {
                                MessageBox.Show("Invalid grade was input", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtExam.SelectionStart = 0;
                                txtExam.SelectionLength = txtExam.TextLength;
                                txtExam.Focus();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            filter(txtID.Text, txtFN.Text, txtLN.Text, txtLec.Text, txtLab.Text, txtExam.Text);
            if ((txtID.MaskCompleted == true) && (txtFN.Text != "") && (txtLN.Text != "") && (txtLec.Text != "") && (txtLab.Text != "") && (txtExam.Text != "") 
                && (!(int.TryParse(txtFN.Text, out t1))) && (!(int.TryParse(txtLN.Text, out t2))) && (int.TryParse(txtLec.Text, out t3)) && (int.TryParse(txtLab.Text, out t4)) && (int.TryParse(txtExam.Text, out t5))
                && ((Convert.ToInt32(txtLec.Text) >= 0) && ((Convert.ToInt32(txtLec.Text)) <= 100)) && ((Convert.ToInt32(txtLab.Text) >= 0) && ((Convert.ToInt32(txtLab.Text)) <= 100)) && ((Convert.ToInt32(txtExam.Text) >= 0) && ((Convert.ToInt32(txtExam.Text)) <= 100)))
            {
                try
                {
                    File.Copy(dataPath, retrievePath, true);;
                    FileStream fs = new FileStream(dataPath, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    double grade = CalculateGrade(txtLec.Text, txtLab.Text, txtExam.Text);
                    sw.WriteLine(txtID.Text.Trim() + "|" + txtFN.Text.Trim() + "|" + txtLN.Text.Trim() + "|" + txtLec.Text.Trim() + "|" + txtLab.Text.Trim() + "|" + txtExam.Text.Trim() + "|" + grade);
                    MessageBox.Show("Data saved successfully", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    sw.Flush();
                    sw.Close();
                    LoadForm();
                }
                catch (IOException io)
                {
                    MessageBox.Show(io.Message);
                }
            }
        }
    }
}