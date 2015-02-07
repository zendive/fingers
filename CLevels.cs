using System;
using System.Collections;
using System.Xml;
using System.Windows.Forms;

namespace NFingers
{
  // static methods provider
  public class CLevels
  {
    private static int m_iLevelsCount = 0;

    public static int LevelsCount
    {
      get { return m_iLevelsCount; }
    }

    private CLevels() { }

    public static ArrayList GetLevel(int _iLevel, string _strFilename)
    {
      XmlDocument xmlDoc = new XmlDocument();
      XmlNode xmlLevel = null;
      ArrayList alLines = new ArrayList(31);
      string strLevelInfo;

      try
      {
        xmlDoc.Load(_strFilename);
        m_iLevelsCount = xmlDoc.GetElementsByTagName("level").Count;

        // check on bounds
        if ((_iLevel < 1) || (_iLevel > m_iLevelsCount))
        {
          throw new ApplicationException(
            "@> CLevels::GetLevel(_iLevel="+_iLevel+", _strFilename="+_strFilename+") -> _iLevel out of bounds!");
        }

        // get required level
        xmlLevel = xmlDoc.GetElementsByTagName("level")[(_iLevel - 1)];

        // get level information...
        strLevelInfo = String.Format("Level {0} Keys {1}"
          , xmlLevel.Attributes["num"].InnerText
          , xmlLevel.Attributes["info"].InnerText);
        // ...add as first leaf (index 0)
        alLines.Add(strLevelInfo);

        // add all lines of the level
        foreach (XmlNode xmlLine in xmlLevel.ChildNodes)
        {
          alLines.Add(xmlLine.InnerText);
        }

        return alLines;
      }
      catch (Exception xcp)
      {
        throw new ApplicationException(xcp.Message, xcp);
      }
    }

  };
}
