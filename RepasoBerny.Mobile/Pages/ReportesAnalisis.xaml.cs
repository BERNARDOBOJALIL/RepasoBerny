using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp.Views.Maui;
using RepasoBerny.Mobile.ViewModels;

namespace RepasoBerny.Mobile.Pages;

public partial class ReportesAnalisis : ContentPage
{
    private readonly ReportesAnalisisViewModel _viewModel;

    public ReportesAnalisis()
    {
        InitializeComponent();
        _viewModel = new ReportesAnalisisViewModel();
        BindingContext = _viewModel;
    }

    private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        var surface = e.Surface;
        var canvas = surface.Canvas;

        // Clear the canvas
        canvas.Clear(SKColors.White);

        // Get the chart dimensions
        var width = e.Info.Width;
        var height = e.Info.Height;

        // Reduce graph height - leave more space for labels
        var graphHeight = height * 0.7f;  // Use only 70% of height for graph
        var labelSpace = height * 0.3f;   // 30% for labels

        // Set up the paint for the lines
        var linePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 4,
            IsAntialias = true,
            Shader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(width, 0),
                new SKColor[]
                {
                SKColor.Parse("#FF5733"),   // Red
                SKColor.Parse("#33FF57"),   // Green
                SKColor.Parse("#3357FF")    // Blue
                },
                null,
                SKShaderTileMode.Clamp
            )
        };

        // Calculate the scale for the chart
        var maxValue = _viewModel.Entries.Max(entry => entry.Value);
        var minValue = _viewModel.Entries.Min(entry => entry.Value);
        var scaleY = (graphHeight - 40) / (maxValue - minValue);

        // X-axis spacing
        var xSpacing = (width - 40) / (_viewModel.Entries.Count - 1);

        // Prepare path for the line
        var path = new SKPath();

        // Move to the first point
        var firstEntry = _viewModel.Entries[0];
        var startX = 20;
        var startY = graphHeight - 20 - (firstEntry.Value - minValue) * scaleY;
        path.MoveTo(startX, startY);

        // Draw lines connecting the points
        for (int i = 1; i < _viewModel.Entries.Count; i++)
        {
            var entry = _viewModel.Entries[i];
            var x = 20 + i * xSpacing;
            var y = graphHeight - 20 - (entry.Value - minValue) * scaleY;
            path.LineTo(x, y);
        }

        // Draw the gradient line
        canvas.DrawPath(path, linePaint);

        // Draw points
        var pointPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            IsAntialias = true
        };

        // Value label font and paint
        var valueLabelFont = new SKFont
        {
            Size = 20,
            Typeface = SKTypeface.Default
        };

        var valueLabelPaint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true
        };

        for (int i = 0; i < _viewModel.Entries.Count; i++)
        {
            var entry = _viewModel.Entries[i];
            var x = 20 + i * xSpacing;
            var y = graphHeight - 20 - (entry.Value - minValue) * scaleY;

            // Gradient for points
            pointPaint.Shader = SKShader.CreateLinearGradient(
                new SKPoint(x - 10, y - 10),
                new SKPoint(x + 10, y + 10),
                new SKColor[]
                {
                entry.Color,
                SKColors.White
                },
                null,
                SKShaderTileMode.Clamp
            );

            canvas.DrawCircle(x, y, 10, pointPaint);

            // Draw value label above the point
            canvas.DrawText(entry.Value.ToString(), x-15, y + 30, valueLabelFont, valueLabelPaint);
        }

        // Add month labels
        var monthLabelFont = new SKFont
        {
            Size = 25,
            Typeface = SKTypeface.Default
        };

        var monthLabelPaint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true
        };

        for (int i = 0; i < _viewModel.Entries.Count; i++)
        {
            var entry = _viewModel.Entries[i];
            var x = 20 + i * xSpacing;
            canvas.DrawText(entry.Label, x, height - 10, SKTextAlign.Center, monthLabelFont, monthLabelPaint);
        }
    }
    private void OnVerDetallesClicked(object sender, EventArgs e)
    {
        // Alternar visibilidad
        contenidoOculto.IsVisible = !contenidoOculto.IsVisible;

        // Cambiar texto del botón
        btnDetalles.Text = contenidoOculto.IsVisible ? "Ocultar detalles" : "Ver detalles";
    }
}


