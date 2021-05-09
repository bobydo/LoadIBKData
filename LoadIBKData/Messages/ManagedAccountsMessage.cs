﻿/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoadIBKData.Messages
{
    class ManagedAccountsMessage
    {
        List<string> managedAccounts;

        public ManagedAccountsMessage(string managedAccounts)
        {
            this.managedAccounts = new List<string>(managedAccounts.Split(','));
        }

        public List<string> ManagedAccounts
        {
            get { return managedAccounts; }
            set { managedAccounts = value; }
        }
    }
}
