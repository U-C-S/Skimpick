﻿namespace Hurl.Settings;

public class Program
{
    [global::System.Runtime.InteropServices.DllImport("Microsoft.ui.xaml.dll")]
    [global::System.Runtime.InteropServices.DefaultDllImportSearchPaths(global::System.Runtime.InteropServices.DllImportSearchPath.SafeDirectories)]
    private static extern void XamlCheckProcessRequirements();

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler", " 3.0.0.2408")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.STAThreadAttribute]
    static void Main(string[] args)
    {
        InitializeWASDK();
        XamlCheckProcessRequirements();

        global::WinRT.ComWrappersSupport.InitializeComWrappers();
        global::Microsoft.UI.Xaml.Application.Start((p) =>
        {
            var context = new global::Microsoft.UI.Dispatching.DispatcherQueueSynchronizationContext(global::Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread());
            global::System.Threading.SynchronizationContext.SetSynchronizationContext(context);
            new App();
        });
    }

    public static bool InitializeWASDK()
    {
        uint minSupportedMinorVersion = global::Microsoft.WindowsAppSDK.Release.MajorMinor; // 0x00010005
        uint maxSupportedMinorVersion = 0x00010007;
        for (uint version = minSupportedMinorVersion; version <= maxSupportedMinorVersion; version++)
        {
            if (global::Microsoft.Windows.ApplicationModel.DynamicDependency.Bootstrap.TryInitialize(version, out _))
                return true;
        }

        System.Environment.Exit(-1);
        return false;
    }
}