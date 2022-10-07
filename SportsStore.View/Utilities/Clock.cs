using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SportsStore.Utilities
{
    public static class Clock
    {
        private static Label labl;
        public static void Start(Label lbl)
        {
            labl = lbl ?? throw new ArgumentNullException(nameof(lbl));

            DispatcherTimer clock = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            clock.Tick += Tick;
            clock.Start();
        }
        private static void Tick(object? sender, EventArgs e)
        {
            labl.Content = DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }
    }
}
