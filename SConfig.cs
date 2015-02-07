using System;
using System.Drawing;

namespace NFingers
{
  public enum EMode
  {
    Keypad,
    Numpad
  };

  public struct SConfig
  {
    // common
    public EMode eMode;
    // keypad
    public string strKeypadFilename;
    public int iKeypadLastLevel;
    public int iKeypadLevelSpeedUp;
    public int iKeypadLevelSdeedDown;
    public int iKeypadLevelErrorDown;
    public int iKeypadCharPermin;
    public int iKeypadErrPermin;
    public Font fontKeypad;
    // numpad
    public string strNumpadFilename;
    public int iNumpadLastLevel;
    public int iNumpadLevelSpeedUp;
    public int iNumpadLevelSpeedDown;
    public int iNumpadLevelErrorDown;
    public int iNumpadCharPermin;
    public int iNumpadErrPermin;
    public Font fontNumpad;

    public string Filename
    {
      get
      {
        if (eMode == EMode.Keypad)
        {
          return strKeypadFilename;
        }
        else
        {
          return strNumpadFilename;
        }
      }
    }

    public int LastLevel
    {
      get
      {
        if (eMode == EMode.Keypad)
        {
          return iKeypadLastLevel;
        }
        else
        {
          return iNumpadLastLevel;
        }
      }
      set
      {
        if ((value >=1) && (value <= CLevels.LevelsCount))
        {
          if (eMode == EMode.Keypad)
          {
            iKeypadLastLevel = value;
          }
          else
          {
            iNumpadLastLevel = value;
          }
        }
      }
    }

    public int LevelSpeedUp
    {
      get
      {
        if (eMode == EMode.Keypad)
        {
          return iKeypadLevelSpeedUp;
        }
        else
        {
          return iNumpadLevelSpeedUp;
        }
      }
    }

    public int LevelSpeedDown
    {
      get
      {
        if (eMode == EMode.Keypad)
        {
          return iKeypadLevelSdeedDown;
        }
        else
        {
          return iNumpadLevelSpeedDown;
        }
      }
    }

    public int LevelErrorDown
    {
      get
      {
        if (eMode == EMode.Keypad)
        {
          return iKeypadLevelErrorDown;
        }
        else
        {
          return iNumpadLevelErrorDown;
        }
      }
    }

    public int CharPermin
    {
      get
      {
        if (eMode == EMode.Keypad)
        {
          return iKeypadCharPermin;
        }
        else
        {
          return iNumpadCharPermin;
        }
      }
      set
      {
        if (eMode == EMode.Keypad)
        {
          iKeypadCharPermin = value;
        }
        else
        {
          iNumpadCharPermin = value;
        }
      }
    }

    public int ErrPermin
    {
      get
      {
        if (eMode == EMode.Keypad)
        {
          return iKeypadErrPermin;
        }
        else
        {
          return iNumpadErrPermin;
        }
      }
      set
      {
        if (eMode == EMode.Keypad)
        {
          iKeypadErrPermin = value;
        }
        else
        {
          iNumpadErrPermin = value;
        }
      }
    }

    public System.Drawing.Font Font
    {
      get
      {
        if (eMode == EMode.Keypad)
        {
          return fontKeypad;
        }
        else
        {
          return fontNumpad;
        }
      }
    }

  };
}
