using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackBotDBEditor
{
    public partial class mainForm : Form
    {
        DatabaseAdapter DBAdapter = new DatabaseAdapter();

        int groupTabChosenGroup = 0;
        string userTabChosenUser = "";
        int userTabChosenTask = 0;
        int docTabChosenDoc = 0;
        int docTabChosenGroup = 0;
        int eventTabChosenEvent = 0;
        int eventTabChosenGroup = 0;
        public mainForm()
        {
            InitializeComponent();
        }

         

        void Search(DataGridView targetDataGrid, DataTable targetTable, string searchingText, string scopeColumn)
        {
            targetTable.DefaultView.RowFilter = $"{scopeColumn} LIKE '%{searchingText}%'";

            targetDataGrid.DataSource = targetTable;
            for (int i = 0; i < targetDataGrid.ColumnCount - 1; i++)
            {
                targetDataGrid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            targetDataGrid.Columns[targetDataGrid.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        void FormDGV(DataGridView target)
        {
            for (int i = 0; i < target.ColumnCount - 1; i++)
            {
                target.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            target.Columns[target.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        void loadUserTab()
        {
            Search(UsersTabUsersDatagrid, DBAdapter.getUsersTable(), UserTabFindUserText.Text, "login");
        }

        void loadGroupsTab()
        {
            Search(GroupsTabGroupsDataGrid, DBAdapter.getGroups(), "", "name");
            Search(GroupTabAllUsersDataGrid, DBAdapter.getUsersTable(), GroupTabAllUsersText.Text, "login");
        }

        void loadDocumentsTab()
        {
            Search(docsDataGrid, DBAdapter.getDocs(), findDocsText.Text, "name");
        }

        void loadEventsTab()
        {
            Search(eventDataGrid, DBAdapter.getEvents(), findEventText.Text, "name");
        }

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            DBAdapter.createOrUpdateUserTask(0, userTabChosenUser, addTaskText.Text);
            userTaskDataGrid.DataSource = DBAdapter.getUserTasks(userTabChosenUser);
            FormDGV(userTaskDataGrid);
        }


        private void UserTabFindUserButton_Click(object sender, EventArgs e)
        {
            Search(UsersTabUsersDatagrid, DBAdapter.getUsersTable(), UserTabFindUserText.Text, "login");
        }

        private void MainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Name)
            {
                case "usersTab":
                    loadUserTab();
                    break;
                case "groupsPage":
                    loadGroupsTab();
                    break;
                case "documentsTab":
                    loadDocumentsTab();
                    break;
                case "eventTab":
                    loadEventsTab();
                    break;


                default:
                    break;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            loadUserTab();
        }

        private void findUserInGroup_Click(object sender, EventArgs e)
        {
            Search(GroupTabGroupUsersDataGrid, DBAdapter.getGroupUsers(groupTabChosenGroup), GroupsTabFindGroupUser.Text, "login");
        }

        private void GroupTabFindAllUsersButton_Click(object sender, EventArgs e)
        {
            Search(GroupTabAllUsersDataGrid, DBAdapter.getUsersTable(), GroupTabAllUsersText.Text, "login");
        }

        private void findDocsButton_Click(object sender, EventArgs e)
        {
            Search(docsDataGrid, DBAdapter.getDocs(), findDocsText.Text, "name");
        }

        private void dindEventButton_Click(object sender, EventArgs e)
        {
            Search(eventDataGrid, DBAdapter.getEvents(), findEventText.Text, "name");
        }

        private void UsersTabUsersDatagrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            userTabChosenUser = UsersTabUsersDatagrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            userTaskDataGrid.DataSource = DBAdapter.getUserTasks(userTabChosenUser);
            FormDGV(userTaskDataGrid);
        }

        private void userTaskDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            userTabChosenTask = Convert.ToInt32(userTaskDataGrid[0, e.RowIndex].Value);
            DataTable taskInfo = DBAdapter.getUserTaskById(userTabChosenTask);

            DataRow payload = taskInfo.Rows[0];

            editTaskText.Text = payload[0].ToString();
            chosenTaskLabel.Text = $"Выбрано задание №{userTabChosenTask}";
        }

        private void editTaskTextButton_Click(object sender, EventArgs e)
        {
            DBAdapter.createOrUpdateUserTask(userTabChosenTask, userTabChosenUser, editTaskText.Text);
            userTaskDataGrid.DataSource = DBAdapter.getUserTasks(userTabChosenUser);
            FormDGV(userTaskDataGrid);
        }

        private void completeTaskText_Click(object sender, EventArgs e)
        {
            DBAdapter.setTaskAsComplete(userTabChosenTask);
            userTaskDataGrid.DataSource = DBAdapter.getUserTasks(userTabChosenUser);
            FormDGV(userTaskDataGrid);

        }

        private void deleteTaskText_Click(object sender, EventArgs e)
        {
            DBAdapter.deleteTask(userTabChosenTask);
            userTaskDataGrid.DataSource = DBAdapter.getUserTasks(userTabChosenUser);
            editTaskText.Text = "";
            chosenTaskLabel.Text = "Выбрано задание №";
            userTabChosenTask = 0;
            FormDGV(userTaskDataGrid);

        }

        private void addGroupButton_Click(object sender, EventArgs e)
        {
            DBAdapter.createGroup(addGroupText.Text);
            loadGroupsTab();
        }

        private void deleteGroupButton_Click(object sender, EventArgs e)
        {
            DBAdapter.deleteGroup(Convert.ToInt32(GroupsTabGroupsDataGrid.Rows[GroupsTabGroupsDataGrid.SelectedCells[0].RowIndex].Cells[0].Value));
            loadGroupsTab();
        }

        private void GroupTabAllUsersDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(groupTabChosenGroup!=0)
            {
                DBAdapter.createUserGroupConn(GroupTabAllUsersDataGrid.Rows[e.RowIndex].Cells[0].Value.ToString(), groupTabChosenGroup);
            }
            
        }

        private void GroupsTabGroupsDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            groupTabChosenGroup = Convert.ToInt32(GroupsTabGroupsDataGrid.Rows[e.RowIndex].Cells[0].Value);
            chosenGroupLabel.Text = GroupsTabGroupsDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            GroupTabGroupUsersDataGrid.DataSource=DBAdapter.getGroupUsers(groupTabChosenGroup);
            FormDGV(GroupTabGroupUsersDataGrid);
        }

        private void GroupTabAllUsersDataGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void GroupTabGroupUsersDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GroupTabGroupUsersDataGrid.Rows.Count != 0)
            {
                DBAdapter.deleteUserGroupConn(Convert.ToInt32(GroupTabGroupUsersDataGrid.Rows[e.RowIndex].Cells[0].Value));
            }


        }

        private void docsDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            docTabChosenDoc = Convert.ToInt32(docsDataGrid[0, e.RowIndex].Value);
            chosenDocLabel.Text = $"Выбран документ №{docTabChosenDoc}";
            editDocNameText.Text= docsDataGrid[2, e.RowIndex].Value.ToString();
            editDocLinkText.Text= docsDataGrid[1, e.RowIndex].Value.ToString();
            docGroupsDataGrid.DataSource = DBAdapter.getDocGroups(docTabChosenDoc);
            FormDGV(docGroupsDataGrid);
        }

        private void editDocButton_Click(object sender, EventArgs e)
        {
            DBAdapter.createOrUpdateDoc(docTabChosenDoc,editDocNameText.Text, editDocLinkText.Text);
            loadDocumentsTab();
        }

        private void addDocButton_Click(object sender, EventArgs e)
        {
            DBAdapter.createOrUpdateDoc(0, addDocNameText.Text, addDocLinkText.Text);
            loadDocumentsTab();
        }

        private void deleteDocGroupButton_Click(object sender, EventArgs e)
        {
            if(docGroupsDataGrid.Rows.Count>0)
            {
                int groupConnId = Convert.ToInt32(docGroupsDataGrid[0, docGroupsDataGrid.SelectedCells[0].RowIndex].Value);
                DBAdapter.deleteGroupDocConn(groupConnId);
                docGroupsDataGrid.DataSource = DBAdapter.getDocGroups(docTabChosenDoc);
                FormDGV(docGroupsDataGrid);
            }
        }

        private void eventDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            eventTabChosenEvent = Convert.ToInt32(eventDataGrid[0, e.RowIndex].Value);
            chosenEventLabel.Text = $"Выбрано событие №{docTabChosenDoc}";
            editEventLinkText.Text = eventDataGrid[3, e.RowIndex].Value.ToString();
            editEventNameText.Text = eventDataGrid[1, e.RowIndex].Value.ToString();
            DateTime dt = (DateTime)eventDataGrid[2, e.RowIndex].Value;
            editEventTimestamp.Value = dt;
            //editEventTimestamp.Text = DateTime.ParseExact(eventDataGrid[2, e.RowIndex].Value,
            //    "dd.MM.yyyy hh:mm:ss", CultureInfo.InvariantCulture);

            eventGroupDataGrid.DataSource = DBAdapter.getEventGroups(eventTabChosenEvent);
            //eventGr.DataSource = DBAdapter.getDocGroups(docTabChosenDoc);
            FormDGV(eventGroupDataGrid);

        }

        private void editEventButton_Click(object sender, EventArgs e)
        {
            DBAdapter.createOrUpdateEvent(eventTabChosenEvent, editEventNameText.Text, editEventLinkText.Text, editEventTimestamp.Value);
            loadEventsTab();
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            DBAdapter.createOrUpdateEvent(0, addEventNameText.Text, addEventLinkText.Text, addEventTimestamp.Value);
            loadEventsTab();
        }

        private void deleteEventButton_Click(object sender, EventArgs e)
        {
            DBAdapter.deleteEvent(eventTabChosenEvent);
            chosenEventLabel.Text = $"Выбрано событие №";
            editEventLinkText.Text = "";
            editEventLinkText.Text = "";
            eventGroupDataGrid.DataSource = null;
            FormDGV(eventGroupDataGrid);

        }

        private void deleteEventGroupButton_Click(object sender, EventArgs e)
        {
            if (eventGroupDataGrid.Rows.Count > 0)
            {
                int groupConnId = Convert.ToInt32(eventGroupDataGrid[0, docGroupsDataGrid.SelectedCells[0].RowIndex].Value);
                DBAdapter.deleteEventGroupConn(groupConnId);
                eventGroupDataGrid.DataSource = DBAdapter.getEventGroups(eventTabChosenEvent);
                FormDGV(eventGroupDataGrid);
            }
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            createUserForm crUSFrm = new createUserForm();
            crUSFrm.ShowDialog();
            loadUserTab();
        }

        private void addDocGroupButton_Click(object sender, EventArgs e)
        {
            addGroupForm addGrForm = new addGroupForm(0, docTabChosenDoc);
            addGrForm.ShowDialog();
            docGroupsDataGrid.DataSource = DBAdapter.getDocGroups(docTabChosenDoc);
            FormDGV(docGroupsDataGrid);


        }

        private void addEventGroupButton_Click(object sender, EventArgs e)
        {
            addGroupForm addGrForm = new addGroupForm(1, eventTabChosenEvent);
            addGrForm.ShowDialog();
            eventGroupDataGrid.DataSource = DBAdapter.getEventGroups(eventTabChosenEvent);
            FormDGV(eventGroupDataGrid);
        }
    }
}
