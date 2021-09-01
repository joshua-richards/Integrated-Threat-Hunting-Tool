using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Integrated_Threat_Hunting_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Initalise BackgroundWorker to do the Event Log gathering
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (MessageBox.Show("Before using this software you must accept the terms and conditions.\n\nDo you accept the T&Cs?\n\nNote: Please run this program with Administrative privilages in order to retrieve all logs.", "Integrated Threat Hunting Tool - Agreement", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                System.Windows.Forms.Application.Exit();
            }
            //Itilialise Form1 labels
            systemInfoMachineLabel.Text = "PC Name: " + Environment.MachineName;
            systemInfoOSLabel.Text = "Windows Version: " + Environment.OSVersion;
            systemInfoUserNameLabel.Text = "Username : " + Environment.UserName;
            toolStripStatusLabel.Text = "";
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to exit this application?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        
        private void readEventLog()
        {
            //Set event log variables to retrieve
            //Security, 4624
            //Application, 1001
            //TODO: ADD CONDITION TO FETCH SYSMON, Convert 'Sysmon' combo to full length directory in Event Viewer to work properly.
            //Add this to another method/function instead of this one because it's getting too clutered and won't flow well.
            string filterType;
            int instanceID = 0;
            bool instanceIDFilterIsNull = false;

            if (string.IsNullOrEmpty(filterTypeToolStripTextBox.Text))
            {
                MessageBox.Show("Please select a filter from the dropdown list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (filterTypeToolStripTextBox.Text == "Sysmon")
            {
                //TODO: Implement Sysmon EventLog here (will have to use EventLogQuery/Reader instead of standard approach)
                MessageBox.Show("Sysmon not yet implemented.", "Sysmon");
                filterType = "Sysmon";
                return;
            }
            else
            {
                //Selects filter value from dropdown list
                filterType = filterTypeToolStripTextBox.Text;
            }

            //Verifies that the instanceID value entered by the user is a valid number
            if (string.IsNullOrEmpty(filterInstanceIDToolStripTextBox.Text) && filterSourceToolStripTextBox.SelectedIndex >= 1)
            {
                instanceIDFilterIsNull = true;
                if (MessageBox.Show("Retrieving event logs based on source.\n\nAre you sure you wish to continue?", "This process may take a while", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                    return;
                }
            }
            else if (string.IsNullOrEmpty(filterInstanceIDToolStripTextBox.Text))
            {
                //Use filter is null bool for determining which EventLog class to use below (if/else)
                instanceIDFilterIsNull = true;
                if (MessageBox.Show("Retrieving all event logs.\n\nAre you sure you wish to continue?", "This process may take a while", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                {
                    return;
                }
            }
            else
            {
                try
                {
                    instanceID = Int32.Parse(filterInstanceIDToolStripTextBox.Text);
                    instanceIDFilterIsNull = false;
                }
                catch (Exception)
                {
                    //Throw an error message if the instanceID cannot be parsed to a string.
                    if (MessageBox.Show("The Instance/Event ID is incorrect. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        return;
                    }
                    throw;
                }
            }

            EventLog log = new EventLog(filterType);
            var entries = log.Entries.Cast<EventLogEntry>();
            //If the filter has an Instance ID and/or source value, then it will search for it using the below statement
            if (instanceIDFilterIsNull && filterSourceToolStripTextBox.SelectedIndex >= 1)
            {
                entries = entries.Where(x => x.Source == filterSourceToolStripTextBox.Text.ToString());
            }
            else if (!instanceIDFilterIsNull && filterSourceToolStripTextBox.SelectedIndex == 1)
            {
                entries = entries.Where(x => x.InstanceId == instanceID);
            }
            else if (!instanceIDFilterIsNull && filterSourceToolStripTextBox.SelectedIndex >= 1)
            {
                entries = entries.Where(x => x.InstanceId == instanceID);
                entries = entries.Where(x => x.Source == filterSourceToolStripTextBox.Text.ToString());
            }

            var entriesQuery = entries.Select(x => new
            {
                x.MachineName,
                x.Site,
                x.Source,
                x.Message,
                x.TimeGenerated,
                x.Index,
                x.UserName,
                x.Category,
                x.InstanceId,
                x.EntryType
            }).ToList();

            //Progress bar parameters
            //TODO: GET RID AND ADD TO BW WORKER PROGREES
            toolStripProgressBar.Minimum = 1;
            toolStripProgressBar.Maximum = entriesQuery.Count;
            toolStripProgressBar.Step = 1;
            //Alerts the user with an error message if the instanceID does not exist for the selected event log source.
            if (entriesQuery.Count > 0)
            {
                toolStripProgressBar.Value = 1;
            }
            else
            {
                MessageBox.Show("The Instance/Event ID " + instanceID + " does not exist for this source\n\nPlease select another filter or valid ID", "Incorrect Instance/Event ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Create table object and table columns
            DataTable table = new DataTable();
            table.Columns.Add("Log No", typeof(int));
            table.Columns.Add("Time", typeof(string));
            table.Columns.Add("Instance/Event ID", typeof(string));
            table.Columns.Add("Entry Type", typeof(string));
            table.Columns.Add("Source", typeof(string));
            table.Columns.Add("PC Name", typeof(string));

            //TODO: Add this to BackgroundWorker in order to run on another thread whilst also keeping the UI + Progress bar running
            int logNumber = 1;
            foreach (var item in entriesQuery)
            {
                //TODO: Change this to the new background worker step, does not need to be added here, can be done in seperate BW method
                toolStripProgressBar.PerformStep();
                //Add Rows to the DataGridView based on retrieved log entries
                table.Rows.Add(
                    logNumber,
                    item.TimeGenerated.ToString(), 
                    item.InstanceId.ToString(),
                    item.EntryType.ToString(),
                    item.Source.ToString(),
                    item.MachineName.ToString()
                    );
                logNumber++;
                /* Some instanceIDs take a long time to load and give the "ContextSwitchDeadlock" exception.
                 * The code below prevents the program from crashing and throwing the exception.
                 */
                System.Threading.Thread.CurrentThread.Join(0);
            }

            //Show message with number of logs found
            MessageBox.Show(entriesQuery.Count + " log(s) have loaded.", "Filter Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Add retrieved entries to DataGridView
            dataGridView1.DataSource = table;
            dataGridView1.Sort(this.dataGridView1.Columns["Time"], ListSortDirection.Descending);
            //Clear progress bar and show number of logs
            toolStripStatusLabel.Text = entriesQuery.Count + " Log(s) Loaded";
            toolStripProgressBar.Value = 1;   
        }

        //Background worker is used in order to populate the DataGridView using another thread in the background
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //TODO: Add here the iteration used in the for each loop of the readEventLog method
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar.Value = e.ProgressPercentage;
        }

        private void readEventLogHandler()
        {
            //TODO: This code should check whether the filter is standard or Sysmon source and determine which EventLog class 
            //should be used.
        }

        private void getSources(string filterType)
        {
            //TODO: Ensure that Sysmon is handled correctly (if statement needed)
            if (filterType != "Sysmon")
            {
                EventLog log = new EventLog(filterType);
                var entries = log.Entries.Cast<EventLogEntry>();
                var entriesQuery = entries.Select(x => new { x.Source }).ToList();
                //Remove current values and add a blank value to the source filter dropdownlist
                filterSourceToolStripTextBox.Items.Clear();
                filterSourceToolStripTextBox.Items.Add("");
                //Iterate through the type of entry logs and gather the sources available and add them to the dropdown list 
                foreach (var item in entriesQuery)
                {
                    //If the source does not exist in the dropdownlist already then add it to the list
                    //TODO: Messagebox with progress (Generating results auto close)
                    //TODO: Security type hard code the sources (only 2 sources)
                    if (!filterSourceToolStripTextBox.Items.Contains(item.Source.ToString()))
                    {
                        filterSourceToolStripTextBox.Items.Add(item.Source.ToString());
                    }
                }
            }
        }
                              
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "The Integrated Threat Hunting Tool was created as part of an MSc project for the University of South Wales by Joshua Richards #17025745.\n\nAll code and subsequent libraries are used under fair use as this tool is not-for-profit and for educational purposes only.";
            MessageBox.Show(msg, "About");
        }

        private void maximiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void minimiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void windowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void windowsEventViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\Event Viewer.lnk");
        }

        private void windowsPerformanceMonitorButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\Performance Monitor.lnk");
        }

        private void windowsEventViewerButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\Event Viewer.lnk");
        }

        private void windowsPerformanceMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools\Performance Monitor.lnk");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "Version: v0.1\n\nMore information is available on the public GitHub repository.\n\nhttps://github.com/joshua-richards/Integrated-Threat-Hunting-Tool";
            MessageBox.Show(msg, "Version");
        }

        private void filterToolStripButton_Click(object sender, EventArgs e)
        {
            readEventLog();
        }

        private void toolStripClearResultsButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                if (MessageBox.Show("Are you sure you want to clear the results", "Verify your actions", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();
                    toolStripStatusLabel.Text = "";
                    filterSourceToolStripTextBox.SelectedIndex = 0;
                    filterInstanceIDToolStripTextBox.Text = "";
                }
            }  
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Double clicking on the cells (instance ID, source) adds the current cell value to the corresponding filter text boxes
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                {
                    filterInstanceIDToolStripTextBox.Text = dataGridView1.CurrentCell.Value.ToString();
                }
            }
            else if (dataGridView1.CurrentCell.ColumnIndex.Equals(4) && e.RowIndex != -1)
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                {
                    var sourceIndex = filterSourceToolStripTextBox.Items.IndexOf(dataGridView1.CurrentCell.Value.ToString());
                    filterSourceToolStripTextBox.SelectedIndex = sourceIndex;
                }
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Display the time with seconds in the toolStrip for convenience
            DateTime now = DateTime.Now;
            datetimeToolStripLabel.Text = now.ToString("T");
        }

        private void filterTypeToolStripTextBox_DropDownClosed(object sender, EventArgs e)
        {
            //Runs the getSources function if a type is selected from the filter dropdown list
            if (filterTypeToolStripTextBox.SelectedIndex >= 0)
            {
                var filterType = filterTypeToolStripTextBox.SelectedItem.ToString();
                getSources(filterType);
            }
        }

        private void clearAllParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterTypeToolStripTextBox.SelectedIndex = 0;
            filterSourceToolStripTextBox.SelectedIndex = 0;
            filterInstanceIDToolStripTextBox.Text = "";
        }
    }
}
