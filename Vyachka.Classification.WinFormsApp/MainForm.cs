using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Vyachka.Classification.Core;
using Vyachka.Classification.Core.Models;
using Vyachka.Classification.Core.Settings;
using Vyachka.Classification.FileParsing;
using Vyachka.Classification.FileParsing.Models;

namespace Vyachka.Classification.WinFormsApp
{
    public partial class MainForm : Form
    {
        private DataReadingResult _parameterValues;
        private IReadOnlyCollection<ParameterSetting> _parameterSettings;

        public MainForm()
        {
            InitializeComponent();
            InitializeFileFilters();
            InitializeParameterSetting();
        }

        private void InitializeParameterSetting()
        {
            var path = ConfigurationManager.AppSettings["ParameterSettingsFilePath"];
            _parameterSettings =
                FileSettingParser.ParseFileToParameterCollection(path);
        }

        private void InitializeFileFilters()
        {
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = @"bin files (.bin)|*.bin";
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;

                _parameterValues = ParameterValuesFileReader.ReadParamsFromBinaryFile(filePath);
                fileDateTimeLabel.Text =
                    _parameterValues.RecordingDateTimeUtc.ToString(CultureInfo.CurrentCulture);

                buildChartButton.Enabled = true;
                parametersComboBox.Enabled = true;
                fileDateTimeLabel.Visible = true;

                InitializeComboBox();
                MessageBox.Show(@"Parameter values were uploaded");
            }
        }

        private void InitializeComboBox()
        {
            parametersComboBox.Items.Clear();
            var classificatorSettings = new ClassificatorSettings(_parameterSettings.ToList());
            var parameterSettingsParamNames = classificatorSettings.ParamNames;
            var paramNamesWithoutSettings = "";

            foreach (var paramName in _parameterValues.ParametersCollection.ParamNames)
            {
                FillComboBox(parameterSettingsParamNames, paramName);

                if (!parametersComboBox.Items.Contains(paramName))
                {
                    paramNamesWithoutSettings += paramName;
                    paramNamesWithoutSettings += "\n";
                }
                else
                {
                    parametersComboBox.Text = paramName;
                }
            }

            if (paramNamesWithoutSettings.Length > 0)
            {
                MessageBox.Show("The following parameters don't have settings: \n" + paramNamesWithoutSettings,
                    @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FillComboBox(IReadOnlyCollection<string> parameterSettingsParamNames, string paramName)
        {
            foreach (var parameterSettingsName in parameterSettingsParamNames)
            {
                if (paramName == parameterSettingsName)
                {
                    parametersComboBox.Items.Add(paramName);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dialog = MessageBox.Show(@"Are you sure you want to exit?", @"Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = dialog != DialogResult.Yes;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BuildChartButton_Click(object sender, EventArgs e)
        {
            if (CollectionHasParamNameFromComboBox(parametersComboBox.Text))
            {
                valuesChart.Series.Clear();

                if (isNeedToDrawAsClassifiedCheckBox.Checked)
                {
                    DrawClassifiedParameterValues(parametersComboBox.Text);
                }
                else
                {
                    DrawRawParameterValues(parametersComboBox.Text);
                }
            }
            else
            {
                MessageBox.Show(@"This parameter does not exists!", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }

        private bool CollectionHasParamNameFromComboBox(string name)
        {
            foreach (var paramName in _parameterValues.ParametersCollection.ParamNames)
            {
                if (name == paramName)
                {
                    return true;
                }
            }

            return false;
        }

        private void DrawRawParameterValues(string paramName)
        {
            var parameterValues = _parameterValues.ParametersCollection[paramName];

            var parameterValuesSeries = ConvertParameterValuesToSeries(parameterValues);

            valuesChart.Series.Add(parameterValuesSeries);
        }

        private static Series ConvertParameterValuesToSeries(ParameterValuesContainer parameterValues)
        {
            var parameterValuesSeries = new Series(parameterValues.ParamName)
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.DarkViolet
            };

            for (var i = 0; i < parameterValues.Values.Length; i++)
            {
                parameterValuesSeries.Points.AddXY(i, parameterValues.Values[i]);
            }

            return parameterValuesSeries;
        }

        private void DrawClassifiedParameterValues(string paramName)
        {
            var parameterValues = _parameterValues.ParametersCollection[paramName];
            var classificatorSettings = new ClassificatorSettings(_parameterSettings.ToList());
            var classificator = new Classificator(classificatorSettings);
            var result = classificator.Process(parameterValues);

            var goodRanges = result.GoodRanges;
            var badRanges = result.BadRanges;

            var goodSeries = new Series("Good ranges")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.Aqua
            };
            var badSeries = new Series("Bad ranges")
            {
                ChartType = SeriesChartType.Spline,
                Color = Color.DeepPink
            };

            //todo сделать с помощью convertToSeries(или сделать новый)!

            var mainSeries = goodSeries;
            for (var i = 0; i < parameterValues.Values.Length; i++)
            {
                mainSeries.Points.AddXY(i, parameterValues.Values[i]);
            }

            foreach (var range in goodRanges)
            {
                SetLineColorOnInterval(mainSeries, range.StartIndex, range.EndIndex, goodSeries.Color);
            }

            foreach (var range in badRanges)
            {
                SetLineColorOnInterval(mainSeries, range.StartIndex, range.EndIndex, badSeries.Color);
            }

            valuesChart.Series.Add(goodSeries);
            valuesChart.Series.Add(badSeries);
        }

        private static void SetLineColorOnInterval(Series series, int from, int to, Color lineColor)
        {
            for (var i = from + 1; i <= to; i++)
            {
                series.Points[i].Color = lineColor;
            }
        }

        private void ParametersComboBox_SelectedValueChanged(object sender, EventArgs e)
        {

            var classificatorSettings = new ClassificatorSettings(_parameterSettings.ToList());

            measureFreqLabel.Text =
                @"Measurement frequency = " + classificatorSettings[parametersComboBox.Text].MeasureFreq;
            maxDeviationLabel.Text =
                @"Max deviation = " + classificatorSettings[parametersComboBox.Text].MaxDeviation;

            parameterSettingsLabel.Visible = true;
            measureFreqLabel.Visible = true;
            maxDeviationLabel.Visible = true;
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            var helpForm = new HelpForm();
            helpForm.Show();
        }

        private void AboutAuthorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"This program was created by Viachaslau Viarbitski, student of the group 951007.");
        }

        private void AboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"This program classifies arrays of values and build charts.");
        }
    }
}