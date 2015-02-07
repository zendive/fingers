using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace NCurveGram
{
	/// <summary>
	/// Summary description for CC_CurveGram.
	/// </summary>
	[ToolboxBitmap(typeof(CC_CurveGram), "CC_CurveGram.ico")]
  public class CC_CurveGram : System.Windows.Forms.Control
  {
    #region class AlarmStateChangeEventArgs
    public class AlarmStateChangeEventArgs
    {
      private CC_CurveGram m_cgSender = null;
      private bool m_bPreviousState;
      private bool m_bCurrentState;

      public CC_CurveGram Sender
      {
        get { return m_cgSender; }
        set { m_cgSender = value; }
      }

      public bool PreviousState
      {
        get { return m_bPreviousState; }
        set { m_bPreviousState = value; }
      }

      public bool CurrentState
      {
        get { return m_bCurrentState; }
        set { m_bCurrentState = value; }
      }
    }
    #endregion class AlarmStateChangeEventArgs

    public delegate void OnAlarmStateChange_Delegate(AlarmStateChangeEventArgs _args);
    public event OnAlarmStateChange_Delegate OnAlarmStateChange;

    private bool m_bFirst = true;       // flag of first draw after control creation
    private float m_fCos_ArrowAngle = 0f;
    private float m_fSin_ArrowAngle = 0f;

    // Control Limits definition
    private int m_iValue = 0;
    private int m_iMin = 0;
    private int m_iMax = 100;
    private float m_fUTurnBase = 100f;  // u-turn base zero angle [regarding to 0X axis, clockwise]
    private float m_fUTurn = 160f;      // u-turn, arrow working area
    private int m_iTickFrequency = 10;

    // Arrow definition
    private float m_fArrowAngle = 0f;             // curr. value angle for the arrow
    private float m_fArrowWidth = 3f;             // width of arrow pie in grads
    private Color m_clrArrow = Color.DarkOrange;  // arrow color

    // Arrow-Base definition
    private float m_fBaseDmtr = 20;               // arrow base diameter
    private float m_fFillDmtr = 19;               // diameter for gradient fill of the arrow base
    private Color m_clrBaseBorder = Color.Gray;
    private Color m_clrBaseCircle = Color.Gray;   // arrow base color 1
    private Color m_clrBaseCircleGlare = Color.LightGray; // arrow base color 2

    // Alarm-state definition
    private bool m_bAlarm = false;                // control alarm state
    private bool m_bShowAlarm = false;            // while alarmed, the arrow-base changes color
    private Color m_clrAlarm = Color.Red;
    // timer to allow colored signaling while in Alarmed state
    private System.Windows.Forms.Timer m_timerAlarm;

    // Auto-Alarm definition
    private bool m_bEnableAlarmCondition = false; // activates checking alarm conditions
    // range of hot values, reaching thouse - control will triger alarm and will post the event
    private int m_iAlarmRangeMin = 100;
    private int m_iAlarmRangeMax = 100;

    // Scale definition
    private Pen m_penScale = new Pen(Color.Black, 2f);// control border
    private Point m_pCenter;                      // center point of the control

    // drawing tools
    private Graphics m_G;                         // initialized on paint event, cross used
    private GraphicsPath m_pathScale = null;
    private GraphicsPath m_pathScaleMark = null;
    private GraphicsPath m_pathBaseBorder = new GraphicsPath();
    private GraphicsPath m_pathBaseBody = new GraphicsPath();
    private PathGradientBrush m_brushBaseBody = null;
    private GraphicsPath m_pathArrow = new GraphicsPath();

    private System.ComponentModel.IContainer components;

    /// <summary>calculable center of the control</summary>
    private Point _0
    {
      get { return m_pCenter; }
      set { m_pCenter = value; }
    }

    [System.ComponentModel.Browsable(false)]
    public bool Alarm
    {
      get { return m_timerAlarm.Enabled; }
      set
      {
        // do not set alarm while automatic alarm control is enabled
        if (m_bEnableAlarmCondition)
        {
          return;
        }

        m_bAlarm = value;
        m_timerAlarm.Enabled = m_bAlarm;
        m_bShowAlarm = value;
        this.Refresh();
      }
    }

    #region USER PROPERTIES
    public int Value
    {
      get { return m_iValue; }
      set
      {
        if (value > Maximum)
        {
          m_iValue = Maximum;
        }
        else if (value < Minimum)
        {
          m_iValue = Minimum;
        }
        else
        {
          m_iValue = value;
        }

        // Recalculate the Arow-Angle in radians
        float fRealMax = Maximum - Minimum;
        float fRealValue = Value - Minimum;
        float fFactor = (fRealValue)/fRealMax;  // factor on the scale of Min & Max

        // arrow angle regarding to 0X axis clockwise
        m_fArrowAngle = (float) ToRadians(fFactor * m_fUTurn + m_fUTurnBase);

        // prepeare trigonometry too
        m_fCos_ArrowAngle = (float)Math.Cos(m_fArrowAngle);
        m_fSin_ArrowAngle = (float)Math.Sin(m_fArrowAngle);

        CheckAlarmCondition();

        this.Refresh();
      }
    }

    public bool EnableAlarmCondition
    {
      get { return m_bEnableAlarmCondition; }
      set
      {
        m_bEnableAlarmCondition = value;

        CheckAlarmCondition();
      }
    }

    public int AlarmConditionMax
    {
      get { return m_iAlarmRangeMax; }
      set {
        m_iAlarmRangeMax = value;
        CheckAlarmCondition();
      }
    }

    public int AlarmConditionMin
    {
      get { return m_iAlarmRangeMin; }
      set {
        m_iAlarmRangeMin = value;
        CheckAlarmCondition();
      }
    }

    public int Minimum
    {
      get { return m_iMin; }
      set
      {
        m_iMin = value;
        this.ResetStaticGPath();
        this.Refresh();
      }
    }

    public int Maximum
    {
      get { return m_iMax; }
      set
      {
        m_iMax = value;
        this.ResetStaticGPath();
        this.Refresh();
      }
    }

    // in degrees
    public float UTurn
    {
      get { return m_fUTurn; }
      set
      {
        m_fUTurn = (value%341);

        ResetStaticGPath();
        this.Refresh();
      }
    }

    public float UTurnBase
    {
      get { return m_fUTurnBase; }
      set
      {
        m_fUTurnBase = Math.Abs(value%341);

        ResetStaticGPath();
        this.Refresh();
      }
    }

    public int TickFrequency
    {
      get { return m_iTickFrequency; }
      set { m_iTickFrequency = value; }
    }

    public float ArrowWidth
    {
      get { return m_fArrowWidth; }
      set { m_fArrowWidth = value; this.Refresh(); }
    }

    public Color ArrowColor
    {
      get { return m_clrArrow; }
      set { m_clrArrow = value; this.Refresh(); }
    }

    public Color BorderColor
    {
      get { return m_penScale.Color; }
      set { m_penScale.Color = value; this.Refresh(); }
    }

    public Color BaseCircleColor
    {
      get { return m_clrBaseCircle; }
      set { m_clrBaseCircle = value; this.Refresh(); }
    }

    public Color BaseCircleGlareColor
    {
      get { return m_clrBaseCircleGlare; }
      set { m_clrBaseCircleGlare = value; this.Refresh(); }
    }

    public Color BaseBorderColor
    {
      get { return m_clrBaseBorder; }
      set { m_clrBaseBorder = value; this.Refresh(); }
    }

    public Color AlarmColor
    {
      get { return m_clrAlarm; }
      set { m_clrAlarm = value; }
    }

    public float BaseDiameter
    {
      get { return m_fBaseDmtr; }
      set
      {
        m_fBaseDmtr = value;
        m_fFillDmtr = m_fBaseDmtr - 1;
        this.Refresh();
      }
    }
    #endregion USER PROPERTIES

    public CC_CurveGram()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

      m_timerAlarm.Tick += new EventHandler(m_timerAlarm_Tick);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.components = new System.ComponentModel.Container();
      this.m_timerAlarm = new System.Windows.Forms.Timer(this.components);
      // 
      // m_timerAlarm
      // 
      this.m_timerAlarm.Interval = 300;

    }
		#endregion

    private void m_timerAlarm_Tick(object sender, EventArgs e)
    {
      // change alarm state [color in the arrow base depends on that]
      m_bShowAlarm = !m_bShowAlarm;
      this.Refresh();
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick (e);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      DefineCenter();
      ResetStaticGPath();
      this.Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      Init(e.Graphics);

      DrawScaleBody();
      DrawArrow();
      DrawArrowBase();

      m_G.DrawPath(m_penScale, m_pathScale);
      m_G.DrawPath(new Pen(m_penScale.Color, 1f), m_pathScaleMark);
      m_G.DrawPath(new Pen(m_clrBaseBorder, 2.5f), m_pathBaseBorder);
      m_G.FillPath(m_brushBaseBody, m_pathBaseBody);
      m_G.DrawPath(new Pen(m_clrArrow, m_fArrowWidth), m_pathArrow);
    }

    //----------------------------------------------------------------
    private void Init(Graphics _G)
    {
      m_G = _G;
      
      if (m_bFirst)
      {
        DefineCenter();
        m_bFirst = !m_bFirst;
      }
    }

    private void DrawScaleBody()
    {
      // calculate Scale path only once
      if (m_pathScale.PointCount == 0)
      {
        ResetStaticGPath();

        // Draw Scale
        float a = (Width)/2 -1;
        float b = (Height)/2 -1;
        PointF ptScale = new PointF(_0.X, _0.Y);
        PointF ptMark = new PointF(0, 0);

        float fRealMax = Maximum - Minimum;
        float fFactor;
        float fCos_Angle;
        float fSin_Angle;
        float fAngle;

        // loop through the Min value to the Max
        for (int i = 0; i <= fRealMax; i++)
        {
          fFactor = i/fRealMax; // relation factor on the scale of Min & Max
                                // arrow angle regarding to 0X axis clockwise
          fAngle = (float) ToRadians(fFactor * m_fUTurn + m_fUTurnBase);

          fCos_Angle = (float) Math.Cos(fAngle);
          fSin_Angle = (float) Math.Sin(fAngle);

          ptScale.X = (a) *  fCos_Angle + _0.X;
          ptScale.Y = (b) *  fSin_Angle + _0.Y;

          m_pathScale.AddLine(ptScale, ptScale);  

          // Draw Marks eath 10 degrees
          if (i%m_iTickFrequency == 0)
          {
            ptMark.X = (a*0.9f) * fCos_Angle + _0.X;
            ptMark.Y = (b*0.9f) * fSin_Angle + _0.Y;

            m_pathScaleMark.AddLine(ptMark, ptScale);
            m_pathScaleMark.CloseFigure();
          }
        }

        m_pathScale.AddLine(ptScale, ptScale);

        float DBrdr = m_fBaseDmtr + 4;  // outer boarder diameter
        m_pathScale.AddEllipse(_0.X - DBrdr/2f, _0.Y - DBrdr/2f, DBrdr+1, DBrdr+1);
      }

      // to draw arrow-base midle boarder
      m_pathBaseBorder.Reset();
      m_pathBaseBorder.AddEllipse(_0.X - m_fBaseDmtr/2f, _0.Y - m_fBaseDmtr/2f
        , m_fBaseDmtr+1, m_fBaseDmtr+1);
    }

    private void DrawArrowBase()
    {
      m_pathBaseBody.Reset();
      m_pathBaseBody.AddEllipse(_0.X - m_fFillDmtr/2f, _0.Y - m_fFillDmtr/2f
        , m_fFillDmtr+1, m_fFillDmtr+1);

      // define gradient brush colors
      m_brushBaseBody = new PathGradientBrush(m_pathBaseBody);

      // differ base colors for alarmed state
      m_brushBaseBody.SurroundColors = ((m_bShowAlarm)?
        (new Color[] { m_clrAlarm }) : (new Color[] { m_clrBaseCircle }));

      // gradient angle repeats arrow angle and moves synchroniosly
      PointF ptGlare = new PointF(_0.X, _0.Y);

      ptGlare.X += (m_fFillDmtr*0.4f) * m_fCos_ArrowAngle;
      ptGlare.Y += (m_fFillDmtr*0.4f) * m_fSin_ArrowAngle;

      m_brushBaseBody.CenterPoint = ptGlare;
      m_brushBaseBody.CenterColor = m_clrBaseCircleGlare;
    }

    private void DrawArrow()
    {
      PointF ptPica = new PointF(_0.X, _0.Y);
      PointF ptArrowBase = new PointF(_0.X, _0.Y);
      float a = Width/2f;
      float b = Height/2f;

      ptPica.X += a * m_fCos_ArrowAngle;
      ptPica.Y += b * m_fSin_ArrowAngle;
      ptArrowBase.X += (a*0.6f) * m_fCos_ArrowAngle;
      ptArrowBase.Y += (b*0.6f) * m_fSin_ArrowAngle;

      m_pathArrow.Reset();
      m_pathArrow.AddLine(ptPica, ptArrowBase);
    }

    // convert Degrees to Radians
    private double ToRadians(double _fAngle)
    {
      return (_fAngle*Math.PI)/180.0d;
    }

    private void DefineCenter()
    {
      _0 = new Point((Width-1)/2, (Height-1)/2);  // Center
    }

    private void ResetStaticGPath()
    {
      if (m_pathScale == null)
      {
        m_pathScale = new GraphicsPath();
      }
      else
      {
        m_pathScale.Reset();
      }

      if (m_pathScaleMark == null)
      {
        m_pathScaleMark = new GraphicsPath();
      }
      else
      {
        m_pathScaleMark.Reset();
      }
    }

    private void CheckAlarmCondition()
    {
      if (!m_bEnableAlarmCondition)
      {
        return;
      }

      bool bOldAlarmState = m_bAlarm;
      bool bNewAlarmState = ((m_iValue >= m_iAlarmRangeMin) && (m_iValue <= m_iAlarmRangeMax));

      if (bOldAlarmState != bNewAlarmState)
      {
        m_bAlarm = bNewAlarmState;

        // change alarm signaling sequence
        m_bShowAlarm = m_bAlarm;
        m_timerAlarm.Enabled = m_bAlarm;

        if (OnAlarmStateChange != null)
        {
          AlarmStateChangeEventArgs args = new AlarmStateChangeEventArgs();
          args.Sender = this;
          args.PreviousState = bOldAlarmState;
          args.CurrentState = bNewAlarmState;

          OnAlarmStateChange(args);
        }
      }
    }

    public void SetAlarmCondition(int _iAlarmRangeMin, int _iAlarmRangeMax)
    {
      m_iAlarmRangeMin = _iAlarmRangeMin;
      m_iAlarmRangeMax = _iAlarmRangeMax;

      CheckAlarmCondition();
    }

  };
}
