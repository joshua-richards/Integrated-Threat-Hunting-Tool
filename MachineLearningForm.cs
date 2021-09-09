using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Integrated_Threat_Hunting_Tool
{
    public partial class MachineLearningForm : Form
    {
        //static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Datasets", "sysmon.csv");
        //const int _docsize = 67134;

        public MachineLearningForm()
        {
            InitializeComponent();
            toolStripStatusLabel.Text = "";
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you wish to exit the Machine Learing window?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        static IDataView CreateEmptyDataView(MLContext mLContext)
        {
            //Create empty DataView. Need the schema to call Fit() for the time series transforms.
            IEnumerable<SysmonData> enumerableData = new List<SysmonData>();
            return mLContext.Data.LoadFromEnumerable(enumerableData);
        }

        private void DetectSpike(MLContext mlContext, int docSize, IDataView sysmonResults, List<string> timeColumn)
        {
            var iidSpikeEstimator = mlContext.Transforms.DetectIidSpike(outputColumnName: nameof(SysmonPrediction.Prediction),
                inputColumnName: nameof(SysmonData.EventId), confidence: 95, pvalueHistoryLength: docSize / 4);
            MessageBox.Show("Fitting data");
            ITransformer iidSpikeTransform = iidSpikeEstimator.Fit(CreateEmptyDataView(mlContext));
            MessageBox.Show("Transforming data");
            IDataView transformedData = iidSpikeTransform.Transform(sysmonResults);
            MessageBox.Show("Predicting data");
            var predictions = mlContext.Data.CreateEnumerable<SysmonPrediction>(transformedData, reuseRowObject: false);
            toolStripProgressBar.Minimum = 1;
            toolStripProgressBar.Maximum = docSize;
            toolStripProgressBar.Step = 1;
            //Create Table
            DataTable table = new DataTable();
            table.Columns.Add("Log No", typeof(int));
            table.Columns.Add("Time", typeof(string));
            table.Columns.Add("Alert", typeof(string));
            table.Columns.Add("Score (Event ID)", typeof(string));
            table.Columns.Add("P-Value", typeof(string));
            table.Columns.Add("Anomaly Detected?", typeof(string));
            int logNumber = 1;
            int noOfAnomalies = 0;
            foreach (var p in predictions)
            {
                toolStripProgressBar.PerformStep();
                //var results = $"{p.Prediction[0]}\t\t\t{p.Prediction[1]:f2}\t\t\t{p.Prediction[2]:F2}";
                if (p.Prediction[0] == 1)
                {
                    //TODO: Time value from CSV/property of object.
                    //TODO: Double click on log brings up more info.
                    table.Rows.Add(logNumber, timeColumn[logNumber - 1], p.Prediction[0], p.Prediction[1], p.Prediction[2], " <-- ANOMALY DETECTED (Spike - This log may be a cause for further investigation)");
                    noOfAnomalies++;
                    //results += " <-- ANOMALY DETECTED (Spike - This log may be a cause for further investigation)";
                }
                else
                {
                    table.Rows.Add(logNumber, timeColumn[logNumber - 1], p.Prediction[0], p.Prediction[1], p.Prediction[2], "");
                }
                logNumber++;
                //mlResultTextBox.Text += results + Environment.NewLine;
            }
            //mlResultTextBox.Text += "- - - END - - -" + Environment.NewLine;
            dataGridView1.DataSource = table;
            MessageBox.Show("The algorithm found " + noOfAnomalies + " possible anomalies.", "Anomalies found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolStripStatusLabel.Text = docSize + " Log(s) Loaded";
            toolStripProgressBar.Value = 1;
        }

        private void toolStripButtonCSV_Click(object sender, EventArgs e)
        {
            //Get the file name and number of rows of the CSV (-1 the first row which contains the labels)
            string _dataPath = getFilePath();
            int _docsize = getCSVSize(_dataPath) - 1;
            MessageBox.Show(_dataPath + "\n\n" + _docsize);
            //Get Time from CSV for datagridview
            var timeColumn = new List<string>();
            using (var fileReader = File.OpenText(_dataPath))
            using (var csvResult = new CsvHelper.CsvReader(fileReader, System.Globalization.CultureInfo.CurrentCulture))
            {
                csvResult.Read();
                csvResult.ReadHeader();
                while (csvResult.Read())
                {
                    var field = csvResult.GetField<string>("TimeCreated");
                    timeColumn.Add(field);
                }
            }
            //Create MLContext
            MLContext mlContext = new MLContext();
            IDataView dataView = mlContext.Data.LoadFromTextFile<SysmonData>(path: _dataPath, hasHeader: true, separatorChar: ',');
            DetectSpike(mlContext, _docsize, dataView, timeColumn);
        }

        private string getFilePath()
        {
            //Get CSV file from open dialog
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = false;
            string csvFileName = "Filename";

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                csvFileName = choofdlog.FileName;
            }

            return csvFileName;
        }

        private int getCSVSize(string filePath)
        {
            //Get CSV number of rows
            int CSVNOofRows = System.IO.File.ReadAllLines(filePath).Length;
            return CSVNOofRows;
        }

        private void evtxECmdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "Use the EvtxECmd.exe program to generate a Sysmon CSV for analysis\n" +
                "Use the following command:\n\n" +
                "."+@"\EvtxECmd.exe -f PATHTOSYSMONLOG --csv PATHTOSAVE --csvf sysmon.csv";
            MessageBox.Show(msg, "Guide - Create Sysmon CSV");
            /*> .\EvtxECmd.exe -f "C:\Windows\System32\winevt\Logs\Microsoft-Windows-Sysmon%4Operational.evtx" 
             * --csv C:\Users\jtr12\OneDrive\Desktop\EvtxExplorer  --csvf sysmon.csv
             */
        }

        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            //MLContext mlContext = new MLContext();
            //IDataView dataView = mlContext.Data.LoadFromTextFile<SysmonData>(path: _dataPath, hasHeader: true, separatorChar: ',');
            //DetectSpike(mlContext, _docsize, dataView);
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "The Integrated Threat Hunting Tool was created as part of an MSc project for the University of South Wales by Joshua Richards #17025745.\n\nAll code and subsequent libraries are used under fair use as this tool is not-for-profit and for educational purposes only.";
            MessageBox.Show(msg, "About");
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "Version: v0.1\n\nMore information is available on the public GitHub repository.\n\nhttps://github.com/joshua-richards/Integrated-Threat-Hunting-Tool";
            MessageBox.Show(msg, "Version");
        }
    }

    public class SysmonData
    {
        //0-26 columns from CSV
        //[LoadColumn(0)]
        //public int RecordNumber;
        //[LoadColumn(1)]
        //public int EventRecordId;
        [LoadColumn(2)]
        public string TimeCreated;
        [LoadColumn(3)]
        public float EventId;
        //[LoadColumn(4)]
        //public int ProcessId;
        //[LoadColumn(5)]
        //public int ThreadId;
        //[LoadColumn(6)]
        //public string Computer;
        //[LoadColumn(7)]
        //public int ChunkNumber;
        //[LoadColumn(8)]
        //public string UserId;
        //[LoadColumn(9)]
        //public string MapDescription;
        //[LoadColumn(10)]
        //public string UserName;
        //[LoadColumn(11)]
        //public string PayloadData1;
        //[LoadColumn(12)]
        //public string PayloadData2;
        //[LoadColumn(13)]
        //public string PayloadData3;
        //[LoadColumn(14)]
        //public string PayloadData4;
        //[LoadColumn(15)]
        //public string PayloadData5;
        //[LoadColumn(16)]
        //public string PayloadData6;
        //[LoadColumn(17)]
        //public string ExecutableInfo;
        //[LoadColumn(18)]
        //public string HiddenRecord;
        //[LoadColumn(19)]
        //public string SourceFile;
        //[LoadColumn(20)]
        //public string Keywords;
        //[LoadColumn(21)]
        //public string Payload;
        //Removed 5 columns from CSV for unneccassary data.
        //[LoadColumn(22)]
        //[LoadColumn(23)]
        //[LoadColumn(24)]
        //[LoadColumn(25)]
        //[LoadColumn(26)]
    }

    public class SysmonPrediction
    {
        //Vector to hold alert, score and p-value values
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}
