namespace AE_extensive_project.Config
{
    //perhaps be implemented later on
    public class TestSettings
    {
        public string[] Args { get; set; }

        public float Timeout { get; set; }
        public bool Headless { get; set; }

        public bool DevTools { get; set; }

        public float SlowMo { get; set; }

        public DriverType DriverType { get; set; }
        public string ApplicationUrl { get; set; }
    }

    public enum DriverType
    {
        Chromium,
        Firefox,
        Edge,
        Chrome,
        WebKit
    }
}
