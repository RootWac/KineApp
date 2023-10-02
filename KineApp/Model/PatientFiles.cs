namespace KineApp.Controller
{
    internal class PatientFiles
    {
        public int record { get; private set; }
        public string type { get; private set; }
        public string filename { get; private set; }
        public string providerName { get; private set; }

        public PatientFiles(int record, string type, string filename, string providerName)
        {
            this.record = record;
            this.type = type;
            this.filename = filename;
            this.providerName = providerName;
        }
    }
}