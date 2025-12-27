using System;
using System.Windows.Forms;

namespace ClubForm
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;

        private int ID, Age;
        private long StudentId;

        private const string CONNECTION_STRING = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\School_Files\MACASILHIG-CS301A\Comprog\Macasilhig_KhyleMyrvin_RegistrationWithDB\ClubForm\ClubDB.mdf;Integrated Security=True;Connect Timeout=30";
        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery(CONNECTION_STRING);

            cmbGender.Items.Clear();
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });

            cmbProgram.Items.Clear();
            cmbProgram.Items.AddRange(new object[] { "BS in Computer Science", "BS In Information Technology", "BS in Hospitality Management", "BS in Accountancy" });

            RefreshListOfClubMembers();
        }

        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridViewMembers.DataSource = clubRegistrationQuery.bindingSource;
        }

        private int RegistrationID()
        {
            return clubRegistrationQuery.GetNextID();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(txtStudentId.Text, out StudentId))
            {
                MessageBox.Show("Enter a Valid Student ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtAge.Text.Trim(), out Age))
            {
                MessageBox.Show("Enter a valid age.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbGender.SelectedItem == null || cmbProgram.SelectedItem == null)
            {
                MessageBox.Show("Please select Gender and Program.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string FirstName = txtFirstName.Text.Trim();
            string MiddleName = txtMiddleName.Text.Trim();
            string LastName = txtLastName.Text.Trim();
            string Gender = cmbGender.SelectedItem.ToString();
            string Program = cmbProgram.SelectedItem.ToString();

            ID = RegistrationID();

            bool success = clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);
            if (success)
            {
                MessageBox.Show("Student registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshListOfClubMembers();
                ClearInputs();
            }
            else
            {
            }
        }

        private void ClearInputs()
        {
            txtStudentId.Text = "";
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtAge.Text = "";
            cmbGender.SelectedIndex = -1;
            cmbProgram.SelectedIndex = -1;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdateMember updateForm = new FrmUpdateMember(clubRegistrationQuery);

            updateForm.MemberUpdated += UpdateForm_MemberUpdated;

            updateForm.ShowDialog(this);
        }

        private void UpdateForm_MemberUpdated(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }
    }
}