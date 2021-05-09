/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using LoadIBKData.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoadIBKData.Messages
{
    class HistogramDataMessage
    {
        public int ReqId { get; private set; }
        public HistogramEntry[] Data { get; private set; }

        public HistogramDataMessage(int reqId, HistogramEntry[] data)
        {
            ReqId = reqId;
            Data = data;
        }
    }
}
