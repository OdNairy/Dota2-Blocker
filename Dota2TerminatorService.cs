using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;

public partial class Dota2TerminatorService : ServiceBase
{
    private Timer _timer;

    public Dota2TerminatorService()
    {
        //InitializeComponent();
    }

    protected override void OnStart(string[] args)
    {
        _timer = new Timer(5000); // Check every 5 seconds
        _timer.Elapsed += OnElapsedTime;
        _timer.Start();
    }

    private void OnElapsedTime(object sender, ElapsedEventArgs e)
    {
        foreach (var process in Process.GetProcessesByName("chrome"))
        {
            try
            {
                process.Kill();
                EventLog.WriteEntry("Dota2TerminatorService", "dota2.exe process terminated.", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Dota2TerminatorService", $"Failed to terminate dota2.exe: {ex.Message}", EventLogEntryType.Error);
            }
        }
    }

    protected override void OnStop()
    {
        _timer.Stop();
        _timer.Dispose();
    }
}
