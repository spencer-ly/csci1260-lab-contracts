using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class CountLog : IDisposable
    {
        private StreamWriter _writer;
        private string _path;
        private int _count;
        private bool _isClosed;

        public string Path { get { return _path; } }
        public int Count { get { return _count; } }
        public bool IsClosed { get { return _isClosed; } }


        public CountLog(string path)
        {
            _writer = new StreamWriter(path);
            _writer.WriteLine("LOG OPENED");
            _count = 0;
            _isClosed = false;
        }

        public void Write(ShelfCount r)
        {
            String.Format("{0,3} {1}", Count/*, somethign else*/); 
            _count++;
        }

        public void Dispose()
        {
            if (_isClosed) return;

            else
            {
                String.Format("LOG CLOSED, {0} lines written", Count);
                _writer.Dispose();
                _isClosed = true;
            }

        }
    }
}
