using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimDynoDataRecorder.Models;
public enum AppState
{
    Idle,
    Listen,
    Recording,
    Broadcast
}
