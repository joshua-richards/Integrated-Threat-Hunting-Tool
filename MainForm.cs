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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //Initialise the List used to display messages if the user double clicks on log number
        List<string> logMessages = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Instructions on how to use this application are in the 'Help' section of the menu bar." +
                "\n\nNote: Run this application with Administrative privilages in order to retrieve all logs.",
                "Integrated Threat Hunting Tool - Welcome", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
        
        private void readEventLog(string filterType)
        {
            //Set event log variables to retrieve
            int instanceID = 0;
            bool instanceIDFilterIsNull = false;
         
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

            int logNumber = 1;
            foreach (var item in entriesQuery)
            {
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
                //Add Information to the list for use in viewing the message by double clicking on log number
                logMessages.Add(item.Message.ToString());
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

        private void readSysmonEventLog(string filterType)
        {
            //Set event log variables to retrieve (UNSET BELOW VARIABLES BEFORE CONTINUE)
            int instanceID = 0;
            bool instanceIDFilterIsNull = false;
            
            //Verifies that the instanceID value entered by the user is a valid number (Source not used for SYSMON, SO NO CHECK REQD)
            if (string.IsNullOrEmpty(filterInstanceIDToolStripTextBox.Text))
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

            //https://docs.microsoft.com/en-us/previous-versions/bb399427(v=vs.90)?redirectedfrom=MSDN
            //Creates the Sysmon event query whether there is an instance ID or not
            string sysmonQueryEventID = "*[System/EventID=" + instanceID + "]";
            EventLogQuery sysmonQuery = new EventLogQuery("Microsoft-Windows-Sysmon/Operational", PathType.LogName, "*");
            if (!instanceIDFilterIsNull)
            {
                sysmonQuery = new EventLogQuery("Microsoft-Windows-Sysmon/Operational", PathType.LogName, sysmonQueryEventID);
            }

            //Declare SYSMON eventRecord for use
            //Gets sysmonQuery from previous if statement based on whether ID is null or not in the textbox
            EventLogReader sysmonReader = new EventLogReader(sysmonQuery);
            EventRecord eventRecord;

            //Create table object and table columns for SYSMON logs
            DataTable table = new DataTable();
            table.Columns.Add("Log No", typeof(int));
            table.Columns.Add("Time", typeof(string));
            table.Columns.Add("Instance/Event ID", typeof(string));
            //table.Columns.Add("Task Category", typeof(string));
            table.Columns.Add("PC Name", typeof(string));

            if ((eventRecord = sysmonReader.ReadEvent()) != null)
            {
                toolStripProgressBar.Value = 1;
            }
            else
            {
                MessageBox.Show("The Instance/Event ID " + instanceID + " does not exist for this source\n\nPlease select another filter or valid ID", "Incorrect Instance/Event ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int logNumber = 1;
            while ((eventRecord = sysmonReader.ReadEvent()) != null )
            {
                toolStripProgressBar.PerformStep();
                try
                {
                    //logMessages.Add(eventRecord.FormatDescription());
                    logMessages.Add(eventRecord.TimeCreated.ToString());
                }
                catch (Exception)
                {
                    logMessages.Add("No Information Available");
                    //MessageBox.Show(logNumber.ToString());
                    continue;
                }
                table.Rows.Add(
                    logNumber,
                    eventRecord.TimeCreated.ToString(),
                    eventRecord.Id.ToString(),
                    //eventRecord.TaskDisplayName.ToString(),
                    eventRecord.MachineName.ToString());
                logNumber++;
                /* Some instanceIDs take a long time to load and give the "ContextSwitchDeadlock" exception.
                 * The code below prevents the program from crashing and throwing the exception.
                 */
                System.Threading.Thread.CurrentThread.Join(0);
            }

            //Show message with number of logs found
            MessageBox.Show(logNumber-1 + " log(s) have loaded.", "Filter Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Add retrieved entries to DataGridView
            MessageBox.Show("Adding logs to table\n\nThis may take a few moments.\nGrab a cup of coffee while you wait!");
            dataGridView1.DataSource = table;
            dataGridView1.Sort(this.dataGridView1.Columns["Time"], ListSortDirection.Descending);
            //Clear progress bar and show number of logs
            toolStripStatusLabel.Text = logNumber-1 + " Log(s) Loaded";
            toolStripProgressBar.Value = 0;
        }

        private void readEventLogHandler(string filterType)
        {
            /*This code should check whether the filter is standard or Sysmon source and determine which EventLog method 
             *should be used.
            */
            if (filterType == "Sysmon")
            {
                readSysmonEventLog(filterType);
            }
            else
            {
                readEventLog(filterType);
            }
        }

        private void getSources(string filterType)
        {
            //Adds the sources from the event log to the combobox available for selection
            if (filterType == "Security")
            {
                //Two security sources are hard coded as there are only ever 2 sources.
                //This speeds up the process by preventing unnecessary iteration through the event logs to check for them.
                filterSourceToolStripTextBox.Items.Clear();
                filterSourceToolStripTextBox.Items.Add("");
                if (!filterSourceToolStripTextBox.Items.Contains("Microsoft-Windows-Eventlog") &&
                    !filterSourceToolStripTextBox.Items.Contains("Microsoft-Windows-Security-Auditing"))
                {
                    filterSourceToolStripTextBox.Items.Add("Microsoft-Windows-Eventlog");
                    filterSourceToolStripTextBox.Items.Add("Microsoft-Windows-Security-Auditing");
                }
            }
            else if (filterType != "Sysmon")
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
                    if (!filterSourceToolStripTextBox.Items.Contains(item.Source.ToString()))
                    {
                        filterSourceToolStripTextBox.Items.Add(item.Source.ToString());
                    }
                }
            }
            else if (filterType == "Sysmon")
            {
                //Sysmon has no sources, as such, combobox is blank
                filterSourceToolStripTextBox.Items.Clear();
                filterSourceToolStripTextBox.Items.Add("");
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
            if (string.IsNullOrEmpty(filterTypeToolStripTextBox.Text))
            {
                MessageBox.Show("Please select a filter from the dropdown list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                var filterType = filterTypeToolStripTextBox.SelectedItem.ToString();
                logMessages.Clear();
                readEventLogHandler(filterType);
            }
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
                    filterSourceToolStripTextBox.SelectedIndex = -1;
                    filterInstanceIDToolStripTextBox.Text = "";
                    //Clear the Message list that's used to view message of event
                    logMessages.Clear();
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
            else if (dataGridView1.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                {
                    int logIndex = Convert.ToInt32(dataGridView1.CurrentCell.Value) - 1;
                    MessageBox.Show(logMessages[logIndex]);
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

        private void sysmonEventIDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "The following Event IDs can be used in the application filter textbox." +
                "\n\nSysmon Event IDs:\n1 - Process Creation\n2 - A process changed a file creation time" +
                "\n3 - Network connection\n4 - Sysmon service state changed\n5 - Process terminated" +
                "\n6 - Driver loaded\n7 - Image loaded\n8 - CreateRemoteThread\n9 - RawAccessRead" +
                "\n10 - ProcessAccess\n11 - FileCreate\n12 - RegistryEvent (Object create and delete)" +
                "\n13 - RegistryEvent (Value Set)\n14 - RegistryEvent (Key and Value Rename)" +
                "\n15 - FileCreateStreamHash\n16 - ServiceConfigurationChange\n17 - PipeEvent (Pipe Created)" +
                "\n18 - PipeEvent (Pipe Connected)\n19 - WmiEvent (WmiEventFilter activity detected)" +
                "\n20 - WmiEvent (WmiEventConsumer activity detected)\n21 - WmiEvent (WmiEventConsumerToFilter activity detected)" +
                "\n22 - DNSEvent (DNS query)\n23 - FileDelete (File Delete archived)\n24 ClipboardChange (New content in the clipboard)" +
                "\n25 - ProcessTampering (Process image change)\n26 - FileDeleteDetected (File Delete logged)" +
                "\n255 - Error" +
                "\n\nMore information for each event at: https://docs.microsoft.com/en-us/sysinternals/downloads/sysmon#events";
            MessageBox.Show(msg, "Guide - Sysmon Event IDs");
        }

        private void panel1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Open new Machine Learning Form
            MachineLearningForm mlForm = new MachineLearningForm();
            mlForm.ShowDialog();
        }

        private void gridViewInstructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "Here are some things you can do to assist with threat hunting in this application:\n\n" +
                "- Double-click the 'Log No' field of any row and a pop-up with more log information will appear.\n" +
                "- Double-click the 'Instance/Event ID' field of any row and it will autopopulate the filter with that ID number.\n" +
                "- Double-click the 'Source' field of any row and it will autopopulate the filter with that source.\n" +
                "- Clicking on the field name at the top of the grid will sort by ascending/descending order (Descending by default).\n" +
                "- The bin icon in the tool strip will clear all logs and filters and prompt you for confirmation beforehand.\n" +
                "- Some sources may take a while, the application will prompt you if this may be the case.";
            MessageBox.Show(msg, "Guide - Grid View Instructions");
        }
    }
}
