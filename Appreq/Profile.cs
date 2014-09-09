﻿using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Profile {    
    public App App { get; set; }    
    public Env Environment { get; set; }
    [XmlArray("Dependencies")]
    public App[] Dependencies { get; set; }
    public bool CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(Profile other) {
      other.IsDiffMode = true;
      //App.Diff(other.App);
      Environment.Diff(other.Environment);
    }
  }
}
