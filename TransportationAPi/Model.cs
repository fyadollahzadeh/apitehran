using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportationApi
{
    public class Model
    {
    }
    public class MetroLine
    {
        public string DESTINATION { get; set; }
        public string NAME { get; set; }
        public string ORIGIN { get; set; }
        public string WKT_GEOMETRY { get; set; }
    }
    public class BusTerminal
    {
        public string ADDRESS { get; set; }
        public string NAME { get; set; }
        public string WKT_GEOMETRY { get; set; }
    }

    public class TaxiStation
    {
        public int LINECODE { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public string WKT_GEOMETRY { get; set; }
    }

    public class TaxiStationRoot
    {
        public List<object> MessageDetail { get; set; }
        public List<TaxiStation> TaxiStation { get; set; }
    }

    public class LineRstList
    {
        public int Amount { get; set; }
        public string Code { get; set; }
        public string Dest { get; set; }
        public string Source { get; set; }
        public int VanAmount { get; set; }
    }

    public class TaxiPriceRoot
    {
        public List<LineRstList> LineRstList { get; set; }
        public object MessageDetail { get; set; }
    }
    public class TaxiTerminal
    {
        public int? LINECOUNT { get; set; }
        public string NAME { get; set; }
        public int TERMINALCODE { get; set; }
        public string WKT_GEOMETRY { get; set; }
    }

    public class TaxiRoot
    {
        public List<object> MessageDetail { get; set; }
        public List<TaxiTerminal> TaxiTerminal { get; set; }
    }

    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int BufferSize { get; set; } = 3000;
    }

    public class MetroStation
    {
        public string LINE { get; set; }
        public string NAME { get; set; }
        public string WKT_GEOMETRY { get; set; }
    }

    public class MetroStationRoot
    {
        public List<object> MessageDetail { get; set; }
        public List<MetroStation> MetroStation { get; set; }
    }

    public class BusRoot
    {
        public List<object> MessageDetail { get; set; }
        public List<BusTerminal> BusTerminal { get; set; }
    }

    public class MetroRoot
    {
        public List<object> MessageDetail { get; set; }
        public List<MetroLine> MetroLine { get; set; }
    }
}
