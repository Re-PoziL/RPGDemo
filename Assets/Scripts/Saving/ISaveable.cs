using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Saving
{
    interface ISaveable
    {
        public object CaptureState();
        public void RestoreState(object state);
    }
}
    