using System;
using System.Data;
using System.Drawing;

namespace NFingers
{
  // static methods provider
  public class CConfig
  {
    /// <summary>
    /// mnemonic names of configuration xml file</summary>
    private enum XML
    {
      TableKeypad,
      TableNumpad,
      Filename,
      LastLevel,
      LevelUpBySpeed,
      LevelDownBySpeed,
      LevelDownByError,
      CharPerMinute,
      ErrorPerMinute,
      Fontname,
      Fontsize
    };

    private static string strConfigFilename = "."+System.IO.Path.DirectorySeparatorChar+@"fingers.config.xml";

    private static string ConfigFilename
    {
      get { return strConfigFilename; }
    }

    private CConfig() {}

    public static SConfig ReadConfig()
    {
      SConfig cfg = new SConfig();
      string strFontname;
      float fFontsize;

      try
      {
        DataSet ds = new DataSet(ConfigFilename);

        ds.ReadXmlSchema(ConfigFilename.Substring(0, ConfigFilename.Length - 3) + "xsd");
        ds.ReadXml(ConfigFilename);

        DataRow dr;
        dr = ds.Tables[XML.TableKeypad.ToString()].Rows[0];

        cfg.strKeypadFilename = dr[XML.Filename.ToString()].ToString();
        cfg.iKeypadLastLevel = Convert.ToInt32(dr[XML.LastLevel.ToString()].ToString());
        cfg.iKeypadLevelSpeedUp = Convert.ToInt32(dr[XML.LevelUpBySpeed.ToString()].ToString());
        cfg.iKeypadLevelSdeedDown = Convert.ToInt32(dr[XML.LevelDownBySpeed.ToString()].ToString());
        cfg.iKeypadLevelErrorDown = Convert.ToInt32(dr[XML.LevelDownByError.ToString()].ToString());
        cfg.iKeypadCharPermin = Convert.ToInt32(dr[XML.CharPerMinute.ToString()].ToString());
        cfg.iKeypadErrPermin = Convert.ToInt32(dr[XML.ErrorPerMinute.ToString()].ToString());

        strFontname = dr[XML.Fontname.ToString()].ToString();
        fFontsize = Convert.ToSingle(dr[XML.Fontsize.ToString()].ToString());
        cfg.fontKeypad = new Font(strFontname, fFontsize);

        dr = ds.Tables[XML.TableNumpad.ToString()].Rows[0];

        cfg.strNumpadFilename = dr[XML.Filename.ToString()].ToString();
        cfg.iNumpadLastLevel = Convert.ToInt32(dr[XML.LastLevel.ToString()].ToString());
        cfg.iNumpadLevelSpeedUp = Convert.ToInt32(dr[XML.LevelUpBySpeed.ToString()].ToString());
        cfg.iNumpadLevelSpeedDown = Convert.ToInt32(dr[XML.LevelDownBySpeed.ToString()].ToString());
        cfg.iNumpadLevelErrorDown = Convert.ToInt32(dr[XML.LevelDownByError.ToString()].ToString());
        cfg.iNumpadCharPermin = Convert.ToInt32(dr[XML.CharPerMinute.ToString()].ToString());
        cfg.iNumpadErrPermin = Convert.ToInt32(dr[XML.ErrorPerMinute.ToString()].ToString());

        strFontname = dr[XML.Fontname.ToString()].ToString();
        fFontsize = Convert.ToSingle(dr[XML.Fontsize.ToString()].ToString());
        cfg.fontNumpad = new Font(strFontname, fFontsize);
      }
      catch (Exception xcp)
      {
        throw new ApplicationException(xcp.Message, xcp);
      }

      return cfg;
    }

    public static void SaveConfig(SConfig _cfg)
    {
      try
      {
        DataSet ds = new DataSet(ConfigFilename);
        ds.ReadXmlSchema(ConfigFilename.Substring(0, ConfigFilename.Length - 3) + "xsd");
        ds.ReadXml(ConfigFilename);

        DataRow dr = ds.Tables[XML.TableKeypad.ToString()].Rows[0];

        dr[XML.Filename.ToString()] = _cfg.strKeypadFilename;
        dr[XML.LastLevel.ToString()] = _cfg.iKeypadLastLevel;
        dr[XML.LevelUpBySpeed.ToString()] = _cfg.iKeypadLevelSpeedUp;
        dr[XML.LevelDownBySpeed.ToString()] = _cfg.iKeypadLevelSdeedDown;
        dr[XML.LevelDownByError.ToString()] = _cfg.iKeypadLevelErrorDown;
        dr[XML.CharPerMinute.ToString()] = _cfg.iKeypadCharPermin;
        dr[XML.ErrorPerMinute.ToString()] = _cfg.iKeypadErrPermin;
        dr[XML.Fontname.ToString()] = _cfg.fontKeypad.Name;
        dr[XML.Fontsize.ToString()] = _cfg.fontKeypad.Size.ToString();

        dr = ds.Tables[XML.TableNumpad.ToString()].Rows[0];

        dr[XML.Filename.ToString()] = _cfg.strNumpadFilename;
        dr[XML.LastLevel.ToString()] = _cfg.iNumpadLastLevel;
        dr[XML.LevelUpBySpeed.ToString()] = _cfg.iNumpadLevelSpeedUp;
        dr[XML.LevelDownBySpeed.ToString()] = _cfg.iNumpadLevelSpeedDown;
        dr[XML.LevelDownByError.ToString()] = _cfg.iNumpadLevelErrorDown;
        dr[XML.CharPerMinute.ToString()] = _cfg.iNumpadCharPermin;
        dr[XML.ErrorPerMinute.ToString()] = _cfg.iNumpadErrPermin;
        dr[XML.Fontname.ToString()] = _cfg.fontNumpad.Name;
        dr[XML.Fontsize.ToString()] = _cfg.fontNumpad.Size.ToString();

        ds.WriteXml(ConfigFilename);
      }
      catch (Exception e)
      {
        throw e;
      }
    }

  };
}