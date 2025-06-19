using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimDynoDevSuite.Models;
public class TimedPacket
{
    public long TimestampTicks { get; set; }
    public byte[] Data { get; set; } = Array.Empty<byte>();
}
