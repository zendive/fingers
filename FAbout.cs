using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace NFingers
{
  public class FAbout : System.Windows.Forms.Form
  {
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.TabControl tabAbout;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtAbout;
    private System.Windows.Forms.TextBox txtLicese;
    private System.Windows.Forms.TabPage tabAbout_License;
    private System.Windows.Forms.TabPage tabAbout_Author;
    private System.Windows.Forms.TabPage tabAbout_About;
    private System.Windows.Forms.TabPage tabAbout_Thanks;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Label label9;
    private System.ComponentModel.Container components = null;

    public FAbout()
    {
      InitializeComponent();
    }

    protected override void Dispose( bool disposing )
    {
      if (disposing)
      {
        if (components != null)
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
        this.btnOK = new System.Windows.Forms.Button();
        this.tabAbout = new System.Windows.Forms.TabControl();
        this.tabAbout_About = new System.Windows.Forms.TabPage();
        this.txtAbout = new System.Windows.Forms.TextBox();
        this.tabAbout_Author = new System.Windows.Forms.TabPage();
        this.label9 = new System.Windows.Forms.Label();
        this.linkLabel1 = new System.Windows.Forms.LinkLabel();
        this.label4 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.tabAbout_License = new System.Windows.Forms.TabPage();
        this.txtLicese = new System.Windows.Forms.TextBox();
        this.tabAbout_Thanks = new System.Windows.Forms.TabPage();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.tabAbout.SuspendLayout();
        this.tabAbout_About.SuspendLayout();
        this.tabAbout_Author.SuspendLayout();
        this.tabAbout_License.SuspendLayout();
        this.tabAbout_Thanks.SuspendLayout();
        this.SuspendLayout();
        // 
        // btnOK
        // 
        this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
        this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnOK.Location = new System.Drawing.Point(144, 377);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new System.Drawing.Size(72, 24);
        this.btnOK.TabIndex = 0;
        this.btnOK.Text = "OK";
        this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
        // 
        // tabAbout
        // 
        this.tabAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.tabAbout.Controls.Add(this.tabAbout_About);
        this.tabAbout.Controls.Add(this.tabAbout_Author);
        this.tabAbout.Controls.Add(this.tabAbout_License);
        this.tabAbout.Controls.Add(this.tabAbout_Thanks);
        this.tabAbout.Location = new System.Drawing.Point(0, 0);
        this.tabAbout.Name = "tabAbout";
        this.tabAbout.SelectedIndex = 0;
        this.tabAbout.Size = new System.Drawing.Size(360, 369);
        this.tabAbout.TabIndex = 1;
        // 
        // tabAbout_About
        // 
        this.tabAbout_About.Controls.Add(this.txtAbout);
        this.tabAbout_About.Location = new System.Drawing.Point(4, 22);
        this.tabAbout_About.Name = "tabAbout_About";
        this.tabAbout_About.Size = new System.Drawing.Size(352, 343);
        this.tabAbout_About.TabIndex = 2;
        this.tabAbout_About.Text = "About";
        // 
        // txtAbout
        // 
        this.txtAbout.CausesValidation = false;
        this.txtAbout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtAbout.Location = new System.Drawing.Point(0, 0);
        this.txtAbout.Multiline = true;
        this.txtAbout.Name = "txtAbout";
        this.txtAbout.ReadOnly = true;
        this.txtAbout.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.txtAbout.Size = new System.Drawing.Size(352, 343);
        this.txtAbout.TabIndex = 0;
        // 
        // tabAbout_Author
        // 
        this.tabAbout_Author.Controls.Add(this.label9);
        this.tabAbout_Author.Controls.Add(this.linkLabel1);
        this.tabAbout_Author.Controls.Add(this.label4);
        this.tabAbout_Author.Controls.Add(this.label3);
        this.tabAbout_Author.Controls.Add(this.label2);
        this.tabAbout_Author.Controls.Add(this.label1);
        this.tabAbout_Author.Location = new System.Drawing.Point(4, 22);
        this.tabAbout_Author.Name = "tabAbout_Author";
        this.tabAbout_Author.Size = new System.Drawing.Size(352, 343);
        this.tabAbout_Author.TabIndex = 1;
        this.tabAbout_Author.Text = "Author";
        // 
        // label9
        // 
        this.label9.Location = new System.Drawing.Point(72, 40);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(272, 16);
        this.label9.TabIndex = 5;
        // 
        // linkLabel1
        // 
        this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
        this.linkLabel1.Location = new System.Drawing.Point(72, 96);
        this.linkLabel1.Name = "linkLabel1";
        this.linkLabel1.Size = new System.Drawing.Size(272, 16);
        this.linkLabel1.TabIndex = 4;
        this.linkLabel1.TabStop = true;
        this.linkLabel1.Text = "http://edu.kde.org/ktouch";
        this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
        // 
        // label4
        // 
        this.label4.Location = new System.Drawing.Point(72, 80);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(272, 16);
        this.label4.TabIndex = 3;
        this.label4.Text = "KTouch 1.2 (GPL KDE Linux)";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.label3.Location = new System.Drawing.Point(8, 64);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(168, 13);
        this.label3.TabIndex = 2;
        this.label3.Text = "Inspiration and original idea:";
        // 
        // label2
        // 
        this.label2.Location = new System.Drawing.Point(72, 24);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(272, 16);
        this.label2.TabIndex = 1;
        this.label2.Text = "Copyright © 2011 Block Alexander";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.label1.Location = new System.Drawing.Point(8, 8);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(89, 13);
        this.label1.TabIndex = 0;
        this.label1.Text = "Programmer/s:";
        // 
        // tabAbout_License
        // 
        this.tabAbout_License.AutoScroll = true;
        this.tabAbout_License.Controls.Add(this.txtLicese);
        this.tabAbout_License.Location = new System.Drawing.Point(4, 22);
        this.tabAbout_License.Name = "tabAbout_License";
        this.tabAbout_License.Size = new System.Drawing.Size(352, 343);
        this.tabAbout_License.TabIndex = 0;
        this.tabAbout_License.Text = "License Agreement";
        // 
        // txtLicese
        // 
        this.txtLicese.CausesValidation = false;
        this.txtLicese.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtLicese.Location = new System.Drawing.Point(0, 0);
        this.txtLicese.MaxLength = 18353;
        this.txtLicese.Multiline = true;
        this.txtLicese.Name = "txtLicese";
        this.txtLicese.ReadOnly = true;
        this.txtLicese.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.txtLicese.Size = new System.Drawing.Size(352, 343);
        this.txtLicese.TabIndex = 0;
        this.txtLicese.WordWrap = false;
        // 
        // tabAbout_Thanks
        // 
        this.tabAbout_Thanks.Controls.Add(this.label8);
        this.tabAbout_Thanks.Controls.Add(this.label7);
        this.tabAbout_Thanks.Controls.Add(this.label6);
        this.tabAbout_Thanks.Location = new System.Drawing.Point(4, 22);
        this.tabAbout_Thanks.Name = "tabAbout_Thanks";
        this.tabAbout_Thanks.Size = new System.Drawing.Size(352, 343);
        this.tabAbout_Thanks.TabIndex = 3;
        this.tabAbout_Thanks.Text = "Special thanks";
        // 
        // label8
        // 
        this.label8.Location = new System.Drawing.Point(101, 120);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(86, 16);
        this.label8.TabIndex = 2;
        this.label8.Text = "keep in that way";
        this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // label7
        // 
        this.label7.Location = new System.Drawing.Point(8, 63);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(272, 40);
        this.label7.TabIndex = 1;
        this.label7.Text = "Thank to .NET Framework develorer team.";
        // 
        // label6
        // 
        this.label6.Location = new System.Drawing.Point(8, 8);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(272, 32);
        this.label6.TabIndex = 0;
        this.label6.Text = "Thank to Haavard Froeiland and Steinar Theigmann (ktouch).";
        // 
        // FAbout
        // 
        this.AcceptButton = this.btnOK;
        this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
        this.CancelButton = this.btnOK;
        this.ClientSize = new System.Drawing.Size(360, 406);
        this.Controls.Add(this.tabAbout);
        this.Controls.Add(this.btnOK);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FAbout";
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "About Fingers";
        this.Load += new System.EventHandler(this.FAbout_Load);
        this.tabAbout.ResumeLayout(false);
        this.tabAbout_About.ResumeLayout(false);
        this.tabAbout_About.PerformLayout();
        this.tabAbout_Author.ResumeLayout(false);
        this.tabAbout_Author.PerformLayout();
        this.tabAbout_License.ResumeLayout(false);
        this.tabAbout_License.PerformLayout();
        this.tabAbout_Thanks.ResumeLayout(false);
        this.ResumeLayout(false);

    }
    #endregion

    private void FAbout_Load(object sender, System.EventArgs e)
    {
      // LOAD LICENSE FILE
      try
      {
        StreamReader sr = File.OpenText(@"LICENSE.txt");
        using (sr)
        {
          txtLicese.Text = sr.ReadToEnd();
        }
      }
      catch
      {
        txtLicese.Text = 
          "License.txt file with GPL2 license is not found."
          + System.Environment.NewLine
          + "Please reinstall this programm"
          + System.Environment.NewLine
          + "or discover this license in WEB.";
      }

      // LOAD ABOUT FILE
      try
      {
        StreamReader sr = File.OpenText(@"ABOUT.txt");
        using (sr)
        {
          txtAbout.Text = sr.ReadToEnd();
        }
      }
      catch
      {
        txtAbout.Text =
          "About.txt file about this program is not found."
          + System.Environment.NewLine
          + "Please reinstall his programm.";
      }
    }

    private void btnOK_Click(object sender, System.EventArgs e)
    {
      this.Close();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        LinkLabel link = sender as LinkLabel;
        if (null == link) { return; }

        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = link.Text;
        proc.StartInfo.UseShellExecute = true;

        proc.Start();
      }
      catch
      {
        ;
      }
    }

  };
}
