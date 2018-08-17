using System;
using System.Collections.Generic;
using System.Text;

namespace Quote
{
    public interface IValidation
    {
        void ValidateSession(string sessionId);
    }
}
