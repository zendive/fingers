using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using NCurveGram;

namespace NFingers
{
  public class FFingers: System.Windows.Forms.Form
  {
    public static Regex reProductVersion = new Regex(@"(\d+\.\d+.\d+).*", RegexOptions.Compiled);
    public static string strProductVersion = "";

    private CTeacher m_teacher = null;
    private bool m_bBadDeal = false;
    private string m_strBeforePauseMode;
    private string m_strPauseMode = "... PAUSED, press [Esc] ...";
    private Label m_lblFingerOn = null;
    private Label m_lblFingerTo = null;
    private Color m_clrFingerTo;
    private Color m_clrPrevShift;
    private Label m_lblPrevShift;
    private string m_strFingerOn;
    private string m_strFingerTo;
    
    private System.Windows.Forms.Panel panelKeypad;
    private System.Windows.Forms.TextBox txtTarget;
    private System.Windows.Forms.MenuItem menuFile;
    private System.Windows.Forms.MenuItem menuFile_Exit;
    private System.Windows.Forms.MainMenu mainMenu;
    private System.Windows.Forms.MenuItem menuView;
    private System.Windows.Forms.MenuItem menuView_Configure;
    private System.Windows.Forms.Label lblSpeed;
    private System.Windows.Forms.Label lblErrors;
    private System.Windows.Forms.Label lblLevelSpeedDown;
    private System.Windows.Forms.Label lblLevelSpeedUp;
    private System.Windows.Forms.Label lblLevelErrorDown;
    private System.Windows.Forms.Panel panelNumpad;
    private System.Windows.Forms.Label kEnt;
    private System.Windows.Forms.Label kLsh;
    private System.Windows.Forms.Label kLbs;
    private System.Windows.Forms.Label kSpace;
    private System.Windows.Forms.Label kRsh;
    private System.Windows.Forms.Label kRs;
    private System.Windows.Forms.Label kDot;
    private System.Windows.Forms.Label kCom;
    private System.Windows.Forms.Label kM;
    private System.Windows.Forms.Label kN;
    private System.Windows.Forms.Label kB;
    private System.Windows.Forms.Label kV;
    private System.Windows.Forms.Label kC;
    private System.Windows.Forms.Label kX;
    private System.Windows.Forms.Label kZ;
    private System.Windows.Forms.Label kRbs;
    private System.Windows.Forms.Label kAca;
    private System.Windows.Forms.Label kSmi;
    private System.Windows.Forms.Label kL;
    private System.Windows.Forms.Label kK;
    private System.Windows.Forms.Label kJ;
    private System.Windows.Forms.Label kH;
    private System.Windows.Forms.Label kG;
    private System.Windows.Forms.Label kF;
    private System.Windows.Forms.Label kD;
    private System.Windows.Forms.Label kS;
    private System.Windows.Forms.Label kA;
    private System.Windows.Forms.Label kCap;
    private System.Windows.Forms.Label kLsb;
    private System.Windows.Forms.Label kP;
    private System.Windows.Forms.Label kO;
    private System.Windows.Forms.Label kI;
    private System.Windows.Forms.Label kU;
    private System.Windows.Forms.Label kY;
    private System.Windows.Forms.Label kT;
    private System.Windows.Forms.Label kR;
    private System.Windows.Forms.Label kE;
    private System.Windows.Forms.Label kW;
    private System.Windows.Forms.Label kQ;
    private System.Windows.Forms.Label kTab;
    private System.Windows.Forms.Label kBsp;
    private System.Windows.Forms.Label kEqu;
    private System.Windows.Forms.Label kMin;
    private System.Windows.Forms.Label k0;
    private System.Windows.Forms.Label k9;
    private System.Windows.Forms.Label k8;
    private System.Windows.Forms.Label k7;
    private System.Windows.Forms.Label k6;
    private System.Windows.Forms.Label k5;
    private System.Windows.Forms.Label k4;
    private System.Windows.Forms.Label k3;
    private System.Windows.Forms.Label k2;
    private System.Windows.Forms.Label k1;
    private System.Windows.Forms.Label kAp;
    private System.Windows.Forms.Label nDot;
    private System.Windows.Forms.Label n2;
    private System.Windows.Forms.Label n0;
    private System.Windows.Forms.Label nEnt;
    private System.Windows.Forms.Label nPls;
    private System.Windows.Forms.Label n3;
    private System.Windows.Forms.Label n1;
    private System.Windows.Forms.Label n6;
    private System.Windows.Forms.Label n5;
    private System.Windows.Forms.Label n4;
    private System.Windows.Forms.Label n9;
    private System.Windows.Forms.Label n8;
    private System.Windows.Forms.Label n7;
    private System.Windows.Forms.Label nMin;
    private System.Windows.Forms.Label nMul;
    private System.Windows.Forms.Label nDiv;
    private System.Windows.Forms.Label nNumLock;
    private System.Windows.Forms.Label lblLevelError0;
    private System.Windows.Forms.Label kRsb;
    private System.Windows.Forms.ProgressBar pbLevel;
    private System.Windows.Forms.MenuItem menuLevel;
    private System.Windows.Forms.MenuItem menuAbout;
    private System.Windows.Forms.MenuItem menuLevel_Restart;
    private System.Windows.Forms.MenuItem menuLevel_Separator1;
    private CC_CurveGram m_cgError;
    private Label lblErrorTitle;
    private Label lblSpeedTitle;
    private CC_CurveGram m_cgSpeed;
    private Panel panelSpeed;
    private Panel panelError;
    private IContainer components;

    [STAThread]
    static void Main()
    {
      try
      {
        Application.EnableVisualStyles();
        FFingers.strProductVersion = Application.ProductName + " v"
          + FFingers.reProductVersion.Replace(Application.ProductVersion, "$1");
        Application.Run(new FFingers());
      }
#if DEBUG
      catch (Exception xcp)
      {
        DEBUG.DUMP_EXCEPTION(xcp);
      }
#else
      catch (Exception)
      {
        MessageBox.Show("Program terminated unexpectedly."
          + Environment.NewLine
          + "Please try to repair this installation."
          , "Fingers"
          , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
#endif
    }

    public FFingers()
    {
      InitializeComponent();
      InitTask();
    }

    public void InitTask()
    {
      SConfig cfg;
      
      if (m_teacher == null)
      {
        cfg = CConfig.ReadConfig();
        cfg.eMode = EMode.Keypad;
      }
      else
      {
        cfg = m_teacher.Config;
      }
      m_teacher = new CTeacher(cfg);

      m_teacher.OnBadAnswer += new NFingers.CTeacher.OnBadAnswerDelegate(m_teacher_OnBadAnswer);
      m_teacher.OnGoodAnswer += new NFingers.CTeacher.OnGoodAnswerDelegate(m_teacher_OnGoodAnswer);
      m_teacher.OnNewLine += new NFingers.CTeacher.OnNewLineDelegate(m_teacher_OnNewLine);
      m_teacher.OnNewLevel += new NFingers.CTeacher.OnNewLevelDelegate(m_teacher_OnNewLevel);

      m_teacher.Init();

      if (0 == System.IO.Path.DirectorySeparatorChar.CompareTo('/'))
      { // lazy detection of "mono on linux"
        // looks like that boddy hides left part of the text if it not fitts into "some"
        // visible control area while text-align is centered, so make it Left permanently
        // for `EMode.Keypad` mode only
        if (cfg.eMode == EMode.Keypad)
        {
          txtTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
        }
        else
        {
          txtTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        }
      }

      char chOnTarget;
      txtTarget.Text = m_teacher.RefreshTaskForward(out chOnTarget);
      Feedback(chOnTarget);
      txtTarget.Font = cfg.Font;
      txtTarget.BackColor = Color.White;

      lblSpeed.Text = "0";
      lblLevelSpeedDown.Text = cfg.LevelSpeedDown.ToString();
      lblLevelSpeedUp.Text = cfg.LevelSpeedUp.ToString();
      m_cgSpeed.Maximum = cfg.LevelSpeedUp;
      m_cgSpeed.Minimum = cfg.LevelSpeedDown;
      m_cgSpeed.Value = m_cgSpeed.Minimum;
      m_cgSpeed.AlarmConditionMin = m_cgSpeed.Minimum;
      m_cgSpeed.AlarmConditionMax = m_cgSpeed.Minimum;
      m_cgSpeed.EnableAlarmCondition = true;

      lblErrors.Text = "0";
      lblLevelErrorDown.Text = cfg.LevelErrorDown.ToString();

      m_cgError.Maximum = cfg.LevelErrorDown*10;
      m_cgError.Minimum = 0;
      m_cgError.Value = m_cgError.Minimum*10;
      m_cgError.AlarmConditionMin = m_cgError.Maximum;
      m_cgError.AlarmConditionMax = m_cgError.Maximum;
      m_cgError.EnableAlarmCondition = true;

      if (cfg.eMode == EMode.Keypad)
      {
        panelNumpad.Visible = false;
        panelKeypad.Visible = true;
        panelError.Location = new Point(panelError.Location.X, 230);
        panelSpeed.Location = new Point(panelSpeed.Location.X, 230);
      }
      else
      {
        panelNumpad.Visible = true;
        panelKeypad.Visible = false;
        panelError.Location = new Point(panelError.Location.X, 72);
        panelSpeed.Location = new Point(panelSpeed.Location.X, 72);
      }
      
      InitLevelSelectionMenu();
    }
    
    ///<summary>init menu of level selection</summary>
    private void InitLevelSelectionMenu()
    {
      // get first separator index
      int iSep = menuLevel.MenuItems.IndexOf(menuLevel_Separator1);
      // remove the rest
      //@note: assume that after `menuLevel_Separator1` are dinamic items only
      // `*.Count` is decreasing )
      while ( ((menuLevel.MenuItems.Count - 1) - iSep) > 0 )
      {
        menuLevel.MenuItems.RemoveAt(iSep + 1);
      }
      
      // append new ones to the end of submenu
      for (int c = 0; c < CLevels.LevelsCount; c++)
      {
        menuLevel.MenuItems.Add((c + 1).ToString()
          , new EventHandler(menuLevel_submenuItem_Click));
      }
    }

    /// <summary>
    /// Whent the control is activated, text is selected and/or carret at the end of selection
    /// Here the method to return the carret at the beginning, removing selection
    /// </summary>
    private void FFingers_Activated(object sender, System.EventArgs e)
    {
      txtTarget.Focus();            // set focus before initialization
      txtTarget.SelectionStart = 0;
      txtTarget.SelectionLength = 0;
      txtTarget.Select(0, 0);
      txtTarget.ScrollToCaret();
    }

    private void FFingers_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      CConfig.SaveConfig(m_teacher.Config);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FFingers));
      this.panelKeypad = new System.Windows.Forms.Panel();
      this.kEnt = new System.Windows.Forms.Label();
      this.kLsh = new System.Windows.Forms.Label();
      this.kLbs = new System.Windows.Forms.Label();
      this.kSpace = new System.Windows.Forms.Label();
      this.kRsh = new System.Windows.Forms.Label();
      this.kRs = new System.Windows.Forms.Label();
      this.kDot = new System.Windows.Forms.Label();
      this.kCom = new System.Windows.Forms.Label();
      this.kM = new System.Windows.Forms.Label();
      this.kN = new System.Windows.Forms.Label();
      this.kB = new System.Windows.Forms.Label();
      this.kV = new System.Windows.Forms.Label();
      this.kC = new System.Windows.Forms.Label();
      this.kX = new System.Windows.Forms.Label();
      this.kZ = new System.Windows.Forms.Label();
      this.kRbs = new System.Windows.Forms.Label();
      this.kAca = new System.Windows.Forms.Label();
      this.kSmi = new System.Windows.Forms.Label();
      this.kL = new System.Windows.Forms.Label();
      this.kK = new System.Windows.Forms.Label();
      this.kJ = new System.Windows.Forms.Label();
      this.kH = new System.Windows.Forms.Label();
      this.kG = new System.Windows.Forms.Label();
      this.kF = new System.Windows.Forms.Label();
      this.kD = new System.Windows.Forms.Label();
      this.kS = new System.Windows.Forms.Label();
      this.kA = new System.Windows.Forms.Label();
      this.kCap = new System.Windows.Forms.Label();
      this.kRsb = new System.Windows.Forms.Label();
      this.kLsb = new System.Windows.Forms.Label();
      this.kP = new System.Windows.Forms.Label();
      this.kO = new System.Windows.Forms.Label();
      this.kI = new System.Windows.Forms.Label();
      this.kU = new System.Windows.Forms.Label();
      this.kY = new System.Windows.Forms.Label();
      this.kT = new System.Windows.Forms.Label();
      this.kR = new System.Windows.Forms.Label();
      this.kE = new System.Windows.Forms.Label();
      this.kW = new System.Windows.Forms.Label();
      this.kQ = new System.Windows.Forms.Label();
      this.kTab = new System.Windows.Forms.Label();
      this.kBsp = new System.Windows.Forms.Label();
      this.kEqu = new System.Windows.Forms.Label();
      this.kMin = new System.Windows.Forms.Label();
      this.k0 = new System.Windows.Forms.Label();
      this.k9 = new System.Windows.Forms.Label();
      this.k8 = new System.Windows.Forms.Label();
      this.k7 = new System.Windows.Forms.Label();
      this.k6 = new System.Windows.Forms.Label();
      this.k5 = new System.Windows.Forms.Label();
      this.k4 = new System.Windows.Forms.Label();
      this.k3 = new System.Windows.Forms.Label();
      this.k2 = new System.Windows.Forms.Label();
      this.k1 = new System.Windows.Forms.Label();
      this.kAp = new System.Windows.Forms.Label();
      this.lblLevelSpeedDown = new System.Windows.Forms.Label();
      this.lblLevelSpeedUp = new System.Windows.Forms.Label();
      this.lblSpeed = new System.Windows.Forms.Label();
      this.lblSpeedTitle = new System.Windows.Forms.Label();
      this.lblLevelErrorDown = new System.Windows.Forms.Label();
      this.lblErrors = new System.Windows.Forms.Label();
      this.lblErrorTitle = new System.Windows.Forms.Label();
      this.lblLevelError0 = new System.Windows.Forms.Label();
      this.txtTarget = new System.Windows.Forms.TextBox();
      this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
      this.menuFile = new System.Windows.Forms.MenuItem();
      this.menuFile_Exit = new System.Windows.Forms.MenuItem();
      this.menuView = new System.Windows.Forms.MenuItem();
      this.menuView_Configure = new System.Windows.Forms.MenuItem();
      this.menuLevel = new System.Windows.Forms.MenuItem();
      this.menuLevel_Restart = new System.Windows.Forms.MenuItem();
      this.menuLevel_Separator1 = new System.Windows.Forms.MenuItem();
      this.menuAbout = new System.Windows.Forms.MenuItem();
      this.panelNumpad = new System.Windows.Forms.Panel();
      this.nDot = new System.Windows.Forms.Label();
      this.n2 = new System.Windows.Forms.Label();
      this.n0 = new System.Windows.Forms.Label();
      this.nEnt = new System.Windows.Forms.Label();
      this.nPls = new System.Windows.Forms.Label();
      this.n3 = new System.Windows.Forms.Label();
      this.n1 = new System.Windows.Forms.Label();
      this.n6 = new System.Windows.Forms.Label();
      this.n5 = new System.Windows.Forms.Label();
      this.n4 = new System.Windows.Forms.Label();
      this.n9 = new System.Windows.Forms.Label();
      this.n8 = new System.Windows.Forms.Label();
      this.n7 = new System.Windows.Forms.Label();
      this.nMin = new System.Windows.Forms.Label();
      this.nMul = new System.Windows.Forms.Label();
      this.nDiv = new System.Windows.Forms.Label();
      this.nNumLock = new System.Windows.Forms.Label();
      this.pbLevel = new System.Windows.Forms.ProgressBar();
      this.panelSpeed = new System.Windows.Forms.Panel();
      this.m_cgSpeed = new NCurveGram.CC_CurveGram();
      this.panelError = new System.Windows.Forms.Panel();
      this.m_cgError = new NCurveGram.CC_CurveGram();
      this.panelKeypad.SuspendLayout();
      this.panelNumpad.SuspendLayout();
      this.panelSpeed.SuspendLayout();
      this.panelError.SuspendLayout();
      this.SuspendLayout();
      // 
      // panelKeypad
      // 
      this.panelKeypad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.panelKeypad.Controls.Add(this.kEnt);
      this.panelKeypad.Controls.Add(this.kLsh);
      this.panelKeypad.Controls.Add(this.kLbs);
      this.panelKeypad.Controls.Add(this.kSpace);
      this.panelKeypad.Controls.Add(this.kRsh);
      this.panelKeypad.Controls.Add(this.kRs);
      this.panelKeypad.Controls.Add(this.kDot);
      this.panelKeypad.Controls.Add(this.kCom);
      this.panelKeypad.Controls.Add(this.kM);
      this.panelKeypad.Controls.Add(this.kN);
      this.panelKeypad.Controls.Add(this.kB);
      this.panelKeypad.Controls.Add(this.kV);
      this.panelKeypad.Controls.Add(this.kC);
      this.panelKeypad.Controls.Add(this.kX);
      this.panelKeypad.Controls.Add(this.kZ);
      this.panelKeypad.Controls.Add(this.kRbs);
      this.panelKeypad.Controls.Add(this.kAca);
      this.panelKeypad.Controls.Add(this.kSmi);
      this.panelKeypad.Controls.Add(this.kL);
      this.panelKeypad.Controls.Add(this.kK);
      this.panelKeypad.Controls.Add(this.kJ);
      this.panelKeypad.Controls.Add(this.kH);
      this.panelKeypad.Controls.Add(this.kG);
      this.panelKeypad.Controls.Add(this.kF);
      this.panelKeypad.Controls.Add(this.kD);
      this.panelKeypad.Controls.Add(this.kS);
      this.panelKeypad.Controls.Add(this.kA);
      this.panelKeypad.Controls.Add(this.kCap);
      this.panelKeypad.Controls.Add(this.kRsb);
      this.panelKeypad.Controls.Add(this.kLsb);
      this.panelKeypad.Controls.Add(this.kP);
      this.panelKeypad.Controls.Add(this.kO);
      this.panelKeypad.Controls.Add(this.kI);
      this.panelKeypad.Controls.Add(this.kU);
      this.panelKeypad.Controls.Add(this.kY);
      this.panelKeypad.Controls.Add(this.kT);
      this.panelKeypad.Controls.Add(this.kR);
      this.panelKeypad.Controls.Add(this.kE);
      this.panelKeypad.Controls.Add(this.kW);
      this.panelKeypad.Controls.Add(this.kQ);
      this.panelKeypad.Controls.Add(this.kTab);
      this.panelKeypad.Controls.Add(this.kBsp);
      this.panelKeypad.Controls.Add(this.kEqu);
      this.panelKeypad.Controls.Add(this.kMin);
      this.panelKeypad.Controls.Add(this.k0);
      this.panelKeypad.Controls.Add(this.k9);
      this.panelKeypad.Controls.Add(this.k8);
      this.panelKeypad.Controls.Add(this.k7);
      this.panelKeypad.Controls.Add(this.k6);
      this.panelKeypad.Controls.Add(this.k5);
      this.panelKeypad.Controls.Add(this.k4);
      this.panelKeypad.Controls.Add(this.k3);
      this.panelKeypad.Controls.Add(this.k2);
      this.panelKeypad.Controls.Add(this.k1);
      this.panelKeypad.Controls.Add(this.kAp);
      this.panelKeypad.Location = new System.Drawing.Point(8, 72);
      this.panelKeypad.Name = "panelKeypad";
      this.panelKeypad.Size = new System.Drawing.Size(616, 229);
      this.panelKeypad.TabIndex = 0;
      // 
      // kEnt
      // 
      this.kEnt.BackColor = System.Drawing.Color.Black;
      this.kEnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kEnt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kEnt.ForeColor = System.Drawing.Color.White;
      this.kEnt.Location = new System.Drawing.Point(564, 40);
      this.kEnt.Name = "kEnt";
      this.kEnt.Size = new System.Drawing.Size(40, 76);
      this.kEnt.TabIndex = 54;
      this.kEnt.Text = "E";
      this.kEnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kLsh
      // 
      this.kLsh.BackColor = System.Drawing.Color.Black;
      this.kLsh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kLsh.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kLsh.ForeColor = System.Drawing.Color.White;
      this.kLsh.Location = new System.Drawing.Point(12, 120);
      this.kLsh.Name = "kLsh";
      this.kLsh.Size = new System.Drawing.Size(40, 36);
      this.kLsh.TabIndex = 53;
      this.kLsh.Text = "Sh";
      this.kLsh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kLbs
      // 
      this.kLbs.BackColor = System.Drawing.Color.Black;
      this.kLbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kLbs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kLbs.ForeColor = System.Drawing.Color.White;
      this.kLbs.Location = new System.Drawing.Point(60, 120);
      this.kLbs.Name = "kLbs";
      this.kLbs.Size = new System.Drawing.Size(36, 36);
      this.kLbs.TabIndex = 52;
      this.kLbs.Text = "\\";
      this.kLbs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kSpace
      // 
      this.kSpace.BackColor = System.Drawing.Color.Black;
      this.kSpace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kSpace.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kSpace.ForeColor = System.Drawing.Color.White;
      this.kSpace.Location = new System.Drawing.Point(178, 160);
      this.kSpace.Name = "kSpace";
      this.kSpace.Size = new System.Drawing.Size(228, 32);
      this.kSpace.TabIndex = 51;
      this.kSpace.Text = " spacebar";
      this.kSpace.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kRsh
      // 
      this.kRsh.BackColor = System.Drawing.Color.Black;
      this.kRsh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kRsh.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kRsh.ForeColor = System.Drawing.Color.White;
      this.kRsh.Location = new System.Drawing.Point(500, 120);
      this.kRsh.Name = "kRsh";
      this.kRsh.Size = new System.Drawing.Size(104, 36);
      this.kRsh.TabIndex = 50;
      this.kRsh.Text = "Sh";
      this.kRsh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kRs
      // 
      this.kRs.BackColor = System.Drawing.Color.Black;
      this.kRs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kRs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kRs.ForeColor = System.Drawing.Color.White;
      this.kRs.Location = new System.Drawing.Point(460, 120);
      this.kRs.Name = "kRs";
      this.kRs.Size = new System.Drawing.Size(36, 36);
      this.kRs.TabIndex = 49;
      this.kRs.Text = "/";
      this.kRs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kDot
      // 
      this.kDot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kDot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kDot.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kDot.ForeColor = System.Drawing.Color.White;
      this.kDot.Location = new System.Drawing.Point(420, 120);
      this.kDot.Name = "kDot";
      this.kDot.Size = new System.Drawing.Size(36, 36);
      this.kDot.TabIndex = 48;
      this.kDot.Text = ".";
      this.kDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kCom
      // 
      this.kCom.BackColor = System.Drawing.Color.Gray;
      this.kCom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kCom.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kCom.ForeColor = System.Drawing.Color.White;
      this.kCom.Location = new System.Drawing.Point(380, 120);
      this.kCom.Name = "kCom";
      this.kCom.Size = new System.Drawing.Size(36, 36);
      this.kCom.TabIndex = 47;
      this.kCom.Text = ",";
      this.kCom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kM
      // 
      this.kM.BackColor = System.Drawing.Color.Black;
      this.kM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kM.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kM.ForeColor = System.Drawing.Color.White;
      this.kM.Location = new System.Drawing.Point(340, 120);
      this.kM.Name = "kM";
      this.kM.Size = new System.Drawing.Size(36, 36);
      this.kM.TabIndex = 46;
      this.kM.Text = "M";
      this.kM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kN
      // 
      this.kN.BackColor = System.Drawing.Color.Black;
      this.kN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kN.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kN.ForeColor = System.Drawing.Color.White;
      this.kN.Location = new System.Drawing.Point(300, 120);
      this.kN.Name = "kN";
      this.kN.Size = new System.Drawing.Size(36, 36);
      this.kN.TabIndex = 45;
      this.kN.Text = "N";
      this.kN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kB
      // 
      this.kB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kB.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kB.ForeColor = System.Drawing.Color.White;
      this.kB.Location = new System.Drawing.Point(260, 120);
      this.kB.Name = "kB";
      this.kB.Size = new System.Drawing.Size(36, 36);
      this.kB.TabIndex = 44;
      this.kB.Text = "B";
      this.kB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kV
      // 
      this.kV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kV.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kV.ForeColor = System.Drawing.Color.White;
      this.kV.Location = new System.Drawing.Point(220, 120);
      this.kV.Name = "kV";
      this.kV.Size = new System.Drawing.Size(36, 36);
      this.kV.TabIndex = 43;
      this.kV.Text = "V";
      this.kV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kC
      // 
      this.kC.BackColor = System.Drawing.Color.Gray;
      this.kC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kC.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kC.ForeColor = System.Drawing.Color.White;
      this.kC.Location = new System.Drawing.Point(180, 120);
      this.kC.Name = "kC";
      this.kC.Size = new System.Drawing.Size(36, 36);
      this.kC.TabIndex = 42;
      this.kC.Text = "C";
      this.kC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kX
      // 
      this.kX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kX.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kX.ForeColor = System.Drawing.Color.White;
      this.kX.Location = new System.Drawing.Point(140, 120);
      this.kX.Name = "kX";
      this.kX.Size = new System.Drawing.Size(36, 36);
      this.kX.TabIndex = 41;
      this.kX.Text = "X";
      this.kX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kZ
      // 
      this.kZ.BackColor = System.Drawing.Color.Black;
      this.kZ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kZ.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kZ.ForeColor = System.Drawing.Color.White;
      this.kZ.Location = new System.Drawing.Point(100, 120);
      this.kZ.Name = "kZ";
      this.kZ.Size = new System.Drawing.Size(36, 36);
      this.kZ.TabIndex = 40;
      this.kZ.Text = "Z";
      this.kZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kRbs
      // 
      this.kRbs.BackColor = System.Drawing.Color.Black;
      this.kRbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kRbs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kRbs.ForeColor = System.Drawing.Color.White;
      this.kRbs.Location = new System.Drawing.Point(524, 80);
      this.kRbs.Name = "kRbs";
      this.kRbs.Size = new System.Drawing.Size(36, 36);
      this.kRbs.TabIndex = 39;
      this.kRbs.Text = "\\";
      this.kRbs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kAca
      // 
      this.kAca.BackColor = System.Drawing.Color.Black;
      this.kAca.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kAca.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kAca.ForeColor = System.Drawing.Color.White;
      this.kAca.Location = new System.Drawing.Point(484, 80);
      this.kAca.Name = "kAca";
      this.kAca.Size = new System.Drawing.Size(36, 36);
      this.kAca.TabIndex = 38;
      this.kAca.Text = "\'";
      this.kAca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kSmi
      // 
      this.kSmi.BackColor = System.Drawing.Color.Black;
      this.kSmi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kSmi.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kSmi.ForeColor = System.Drawing.Color.White;
      this.kSmi.Location = new System.Drawing.Point(444, 80);
      this.kSmi.Name = "kSmi";
      this.kSmi.Size = new System.Drawing.Size(36, 36);
      this.kSmi.TabIndex = 37;
      this.kSmi.Text = "&;";
      this.kSmi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kL
      // 
      this.kL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kL.ForeColor = System.Drawing.Color.White;
      this.kL.Location = new System.Drawing.Point(404, 80);
      this.kL.Name = "kL";
      this.kL.Size = new System.Drawing.Size(36, 36);
      this.kL.TabIndex = 36;
      this.kL.Text = "&L";
      this.kL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kK
      // 
      this.kK.BackColor = System.Drawing.Color.Gray;
      this.kK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kK.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kK.ForeColor = System.Drawing.Color.White;
      this.kK.Location = new System.Drawing.Point(364, 80);
      this.kK.Name = "kK";
      this.kK.Size = new System.Drawing.Size(36, 36);
      this.kK.TabIndex = 35;
      this.kK.Text = "&K";
      this.kK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kJ
      // 
      this.kJ.BackColor = System.Drawing.Color.Black;
      this.kJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kJ.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kJ.ForeColor = System.Drawing.Color.White;
      this.kJ.Location = new System.Drawing.Point(324, 80);
      this.kJ.Name = "kJ";
      this.kJ.Size = new System.Drawing.Size(36, 36);
      this.kJ.TabIndex = 34;
      this.kJ.Text = "&J";
      this.kJ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kH
      // 
      this.kH.BackColor = System.Drawing.Color.Black;
      this.kH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kH.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kH.ForeColor = System.Drawing.Color.White;
      this.kH.Location = new System.Drawing.Point(284, 80);
      this.kH.Name = "kH";
      this.kH.Size = new System.Drawing.Size(36, 36);
      this.kH.TabIndex = 33;
      this.kH.Text = "H";
      this.kH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kG
      // 
      this.kG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kG.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kG.ForeColor = System.Drawing.Color.White;
      this.kG.Location = new System.Drawing.Point(244, 80);
      this.kG.Name = "kG";
      this.kG.Size = new System.Drawing.Size(36, 36);
      this.kG.TabIndex = 32;
      this.kG.Text = "G";
      this.kG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kF
      // 
      this.kF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kF.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kF.ForeColor = System.Drawing.Color.White;
      this.kF.Location = new System.Drawing.Point(204, 80);
      this.kF.Name = "kF";
      this.kF.Size = new System.Drawing.Size(36, 36);
      this.kF.TabIndex = 31;
      this.kF.Text = "&F";
      this.kF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kD
      // 
      this.kD.BackColor = System.Drawing.Color.Gray;
      this.kD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kD.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kD.ForeColor = System.Drawing.Color.White;
      this.kD.Location = new System.Drawing.Point(164, 80);
      this.kD.Name = "kD";
      this.kD.Size = new System.Drawing.Size(36, 36);
      this.kD.TabIndex = 30;
      this.kD.Text = "&D";
      this.kD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kS
      // 
      this.kS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kS.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kS.ForeColor = System.Drawing.Color.White;
      this.kS.Location = new System.Drawing.Point(124, 80);
      this.kS.Name = "kS";
      this.kS.Size = new System.Drawing.Size(36, 36);
      this.kS.TabIndex = 29;
      this.kS.Text = "&S";
      this.kS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kA
      // 
      this.kA.BackColor = System.Drawing.Color.Black;
      this.kA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kA.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kA.ForeColor = System.Drawing.Color.White;
      this.kA.Location = new System.Drawing.Point(84, 80);
      this.kA.Name = "kA";
      this.kA.Size = new System.Drawing.Size(36, 36);
      this.kA.TabIndex = 28;
      this.kA.Text = "&A";
      this.kA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kCap
      // 
      this.kCap.BackColor = System.Drawing.Color.Black;
      this.kCap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kCap.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kCap.ForeColor = System.Drawing.Color.White;
      this.kCap.Location = new System.Drawing.Point(12, 80);
      this.kCap.Name = "kCap";
      this.kCap.Size = new System.Drawing.Size(56, 36);
      this.kCap.TabIndex = 27;
      this.kCap.Text = "Cap";
      this.kCap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kRsb
      // 
      this.kRsb.BackColor = System.Drawing.Color.Black;
      this.kRsb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kRsb.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kRsb.ForeColor = System.Drawing.Color.White;
      this.kRsb.Location = new System.Drawing.Point(516, 40);
      this.kRsb.Name = "kRsb";
      this.kRsb.Size = new System.Drawing.Size(36, 36);
      this.kRsb.TabIndex = 26;
      this.kRsb.Text = "]";
      this.kRsb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kLsb
      // 
      this.kLsb.BackColor = System.Drawing.Color.Black;
      this.kLsb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kLsb.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kLsb.ForeColor = System.Drawing.Color.White;
      this.kLsb.Location = new System.Drawing.Point(476, 40);
      this.kLsb.Name = "kLsb";
      this.kLsb.Size = new System.Drawing.Size(36, 36);
      this.kLsb.TabIndex = 25;
      this.kLsb.Text = "[";
      this.kLsb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kP
      // 
      this.kP.BackColor = System.Drawing.Color.Black;
      this.kP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kP.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kP.ForeColor = System.Drawing.Color.White;
      this.kP.Location = new System.Drawing.Point(436, 40);
      this.kP.Name = "kP";
      this.kP.Size = new System.Drawing.Size(36, 36);
      this.kP.TabIndex = 24;
      this.kP.Text = "P";
      this.kP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kO
      // 
      this.kO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kO.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kO.ForeColor = System.Drawing.Color.White;
      this.kO.Location = new System.Drawing.Point(396, 40);
      this.kO.Name = "kO";
      this.kO.Size = new System.Drawing.Size(36, 36);
      this.kO.TabIndex = 23;
      this.kO.Text = "O";
      this.kO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kI
      // 
      this.kI.BackColor = System.Drawing.Color.Gray;
      this.kI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kI.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kI.ForeColor = System.Drawing.Color.White;
      this.kI.Location = new System.Drawing.Point(356, 40);
      this.kI.Name = "kI";
      this.kI.Size = new System.Drawing.Size(36, 36);
      this.kI.TabIndex = 22;
      this.kI.Text = "I";
      this.kI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kU
      // 
      this.kU.BackColor = System.Drawing.Color.Black;
      this.kU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kU.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kU.ForeColor = System.Drawing.Color.White;
      this.kU.Location = new System.Drawing.Point(316, 40);
      this.kU.Name = "kU";
      this.kU.Size = new System.Drawing.Size(36, 36);
      this.kU.TabIndex = 21;
      this.kU.Text = "U";
      this.kU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kY
      // 
      this.kY.BackColor = System.Drawing.Color.Black;
      this.kY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kY.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kY.ForeColor = System.Drawing.Color.White;
      this.kY.Location = new System.Drawing.Point(276, 40);
      this.kY.Name = "kY";
      this.kY.Size = new System.Drawing.Size(36, 36);
      this.kY.TabIndex = 20;
      this.kY.Text = "Y";
      this.kY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kT
      // 
      this.kT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kT.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kT.ForeColor = System.Drawing.Color.White;
      this.kT.Location = new System.Drawing.Point(236, 40);
      this.kT.Name = "kT";
      this.kT.Size = new System.Drawing.Size(36, 36);
      this.kT.TabIndex = 19;
      this.kT.Text = "T";
      this.kT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kR
      // 
      this.kR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kR.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kR.ForeColor = System.Drawing.Color.White;
      this.kR.Location = new System.Drawing.Point(196, 40);
      this.kR.Name = "kR";
      this.kR.Size = new System.Drawing.Size(36, 36);
      this.kR.TabIndex = 18;
      this.kR.Text = "R";
      this.kR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kE
      // 
      this.kE.BackColor = System.Drawing.Color.Gray;
      this.kE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kE.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kE.ForeColor = System.Drawing.Color.White;
      this.kE.Location = new System.Drawing.Point(156, 40);
      this.kE.Name = "kE";
      this.kE.Size = new System.Drawing.Size(36, 36);
      this.kE.TabIndex = 17;
      this.kE.Text = "E";
      this.kE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kW
      // 
      this.kW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.kW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kW.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kW.ForeColor = System.Drawing.Color.White;
      this.kW.Location = new System.Drawing.Point(116, 40);
      this.kW.Name = "kW";
      this.kW.Size = new System.Drawing.Size(36, 36);
      this.kW.TabIndex = 16;
      this.kW.Text = "W";
      this.kW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kQ
      // 
      this.kQ.BackColor = System.Drawing.Color.Black;
      this.kQ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kQ.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kQ.ForeColor = System.Drawing.Color.White;
      this.kQ.Location = new System.Drawing.Point(76, 40);
      this.kQ.Name = "kQ";
      this.kQ.Size = new System.Drawing.Size(36, 36);
      this.kQ.TabIndex = 15;
      this.kQ.Text = "Q";
      this.kQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kTab
      // 
      this.kTab.BackColor = System.Drawing.Color.Black;
      this.kTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kTab.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kTab.ForeColor = System.Drawing.Color.White;
      this.kTab.Location = new System.Drawing.Point(12, 40);
      this.kTab.Name = "kTab";
      this.kTab.Size = new System.Drawing.Size(56, 36);
      this.kTab.TabIndex = 14;
      this.kTab.Text = "Tab";
      this.kTab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kBsp
      // 
      this.kBsp.BackColor = System.Drawing.Color.Black;
      this.kBsp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kBsp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kBsp.ForeColor = System.Drawing.Color.White;
      this.kBsp.Location = new System.Drawing.Point(532, 0);
      this.kBsp.Name = "kBsp";
      this.kBsp.Size = new System.Drawing.Size(72, 36);
      this.kBsp.TabIndex = 13;
      this.kBsp.Text = "<-";
      this.kBsp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kEqu
      // 
      this.kEqu.BackColor = System.Drawing.Color.Black;
      this.kEqu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kEqu.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kEqu.ForeColor = System.Drawing.Color.White;
      this.kEqu.Location = new System.Drawing.Point(492, 0);
      this.kEqu.Name = "kEqu";
      this.kEqu.Size = new System.Drawing.Size(36, 36);
      this.kEqu.TabIndex = 12;
      this.kEqu.Text = "=";
      this.kEqu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kMin
      // 
      this.kMin.BackColor = System.Drawing.Color.Black;
      this.kMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kMin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kMin.ForeColor = System.Drawing.Color.White;
      this.kMin.Location = new System.Drawing.Point(452, 0);
      this.kMin.Name = "kMin";
      this.kMin.Size = new System.Drawing.Size(36, 36);
      this.kMin.TabIndex = 11;
      this.kMin.Text = "-";
      this.kMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k0
      // 
      this.k0.BackColor = System.Drawing.Color.Black;
      this.k0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k0.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k0.ForeColor = System.Drawing.Color.White;
      this.k0.Location = new System.Drawing.Point(412, 0);
      this.k0.Name = "k0";
      this.k0.Size = new System.Drawing.Size(36, 36);
      this.k0.TabIndex = 10;
      this.k0.Text = "0";
      this.k0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k9
      // 
      this.k9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.k9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k9.ForeColor = System.Drawing.Color.White;
      this.k9.Location = new System.Drawing.Point(372, 0);
      this.k9.Name = "k9";
      this.k9.Size = new System.Drawing.Size(36, 36);
      this.k9.TabIndex = 9;
      this.k9.Text = "9";
      this.k9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k8
      // 
      this.k8.BackColor = System.Drawing.Color.Gray;
      this.k8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k8.ForeColor = System.Drawing.Color.White;
      this.k8.Location = new System.Drawing.Point(332, 0);
      this.k8.Name = "k8";
      this.k8.Size = new System.Drawing.Size(36, 36);
      this.k8.TabIndex = 8;
      this.k8.Text = "8";
      this.k8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k7
      // 
      this.k7.BackColor = System.Drawing.Color.Black;
      this.k7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k7.ForeColor = System.Drawing.Color.White;
      this.k7.Location = new System.Drawing.Point(292, 0);
      this.k7.Name = "k7";
      this.k7.Size = new System.Drawing.Size(36, 36);
      this.k7.TabIndex = 7;
      this.k7.Text = "7";
      this.k7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k6
      // 
      this.k6.BackColor = System.Drawing.Color.Black;
      this.k6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k6.ForeColor = System.Drawing.Color.White;
      this.k6.Location = new System.Drawing.Point(252, 0);
      this.k6.Name = "k6";
      this.k6.Size = new System.Drawing.Size(36, 36);
      this.k6.TabIndex = 6;
      this.k6.Text = "6";
      this.k6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k5
      // 
      this.k5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.k5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k5.ForeColor = System.Drawing.Color.White;
      this.k5.Location = new System.Drawing.Point(212, 0);
      this.k5.Name = "k5";
      this.k5.Size = new System.Drawing.Size(36, 36);
      this.k5.TabIndex = 5;
      this.k5.Text = "5";
      this.k5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k4
      // 
      this.k4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.k4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k4.ForeColor = System.Drawing.Color.White;
      this.k4.Location = new System.Drawing.Point(172, 0);
      this.k4.Name = "k4";
      this.k4.Size = new System.Drawing.Size(36, 36);
      this.k4.TabIndex = 4;
      this.k4.Text = "4";
      this.k4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k3
      // 
      this.k3.BackColor = System.Drawing.Color.Gray;
      this.k3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k3.ForeColor = System.Drawing.Color.White;
      this.k3.Location = new System.Drawing.Point(132, 0);
      this.k3.Name = "k3";
      this.k3.Size = new System.Drawing.Size(36, 36);
      this.k3.TabIndex = 3;
      this.k3.Text = "3";
      this.k3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k2
      // 
      this.k2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.k2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k2.ForeColor = System.Drawing.Color.White;
      this.k2.Location = new System.Drawing.Point(92, 0);
      this.k2.Name = "k2";
      this.k2.Size = new System.Drawing.Size(36, 36);
      this.k2.TabIndex = 2;
      this.k2.Text = "2";
      this.k2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // k1
      // 
      this.k1.BackColor = System.Drawing.Color.Black;
      this.k1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.k1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.k1.ForeColor = System.Drawing.Color.White;
      this.k1.Location = new System.Drawing.Point(52, 0);
      this.k1.Name = "k1";
      this.k1.Size = new System.Drawing.Size(36, 36);
      this.k1.TabIndex = 1;
      this.k1.Text = "1";
      this.k1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // kAp
      // 
      this.kAp.BackColor = System.Drawing.Color.Black;
      this.kAp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.kAp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.kAp.ForeColor = System.Drawing.Color.White;
      this.kAp.Location = new System.Drawing.Point(12, 0);
      this.kAp.Name = "kAp";
      this.kAp.Size = new System.Drawing.Size(36, 36);
      this.kAp.TabIndex = 0;
      this.kAp.Text = "`";
      this.kAp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblLevelSpeedDown
      // 
      this.lblLevelSpeedDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblLevelSpeedDown.Location = new System.Drawing.Point(2, 13);
      this.lblLevelSpeedDown.Name = "lblLevelSpeedDown";
      this.lblLevelSpeedDown.Size = new System.Drawing.Size(24, 8);
      this.lblLevelSpeedDown.TabIndex = 7;
      this.lblLevelSpeedDown.Text = "down";
      this.lblLevelSpeedDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblLevelSpeedUp
      // 
      this.lblLevelSpeedUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblLevelSpeedUp.Location = new System.Drawing.Point(170, 13);
      this.lblLevelSpeedUp.Name = "lblLevelSpeedUp";
      this.lblLevelSpeedUp.Size = new System.Drawing.Size(24, 8);
      this.lblLevelSpeedUp.TabIndex = 8;
      this.lblLevelSpeedUp.Text = "up";
      this.lblLevelSpeedUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblSpeed
      // 
      this.lblSpeed.BackColor = System.Drawing.Color.Transparent;
      this.lblSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSpeed.Location = new System.Drawing.Point(125, -2);
      this.lblSpeed.Name = "lblSpeed";
      this.lblSpeed.Size = new System.Drawing.Size(53, 17);
      this.lblSpeed.TabIndex = 5;
      this.lblSpeed.Text = "speed";
      this.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblSpeedTitle
      // 
      this.lblSpeedTitle.Location = new System.Drawing.Point(19, 0);
      this.lblSpeedTitle.Name = "lblSpeedTitle";
      this.lblSpeedTitle.Size = new System.Drawing.Size(85, 13);
      this.lblSpeedTitle.TabIndex = 56;
      this.lblSpeedTitle.Text = "Speed (ch/min)";
      this.lblSpeedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblLevelErrorDown
      // 
      this.lblLevelErrorDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblLevelErrorDown.Location = new System.Drawing.Point(138, 14);
      this.lblLevelErrorDown.Name = "lblLevelErrorDown";
      this.lblLevelErrorDown.Size = new System.Drawing.Size(24, 8);
      this.lblLevelErrorDown.TabIndex = 9;
      this.lblLevelErrorDown.Text = "100";
      this.lblLevelErrorDown.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // lblErrors
      // 
      this.lblErrors.BackColor = System.Drawing.Color.Transparent;
      this.lblErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblErrors.Location = new System.Drawing.Point(95, -2);
      this.lblErrors.Name = "lblErrors";
      this.lblErrors.Size = new System.Drawing.Size(50, 18);
      this.lblErrors.TabIndex = 6;
      this.lblErrors.Text = "errors";
      this.lblErrors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblErrorTitle
      // 
      this.lblErrorTitle.Location = new System.Drawing.Point(17, 1);
      this.lblErrorTitle.Name = "lblErrorTitle";
      this.lblErrorTitle.Size = new System.Drawing.Size(58, 13);
      this.lblErrorTitle.TabIndex = 55;
      this.lblErrorTitle.Text = "Errors (%)";
      this.lblErrorTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblLevelError0
      // 
      this.lblLevelError0.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.lblLevelError0.Location = new System.Drawing.Point(1, 14);
      this.lblLevelError0.Name = "lblLevelError0";
      this.lblLevelError0.Size = new System.Drawing.Size(9, 8);
      this.lblLevelError0.TabIndex = 10;
      this.lblLevelError0.Text = "0";
      this.lblLevelError0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtTarget
      // 
      this.txtTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTarget.BackColor = System.Drawing.Color.White;
      this.txtTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtTarget.CausesValidation = false;
      this.txtTarget.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtTarget.ForeColor = System.Drawing.Color.Black;
      this.txtTarget.Location = new System.Drawing.Point(20, 16);
      this.txtTarget.Name = "txtTarget";
      this.txtTarget.ReadOnly = true;
      this.txtTarget.Size = new System.Drawing.Size(592, 44);
      this.txtTarget.TabIndex = 1;
      this.txtTarget.TabStop = false;
      this.txtTarget.Text = "sample text";
      this.txtTarget.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // mainMenu
      // 
      this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFile,
            this.menuView,
            this.menuLevel,
            this.menuAbout});
      // 
      // menuFile
      // 
      this.menuFile.Index = 0;
      this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuFile_Exit});
      this.menuFile.Text = "&File";
      // 
      // menuFile_Exit
      // 
      this.menuFile_Exit.Index = 0;
      this.menuFile_Exit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
      this.menuFile_Exit.Text = "&Exit";
      this.menuFile_Exit.Click += new System.EventHandler(this.menuFile_Exit_Click);
      // 
      // menuView
      // 
      this.menuView.Index = 1;
      this.menuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuView_Configure});
      this.menuView.Text = "&Edit";
      // 
      // menuView_Configure
      // 
      this.menuView_Configure.Index = 0;
      this.menuView_Configure.Shortcut = System.Windows.Forms.Shortcut.F4;
      this.menuView_Configure.Text = "&Configure";
      this.menuView_Configure.Click += new System.EventHandler(this.menuView_Configure_Click);
      // 
      // menuLevel
      // 
      this.menuLevel.Index = 2;
      this.menuLevel.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuLevel_Restart,
            this.menuLevel_Separator1});
      this.menuLevel.Text = "&Level";
      // 
      // menuLevel_Restart
      // 
      this.menuLevel_Restart.Index = 0;
      this.menuLevel_Restart.Shortcut = System.Windows.Forms.Shortcut.F5;
      this.menuLevel_Restart.Text = "&Restart";
      this.menuLevel_Restart.Click += new System.EventHandler(this.menuLevel_Restart_Click);
      // 
      // menuLevel_Separator1
      // 
      this.menuLevel_Separator1.Index = 1;
      this.menuLevel_Separator1.Text = "-";
      // 
      // menuAbout
      // 
      this.menuAbout.Index = 3;
      this.menuAbout.Shortcut = System.Windows.Forms.Shortcut.F1;
      this.menuAbout.Text = "&About";
      this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
      // 
      // panelNumpad
      // 
      this.panelNumpad.Controls.Add(this.nDot);
      this.panelNumpad.Controls.Add(this.n2);
      this.panelNumpad.Controls.Add(this.n0);
      this.panelNumpad.Controls.Add(this.nEnt);
      this.panelNumpad.Controls.Add(this.nPls);
      this.panelNumpad.Controls.Add(this.n3);
      this.panelNumpad.Controls.Add(this.n1);
      this.panelNumpad.Controls.Add(this.n6);
      this.panelNumpad.Controls.Add(this.n5);
      this.panelNumpad.Controls.Add(this.n4);
      this.panelNumpad.Controls.Add(this.n9);
      this.panelNumpad.Controls.Add(this.n8);
      this.panelNumpad.Controls.Add(this.n7);
      this.panelNumpad.Controls.Add(this.nMin);
      this.panelNumpad.Controls.Add(this.nMul);
      this.panelNumpad.Controls.Add(this.nDiv);
      this.panelNumpad.Controls.Add(this.nNumLock);
      this.panelNumpad.Location = new System.Drawing.Point(219, 72);
      this.panelNumpad.Name = "panelNumpad";
      this.panelNumpad.Size = new System.Drawing.Size(194, 200);
      this.panelNumpad.TabIndex = 11;
      this.panelNumpad.Visible = false;
      // 
      // nDot
      // 
      this.nDot.BackColor = System.Drawing.Color.Black;
      this.nDot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nDot.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nDot.ForeColor = System.Drawing.Color.White;
      this.nDot.Location = new System.Drawing.Point(99, 160);
      this.nDot.Name = "nDot";
      this.nDot.Size = new System.Drawing.Size(36, 36);
      this.nDot.TabIndex = 68;
      this.nDot.Text = ".";
      this.nDot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n2
      // 
      this.n2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.n2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n2.ForeColor = System.Drawing.Color.White;
      this.n2.Location = new System.Drawing.Point(59, 120);
      this.n2.Name = "n2";
      this.n2.Size = new System.Drawing.Size(36, 36);
      this.n2.TabIndex = 67;
      this.n2.Text = "2";
      this.n2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n0
      // 
      this.n0.BackColor = System.Drawing.Color.Gray;
      this.n0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n0.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n0.ForeColor = System.Drawing.Color.White;
      this.n0.Location = new System.Drawing.Point(19, 160);
      this.n0.Name = "n0";
      this.n0.Size = new System.Drawing.Size(76, 36);
      this.n0.TabIndex = 66;
      this.n0.Text = "&0";
      this.n0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nEnt
      // 
      this.nEnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.nEnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nEnt.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nEnt.ForeColor = System.Drawing.Color.White;
      this.nEnt.Location = new System.Drawing.Point(139, 120);
      this.nEnt.Name = "nEnt";
      this.nEnt.Size = new System.Drawing.Size(36, 76);
      this.nEnt.TabIndex = 65;
      this.nEnt.Text = "&E";
      this.nEnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nPls
      // 
      this.nPls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.nPls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nPls.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nPls.ForeColor = System.Drawing.Color.White;
      this.nPls.Location = new System.Drawing.Point(139, 40);
      this.nPls.Name = "nPls";
      this.nPls.Size = new System.Drawing.Size(36, 76);
      this.nPls.TabIndex = 64;
      this.nPls.Text = "+";
      this.nPls.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n3
      // 
      this.n3.BackColor = System.Drawing.Color.Black;
      this.n3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n3.ForeColor = System.Drawing.Color.White;
      this.n3.Location = new System.Drawing.Point(99, 120);
      this.n3.Name = "n3";
      this.n3.Size = new System.Drawing.Size(36, 36);
      this.n3.TabIndex = 63;
      this.n3.Text = "3";
      this.n3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n1
      // 
      this.n1.BackColor = System.Drawing.Color.Black;
      this.n1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n1.ForeColor = System.Drawing.Color.White;
      this.n1.Location = new System.Drawing.Point(19, 120);
      this.n1.Name = "n1";
      this.n1.Size = new System.Drawing.Size(36, 36);
      this.n1.TabIndex = 62;
      this.n1.Text = "1";
      this.n1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n6
      // 
      this.n6.BackColor = System.Drawing.Color.Black;
      this.n6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n6.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n6.ForeColor = System.Drawing.Color.White;
      this.n6.Location = new System.Drawing.Point(99, 80);
      this.n6.Name = "n6";
      this.n6.Size = new System.Drawing.Size(36, 36);
      this.n6.TabIndex = 61;
      this.n6.Text = "&6";
      this.n6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n5
      // 
      this.n5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.n5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n5.ForeColor = System.Drawing.Color.White;
      this.n5.Location = new System.Drawing.Point(59, 80);
      this.n5.Name = "n5";
      this.n5.Size = new System.Drawing.Size(36, 36);
      this.n5.TabIndex = 60;
      this.n5.Text = "&5";
      this.n5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n4
      // 
      this.n4.BackColor = System.Drawing.Color.Black;
      this.n4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n4.ForeColor = System.Drawing.Color.White;
      this.n4.Location = new System.Drawing.Point(19, 80);
      this.n4.Name = "n4";
      this.n4.Size = new System.Drawing.Size(36, 36);
      this.n4.TabIndex = 59;
      this.n4.Text = "&4";
      this.n4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n9
      // 
      this.n9.BackColor = System.Drawing.Color.Black;
      this.n9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n9.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n9.ForeColor = System.Drawing.Color.White;
      this.n9.Location = new System.Drawing.Point(99, 40);
      this.n9.Name = "n9";
      this.n9.Size = new System.Drawing.Size(36, 36);
      this.n9.TabIndex = 58;
      this.n9.Text = "9";
      this.n9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n8
      // 
      this.n8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.n8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n8.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n8.ForeColor = System.Drawing.Color.White;
      this.n8.Location = new System.Drawing.Point(59, 40);
      this.n8.Name = "n8";
      this.n8.Size = new System.Drawing.Size(36, 36);
      this.n8.TabIndex = 57;
      this.n8.Text = "8";
      this.n8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // n7
      // 
      this.n7.BackColor = System.Drawing.Color.Black;
      this.n7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.n7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.n7.ForeColor = System.Drawing.Color.White;
      this.n7.Location = new System.Drawing.Point(19, 40);
      this.n7.Name = "n7";
      this.n7.Size = new System.Drawing.Size(36, 36);
      this.n7.TabIndex = 56;
      this.n7.Text = "7";
      this.n7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nMin
      // 
      this.nMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.nMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nMin.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nMin.ForeColor = System.Drawing.Color.White;
      this.nMin.Location = new System.Drawing.Point(139, 0);
      this.nMin.Name = "nMin";
      this.nMin.Size = new System.Drawing.Size(36, 36);
      this.nMin.TabIndex = 55;
      this.nMin.Text = "-";
      this.nMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nMul
      // 
      this.nMul.BackColor = System.Drawing.Color.Black;
      this.nMul.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nMul.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nMul.ForeColor = System.Drawing.Color.White;
      this.nMul.Location = new System.Drawing.Point(99, 0);
      this.nMul.Name = "nMul";
      this.nMul.Size = new System.Drawing.Size(36, 36);
      this.nMul.TabIndex = 54;
      this.nMul.Text = "*";
      this.nMul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nDiv
      // 
      this.nDiv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.nDiv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nDiv.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nDiv.ForeColor = System.Drawing.Color.White;
      this.nDiv.Location = new System.Drawing.Point(59, 0);
      this.nDiv.Name = "nDiv";
      this.nDiv.Size = new System.Drawing.Size(36, 36);
      this.nDiv.TabIndex = 53;
      this.nDiv.Text = "/";
      this.nDiv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // nNumLock
      // 
      this.nNumLock.BackColor = System.Drawing.Color.Black;
      this.nNumLock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.nNumLock.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.nNumLock.ForeColor = System.Drawing.Color.White;
      this.nNumLock.Location = new System.Drawing.Point(19, 0);
      this.nNumLock.Name = "nNumLock";
      this.nNumLock.Size = new System.Drawing.Size(36, 36);
      this.nNumLock.TabIndex = 52;
      this.nNumLock.Text = "N";
      this.nNumLock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // pbLevel
      // 
      this.pbLevel.Location = new System.Drawing.Point(20, 10);
      this.pbLevel.Name = "pbLevel";
      this.pbLevel.Size = new System.Drawing.Size(592, 6);
      this.pbLevel.TabIndex = 15;
      // 
      // panelSpeed
      // 
      this.panelSpeed.Controls.Add(this.lblLevelSpeedDown);
      this.panelSpeed.Controls.Add(this.lblSpeedTitle);
      this.panelSpeed.Controls.Add(this.lblLevelSpeedUp);
      this.panelSpeed.Controls.Add(this.lblSpeed);
      this.panelSpeed.Controls.Add(this.m_cgSpeed);
      this.panelSpeed.Location = new System.Drawing.Point(415, 230);
      this.panelSpeed.Name = "panelSpeed";
      this.panelSpeed.Size = new System.Drawing.Size(197, 70);
      this.panelSpeed.TabIndex = 58;
      // 
      // m_cgSpeed
      // 
      this.m_cgSpeed.Alarm = false;
      this.m_cgSpeed.AlarmColor = System.Drawing.Color.Red;
      this.m_cgSpeed.AlarmConditionMax = 100;
      this.m_cgSpeed.AlarmConditionMin = 100;
      this.m_cgSpeed.ArrowColor = System.Drawing.Color.DarkOrange;
      this.m_cgSpeed.ArrowWidth = 4F;
      this.m_cgSpeed.BackColor = System.Drawing.Color.Transparent;
      this.m_cgSpeed.BaseBorderColor = System.Drawing.Color.DimGray;
      this.m_cgSpeed.BaseCircleColor = System.Drawing.Color.Gray;
      this.m_cgSpeed.BaseCircleGlareColor = System.Drawing.Color.LightGray;
      this.m_cgSpeed.BaseDiameter = 15F;
      this.m_cgSpeed.BorderColor = System.Drawing.Color.Black;
      this.m_cgSpeed.EnableAlarmCondition = false;
      this.m_cgSpeed.Location = new System.Drawing.Point(2, -8);
      this.m_cgSpeed.Maximum = 200;
      this.m_cgSpeed.Minimum = 50;
      this.m_cgSpeed.Name = "m_cgSpeed";
      this.m_cgSpeed.Size = new System.Drawing.Size(193, 66);
      this.m_cgSpeed.TabIndex = 57;
      this.m_cgSpeed.TickFrequency = 10;
      this.m_cgSpeed.UTurn = -180F;
      this.m_cgSpeed.UTurnBase = 180F;
      this.m_cgSpeed.Value = 100;
      // 
      // panelError
      // 
      this.panelError.Controls.Add(this.lblLevelErrorDown);
      this.panelError.Controls.Add(this.lblLevelError0);
      this.panelError.Controls.Add(this.lblErrors);
      this.panelError.Controls.Add(this.lblErrorTitle);
      this.panelError.Controls.Add(this.m_cgError);
      this.panelError.Location = new System.Drawing.Point(20, 230);
      this.panelError.Name = "panelError";
      this.panelError.Size = new System.Drawing.Size(164, 70);
      this.panelError.TabIndex = 56;
      // 
      // m_cgError
      // 
      this.m_cgError.Alarm = false;
      this.m_cgError.AlarmColor = System.Drawing.Color.Red;
      this.m_cgError.AlarmConditionMax = 100;
      this.m_cgError.AlarmConditionMin = 100;
      this.m_cgError.ArrowColor = System.Drawing.Color.DarkOrange;
      this.m_cgError.ArrowWidth = 4F;
      this.m_cgError.BaseBorderColor = System.Drawing.Color.DimGray;
      this.m_cgError.BaseCircleColor = System.Drawing.Color.Gray;
      this.m_cgError.BaseCircleGlareColor = System.Drawing.Color.LightGray;
      this.m_cgError.BaseDiameter = 15F;
      this.m_cgError.BorderColor = System.Drawing.Color.Black;
      this.m_cgError.CausesValidation = false;
      this.m_cgError.EnableAlarmCondition = false;
      this.m_cgError.Location = new System.Drawing.Point(2, -8);
      this.m_cgError.Maximum = 70;
      this.m_cgError.Minimum = 0;
      this.m_cgError.Name = "m_cgError";
      this.m_cgError.Size = new System.Drawing.Size(159, 66);
      this.m_cgError.TabIndex = 18;
      this.m_cgError.TickFrequency = 10;
      this.m_cgError.UTurn = -180F;
      this.m_cgError.UTurnBase = 180F;
      this.m_cgError.Value = 10;
      // 
      // FFingers
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(632, 308);
      this.Controls.Add(this.panelSpeed);
      this.Controls.Add(this.panelError);
      this.Controls.Add(this.panelNumpad);
      this.Controls.Add(this.pbLevel);
      this.Controls.Add(this.txtTarget);
      this.Controls.Add(this.panelKeypad);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.Menu = this.mainMenu;
      this.Name = "FFingers";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.Activated += new System.EventHandler(this.FFingers_Activated);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.FFingers_Closing);
      this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CFingers_KeyPress);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CFingers_KeyDown);
      this.panelKeypad.ResumeLayout(false);
      this.panelNumpad.ResumeLayout(false);
      this.panelSpeed.ResumeLayout(false);
      this.panelError.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    #endregion

    #region Main Menu
    private void menuFile_Exit_Click(object sender, System.EventArgs e)
    {
      CConfig.SaveConfig(m_teacher.Config);
      Application.Exit();
    }

    private void menuView_Configure_Click(object sender, System.EventArgs e)
    {
      FConfigure form = new FConfigure();
      form.m_cfg = m_teacher.Config;

      if (DialogResult.OK == form.ShowDialog(this))
      {// submit changes
        CConfig.SaveConfig(form.m_cfg);
        m_teacher.Config = form.m_cfg;
        m_teacher.RestartLevel();
        InitTask();
      }
    }

    private void menuLevel_Restart_Click(object sender, System.EventArgs e)
    {
      if (null != m_teacher && !m_teacher.IsLevelStarted)
      { // don't perform if level only started and no key was processed `m_teacher.Check`
        return;
      }

      DialogResult dlg = MessageBox.Show("Restart current level?"
          , FFingers.strProductVersion, MessageBoxButtons.OKCancel);

      if (dlg == DialogResult.OK)
      {
        m_teacher.RestartLevel();
        InitTask();
      }
      else
      {
        FFingers_Activated(null, null);
      }
    }
    
    private void menuLevel_submenuItem_Click(object sender, EventArgs e)
    {
      MenuItem mItem = sender as MenuItem;
      if (null == mItem) { return; }

      m_teacher.SetLevelTo(int.Parse(mItem.Text));
      this.InitTask();
    }

    private void menuAbout_Click(object sender, System.EventArgs e)
    {
      FAbout form = new FAbout();
      form.ShowDialog(this);
    }
    #endregion Main Menu

    private void CFingers_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F11)
      {
        e.Handled = true;
        if (this.Opacity < 0.11d) { return; }
        this.Opacity -= 0.1d;
      }
      else if (e.KeyCode == Keys.F12)
      {
        e.Handled = true;
        this.Opacity += 0.1d;
      }
    }

    private void CFingers_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      char chPressed = e.KeyChar;
      e.Handled = true;

      if (chPressed == '\x1B')
      {
        // handle [Esc] keyboard button
        if (m_teacher.PauseMode)
        {
          m_teacher.PauseMode = false;
          txtTarget.Text = m_strBeforePauseMode;
        }
        else
        {
          m_teacher.PauseMode = true;
          m_strBeforePauseMode = txtTarget.Text;
          txtTarget.Text = m_strPauseMode;
        }
        return;
      }

      
      if (m_teacher.PauseMode)
      {
        return;
      }
        // hanlde [backspace] keyboard button
      else if (chPressed == '\x08')
      {
        char chOnTarget = '\x00';
        txtTarget.Text = m_teacher.RefreshTaskBackward(out chOnTarget);
        Feedback(chOnTarget);
      }
        // handle keys on target line
      else
      {
        m_teacher.Check(chPressed);
      }
    }

    #region Typing feedback
    private void FeedbackSelect(Label _lblFingerOn, Label _lblFingerTo)
    {
      // restore previus style
      if ((m_lblFingerOn != null) && (m_lblFingerTo != null))
      {
        m_lblFingerOn.Text = "&" + m_strFingerOn.Replace("&", "");
        m_lblFingerTo.Text = m_strFingerTo;
        m_lblFingerTo.BackColor = m_clrFingerTo;
        m_lblFingerTo.ForeColor = Color.White;
      }
      if (m_lblPrevShift != null)
      {// restore shift button style if it previous
        m_lblPrevShift.BackColor = m_clrPrevShift;
        m_lblPrevShift = null;
      }

      // remember current style
      m_lblFingerOn = _lblFingerOn;
      m_lblFingerTo = _lblFingerTo;
      m_strFingerOn = _lblFingerOn.Text;
      m_strFingerTo = _lblFingerTo.Text;
      m_clrFingerTo = _lblFingerTo.BackColor;

      // change style on selected labels
      _lblFingerOn.Text = _lblFingerOn.Text.Replace("&", "");
      _lblFingerTo.Text = "&" + _lblFingerTo.Text;
      _lblFingerTo.BackColor = Color.Lime;
      _lblFingerTo.ForeColor = Color.Black;
    }

    private void FeedbackSelect(Label _lblFingerOn, Label _lblFingerTo, Label _lblShift)
    {
      if (m_lblPrevShift != null)
      {
        m_lblPrevShift.BackColor = m_clrPrevShift;
        m_lblPrevShift = null;
      }

      FeedbackSelect(_lblFingerOn, _lblFingerTo);

      m_lblPrevShift = _lblShift;
      m_clrPrevShift = _lblShift.BackColor;
      _lblShift.BackColor = Color.Lime;
    }

    private void Feedback(char _chOnTarget)
    {
      if (_chOnTarget == '\x00') { return; }

      if (m_teacher.Config.eMode == EMode.Numpad)
      {// numpad key highliting
        switch ((int)_chOnTarget)
        {
          case 0x30: FeedbackSelect(n0, n0); break;       // 0
          case 0x31: FeedbackSelect(n4, n1); break;       // 1
          case 0x32: FeedbackSelect(n5, n2); break;       // 2
          case 0x33: FeedbackSelect(n6, n3); break;       // 3
          case 0x34: FeedbackSelect(n4, n4); break;       // 4
          case 0x35: FeedbackSelect(n5, n5); break;       // 5
          case 0x36: FeedbackSelect(n6, n6); break;       // 6
          case 0x37: FeedbackSelect(n4, n7); break;       // 7
          case 0x38: FeedbackSelect(n5, n8); break;       // 8
          case 0x39: FeedbackSelect(n6, n9); break;       // 9
          case 0x0D: FeedbackSelect(nEnt, nEnt); break;   // Enter
          case 0x2A: FeedbackSelect(n6, nMul); break;     // *
          case 0x2B: FeedbackSelect(nEnt, nPls); break;   // +
          case 0x2D: FeedbackSelect(nEnt, nMin); break;   // -
          case 0x2E: FeedbackSelect(n6, nDot); break;     // .
          case 0x2F: FeedbackSelect(n5, nDiv); break;     // /
        }
      }
      else if (m_teacher.Config.eMode == EMode.Keypad)
      {// numpad key highliting
        switch ((int)_chOnTarget)
        {
            // keypad spacebar button
          case 0x20: FeedbackSelect(kSpace, kSpace); break;

            // keypad `1234567890-= line
          case 0x60: FeedbackSelect(kA, kAp); break;      // `
          case 0x31: FeedbackSelect(kA, k1); break;       // 1
          case 0x32: FeedbackSelect(kS, k2); break;       // 2
          case 0x33: FeedbackSelect(kD, k3); break;       // 3
          case 0x34: FeedbackSelect(kF, k4); break;       // 4
          case 0x35: FeedbackSelect(kF, k5); break;       // 5
          case 0x36: FeedbackSelect(kJ, k6); break;       // 6
          case 0x37: FeedbackSelect(kJ, k7); break;       // 7
          case 0x38: FeedbackSelect(kK, k8); break;       // 8
          case 0x39: FeedbackSelect(kL, k9); break;       // 9
          case 0x30: FeedbackSelect(kSmi, k0); break;     // 0
          case 0x2D: FeedbackSelect(kSmi, kMin); break;   // -
          case 0x3D: FeedbackSelect(kSmi, kEqu); break;   // =
            // keypad qwertyuiop[] line
          case 0x09: FeedbackSelect(kA, kTab); break;     // [Tab]
          case 0x71: FeedbackSelect(kA, kQ); break;       // q
          case 0x77: FeedbackSelect(kS, kW); break;       // w
          case 0x65: FeedbackSelect(kD, kE); break;       // e
          case 0x72: FeedbackSelect(kF, kR); break;       // r
          case 0x74: FeedbackSelect(kF, kT); break;       // t
          case 0x79: FeedbackSelect(kJ, kY); break;       // y
          case 0x75: FeedbackSelect(kJ, kU); break;       // u
          case 0x69: FeedbackSelect(kK, kI); break;       // i
          case 0x6F: FeedbackSelect(kL, kO); break;       // o
          case 0x70: FeedbackSelect(kSmi, kP); break;     // p
          case 0x5B: FeedbackSelect(kSmi, kLsb); break;   // [
          case 0x5D: FeedbackSelect(kSmi, kRsb); break;   // ]
            // keypad asdfghjkl;' line
          case 0x61: FeedbackSelect(kA, kA); break;       // a
          case 0x73: FeedbackSelect(kS, kS); break;       // s
          case 0x64: FeedbackSelect(kD, kD); break;       // d
          case 0x66: FeedbackSelect(kF, kF); break;       // f
          case 0x67: FeedbackSelect(kF, kG); break;       // g
          case 0x68: FeedbackSelect(kJ, kH); break;       // h
          case 0x6A: FeedbackSelect(kJ, kJ); break;       // j
          case 0x6B: FeedbackSelect(kK, kK); break;       // k
          case 0x6C: FeedbackSelect(kL, kL); break;       // l
          case 0x3B: FeedbackSelect(kSmi, kSmi); break;   // ;
          case 0x27: FeedbackSelect(kSmi, kAca); break;   // '
            // keypad zxcvbnm,./ line
          case 0x7A: FeedbackSelect(kA, kZ); break;       // z
          case 0x78: FeedbackSelect(kS, kX); break;       // x
          case 0x63: FeedbackSelect(kD, kC); break;       // c
          case 0x76: FeedbackSelect(kF, kV); break;       // v
          case 0x62: FeedbackSelect(kF, kB); break;       // b
          case 0x6E: FeedbackSelect(kJ, kN); break;       // n
          case 0x6D: FeedbackSelect(kJ, kM); break;       // m
          case 0x2C: FeedbackSelect(kK, kCom); break;     // ,
          case 0x2E: FeedbackSelect(kL, kDot); break;     // .
          case 0x2F: FeedbackSelect(kSmi, kRs); break;    // /

            // keypad uppercase ~!@#$%^&*()_+ line
          case 0x7E: FeedbackSelect(kA, kAp, kRsh); break;// ~
          case 0x21: FeedbackSelect(kA, k1, kRsh); break; // !
          case 0x40: FeedbackSelect(kS, k2, kRsh); break; // @
          case 0x23: FeedbackSelect(kD, k3, kRsh); break; // #
          case 0x24: FeedbackSelect(kF, k4, kRsh); break; // $
          case 0x25: FeedbackSelect(kF, k5, kRsh); break; // %
          case 0x5E: FeedbackSelect(kJ, k6, kLsh); break; // ^
          case 0x26: FeedbackSelect(kJ, k7, kLsh); break; // &
          case 0x2A: FeedbackSelect(kK, k8, kLsh); break; // *
          case 0x28: FeedbackSelect(kL, k9, kLsh); break; // (
          case 0x29: FeedbackSelect(kSmi, k0, kLsh); break; // )
          case 0x5F: FeedbackSelect(kSmi, kMin, kLsh); break; // _
          case 0x2B: FeedbackSelect(kSmi, kEqu, kLsh); break; // +
            // keypad uppercase QWERTYUIOP{} line
          case 0x51: FeedbackSelect(kA, kQ, kRsh); break; // Q
          case 0x57: FeedbackSelect(kS, kW, kRsh); break; // W
          case 0x45: FeedbackSelect(kD, kE, kRsh); break; // E
          case 0x52: FeedbackSelect(kF, kR, kRsh); break; // R
          case 0x54: FeedbackSelect(kF, kT, kRsh); break; // T
          case 0x59: FeedbackSelect(kJ, kY, kLsh); break; // Y
          case 0x55: FeedbackSelect(kJ, kU, kLsh); break; // U
          case 0x49: FeedbackSelect(kK, kI, kLsh); break; // I
          case 0x4F: FeedbackSelect(kL, kO, kLsh); break; // O
          case 0x50: FeedbackSelect(kSmi, kP, kLsh); break; // P
          case 0x7B: FeedbackSelect(kSmi, kLsb, kLsh); break; // {
          case 0x7D: FeedbackSelect(kSmi, kRsb, kLsh); break; // }
            // keypad uppercase ASDFGHJKL:" line
          case 0x41: FeedbackSelect(kA, kA, kRsh); break; // A
          case 0x53: FeedbackSelect(kS, kS, kRsh); break; // S
          case 0x44: FeedbackSelect(kD, kD, kRsh); break; // D
          case 0x46: FeedbackSelect(kF, kF, kRsh); break; // F
          case 0x47: FeedbackSelect(kF, kG, kRsh); break; // G
          case 0x48: FeedbackSelect(kJ, kH, kLsh); break; // H
          case 0x4A: FeedbackSelect(kJ, kJ, kLsh); break; // J
          case 0x4B: FeedbackSelect(kK, kK, kLsh); break; // K
          case 0x4C: FeedbackSelect(kL, kL, kLsh); break; // L
          case 0x3A: FeedbackSelect(kSmi, kSmi, kLsh); break; // :
          case 0x22: FeedbackSelect(kSmi, kAca, kLsh); break; // "
            // keypad uppercase ZXCVBNM<>? line
          case 0x5A: FeedbackSelect(kA, kZ, kRsh); break; // Z
          case 0x58: FeedbackSelect(kS, kX, kRsh); break; // X
          case 0x43: FeedbackSelect(kD, kC, kRsh); break; // C
          case 0x56: FeedbackSelect(kF, kV, kRsh); break; // V
          case 0x42: FeedbackSelect(kF, kB, kRsh); break; // B
          case 0x4E: FeedbackSelect(kJ, kN, kLsh); break; // N
          case 0x4D: FeedbackSelect(kJ, kM, kLsh); break; // M
          case 0x3C: FeedbackSelect(kK, kCom, kLsh); break; // <
          case 0x3E: FeedbackSelect(kL, kDot, kLsh); break; // >
          case 0x3F: FeedbackSelect(kSmi, kRs, kLsh); break; // ?
        }
      }
      else
      {
        return;
      }
    }
    #endregion Typing feedback

    private void m_teacher_OnBadAnswer()
    {
      m_bBadDeal = true;
      txtTarget.BackColor = Color.LightPink;
    }

    private void m_teacher_OnGoodAnswer()
    {
      if (m_bBadDeal)
      {
        // recovering from bad answer
        m_bBadDeal = false;
        txtTarget.BackColor = Color.White;
      }

      char chOnTarget = '\x00';
      txtTarget.Text = m_teacher.RefreshTaskForward(out chOnTarget);

      Feedback(chOnTarget);
    }

    private void m_teacher_OnNewLine(int _iCurLine, int _iTotalLines)
    {
      // refresh statistic informators
      RefreshStatistic();
      ChangeLevelProgress(_iCurLine, _iTotalLines);

      // refresh cur level-info display from teacher
      char chOnTarget = '\x00';
      txtTarget.Text = "";
      txtTarget.Text = m_teacher.RefreshTaskForward(out chOnTarget);

      Feedback(chOnTarget);
    }

    private void m_teacher_OnNewLevel(string _strLevelInfo)
    {
      this.Text = FFingers.strProductVersion +" - "+ _strLevelInfo;
      pbLevel.Value = 0;

      lblErrors.Text = "0";
      m_cgError.Value = 0;

      lblSpeed.Text = "0";
      m_cgSpeed.Value = m_cgSpeed.Minimum;
    }

    private void ChangeLevelProgress(int _iCurLine, int _iTotalLines)
    {
      pbLevel.Maximum = _iTotalLines;
      pbLevel.Value = _iCurLine;
    }

    private void RefreshStatistic()
    {
      // refresh Speed label
      lblSpeed.Text = string.Format("{0:0}", m_teacher.Config.CharPermin);

      // refresh Speed progressbar
      if (m_teacher.Config.CharPermin > m_cgSpeed.Maximum)
      {
        m_cgSpeed.Value = m_cgSpeed.Maximum;
      }
      else if (m_teacher.Config.CharPermin < m_cgSpeed.Minimum)
      {
        m_cgSpeed.Value = m_cgSpeed.Minimum;
      }
      else
      {
        m_cgSpeed.Value = m_teacher.Config.CharPermin;
      }

      // refresh Error label
      lblErrors.Text = m_teacher.Config.ErrPermin.ToString();

      // refresh Error progressbar
      if (m_teacher.Config.ErrPermin <= m_cgError.Maximum)
      {
        m_cgError.Value = m_teacher.Config.ErrPermin*10;
      }
      else
      {
        m_cgError.Value = m_cgError.Maximum;
      }
    }
  };
}
