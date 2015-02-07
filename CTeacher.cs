using System;
using System.Collections;
using System.Windows.Forms;

namespace NFingers
{
	public class CTeacher
	{
    public delegate void OnBadAnswerDelegate();
    public delegate void OnGoodAnswerDelegate();
    public delegate void OnNewLineDelegate(int _iCurLine, int _iTotalLines);
    public delegate void OnNewLevelDelegate(string _strLevelInfo);
    public event OnBadAnswerDelegate OnBadAnswer;
    public event OnGoodAnswerDelegate OnGoodAnswer;
    public event OnNewLineDelegate OnNewLine;
    public event OnNewLevelDelegate OnNewLevel;

    private SConfig m_cfg;        // copy of configuration from `FFingers`
    private ArrayList m_al;       // array of level-lines
    private string m_strLevelInfo;
    private string m_strCurLine;  // current task line (CTL)
    private int m_iCurLine;       // position index of CTL in `m_al`
    private int m_iCurChar;       // current char in CTL

    // speed: Char Per Minute. evaluated to ch/min number of pressed (good or bad) chars
    // measured each `ETeacherSay.NEXT_LINE` event
    private double m_dCpm;        // cpm per task-line
    private double m_dCpmAverage; // average cpm per level
    private double m_dEpmPercent;
    private double m_dEpmPercentAverage;

    private int m_iTyped;         // number of typed chars perline
    private int m_iMissTyped;     // number of misstyped chars perline

    // time stamp when current task-line completion is started
    private TimeSpan m_tsLineStart;
    // time stamp when current task-line is complate
    private TimeSpan m_tsLineDone;
    // ratio of misstyped chars to totaly typed per line

    private bool m_bPaused = false;
    private TimeSpan m_tsPauseMoment;

    /////////////////////////////////////////////////////////////////////////

    public SConfig Config
    {
      get { return m_cfg; }
      set { m_cfg = value; }
    }

    public string LevelInfo
    {
      get { return m_strLevelInfo; }

      // Level info it's a first (commonly commented with '#' char) line in
      // level definition file; this method sets this line whithout
      // a comment and ' '.
      set
      {
        if (value[0] == '#')
        {// skip leading '#' recursively
          LevelInfo = value.Substring(1);
        }
        else if (value[0] == ' ')
        {// skip ' ' after '#'
          m_strLevelInfo = value.Substring(1);
        }
        else
        {
          m_strLevelInfo = value;
        }
      }
    }

    /// <summary>
    /// Level is started if no key was processed
    /// </summary>
    public bool IsLevelStarted
    {
      get
      {
        return m_iCurLine > 1 || (m_iCurLine == 1 && m_iCurChar > 0);
      }
    }

    public bool PauseMode
    {
      get
      {
        return m_bPaused;
      }
      set
      {
        if (value)
        {
          if (!m_bPaused)
          {
            m_bPaused = true;
            m_tsPauseMoment = DateTime.Now.TimeOfDay;
          }
        }
        else
        {
          if (m_bPaused)
          {
            m_bPaused = false;
            m_tsLineStart = DateTime.Now.TimeOfDay - (m_tsPauseMoment - m_tsLineStart);
          }
        }
      }
    }

    /////////////////////////////////////////////////////////////////////////

    private CTeacher() { }

		public CTeacher(SConfig _cfg)
		{
      m_cfg = _cfg;
      Init();
		}

    public void Init()
    {
      m_al = CLevels.GetLevel(m_cfg.LastLevel, m_cfg.Filename);
      m_iCurLine = 1;
      m_iCurChar = 0;
      LevelInfo = (m_al[0] as string);
      m_strCurLine = (m_al[1] as string);

      if (OnNewLevel != null)
      {
        OnNewLevel(LevelInfo);
      }

      m_iTyped = 0;
      m_iMissTyped = 0;
      m_dEpmPercent = 0.0;
      m_dEpmPercentAverage = 0.0;
      m_dCpm = 0.0;
      m_dCpmAverage = 0.0;
      m_tsLineStart = new TimeSpan(0);
      m_tsLineDone = new TimeSpan(0);

      RefreshStatistic();
    }

    /// <summary>Statistic refreshement done at every `OnNextLine` event.</summary>
    private void RefreshStatistic()
    {
      if (0 == m_iTyped) { return; }
      TimeSpan tsMin = new TimeSpan(0, 1, 0); // 1 minute etalon

      m_tsLineDone = DateTime.Now.TimeOfDay - m_tsLineStart;
      // ratio of measured time regarding to 1 minute etalon
      double dRatio = m_tsLineDone.TotalSeconds / tsMin.TotalSeconds;

      /** number of characters typed per line is evaluated to
       * number of characters typed per minute */
      --m_iTyped;
      m_dCpm = ((dRatio != 0)? (m_iTyped / dRatio) : 0);
      m_dEpmPercent = ((m_iTyped != 0)? (100.0 * (double) m_iMissTyped / (double) m_iTyped) : 0);

      m_dCpmAverage = (m_dCpmAverage * (m_iCurLine - 1) + m_dCpm) / m_iCurLine;  
      m_cfg.CharPermin = Convert.ToInt32(m_dCpmAverage);

      m_dEpmPercentAverage = ((m_dEpmPercentAverage * (m_iCurLine - 1) + m_dEpmPercent) / m_iCurLine);
      m_cfg.ErrPermin = (int) m_dEpmPercentAverage;

      m_iTyped = 0;
      m_iMissTyped = 0;
    }

    public void Check(char _chUserHit)
    {
      ++m_iTyped;

      if (0 == m_iCurChar)
      {
        // time stamp of the current-line-execution-started taken
        // only at the moment when first char of it has been pressed
        m_tsLineStart = DateTime.Now.TimeOfDay;
      }

      // check the answer
      if (m_strCurLine[m_iCurChar] == _chUserHit)
      {
        // right answer
        if (m_iCurChar < (m_strCurLine.Length - 1))
        {
          // set next char in the current task-line
          ++m_iCurChar;

          if (OnGoodAnswer != null)
          {
            OnGoodAnswer();
          }
        }
        else
        {
          // jump to next line of the current task
          RefreshStatistic();
          
          ++m_iCurLine;
          m_iCurChar = 0;

          if (OnGoodAnswer != null)
          {
            OnGoodAnswer();
          }

          OnNewLine(m_iCurLine - 1, m_al.Count - 1);
        }
      }
      else
      {
        // wrong answer
        ++m_iMissTyped;

        if (OnBadAnswer != null)
        {
          OnBadAnswer();
        }
      }
    }

    public void RestartLevel()
    {
      Init();
    }

    public void SetLevelTo(int _iLevel)
    {
      m_cfg.LastLevel = _iLevel;
      Init();
    }

    public string RefreshTaskForward(out char _chOnTarget)
    {
      if (m_iCurLine > (m_al.Count - 1))
      {
        // current level is ended, look around about new level
        if ((m_dCpmAverage > m_cfg.LevelSpeedUp)
          && (m_dEpmPercentAverage < m_cfg.LevelErrorDown))
        {
          // typing-speed and error-precentage allow to encrease level up
          if ( (m_cfg.LastLevel != CLevels.LevelsCount)
            && (DialogResult.OK == MessageBox.Show(
            "Your typing skills allow to advance to the next level."
            +"\nIncreasing level?"
            , "Level is complete",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Information)) )
          {
            ++m_cfg.LastLevel;
          }
        }
        else if ((m_dCpmAverage < m_cfg.LevelSpeedDown)
          || (m_dEpmPercentAverage > m_cfg.LevelErrorDown))
        {
          // typning-speed or error percentage are too low - so decrease the level
          if ( (m_cfg.LastLevel != 1)
            && (DialogResult.OK == MessageBox.Show(
            "Your typing skills are too low for this level."
            + "\nDecreasing level?"
            , "Level is complete",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)) )
          {
            --m_cfg.LastLevel;
          }
        }
        else
        {
          // else if error percentage and typing speed are still in bounds
          // so stay at current level
          MessageBox.Show("Your typing skills are good however that is not enought to advance to the next level."
            +"\nKeep practicing."
            , "Level is complete"
            , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        Init();
      }

      m_strCurLine = m_al[m_iCurLine] as string;
      string strRet = m_strCurLine.Substring(m_iCurChar);
      _chOnTarget = strRet[0];

      return strRet;
    }

    public string RefreshTaskBackward(out char _chOnTarget)
    {
      string strRet;

      if (m_iCurChar >= 1)
      {// have leading chars
        --m_iCurChar;
      }

      strRet = m_strCurLine.Substring(m_iCurChar);
      _chOnTarget = strRet[0];
      return strRet;
    }
	};
}
