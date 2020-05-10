using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackBotDBEditor
{
    public partial class addGroupForm : Form
    {
        /// <summary>
        /// type: 0 - для доков, 1 - для событий
        /// </summary>
        /// <param name="type">type: 0 - для доков, 1 - для событий</param>
        /// <param name="id"></param>
        int formType;
        int recId;
        DatabaseAdapter DBAdapter = new DatabaseAdapter();
        public addGroupForm(int type, int recorId)
        {
            formType = type;
            recId = recorId;
            InitializeComponent();
        }

        void FormDGV(DataGridView target)
        {
            for (int i = 0; i < target.ColumnCount - 1; i++)
            {
                target.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            target.Columns[target.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void groupDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(formType==0)
            {
                DBAdapter.createDocGroupConn(recId, Convert.ToInt32(groupDGV[0, e.RowIndex].Value));
                this.Close();
            }
            else if(formType==1)
            {
                DBAdapter.createEventGroupConn(recId, Convert.ToInt32(groupDGV[0, e.RowIndex].Value));
                this.Close();
            }
        }

        private void addGroupForm_Load(object sender, EventArgs e)
        {
            groupDGV.DataSource = DBAdapter.getGroups();
            FormDGV(groupDGV);
        }
    }
}
