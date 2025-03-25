using System;
using System.Collections.Generic;
using SkiaSharp;

namespace RepasoBerny.Mobile.ViewModels
{
    public class ReportesAnalisisViewModel
    {
        public List<ChartEntry> Entries { get; set; }
        public ReportesAnalisisViewModel()
        {
            Entries = new List<ChartEntry>
            {
                new ChartEntry(200) {
                    Label = "Ene",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#FF5733")
                },
                new ChartEntry(400) {
                    Label = "Feb",
                    ValueLabel = "400",
                    Color = SKColor.Parse("#33FF57")
                },
                new ChartEntry(300) {
                    Label = "Mar",
                    ValueLabel = "300",
                    Color = SKColor.Parse("#3357FF")
                }
            };
        }
    }

    public class ChartEntry
    {
        public float Value { get; set; }
        public string Label { get; set; } = string.Empty;
        public string ValueLabel { get; set; } = string.Empty;
        public SKColor Color { get; set; } = SKColors.Black;

        public ChartEntry(float value)
        {
            Value = value;
        }
    }
}