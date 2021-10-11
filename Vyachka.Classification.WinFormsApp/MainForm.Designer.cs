namespace Vyachka.Classification.WinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buildChartButton = new System.Windows.Forms.Button();
            this.valuesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.isNeedToDrawAsClassifiedCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileDateTimeLabel = new System.Windows.Forms.Label();
            this.parametersComboBox = new System.Windows.Forms.ComboBox();
            this.parameterSettingsLabel = new System.Windows.Forms.Label();
            this.measureFreqLabel = new System.Windows.Forms.Label();
            this.maxDeviationLabel = new System.Windows.Forms.Label();
            this.openFileButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.recordingLabel = new System.Windows.Forms.Label();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAuthorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenu = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.valuesChart)).BeginInit();
            this.topMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // buildChartButton
            // 
            resources.ApplyResources(this.buildChartButton, "buildChartButton");
            this.buildChartButton.Name = "buildChartButton";
            this.buildChartButton.UseVisualStyleBackColor = true;
            this.buildChartButton.Click += new System.EventHandler(this.BuildChartButton_Click);
            // 
            // valuesChart
            // 
            this.valuesChart.BackColor = System.Drawing.SystemColors.Window;
            this.valuesChart.BorderlineColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.valuesChart.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.valuesChart.BorderlineWidth = 2;
            chartArea1.Name = "ChartArea1";
            this.valuesChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.valuesChart.Legends.Add(legend1);
            resources.ApplyResources(this.valuesChart, "valuesChart");
            this.valuesChart.Name = "valuesChart";
            // 
            // isNeedToDrawAsClassifiedCheckBox
            // 
            resources.ApplyResources(this.isNeedToDrawAsClassifiedCheckBox, "isNeedToDrawAsClassifiedCheckBox");
            this.isNeedToDrawAsClassifiedCheckBox.Name = "isNeedToDrawAsClassifiedCheckBox";
            this.isNeedToDrawAsClassifiedCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // fileDateTimeLabel
            // 
            resources.ApplyResources(this.fileDateTimeLabel, "fileDateTimeLabel");
            this.fileDateTimeLabel.Name = "fileDateTimeLabel";
            // 
            // parametersComboBox
            // 
            resources.ApplyResources(this.parametersComboBox, "parametersComboBox");
            this.parametersComboBox.Name = "parametersComboBox";
            this.parametersComboBox.Sorted = true;
            this.parametersComboBox.SelectedValueChanged += new System.EventHandler(this.ParametersComboBox_SelectedValueChanged);
            // 
            // parameterSettingsLabel
            // 
            resources.ApplyResources(this.parameterSettingsLabel, "parameterSettingsLabel");
            this.parameterSettingsLabel.Name = "parameterSettingsLabel";
            // 
            // measureFreqLabel
            // 
            resources.ApplyResources(this.measureFreqLabel, "measureFreqLabel");
            this.measureFreqLabel.Name = "measureFreqLabel";
            // 
            // maxDeviationLabel
            // 
            resources.ApplyResources(this.maxDeviationLabel, "maxDeviationLabel");
            this.maxDeviationLabel.Name = "maxDeviationLabel";
            // 
            // openFileButton
            // 
            resources.ApplyResources(this.openFileButton, "openFileButton");
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // helpButton
            // 
            resources.ApplyResources(this.helpButton, "helpButton");
            this.helpButton.Name = "helpButton";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // exitButton
            // 
            resources.ApplyResources(this.exitButton, "exitButton");
            this.exitButton.Name = "exitButton";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // recordingLabel
            // 
            resources.ApplyResources(this.recordingLabel, "recordingLabel");
            this.recordingLabel.Name = "recordingLabel";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.exitToolStripMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            resources.ApplyResources(this.fileMenuItem, "fileMenuItem");
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.OpenMenuItem, "OpenMenuItem");
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutAuthorToolStripMenuItem,
            this.aboutProgramToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            // 
            // aboutAuthorToolStripMenuItem
            // 
            this.aboutAuthorToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.aboutAuthorToolStripMenuItem, "aboutAuthorToolStripMenuItem");
            this.aboutAuthorToolStripMenuItem.Name = "aboutAuthorToolStripMenuItem";
            this.aboutAuthorToolStripMenuItem.Click += new System.EventHandler(this.AboutAuthorToolStripMenuItem_Click);
            // 
            // aboutProgramToolStripMenuItem
            // 
            this.aboutProgramToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.aboutProgramToolStripMenuItem, "aboutProgramToolStripMenuItem");
            this.aboutProgramToolStripMenuItem.Name = "aboutProgramToolStripMenuItem";
            this.aboutProgramToolStripMenuItem.Click += new System.EventHandler(this.AboutProgramToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // topMenu
            // 
            this.topMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.topMenu, "topMenu");
            this.topMenu.Name = "topMenu";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.recordingLabel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.maxDeviationLabel);
            this.Controls.Add(this.measureFreqLabel);
            this.Controls.Add(this.parameterSettingsLabel);
            this.Controls.Add(this.parametersComboBox);
            this.Controls.Add(this.fileDateTimeLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.isNeedToDrawAsClassifiedCheckBox);
            this.Controls.Add(this.valuesChart);
            this.Controls.Add(this.buildChartButton);
            this.Controls.Add(this.topMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.topMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.valuesChart)).EndInit();
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buildChartButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart valuesChart;
        private System.Windows.Forms.CheckBox isNeedToDrawAsClassifiedCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileDateTimeLabel;
        private System.Windows.Forms.ComboBox parametersComboBox;
        private System.Windows.Forms.Label parameterSettingsLabel;
        private System.Windows.Forms.Label measureFreqLabel;
        private System.Windows.Forms.Label maxDeviationLabel;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label recordingLabel;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAuthorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip topMenu;
    }
}

