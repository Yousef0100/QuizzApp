using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa_me
{
    internal class Option
    {
        private static int ID = 0;

        private int _id;
        private string _title;

        public int Id
        {
            get {
                return _id;
            }
            private set {
                _id = value;
            }
        }

        public string Title
        {
            get {
                return _title;
            }
            set {
                _title = value;
            }
        }

        public Option(string title)
        {
            Title = title;
            Id = ID++;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
