using System;
using System.Collections.Generic;
using System.Text;

namespace InstaDemo.Contracts.DataContracts.Services
{
    public interface IGVisionService
    {
        IReadOnlyList<GVisionImageResponse> Recognize(string url);
    }
}
