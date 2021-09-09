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

        private void DetectSpike(MLContext mlContext, int docSize, IDataView sysmonResults)
        {
            var iidSpikeEstimator = mlContext.Transforms.DetectIidSpike(outputColumnName: nameof(SysmonPrediction.Prediction),
                inputColumnName: nameof(SysmonData.EventId), confidence: 95, pvalueHistoryLength: docSize / 4);
            MessageBox.Show("Fitting data");
            ITransformer iidSpikeTransform = iidSpikeEstimator.Fit(CreateEmptyDataView(mlContext));
            MessageBox.Show("Transforming data");
            IDataView transformedData = iidSpikeTransform.Transform(sysmonResults);
            MessageBox.Show("Predicting data");
            var predictions = mlContext.Data.CreateEnumerable<SysmonPrediction>(transformedData, reuseRowObject: false);
            mlResultTextBox.Text = "Alert\t\t\ttScore\t\t\ttP-Value" + Environment.NewLine;
            toolStripProgressBar.Minimum = 1;
            toolStripProgressBar.Maximum = docSize;
            toolStripProgressBar.Step = 1;
            foreach (var p in predictions)
            {
                toolStripProgressBar.PerformStep();
                var results = $"{p.Prediction[0]}\t\t\t{p.Prediction[1]:f2}\t\t\t{p.Prediction[2]:F2}";
                if (p.Prediction[0] == 1)
                {
                    results += " <-- ANOMALY DETECTED (Spike - This log may be a cause for further investigation)";
                }
                mlResultTextBox.Text += results + Environment.NewLine;
            }
            mlResultTextBox.Text += "- - - END - - -" + Environment.NewLine;
            toolStripStatusLabel.Text = docSize + " Log(s) Loaded";
            toolStripProgressBar.Value = 1;
        }

        private void toolStripButtonCSV_Click(object sender, EventArgs e)
        {
            //Get the file name and number of rows of the CSV (-1 the first row which contains the labels)
            string _dataPath = getFilePath();
            int _docsize = getCSVSize(_dataPath) - 1;
            MessageBox.Show(_dataPath + "\n\n" + _docsize);
            //Create MLContext
            MLContext mlContext = new MLContext();
            IDataView dataView = mlContext.Data.LoadFromTextFile<SysmonData>(path: _dataPath, hasHeader: true, separatorChar: ',');
            DetectSpike(mlContext, _docsize, dataView);
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
    }

    public class SysmonData
    {
        //0-26 columns from CSV
        //[LoadColumn(0)]
        //public int RecordNumber;
        //[LoadColumn(1)]
        //public int EventRecordId;
        [LoadColumn(0)]
        public string TimeCreated;
        [LoadColumn(1)]
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
