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
            //TODO: NEED TO ADD TRY CATCH VERIFICATION - Instance ID also needs to verify it exists etc.
            //TODO: ADD CONDITION TO FETCH SYSMON, Convert 'Sysmon' combo to full length directory in Event Viewer to work properly.
            string eventName;
            if (filterToolStripTextBox.Text == "")
            {
                MessageBox.Show("Please select a filter from the dropdown list", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                eventName = filterToolStripTextBox.Text;
            }
            int instanceID = 1001;

            EventLog log = new EventLog(eventName);
            var entries = log.Entries.Cast<EventLogEntry>()
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

            //Progress bar
            toolStripProgressBar.Minimum = 1;
            toolStripProgressBar.Maximum = entries.Count;
            toolStripProgressBar.Step = 1;
            toolStripProgressBar.Value = 1;

            foreach (var item in entries)
            {
                textBox1.Text += item.TimeGenerated.ToString() + Environment.NewLine;
                textBox1.Text += item.Source.ToString() + Environment.NewLine;
                //textBox1.Text += "Event ID: "+ instanceID + Environment.NewLine;
                //textBox1.Text += "----------------------------------------------------" + Environment.NewLine;
                toolStripProgressBar.PerformStep();
            }
            //Clear progress bar
            MessageBox.Show(entries.Count + " log(s) have loaded.", "Filter Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolStripStatusLabel.Text = entries.Count + " Log(s) Loaded";
            toolStripProgressBar.Value = 1;
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
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("Are you sure you want to clear the results", "Verify your actions", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    textBox1.Text = "";
                    toolStripStatusLabel.Text = "";
                }
            }  
        }
    }
}
