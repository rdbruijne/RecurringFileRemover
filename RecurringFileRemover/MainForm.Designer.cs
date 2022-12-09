namespace RecurringFileRemover
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.FileListView = new System.Windows.Forms.ListView();
			this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TrayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuItem_Restore = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.intervalUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.RunOnStartupButton = new System.Windows.Forms.Button();
			this.TrayMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.intervalUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// FileListView
			// 
			this.FileListView.AllowDrop = true;
			this.FileListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.FileListView.Location = new System.Drawing.Point(12, 41);
			this.FileListView.Name = "FileListView";
			this.FileListView.Size = new System.Drawing.Size(532, 364);
			this.FileListView.TabIndex = 0;
			this.FileListView.UseCompatibleStateImageBehavior = false;
			this.FileListView.View = System.Windows.Forms.View.List;
			this.FileListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileListView_DragDrop);
			this.FileListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileListView_DragEnter);
			this.FileListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FileListView_KeyDown);
			// 
			// TrayIcon
			// 
			this.TrayIcon.ContextMenuStrip = this.TrayMenuStrip;
			this.TrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TrayIcon.Icon")));
			this.TrayIcon.Text = "Recurring File Remover";
			this.TrayIcon.Visible = true;
			// 
			// TrayMenuStrip
			// 
			this.TrayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Restore,
            this.MenuItem_Exit});
			this.TrayMenuStrip.Name = "contextMenuStrip1";
			this.TrayMenuStrip.Size = new System.Drawing.Size(114, 48);
			// 
			// MenuItem_Restore
			// 
			this.MenuItem_Restore.Name = "MenuItem_Restore";
			this.MenuItem_Restore.Size = new System.Drawing.Size(113, 22);
			this.MenuItem_Restore.Text = "Restore";
			this.MenuItem_Restore.Click += new System.EventHandler(this.MenuItem_Restore_Click);
			// 
			// MenuItem_Exit
			// 
			this.MenuItem_Exit.Name = "MenuItem_Exit";
			this.MenuItem_Exit.Size = new System.Drawing.Size(113, 22);
			this.MenuItem_Exit.Text = "Exit";
			this.MenuItem_Exit.Click += new System.EventHandler(this.MenuItem_Exit_Click);
			// 
			// intervalUpDown
			// 
			this.intervalUpDown.Location = new System.Drawing.Point(12, 12);
			this.intervalUpDown.Name = "intervalUpDown";
			this.intervalUpDown.Size = new System.Drawing.Size(120, 23);
			this.intervalUpDown.TabIndex = 1;
			this.intervalUpDown.ValueChanged += new System.EventHandler(this.IntervalUpDown_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(138, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Interval (in seconds)";
			// 
			// RunOnStartupButton
			// 
			this.RunOnStartupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RunOnStartupButton.Location = new System.Drawing.Point(12, 411);
			this.RunOnStartupButton.Name = "RunOnStartupButton";
			this.RunOnStartupButton.Size = new System.Drawing.Size(120, 23);
			this.RunOnStartupButton.TabIndex = 3;
			this.RunOnStartupButton.Text = "Run On Startup";
			this.RunOnStartupButton.UseVisualStyleBackColor = true;
			this.RunOnStartupButton.Click += new System.EventHandler(this.RunOnStartupButton_Click);
			// 
			// MainForm
			// 
			this.ClientSize = new System.Drawing.Size(556, 446);
			this.Controls.Add(this.RunOnStartupButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.intervalUpDown);
			this.Controls.Add(this.FileListView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Recurring File Remover";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.TrayMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.intervalUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ListView FileListView;
		private NotifyIcon TrayIcon;
		private ContextMenuStrip TrayMenuStrip;
		private ToolStripMenuItem MenuItem_Restore;
		private ToolStripMenuItem MenuItem_Exit;
		private NumericUpDown intervalUpDown;
		private Label label1;
		private Button RunOnStartupButton;
	}
}