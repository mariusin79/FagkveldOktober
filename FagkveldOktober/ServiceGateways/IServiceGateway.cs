using System;

namespace FagkveldOktober.ServiceGateways
{
    public interface IServiceGateway<out T>
    {
        TR Execute<TR>(Func<T, TR> what);
    }
}