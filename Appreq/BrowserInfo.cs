using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Appreq {
  public class BrowserInfo {
    [XmlElement("Browser")]
    public List<Browser> Browser { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeBrowser() {
      return Browser != null && Browser.Count > 0;
    }
    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(BrowserInfo other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = null;
      if (null != Browser && null != other.Browser) {
        foreach (var browser in Browser) {
          foreach (var browserOther in other.Browser) {
            if (!browserOther.CheckPassed.GetValueOrDefault()) {
              browser.Diff(browserOther);
              if (browserOther.CheckPassed.GetValueOrDefault()) {
                other.CheckPassed = true;
              }
            }
          }
        }
      }
    }
  }
}
