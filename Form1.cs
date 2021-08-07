using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (MessageBox.Show("Before using this software you must accept the terms and conditions.\n\nDo you accept the T&Cs?", "Integrated Threat Hunting Tool - Agreement", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
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
            //Security,4624
            //Application, 1001
            //TODO: NEED TO ADD TRY CATCH VERIFICATION - Instance ID also needs to verify it exists etc.
            //TODO: ADD CONDITION TO FETCH SYSMON, Convert 'Sysmon' combo to full length directory in Event Viewer to work properly.
            string eventName;
            int instanceID;

            if (filterToolStripTextBox.Text == "")
            {
                MessageBox.Show("Please select a filter from the dropdown list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                eventName = filterToolStripTextBox.Text;
            }

            //Verifies that the instanceID value entered by the user is a valid number
            if (string.IsNullOrEmpty(filterInstanceIDToolStripTextBox.Text))
            {
                //TODO: Change from 1001 so that instanceID is null and that LINQ query does not query it!
                //TODO: If null, alert user with message box that it may take a long time to retrieve all logs
                instanceID = 1001;
            }
            else
            {
                try
                {
                    instanceID = Int32.Parse(filterInstanceIDToolStripTextBox.Text);
                    //TODO: Need to return the variable back but not outside of the method???
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

            EventLog log = new EventLog(eventName);
            var entries = log.Entries.Cast<EventLogEntry>()
                //TODO: Need to ensure that if value is blank, then instanceID is not queried.
                                      .Where(x => x.InstanceId == instanceID)
                                      .Select(x => new
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
                                      }).ToList();

            //Progress bar parameters
            toolStripProgressBar.Minimum = 1;
            toolStripProgressBar.Maximum = entries.Count;
            toolStripProgressBar.Step = 1;
            //Alerts the user with an error message if the instanceID does not exist for the selected event log source.
            if (entries.Count > 0)
            {
                toolStripProgressBar.Value = 1;
            }
            else
            {
                MessageBox.Show("The Instance/Event ID " + instanceID + " does not exist for this source\n\nPlease select another filter or valid ID", "Incorrect Instance/Event ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var item in entries)
            {
                textBox1.Text += item.TimeGenerated.ToString() + Environment.NewLine;
                textBox1.Text += item.Source.ToString() + Environment.NewLine;
                //textBox1.Text += "Event ID: "+ instanceID + Environment.NewLine;
                //textBox1.Text += "----------------------------------------------------" + Environment.NewLine;
                toolStripProgressBar.PerformStep();
                /* Some instanceIDs take a long time to load and give the "ContextSwitchDeadlock" exception.
                 * The code below prevents the program from crashing and throwing the exception.
                 */
                System.Threading.Thread.CurrentThread.Join(0);
            }
            //Clear progress bar
            MessageBox.Show(entries.Count + " log(s) have loaded.", "Filter Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolStripStatusLabel.Text = entries.Count + " Log(s) Loaded";
            toolStripProgressBar.Value = 1;
        }

        private void readEventLogHandler()
        {
            //TODO: This code should check whether the filter is standard or Sysmon source and determine which EventLog class 
            //should be used.
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
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (MessageBox.Show("Are you sure you want to clear the results", "Verify your actions", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    textBox1.Text = "";
                    toolStripStatusLabel.Text = "";
                }
            }  
        }

        private void filterToolStripTextBox_Click(object sender, EventArgs e)
        {

        }
    }
}
