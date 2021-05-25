using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProject.Core.Models
{
    public enum ETicketState
    {
        WaitingApproval,
        Pending,
        BeingWorkedOn,
        AwaitingTesting,
        TestFailed,
        Completed
    }
}
