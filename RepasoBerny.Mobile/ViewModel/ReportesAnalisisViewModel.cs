using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;

namespace RepasoBerny.Mobile.ViewModels
{
    public class ReportesAnalisisViewModel
    {
        public Chart Chart { get; set; }

        public ReportesAnalisisViewModel()
        {
            Chart = new LineChart
            {
                Entries = new List<ChartEntry>
                {
                    new ChartEntry(200) { Label = "Ene", ValueLabel = "200", Color = SKColor.Parse("#FF5733") },
                    new ChartEntry(400) { Label = "Feb", ValueLabel = "400", Color = SKColor.Parse("#33FF57") },
                    new ChartEntry(300) { Label = "Mar", ValueLabel = "300", Color = SKColor.Parse("#3357FF") }
                },
                LineMode = LineMode.Straight,
                LineSize = 8,
                BackgroundColor = SKColors.White
            };
        }
    }
}
