using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace FlowDiscord
{
    public partial class MainWindow : Window
    {
        private const int GWL_EXSTYLE = -20;
        private const uint WS_EX_TRANSPARENT = 0x00000020;

        [DllImport("user32.dll")]
        private static extern uint GetWindowLong(IntPtr hWnd, int index);

        [DllImport("user32.dll")]
        private static extern uint SetWindowLong(IntPtr hWnd, int index, uint newLong);

        public static List<FlowMessage> messages = new List<FlowMessage>();

        public MainWindow()
        {
            RecieveMessage recieveMessage = new RecieveMessage();
            recieveMessage.StartAsync();
            InitializeComponent();
            SetSlipThrough(this);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                skiaCanvas.InvalidateVisual();
                await Task.Delay(10);
            }
        }

        public static void CreateNewMessage(string message)
        {
            messages.Add(new FlowMessage(message, 3000, generateRandInt(50, 900), 10));
        }

        void PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            List<FlowMessage> messagesToRemove = new List<FlowMessage>();

            foreach (var message in messages)
            {
                var skPaint = new SKPaint
                {
                    TextSize = 45,
                    TextAlign = SKTextAlign.Right,
                    Color = SKColors.White
                };

                skPaint.Typeface = SKTypeface.FromFamilyName("Meiryo UI");
                canvas.DrawText(message.Text, message.PositionX, message.PositionY, skPaint);
                message.PositionX -= message.Speed;

                if (message.PositionX < -200)
                {
                    messagesToRemove.Add(message);
                }
            }

            foreach (var message in messagesToRemove)
            {
                messages.Remove(message);
            } }

        public static int generateRandInt(int minValue, int maxValue)
        {
            Random r = new Random();
            return r.Next(minValue, maxValue);
        }

        private void SetSlipThrough(Window window)
        {
            window.SourceInitialized += ((sender, e) => {
                var handle = new WindowInteropHelper(window).Handle;
                uint style = GetWindowLong(handle, GWL_EXSTYLE);
                SetWindowLong(handle, GWL_EXSTYLE, style | WS_EX_TRANSPARENT);
            });
        }
    }
}
