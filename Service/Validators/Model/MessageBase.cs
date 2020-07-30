using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Service.Validators.Model
{
    public abstract class MessageBase
    {
        public List<bool> _verifications { get; set; }
        public List<string> _messages { get; set; }
        public virtual void AddMessage(string msg, bool isError)
        {
            _messages.Add(msg);

            if (isError)
            {
                _verifications.Add(false);
            }
            else
            {
                _verifications.Add(true);
            }
        }
    }
}
