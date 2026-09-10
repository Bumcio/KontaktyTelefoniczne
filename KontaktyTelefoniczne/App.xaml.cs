namespace KontaktyTelefoniczne
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

            window.MinimumWidth = 1920;
            window.MinimumHeight = 1080;
            window.MaximumWidth = 1920;
            window.MaximumHeight = 1080;

            return window;
        }
    }
}