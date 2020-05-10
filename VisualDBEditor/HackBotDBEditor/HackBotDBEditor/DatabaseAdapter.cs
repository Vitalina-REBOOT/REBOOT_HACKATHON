using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace HackBotDBEditor
{
    public class DatabaseAdapter
    {

        private static string localConnStr = "server=localhost;port=3307;user id=root;password=1234;charset=utf8;persistsecurityinfo=True;database=hted_bot;allowuservariables=True";

        private MySqlConnection conn = new MySqlConnection(localConnStr);
        
        public DataTable getGroups()
        {
            
            
            try
            {

                string query = $"CALL `hted_bot`.`getGroups`();";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getUsersTable()
        {
            
            
            try
            {

                string query = $"CALL `hted_bot`.`getUsers`();";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getDocs()
        {


            try
            {

                string query = $"CALL `hted_bot`.`getDocuments`();";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getEvents()
        {


            try
            {

                string query = $"CALL `hted_bot`.`getEvents`();";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getUserByLogin(string login)
        {
            

            try
            {

                string query = $"CALL `hted_bot`.`getUserByLogin`('{login}');";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getUserTaskById(int id)
        {


            try
            {

                string query = $"CALL `hted_bot`.`getUserTaskById`({id});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getDocGroups(int docId)
        {
            

            try
            {

                string query = $"CALL `hted_bot`.`getDocGroups`({docId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getEventGroups(int eventId)
        {
            

            try
            {

                string query = $"CALL `hted_bot`.`getEventGroups`({eventId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getGroupById(int groupId)
        {
            

            try
            {

                string query = $"CALL `hted_bot`.`getGroupById`({groupId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getGroupUsers(int groupId)
        {
            

            try
            {

                string query = $"CALL `hted_bot`.`getGroupUsers`({groupId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }

        public DataTable getUserTasks(string login)
        {
            

            try
            {

                string query = $"CALL `hted_bot`.`getUserTasks`('{login}');";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                conn.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных.\n" + ex.Message);
                conn.Close();
                return null;
            }
        }


        public bool createEventGroupConn(int eventId, int groupId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`create_event_group_connection`({eventId}, {groupId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool createDocGroupConn(int docId, int groupId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`create_doc_group_connection`({docId}, {groupId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool createUserGroupConn(string login, int groupId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`create_user_group_connection`('{login}', {groupId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool createGroup(string name)
        {
            try
            {

                string query = $"CALL `hted_bot`.`createGroup`('{name}');";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool createOrUpdateDoc(int docId, string docName, string docLink)
        {
            try
            {

                string query = $"CALL `hted_bot`.`createOrUpdateDoc`({docId}, '{docName}', '{docLink}');";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool createOrUpdateEvent(int eventId, string eventName, string eventLink, DateTime eventDate)
        {
            try
            {

                string query = $"CALL `hted_bot`.`createOrUpdateEvent`({eventId}, '{eventName}', '{eventLink}', {eventDate});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool createOrUpdateUserTask(int taskId, string userLogin, string taskText)
        {
            try
            {

                string query = $"CALL `hted_bot`.`createOrUpdateUserTask`('{taskId}', '{userLogin}', '{taskText}');";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool createUser(string login, string lastName, string firstName, string patronym)
        {
            try
            {

                string query = $"CALL `hted_bot`.`createUser`('{login}', '{lastName}', '{firstName}', '{patronym}');";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool setTaskAsComplete(int taskId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`setTaskStatusAsCompleted`({taskId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool deleteEvent(int eventId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`delete_event`({eventId}); ";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool deleteGroup(int groupId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`delete_group`({groupId}); ";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool deleteGroupDocConn(int connId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`delete_group_doc_conn`({connId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool deleteTask(int taskId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`delete_task`({taskId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool deleteUserGroupConn(int connId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`delete_usr_group_conn`({connId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

        public bool deleteEventGroupConn(int connId)
        {
            try
            {

                string query = $"CALL `hted_bot`.`delete_event_group_conn`({connId});";

                MySqlCommand command = new MySqlCommand(query, conn);
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении информации.\n" + ex.Message);
                conn.Close();
                return false;
            }
        }

    }
}
