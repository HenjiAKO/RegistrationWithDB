using System;
using System.Data;
using System.Windows.Forms;

namespace ClubForm
{
    public partial class FrmUpdateMember : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;

        public event EventHandler MemberUpdated;

        public FrmUpdateMember(ClubRegistrationQuery query)
        {
            InitializeComponent();
            this.clubRegistrationQuery = query;
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cmbProgram.Items.AddRange(new object[] { "BS in Computer Science", "BS In Information Technology", "BS in Hospitality Management", "BS in Accountancy" });

            clubRegistrationQuery.DisplayList();

            cmbStudentID_Select.DataSource = clubRegistrationQuery.dataTable;
            cmbStudentID_Select.DisplayMember = "StudentId";
            cmbStudentID_Select.ValueMember = "StudentId";
            cmbStudentID_Select.SelectedIndex = -1; // Deselect initial item
        }

        private void cmbStudentID_Select_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbStudentID_Select.SelectedValue == null) return;

            if (!long.TryParse(cmbStudentID_Select.SelectedValue.ToString(), out long sid)) return;

            DataRow row = clubRegistrationQuery.GetMemberByStudentID(sid);

            if (row != null)
            {
                txtFirstName.Text = row["FirstName"].ToString();
                txtMiddleName.Text = row["MiddleName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtAge.Text = row["Age"].ToString();

                cmbGender.SelectedItem = row["Gender"].ToString();
                cmbProgram.SelectedItem = row["Program"].ToString();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbStudentID_Select.SelectedValue == null || !long.TryParse(cmbStudentID_Select.SelectedValue.ToString(), out long sid))
            {
                MessageBox.Show("Please select a valid Student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAge.Text.Trim(), out int age))
            {
                MessageBox.Show("Enter a valid age.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string first = txtFirstName.Text.Trim();
            string middle = txtMiddleName.Text.Trim();
            string last = txtLastName.Text.Trim();
            string gender = (cmbGender.SelectedItem ?? "").ToString();
            string program = (cmbProgram.SelectedItem ?? "").ToString();

            bool ok = clubRegistrationQuery.UpdateMember(sid, first, middle, last, age, gender, program);

            if (ok)
            {
                MessageBox.Show("Member updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MemberUpdated?.Invoke(this, EventArgs.Empty);
                this.Close();
            }
        }
    }
}