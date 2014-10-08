using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Appreq {
  public class TreeViewHelper {
    private static void AddNode(XmlNode inXmlNode, TreeNode inTreeNode) {
      XmlNode xNode;
      TreeNode tNode;
      XmlNodeList nodeList;
      var i = 0;
      if (inXmlNode.HasChildNodes) {
        nodeList = inXmlNode.ChildNodes;
        for (i = 0; i <= nodeList.Count - 1; i++) {
          xNode = inXmlNode.ChildNodes[i];
          var newTreeNode = new TreeNode(xNode.Name);
          // Set icon
          var check = xNode["CheckPassed"];
          if (null != check) {
            if (check.InnerText == "true") {
              newTreeNode.ImageKey = "accept";
              newTreeNode.SelectedImageKey = "accept";
            } else if (check.InnerText == "false") {
              newTreeNode.ImageKey = "alert";
              newTreeNode.SelectedImageKey = "alert";
            }
          }
          // Don't display the check node
          if (xNode.Name == "CheckPassed") {
            continue;
          }
          inTreeNode.Nodes.Add(newTreeNode);
          tNode = inTreeNode.Nodes[i];
          AddNode(xNode, tNode);
        }
      } else {
        inTreeNode.Text = inXmlNode.Value;
      }
    }

    public static void populateTreeView(Profile profile, TreeView treeView, bool diffOnly = false) {
      if (null == profile || null == treeView) {
        return;
      }
      var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
      var xs = new XmlSerializer(typeof(Profile));
      var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
      using (var ms = new MemoryStream()) {
        using (var xw = XmlWriter.Create(ms, settings)) {
          xs.Serialize(xw, profile, emptyNs);
          var xmldoc = new XmlDocument();
          ms.Seek(0, SeekOrigin.Begin);
          xmldoc.Load(ms);
          //var xmlnode = xmldoc.ChildNodes[0];
          treeView.Nodes.Clear();
          treeView.Nodes.Add(new TreeNode(xmldoc.DocumentElement.Name));
          var tNode = treeView.Nodes[0];
          if (diffOnly) {
            var nodes = xmldoc.SelectNodes("/Profile/*[CheckPassed]");
            if (null != nodes && nodes.Count > 0) {
              AddNode(nodes.Item(0), tNode);
            }
          } else {
            AddNode(xmldoc, tNode);
          }
        }
      }            
      foreach(TreeNode node in treeView.Nodes) {
        ExpandRecursive(node);
      }
      //treeView.ExpandAll();
    }
    public static void ExpandRecursive(TreeNode node) {
      if(null == node || 4 == node.Level) {
        return;
      }
      node.Expand();
      if (null != node.Nodes) {
        foreach (TreeNode innerNode in node.Nodes) {
          ExpandRecursive(innerNode);
        }
      }
    }
  }
}
