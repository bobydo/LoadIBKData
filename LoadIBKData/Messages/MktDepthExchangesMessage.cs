﻿/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using LoadIBKData.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoadIBKData.Messages
{
    class MktDepthExchangesMessage
    {
        public DepthMktDataDescription[] Descriptions { get; private set; }

        public MktDepthExchangesMessage(DepthMktDataDescription[] descriptions)
        {
            this.Descriptions = descriptions;
        }
    }
}
