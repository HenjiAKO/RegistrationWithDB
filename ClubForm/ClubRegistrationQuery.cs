using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

public class ClubRegistrationQuery
{
    private SqlConnection sqlConnect;
    private SqlCommand sqlCommand;
    private SqlDataAdapter sqlAdapter;
    private SqlDataReader sqlReader;

    public DataTable dataTable;
    public BindingSource bindingSource;
    private string connectionString;

    public ClubRegistrationQuery(string connString)
    {
        connectionString = connString;
        sqlConnect = new SqlConnection(connectionString);
        dataTable = new DataTable();
        bindingSource = new BindingSource();
    }

    public bool DisplayList()
    {
        try
        {
            string ViewClubMembers = "SELECT StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers";
            sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);

            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading list: " + ex.Message);
            return false;
        }
    }

    public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
    {
        try
        {
            string insertQuery = "INSERT INTO ClubMembers (ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program) " +
                                 "VALUES (@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)";
            sqlCommand = new SqlCommand(insertQuery, sqlConnect);

            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar, 50).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar, 50).Value = Program;

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error registering student: " + ex.Message);
            return false;
        }
        finally
        {
            if (sqlConnect.State == ConnectionState.Open) sqlConnect.Close();
        }
    }

    public int GetNextID()
    {
        try
        {
            string q = "SELECT ISNULL(MAX(ID), 0) + 1 AS NextID FROM ClubMembers";
            sqlCommand = new SqlCommand(q, sqlConnect);
            sqlConnect.Open();
            object result = sqlCommand.ExecuteScalar();
            return Convert.ToInt32(result);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error getting next ID: " + ex.Message);
            return 1;
        }
        finally
        {
            if (sqlConnect.State == ConnectionState.Open) sqlConnect.Close();
        }
    }

    public DataRow GetMemberByStudentID(long StudentID)
    {
        try
        {
            string q = "SELECT ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers WHERE StudentId = @StudentID";
            sqlCommand = new SqlCommand(q, sqlConnect);
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
            sqlAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sqlAdapter.Fill(dt);
            if (dt.Rows.Count > 0) return dt.Rows[0];
            return null;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error retrieving member: " + ex.Message);
            return null;
        }
    }

    public bool UpdateMember(long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
    {
        try
        {
            string updateQuery = "UPDATE ClubMembers SET FirstName=@FirstName, MiddleName=@MiddleName, LastName=@LastName, Age=@Age, Gender=@Gender, Program=@Program WHERE StudentId=@StudentID";
            sqlCommand = new SqlCommand(updateQuery, sqlConnect);

            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
            sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50).Value = MiddleName;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = LastName;
            sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar, 50).Value = Gender;
            sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar, 50).Value = Program;
            sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;

            sqlConnect.Open();
            int rows = sqlCommand.ExecuteNonQuery();
            return rows > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating member: " + ex.Message);
            return false;
        }
        finally
        {
            if (sqlConnect.State == ConnectionState.Open) sqlConnect.Close();
        }
    }
}