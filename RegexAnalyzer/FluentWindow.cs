using Avalonia.Controls.Primitives;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace RegexAnalyzer
{
    public class FluentWindow : Window, IStyleable
    {
        Type IStyleable.StyleKey => typeof(Window);

        public FluentWindow()
        {


            //In charge of extending the use area, over the "windows title bar", add the flag IsHitTestVisible="False" on non-transparent components to ensure window movement
            ExtendClientAreaToDecorationsHint = true;
            //Sets title bar auto size when client area is extendeded , Avalonia reference --> http://reference.avaloniaui.net/api/Avalonia.Controls/Window/347C8A2E
            ExtendClientAreaTitleBarHeightHint = -1;

            TransparencyLevelHint = WindowTransparencyLevel.AcrylicBlur;

            //Avalonia Control Reference --> http://reference.avaloniaui.net/api/Avalonia.Controls/Window/2CAA4A5D
            this.GetObservable(WindowStateProperty)
                .Subscribe(x =>
                {
                    PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                    PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
                });

            //Avalonia Control Referencel --> http://reference.avaloniaui.net/api/Avalonia.Controls/Window/5C43129E
            this.GetObservable(IsExtendedIntoWindowDecorationsProperty)
                .Subscribe(x =>
                {
                    if (!x)
                    {
                        SystemDecorations = SystemDecorations.Full;
                        TransparencyLevelHint = WindowTransparencyLevel.Blur;
                    }
                });
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            ExtendClientAreaChromeHints =
                ExtendClientAreaChromeHints.PreferSystemChrome |
                ExtendClientAreaChromeHints.OSXThickTitleBar;
        }
    }
}
