using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace NFingers
{
  public class FConfigure : System.Windows.Forms.Form
  {
    public SConfig m_cfg;
    private Font fontKeypad;
    private Font fontNumpad;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TabControl tabConfig;
    private System.Windows.Forms.TabPage tabConfig_Keypad;
    private System.Windows.Forms.TabPage tabConfig_Numpad;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtKeypadFilename;
    private System.Windows.Forms.NumericUpDown nudKeypadLevelUp;
    private System.Windows.Forms.NumericUpDown nudKeypadLevelDown;
    private System.Windows.Forms.Button btnKeypadFilename;
    private System.Windows.Forms.OpenFileDialog openFileDialog;
    private System.Windows.Forms.FontDialog fontDialog;
    private System.Windows.Forms.Button btnKeypadFont;
    private System.Windows.Forms.Button btnNumpadFont;
    private System.Windows.Forms.Button btnNumpadFilename;
    private System.Windows.Forms.TextBox txtNumpadFilename;
    private System.Windows.Forms.NumericUpDown nudNumpadLevelDown;
    private System.Windows.Forms.NumericUpDown nudNumpadLevelUp;
    private System.Windows.Forms.Label lblKeypadFont;
    private System.Windows.Forms.Label lblNumpadFont;
    private System.Windows.Forms.NumericUpDown nudNumpadLevelDownError;
    private System.Windows.Forms.NumericUpDown nudKeypadLevelDownError;
    private System.Windows.Forms.GroupBox gbKeypadLevels;
    private System.Windows.Forms.GroupBox gbKeypadFile;
    private System.Windows.Forms.ToolTip tooltip;
    private System.Windows.Forms.GroupBox gbKeypadFont;
    private System.Windows.Forms.GroupBox gbNumpadFile;
    private System.Windows.Forms.GroupBox gbNumpadFont;
    private System.Windows.Forms.Label lblKeypadRiseLevel;
    private System.Windows.Forms.Label lblKeypadErrors;
    private System.Windows.Forms.Label lblKeypadSpeed;
    private System.Windows.Forms.Label lblKeypadHeader;
    private System.Windows.Forms.Label lblKeypadDecreaseLevel;
    private System.Windows.Forms.GroupBox gbNumpadLevels;
    private System.Windows.Forms.Label lblNumpadHeader;
    private System.Windows.Forms.Label lblNumpadErrors;
    private System.Windows.Forms.Label lblNumpadSpeed;
    private System.Windows.Forms.Label lblNumpadDecreaseLevel;
    private System.Windows.Forms.Label lblNumpadRiseLevel;
    private System.ComponentModel.IContainer components;

    public FConfigure()
    {
      InitializeComponent();
    }

    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.lblKeypadRiseLevel = new System.Windows.Forms.Label();
      this.gbKeypadLevels = new System.Windows.Forms.GroupBox();
      this.nudKeypadLevelDownError = new System.Windows.Forms.NumericUpDown();
      this.lblKeypadErrors = new System.Windows.Forms.Label();
      this.lblKeypadSpeed = new System.Windows.Forms.Label();
      this.lblKeypadHeader = new System.Windows.Forms.Label();
      this.nudKeypadLevelDown = new System.Windows.Forms.NumericUpDown();
      this.nudKeypadLevelUp = new System.Windows.Forms.NumericUpDown();
      this.lblKeypadDecreaseLevel = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.tabConfig = new System.Windows.Forms.TabControl();
      this.tabConfig_Keypad = new System.Windows.Forms.TabPage();
      this.gbKeypadFont = new System.Windows.Forms.GroupBox();
      this.lblKeypadFont = new System.Windows.Forms.Label();
      this.btnKeypadFont = new System.Windows.Forms.Button();
      this.gbKeypadFile = new System.Windows.Forms.GroupBox();
      this.btnKeypadFilename = new System.Windows.Forms.Button();
      this.txtKeypadFilename = new System.Windows.Forms.TextBox();
      this.tabConfig_Numpad = new System.Windows.Forms.TabPage();
      this.gbNumpadFont = new System.Windows.Forms.GroupBox();
      this.lblNumpadFont = new System.Windows.Forms.Label();
      this.btnNumpadFont = new System.Windows.Forms.Button();
      this.gbNumpadFile = new System.Windows.Forms.GroupBox();
      this.txtNumpadFilename = new System.Windows.Forms.TextBox();
      this.btnNumpadFilename = new System.Windows.Forms.Button();
      this.gbNumpadLevels = new System.Windows.Forms.GroupBox();
      this.nudNumpadLevelDownError = new System.Windows.Forms.NumericUpDown();
      this.lblNumpadHeader = new System.Windows.Forms.Label();
      this.lblNumpadErrors = new System.Windows.Forms.Label();
      this.lblNumpadSpeed = new System.Windows.Forms.Label();
      this.nudNumpadLevelDown = new System.Windows.Forms.NumericUpDown();
      this.nudNumpadLevelUp = new System.Windows.Forms.NumericUpDown();
      this.lblNumpadDecreaseLevel = new System.Windows.Forms.Label();
      this.lblNumpadRiseLevel = new System.Windows.Forms.Label();
      this.btnCancel = new System.Windows.Forms.Button();
      this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.fontDialog = new System.Windows.Forms.FontDialog();
      this.tooltip = new System.Windows.Forms.ToolTip(this.components);
      this.gbKeypadLevels.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudKeypadLevelDownError)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudKeypadLevelDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudKeypadLevelUp)).BeginInit();
      this.tabConfig.SuspendLayout();
      this.tabConfig_Keypad.SuspendLayout();
      this.gbKeypadFont.SuspendLayout();
      this.gbKeypadFile.SuspendLayout();
      this.tabConfig_Numpad.SuspendLayout();
      this.gbNumpadFont.SuspendLayout();
      this.gbNumpadFile.SuspendLayout();
      this.gbNumpadLevels.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudNumpadLevelDownError)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudNumpadLevelDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudNumpadLevelUp)).BeginInit();
      this.SuspendLayout();
      // 
      // lblKeypadRiseLevel
      // 
      this.lblKeypadRiseLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblKeypadRiseLevel.Location = new System.Drawing.Point(240, 24);
      this.lblKeypadRiseLevel.Name = "lblKeypadRiseLevel";
      this.lblKeypadRiseLevel.Size = new System.Drawing.Size(56, 16);
      this.lblKeypadRiseLevel.TabIndex = 2;
      this.lblKeypadRiseLevel.Text = "Maximum";
      // 
      // gbKeypadLevels
      // 
      this.gbKeypadLevels.Controls.Add(this.nudKeypadLevelDownError);
      this.gbKeypadLevels.Controls.Add(this.lblKeypadErrors);
      this.gbKeypadLevels.Controls.Add(this.lblKeypadSpeed);
      this.gbKeypadLevels.Controls.Add(this.lblKeypadHeader);
      this.gbKeypadLevels.Controls.Add(this.nudKeypadLevelDown);
      this.gbKeypadLevels.Controls.Add(this.nudKeypadLevelUp);
      this.gbKeypadLevels.Controls.Add(this.lblKeypadDecreaseLevel);
      this.gbKeypadLevels.Controls.Add(this.lblKeypadRiseLevel);
      this.gbKeypadLevels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbKeypadLevels.Location = new System.Drawing.Point(8, 8);
      this.gbKeypadLevels.Name = "gbKeypadLevels";
      this.gbKeypadLevels.Size = new System.Drawing.Size(336, 128);
      this.gbKeypadLevels.TabIndex = 0;
      this.gbKeypadLevels.TabStop = false;
      this.gbKeypadLevels.Text = "Level automatic adjustement limits:";
      // 
      // nudKeypadLevelDownError
      // 
      this.nudKeypadLevelDownError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.nudKeypadLevelDownError.Location = new System.Drawing.Point(240, 88);
      this.nudKeypadLevelDownError.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.nudKeypadLevelDownError.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudKeypadLevelDownError.Name = "nudKeypadLevelDownError";
      this.nudKeypadLevelDownError.Size = new System.Drawing.Size(56, 20);
      this.nudKeypadLevelDownError.TabIndex = 7;
      this.tooltip.SetToolTip(this.nudKeypadLevelDownError, "Level decrease if bigger than...");
      this.nudKeypadLevelDownError.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // lblKeypadErrors
      // 
      this.lblKeypadErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblKeypadErrors.Location = new System.Drawing.Point(24, 88);
      this.lblKeypadErrors.Name = "lblKeypadErrors";
      this.lblKeypadErrors.Size = new System.Drawing.Size(64, 16);
      this.lblKeypadErrors.TabIndex = 6;
      this.lblKeypadErrors.Text = "Errors (%)";
      this.tooltip.SetToolTip(this.lblKeypadErrors, "Decrease Level if bigger than...");
      // 
      // lblKeypadSpeed
      // 
      this.lblKeypadSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblKeypadSpeed.Location = new System.Drawing.Point(24, 56);
      this.lblKeypadSpeed.Name = "lblKeypadSpeed";
      this.lblKeypadSpeed.Size = new System.Drawing.Size(88, 16);
      this.lblKeypadSpeed.TabIndex = 3;
      this.lblKeypadSpeed.Text = "Speed (ch/min)";
      this.tooltip.SetToolTip(this.lblKeypadSpeed, "Change Level if out of bounds");
      this.lblKeypadSpeed.UseMnemonic = false;
      // 
      // lblKeypadHeader
      // 
      this.lblKeypadHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblKeypadHeader.Location = new System.Drawing.Point(24, 24);
      this.lblKeypadHeader.Name = "lblKeypadHeader";
      this.lblKeypadHeader.Size = new System.Drawing.Size(88, 16);
      this.lblKeypadHeader.TabIndex = 0;
      this.lblKeypadHeader.Text = "Type";
      // 
      // nudKeypadLevelDown
      // 
      this.nudKeypadLevelDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.nudKeypadLevelDown.Location = new System.Drawing.Point(144, 56);
      this.nudKeypadLevelDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudKeypadLevelDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudKeypadLevelDown.Name = "nudKeypadLevelDown";
      this.nudKeypadLevelDown.Size = new System.Drawing.Size(56, 20);
      this.nudKeypadLevelDown.TabIndex = 4;
      this.nudKeypadLevelDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudKeypadLevelDown.ValueChanged += new System.EventHandler(this.nudKeypadLevelDown_ValueChanged);
      // 
      // nudKeypadLevelUp
      // 
      this.nudKeypadLevelUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.nudKeypadLevelUp.Location = new System.Drawing.Point(240, 56);
      this.nudKeypadLevelUp.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudKeypadLevelUp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudKeypadLevelUp.Name = "nudKeypadLevelUp";
      this.nudKeypadLevelUp.Size = new System.Drawing.Size(56, 20);
      this.nudKeypadLevelUp.TabIndex = 5;
      this.nudKeypadLevelUp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudKeypadLevelUp.ValueChanged += new System.EventHandler(this.nudKeypadLevelUp_ValueChanged);
      // 
      // lblKeypadDecreaseLevel
      // 
      this.lblKeypadDecreaseLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblKeypadDecreaseLevel.Location = new System.Drawing.Point(144, 24);
      this.lblKeypadDecreaseLevel.Name = "lblKeypadDecreaseLevel";
      this.lblKeypadDecreaseLevel.Size = new System.Drawing.Size(56, 16);
      this.lblKeypadDecreaseLevel.TabIndex = 1;
      this.lblKeypadDecreaseLevel.Text = "Minimum";
      // 
      // btnOK
      // 
      this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnOK.Location = new System.Drawing.Point(184, 312);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "&OK";
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // tabConfig
      // 
      this.tabConfig.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
      this.tabConfig.Controls.Add(this.tabConfig_Keypad);
      this.tabConfig.Controls.Add(this.tabConfig_Numpad);
      this.tabConfig.Cursor = System.Windows.Forms.Cursors.Default;
      this.tabConfig.Dock = System.Windows.Forms.DockStyle.Top;
      this.tabConfig.Location = new System.Drawing.Point(0, 0);
      this.tabConfig.Multiline = true;
      this.tabConfig.Name = "tabConfig";
      this.tabConfig.SelectedIndex = 0;
      this.tabConfig.Size = new System.Drawing.Size(362, 296);
      this.tabConfig.TabIndex = 0;
      // 
      // tabConfig_Keypad
      // 
      this.tabConfig_Keypad.Controls.Add(this.gbKeypadFont);
      this.tabConfig_Keypad.Controls.Add(this.gbKeypadFile);
      this.tabConfig_Keypad.Controls.Add(this.gbKeypadLevels);
      this.tabConfig_Keypad.Location = new System.Drawing.Point(4, 25);
      this.tabConfig_Keypad.Name = "tabConfig_Keypad";
      this.tabConfig_Keypad.Size = new System.Drawing.Size(354, 267);
      this.tabConfig_Keypad.TabIndex = 0;
      this.tabConfig_Keypad.Text = "Keypad";
      this.tabConfig_Keypad.ToolTipText = "Keypad training";
      // 
      // gbKeypadFont
      // 
      this.gbKeypadFont.Controls.Add(this.lblKeypadFont);
      this.gbKeypadFont.Controls.Add(this.btnKeypadFont);
      this.gbKeypadFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbKeypadFont.Location = new System.Drawing.Point(8, 208);
      this.gbKeypadFont.Name = "gbKeypadFont";
      this.gbKeypadFont.Size = new System.Drawing.Size(336, 56);
      this.gbKeypadFont.TabIndex = 2;
      this.gbKeypadFont.TabStop = false;
      this.gbKeypadFont.Text = "Training Text Font:";
      // 
      // lblKeypadFont
      // 
      this.lblKeypadFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblKeypadFont.Location = new System.Drawing.Point(8, 24);
      this.lblKeypadFont.Name = "lblKeypadFont";
      this.lblKeypadFont.Size = new System.Drawing.Size(256, 24);
      this.lblKeypadFont.TabIndex = 0;
      // 
      // btnKeypadFont
      // 
      this.btnKeypadFont.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnKeypadFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnKeypadFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnKeypadFont.Location = new System.Drawing.Point(272, 24);
      this.btnKeypadFont.Name = "btnKeypadFont";
      this.btnKeypadFont.Size = new System.Drawing.Size(56, 23);
      this.btnKeypadFont.TabIndex = 1;
      this.btnKeypadFont.Text = "Change";
      this.btnKeypadFont.Click += new System.EventHandler(this.btnKeypadFont_Click);
      // 
      // gbKeypadFile
      // 
      this.gbKeypadFile.Controls.Add(this.btnKeypadFilename);
      this.gbKeypadFile.Controls.Add(this.txtKeypadFilename);
      this.gbKeypadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbKeypadFile.Location = new System.Drawing.Point(8, 144);
      this.gbKeypadFile.Name = "gbKeypadFile";
      this.gbKeypadFile.Size = new System.Drawing.Size(336, 56);
      this.gbKeypadFile.TabIndex = 1;
      this.gbKeypadFile.TabStop = false;
      this.gbKeypadFile.Text = "Keypad File:";
      // 
      // btnKeypadFilename
      // 
      this.btnKeypadFilename.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnKeypadFilename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnKeypadFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnKeypadFilename.Location = new System.Drawing.Point(272, 24);
      this.btnKeypadFilename.Name = "btnKeypadFilename";
      this.btnKeypadFilename.Size = new System.Drawing.Size(56, 23);
      this.btnKeypadFilename.TabIndex = 1;
      this.btnKeypadFilename.Text = "Change";
      this.btnKeypadFilename.Click += new System.EventHandler(this.btnKeypadFilename_Click);
      // 
      // txtKeypadFilename
      // 
      this.txtKeypadFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.txtKeypadFilename.Location = new System.Drawing.Point(8, 24);
      this.txtKeypadFilename.Name = "txtKeypadFilename";
      this.txtKeypadFilename.ReadOnly = true;
      this.txtKeypadFilename.Size = new System.Drawing.Size(256, 20);
      this.txtKeypadFilename.TabIndex = 0;
      // 
      // tabConfig_Numpad
      // 
      this.tabConfig_Numpad.Controls.Add(this.gbNumpadFont);
      this.tabConfig_Numpad.Controls.Add(this.gbNumpadFile);
      this.tabConfig_Numpad.Controls.Add(this.gbNumpadLevels);
      this.tabConfig_Numpad.Location = new System.Drawing.Point(4, 25);
      this.tabConfig_Numpad.Name = "tabConfig_Numpad";
      this.tabConfig_Numpad.Size = new System.Drawing.Size(354, 267);
      this.tabConfig_Numpad.TabIndex = 1;
      this.tabConfig_Numpad.Text = "Numpad";
      this.tabConfig_Numpad.ToolTipText = "Numpad training";
      // 
      // gbNumpadFont
      // 
      this.gbNumpadFont.Controls.Add(this.lblNumpadFont);
      this.gbNumpadFont.Controls.Add(this.btnNumpadFont);
      this.gbNumpadFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbNumpadFont.Location = new System.Drawing.Point(8, 208);
      this.gbNumpadFont.Name = "gbNumpadFont";
      this.gbNumpadFont.Size = new System.Drawing.Size(336, 56);
      this.gbNumpadFont.TabIndex = 2;
      this.gbNumpadFont.TabStop = false;
      this.gbNumpadFont.Text = "Training Text Font:";
      // 
      // lblNumpadFont
      // 
      this.lblNumpadFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblNumpadFont.Location = new System.Drawing.Point(8, 24);
      this.lblNumpadFont.Name = "lblNumpadFont";
      this.lblNumpadFont.Size = new System.Drawing.Size(256, 24);
      this.lblNumpadFont.TabIndex = 0;
      // 
      // btnNumpadFont
      // 
      this.btnNumpadFont.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnNumpadFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnNumpadFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnNumpadFont.Location = new System.Drawing.Point(272, 24);
      this.btnNumpadFont.Name = "btnNumpadFont";
      this.btnNumpadFont.Size = new System.Drawing.Size(56, 23);
      this.btnNumpadFont.TabIndex = 1;
      this.btnNumpadFont.Text = "Change";
      this.btnNumpadFont.Click += new System.EventHandler(this.btnNumpadFont_Click);
      // 
      // gbNumpadFile
      // 
      this.gbNumpadFile.Controls.Add(this.txtNumpadFilename);
      this.gbNumpadFile.Controls.Add(this.btnNumpadFilename);
      this.gbNumpadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbNumpadFile.Location = new System.Drawing.Point(8, 144);
      this.gbNumpadFile.Name = "gbNumpadFile";
      this.gbNumpadFile.Size = new System.Drawing.Size(336, 56);
      this.gbNumpadFile.TabIndex = 1;
      this.gbNumpadFile.TabStop = false;
      this.gbNumpadFile.Text = "Numpad File:";
      // 
      // txtNumpadFilename
      // 
      this.txtNumpadFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.txtNumpadFilename.Location = new System.Drawing.Point(8, 24);
      this.txtNumpadFilename.Name = "txtNumpadFilename";
      this.txtNumpadFilename.ReadOnly = true;
      this.txtNumpadFilename.Size = new System.Drawing.Size(256, 20);
      this.txtNumpadFilename.TabIndex = 0;
      // 
      // btnNumpadFilename
      // 
      this.btnNumpadFilename.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnNumpadFilename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnNumpadFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnNumpadFilename.Location = new System.Drawing.Point(272, 24);
      this.btnNumpadFilename.Name = "btnNumpadFilename";
      this.btnNumpadFilename.Size = new System.Drawing.Size(56, 23);
      this.btnNumpadFilename.TabIndex = 1;
      this.btnNumpadFilename.Text = "Change";
      this.btnNumpadFilename.Click += new System.EventHandler(this.btnNumpadFilename_Click);
      // 
      // gbNumpadLevels
      // 
      this.gbNumpadLevels.Controls.Add(this.nudNumpadLevelDownError);
      this.gbNumpadLevels.Controls.Add(this.lblNumpadHeader);
      this.gbNumpadLevels.Controls.Add(this.lblNumpadErrors);
      this.gbNumpadLevels.Controls.Add(this.lblNumpadSpeed);
      this.gbNumpadLevels.Controls.Add(this.nudNumpadLevelDown);
      this.gbNumpadLevels.Controls.Add(this.nudNumpadLevelUp);
      this.gbNumpadLevels.Controls.Add(this.lblNumpadDecreaseLevel);
      this.gbNumpadLevels.Controls.Add(this.lblNumpadRiseLevel);
      this.gbNumpadLevels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.gbNumpadLevels.Location = new System.Drawing.Point(8, 8);
      this.gbNumpadLevels.Name = "gbNumpadLevels";
      this.gbNumpadLevels.Size = new System.Drawing.Size(336, 128);
      this.gbNumpadLevels.TabIndex = 0;
      this.gbNumpadLevels.TabStop = false;
      this.gbNumpadLevels.Text = "Level automatic adjustement limits:";
      // 
      // nudNumpadLevelDownError
      // 
      this.nudNumpadLevelDownError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.nudNumpadLevelDownError.Location = new System.Drawing.Point(240, 88);
      this.nudNumpadLevelDownError.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
      this.nudNumpadLevelDownError.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudNumpadLevelDownError.Name = "nudNumpadLevelDownError";
      this.nudNumpadLevelDownError.Size = new System.Drawing.Size(56, 20);
      this.nudNumpadLevelDownError.TabIndex = 7;
      this.nudNumpadLevelDownError.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // lblNumpadHeader
      // 
      this.lblNumpadHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblNumpadHeader.Location = new System.Drawing.Point(24, 24);
      this.lblNumpadHeader.Name = "lblNumpadHeader";
      this.lblNumpadHeader.Size = new System.Drawing.Size(72, 16);
      this.lblNumpadHeader.TabIndex = 0;
      this.lblNumpadHeader.Text = "Type";
      // 
      // lblNumpadErrors
      // 
      this.lblNumpadErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblNumpadErrors.Location = new System.Drawing.Point(24, 88);
      this.lblNumpadErrors.Name = "lblNumpadErrors";
      this.lblNumpadErrors.Size = new System.Drawing.Size(72, 16);
      this.lblNumpadErrors.TabIndex = 6;
      this.lblNumpadErrors.Text = "Errors (%)";
      this.tooltip.SetToolTip(this.lblNumpadErrors, "Decrease Level if bigger than...");
      // 
      // lblNumpadSpeed
      // 
      this.lblNumpadSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblNumpadSpeed.Location = new System.Drawing.Point(24, 56);
      this.lblNumpadSpeed.Name = "lblNumpadSpeed";
      this.lblNumpadSpeed.Size = new System.Drawing.Size(88, 16);
      this.lblNumpadSpeed.TabIndex = 3;
      this.lblNumpadSpeed.Text = "Speed (ch/min)";
      this.tooltip.SetToolTip(this.lblNumpadSpeed, "Change Level if out of bounds");
      // 
      // nudNumpadLevelDown
      // 
      this.nudNumpadLevelDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.nudNumpadLevelDown.Location = new System.Drawing.Point(144, 56);
      this.nudNumpadLevelDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudNumpadLevelDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudNumpadLevelDown.Name = "nudNumpadLevelDown";
      this.nudNumpadLevelDown.Size = new System.Drawing.Size(56, 20);
      this.nudNumpadLevelDown.TabIndex = 4;
      this.nudNumpadLevelDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudNumpadLevelDown.ValueChanged += new System.EventHandler(this.nudNumpadLevelDown_ValueChanged);
      // 
      // nudNumpadLevelUp
      // 
      this.nudNumpadLevelUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.nudNumpadLevelUp.Location = new System.Drawing.Point(240, 56);
      this.nudNumpadLevelUp.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
      this.nudNumpadLevelUp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudNumpadLevelUp.Name = "nudNumpadLevelUp";
      this.nudNumpadLevelUp.Size = new System.Drawing.Size(56, 20);
      this.nudNumpadLevelUp.TabIndex = 5;
      this.nudNumpadLevelUp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.nudNumpadLevelUp.ValueChanged += new System.EventHandler(this.nudNumpadLevelUp_ValueChanged);
      // 
      // lblNumpadDecreaseLevel
      // 
      this.lblNumpadDecreaseLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblNumpadDecreaseLevel.Location = new System.Drawing.Point(144, 24);
      this.lblNumpadDecreaseLevel.Name = "lblNumpadDecreaseLevel";
      this.lblNumpadDecreaseLevel.Size = new System.Drawing.Size(56, 16);
      this.lblNumpadDecreaseLevel.TabIndex = 1;
      this.lblNumpadDecreaseLevel.Text = "Minimum";
      // 
      // lblNumpadRiseLevel
      // 
      this.lblNumpadRiseLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblNumpadRiseLevel.Location = new System.Drawing.Point(240, 24);
      this.lblNumpadRiseLevel.Name = "lblNumpadRiseLevel";
      this.lblNumpadRiseLevel.Size = new System.Drawing.Size(56, 16);
      this.lblNumpadRiseLevel.TabIndex = 2;
      this.lblNumpadRiseLevel.Text = "Maximum";
      // 
      // btnCancel
      // 
      this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnCancel.Location = new System.Drawing.Point(272, 312);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // openFileDialog
      // 
      this.openFileDialog.AddExtension = false;
      this.openFileDialog.Filter = "All files|*";
      this.openFileDialog.InitialDirectory = "."+ System.IO.Path.DirectorySeparatorChar;
      // 
      // FConfigure
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(362, 352);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.tabConfig);
      this.Controls.Add(this.btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FConfigure";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Fingers Configuration";
      this.Load += new System.EventHandler(this.FConfigure_Load);
      this.gbKeypadLevels.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudKeypadLevelDownError)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudKeypadLevelDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudKeypadLevelUp)).EndInit();
      this.tabConfig.ResumeLayout(false);
      this.tabConfig_Keypad.ResumeLayout(false);
      this.gbKeypadFont.ResumeLayout(false);
      this.gbKeypadFile.ResumeLayout(false);
      this.tabConfig_Numpad.ResumeLayout(false);
      this.gbNumpadFont.ResumeLayout(false);
      this.gbNumpadFile.ResumeLayout(false);
      this.gbNumpadLevels.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.nudNumpadLevelDownError)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudNumpadLevelDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudNumpadLevelUp)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    private void FConfigure_Load(object sender, System.EventArgs e)
    {
      InitFields();
    }

    private void InitFields()
    {
      nudKeypadLevelUp.Value = m_cfg.iKeypadLevelSpeedUp;
      nudKeypadLevelDown.Value = m_cfg.iKeypadLevelSdeedDown;
      nudKeypadLevelDownError.Value = m_cfg.iKeypadLevelErrorDown;
      txtKeypadFilename.Text = m_cfg.strKeypadFilename;
      lblKeypadFont.Text = m_cfg.fontKeypad.Name + "; " + m_cfg.fontKeypad.Size;
      lblKeypadFont.Font = new Font(m_cfg.fontKeypad.Name, 10f);
      fontKeypad =  m_cfg.fontKeypad;

      nudNumpadLevelUp.Value = m_cfg.iNumpadLevelSpeedUp;
      nudNumpadLevelDown.Value = m_cfg.iNumpadLevelSpeedDown;
      nudNumpadLevelDownError.Value = m_cfg.iNumpadLevelErrorDown;
      txtNumpadFilename.Text = m_cfg.strNumpadFilename;
      lblNumpadFont.Text = m_cfg.fontNumpad.Name + "; " + m_cfg.fontNumpad.Size;
      lblNumpadFont.Font = new Font(m_cfg.fontNumpad.Name, 10f);
      fontNumpad = m_cfg.fontNumpad;

      tabConfig.SelectedTab = ((m_cfg.eMode == EMode.Keypad)? tabConfig_Keypad : tabConfig_Numpad);
    }

    private void GetFields()
    {
      try
      {
        m_cfg.iKeypadLevelSpeedUp = Convert.ToInt32(nudKeypadLevelUp.Value);
        m_cfg.iKeypadLevelSdeedDown = Convert.ToInt32(nudKeypadLevelDown.Value);
        m_cfg.iKeypadLevelErrorDown = Convert.ToInt32(nudKeypadLevelDownError.Value);
        m_cfg.strKeypadFilename = txtKeypadFilename.Text;
        m_cfg.fontKeypad = fontKeypad;

        m_cfg.iNumpadLevelSpeedUp = Convert.ToInt32(nudNumpadLevelUp.Value);
        m_cfg.iNumpadLevelSpeedDown = Convert.ToInt32(nudNumpadLevelDown.Value);
        m_cfg.iNumpadLevelErrorDown = Convert.ToInt32(nudNumpadLevelDownError.Value);
        m_cfg.strNumpadFilename = txtNumpadFilename.Text;
        m_cfg.fontNumpad = fontNumpad;

        m_cfg.eMode = ((tabConfig.SelectedTab == tabConfig_Keypad)? EMode.Keypad : EMode.Numpad);
      }
      catch (Exception xcp)
      {
        throw new ApplicationException(xcp.Message, xcp);
      }
    }

    private void btnKeypadFilename_Click(object sender, System.EventArgs e)
    {
      DialogResult dr;
      dr = openFileDialog.ShowDialog(this);
      if (dr == DialogResult.OK)
      {
        txtKeypadFilename.Text = openFileDialog.FileName;
      }
    }

    private void btnNumpadFilename_Click(object sender, System.EventArgs e)
    {
      DialogResult dr;
      dr = openFileDialog.ShowDialog(this);
      if (dr == DialogResult.OK)
      {
        txtNumpadFilename.Text = openFileDialog.FileName;
      }
    }

    private void btnKeypadFont_Click(object sender, System.EventArgs e)
    {
      fontDialog.Font = fontKeypad;
      DialogResult dr = fontDialog.ShowDialog(this);
      if (dr == DialogResult.OK)
      {
        try
        {
          Font font = fontDialog.Font;
          lblKeypadFont.Font = new Font(font.Name, 10f);
          fontKeypad = font;
          lblKeypadFont.Text = font.Name + "; " + font.Size;
        }
        catch (ArgumentException xcp)
        {
          string n = Environment.NewLine;
          MessageBox.Show("Incompatible Font" + n
            + n + "Error details: " + n + xcp.Message
            , "Sorry for inconvenience"
            , MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
    }

    private void btnNumpadFont_Click(object sender, System.EventArgs e)
    {
      fontDialog.Font = fontNumpad;
      DialogResult dr = fontDialog.ShowDialog(this);
      if (dr == DialogResult.OK)
      {
        try
        {
          Font font = fontDialog.Font;
          lblNumpadFont.Font = new Font(font.Name, 10f);
          fontNumpad = font;
          lblNumpadFont.Text = font.Name + "; " + font.Size;
        }
        catch (ArgumentException xcp)
        {
          string n = Environment.NewLine;
          MessageBox.Show("Incompatible Font" + n
            + n + "Error details: " + n + xcp.Message
            , "Sorry for inconvenience"
            , MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
    }

    private void btnCancel_Click(object sender, System.EventArgs e)
    {
    }

    private void btnOK_Click(object sender, System.EventArgs e)
    {
      GetFields();
      this.Close();
    }

    private void nudKeypadLevelDown_ValueChanged(object sender, EventArgs e)
    {
      decimal delta = nudKeypadLevelUp.Value - nudKeypadLevelDown.Value;

      if (delta > 100)
      {
        nudKeypadLevelUp.Value = nudKeypadLevelDown.Value + 100;
      }
      else if (delta < 10)
      {
        nudKeypadLevelDown.Value = nudKeypadLevelUp.Value - 10;
      }
    }

    private void nudKeypadLevelUp_ValueChanged(object sender, EventArgs e)
    {
      decimal delta = nudKeypadLevelUp.Value - nudKeypadLevelDown.Value;

      if (delta > 100)
      {
        nudKeypadLevelDown.Value = nudKeypadLevelUp.Value - 100;
      }
      else if (delta < 10)
      {
        nudKeypadLevelUp.Value = nudKeypadLevelDown.Value + 10;
      }
    }

    private void nudNumpadLevelDown_ValueChanged(object sender, EventArgs e)
    {
      decimal delta = nudNumpadLevelUp.Value - nudNumpadLevelDown.Value;

      if (delta > 100)
      {
        nudNumpadLevelUp.Value = nudNumpadLevelDown.Value + 100;
      }
      else if (delta < 10)
      {
        nudNumpadLevelDown.Value = nudNumpadLevelUp.Value - 10;
      }
    }

    private void nudNumpadLevelUp_ValueChanged(object sender, EventArgs e)
    {
      decimal delta = nudNumpadLevelUp.Value - nudNumpadLevelDown.Value;

      if (delta > 100)
      {
        nudNumpadLevelDown.Value = nudNumpadLevelUp.Value - 100;
      }
      else if (delta < 10)
      {
        nudNumpadLevelUp.Value = nudNumpadLevelDown.Value + 10;
      }
    }
  };
}
