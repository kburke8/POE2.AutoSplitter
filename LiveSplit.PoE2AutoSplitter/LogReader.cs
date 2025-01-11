using System;
using System.IO;

namespace LiveSplit.PoE2AutoSplitter
{
    public class LogReader : IDisposable
    {
        private readonly string _logFilePath;
        private readonly PoE2ZoneDetector _zoneDetector;

        private FileStream _fileStream;
        private StreamReader _streamReader;

        public LogReader(string logFilePath, PoE2ZoneDetector zoneDetector)
        {
            _logFilePath = logFilePath;
            _zoneDetector = zoneDetector;

            Initialize();
        }

        private void Initialize()
        {
            _fileStream = new FileStream(_logFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _streamReader = new StreamReader(_fileStream);

            // Jump to the end so we only read new lines
            _fileStream.Seek(0, SeekOrigin.End);
        }

        /// <summary>
        /// Poll for new lines. Call this from a loop or background thread.
        /// </summary>
        public void Update()
        {
            while (!_streamReader.EndOfStream)
            {
                string line = _streamReader.ReadLine();
                if (line != null)
                {
                    _zoneDetector.OnNewLogLine(line);
                }
            }
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
            _fileStream?.Dispose();
        }
    }
}
