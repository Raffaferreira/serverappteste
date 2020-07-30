using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTeste.Service.Validators.Interfaces
{
    public interface IGetIngressosValidator<T>
    {
        T _objeto { get; set; }
        bool IsValid { get; }
        void ExecuteValidate();
    }
}
