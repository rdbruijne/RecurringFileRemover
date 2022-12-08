using Microsoft.Win32;

namespace RecurringFileRemover
{
	public partial class MainForm : Form
	{
		private double intervalInSeconds = 60;
		private List<string> pathsToRemove = new();
		private PeriodicTimer? removeInterval = null;

		// run on startup
		private static readonly string AppName = "RecurringFileRemover";
		private static readonly string StartupRegKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";



		//---------------------------------------------------------------------------------------------------------------------------
		// MainForm
		//---------------------------------------------------------------------------------------------------------------------------
		public MainForm()
		{
			InitializeComponent();
			LoadSettings();

			// update UI elements
			UpdateListview();
			UpdateIntervalUpDown();
			UpdateRunOnStartupButtonText();

			// run removal task
			var task = Task.Run(async () => await RemoverTask());
		}



		//---------------------------------------------------------------------------------------------------------------------------
		// Settings
		//---------------------------------------------------------------------------------------------------------------------------
		private static string GetSettingsDirectory()
		{
			return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.PathSeparator + AppName;
		}



		private static string GetSettingsFile()
		{
			return GetSettingsDirectory() + Path.PathSeparator + "settings.txt";
		}



		private void LoadSettings()
		{
			if (!Directory.Exists(GetSettingsDirectory()))
				return;

			if (!File.Exists(GetSettingsFile()))
				return;

			List<string> lines = File.ReadAllLines(GetSettingsFile()).ToList<string>();
			if (lines.Count > 0)
				_ = double.TryParse(lines[0], out intervalInSeconds);

			if (lines.Count > 1)
				pathsToRemove = lines.Skip(1).ToList();
		}



		private void SaveSettings()
		{
			if (!Directory.Exists(GetSettingsDirectory()))
				Directory.CreateDirectory(GetSettingsDirectory());

			File.WriteAllText(GetSettingsFile(), $"{intervalInSeconds}\n");
			File.AppendAllLines(GetSettingsFile(), pathsToRemove.ToArray());
		}



		//---------------------------------------------------------------------------------------------------------------------------
		// Functionality
		//---------------------------------------------------------------------------------------------------------------------------
		private void AddPathToRemove(string path)
		{
			path = path.Replace("\\", "/");
			pathsToRemove.Add(path);
			pathsToRemove = pathsToRemove.Distinct().ToList();
			pathsToRemove.Sort();
		}



		private void UpdateListview()
		{
			FileListView.Items.Clear();
			foreach (string path in pathsToRemove)
				FileListView.Items.Add(path);
		}



		private async Task RemoverTask()
		{
			removeInterval = new(TimeSpan.FromSeconds(intervalInSeconds));
			while (await removeInterval.WaitForNextTickAsync())
			{
				foreach (string path in pathsToRemove)
				{
					if (File.Exists(path))
						File.Delete(path);
					else if(Directory.Exists(path))
						Directory.Delete(path, true);
				}
				removeInterval = new(TimeSpan.FromSeconds(intervalInSeconds));
			}
		}



		private void RestoreFromTray()
		{
			Show();
			WindowState = FormWindowState.Normal;
			TrayIcon.Visible = false;
		}



		private void MinimizeToTray()
		{
			Hide();
			TrayIcon.Visible = true;
		}



		//---------------------------------------------------------------------------------------------------------------------------
		// Main Form Events
		//---------------------------------------------------------------------------------------------------------------------------
		private void MainForm_Resize(object sender, EventArgs e)
		{
			// Minimize to tray
			if (WindowState == FormWindowState.Minimized)
			{
				MinimizeToTray();
			}
		}



		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			removeInterval?.Dispose();
			SaveSettings();
		}



		private void MainForm_Shown(object sender, EventArgs e)
		{
			MinimizeToTray();
		}



		//---------------------------------------------------------------------------------------------------------------------------
		// Notify Icon Context Meny Events
		//---------------------------------------------------------------------------------------------------------------------------
		private void MenuItem_Restore_Click(object sender, EventArgs e)
		{
			RestoreFromTray();
		}



		private void MenuItem_Exit_Click(object sender, EventArgs e)
		{
			Close();
		}



		//---------------------------------------------------------------------------------------------------------------------------
		// fileListView Events
		//---------------------------------------------------------------------------------------------------------------------------
		private void FileListView_DragDrop(object sender, DragEventArgs e)
		{
			string[]? droppedPaths = (e?.Data?.GetData(DataFormats.FileDrop, true) as string[]) ?? null;
			if (droppedPaths == null)
				return;

			foreach (string path in droppedPaths)
				AddPathToRemove(path);
			UpdateListview();
		}



		private void FileListView_DragEnter(object sender, DragEventArgs e)
		{
			if (e == null)
				return;

			if (e.Data?.GetDataPresent(DataFormats.FileDrop) ?? false)
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}



		private void FileListView_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				foreach (ListViewItem item in FileListView.SelectedItems)
					FileListView.Items.Remove(item);

			// rebuild pathsToRemove
			pathsToRemove.Clear();
			foreach (ListViewItem item in FileListView.Items)
				pathsToRemove.Add(item.Text);

			SaveSettings();
		}



		//---------------------------------------------------------------------------------------------------------------------------
		// Interval Events
		//---------------------------------------------------------------------------------------------------------------------------
		private void UpdateIntervalUpDown()
		{
			intervalUpDown.Value = new Decimal(intervalInSeconds);
		}



		private void IntervalUpDown_ValueChanged(object sender, EventArgs e)
		{
			intervalInSeconds = Decimal.ToDouble(intervalUpDown.Value);
			SaveSettings();
		}



		//---------------------------------------------------------------------------------------------------------------------------
		// Run on Startup
		//---------------------------------------------------------------------------------------------------------------------------
		private static bool RunsAtStartup()
		{
			RegistryKey? key = Registry.CurrentUser.OpenSubKey(StartupRegKey, true);
			if (key == null)
				return false;

			object? val = key.GetValue(AppName);
			return val != null && val.Equals(Application.ExecutablePath);
		}



		private void UpdateRunOnStartupButtonText()
		{
			RunOnStartupButton.Text = RunsAtStartup() ? "Disable on startup" : "Run on startup";
		}



		private void RunOnStartupButton_Click(object sender, EventArgs e)
		{
			RegistryKey? key = Registry.CurrentUser.OpenSubKey(StartupRegKey, true);
			if (key == null)
				return;

			if (RunsAtStartup())
				key.DeleteValue(AppName, false);
			else
				key.SetValue(AppName, Application.ExecutablePath);

			UpdateRunOnStartupButtonText();
		}
	}
}
