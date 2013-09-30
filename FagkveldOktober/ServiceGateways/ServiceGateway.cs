using System;
using System.ServiceModel;

namespace FagkveldOktober.ServiceGateways
{
    public class ServiceGateway<T> : IServiceGateway<T>
    {
        private readonly ChannelFactory<T> _factory;

        public ServiceGateway(Func<Type, string> endpointConfigName)
        {
            _factory = new ChannelFactory<T>(endpointConfigName(typeof(T)));
        }

        public TR Execute<TR>(Func<T, TR> what)
        {
            var channel = _factory.CreateChannel();

            try
            {
                var result = what(channel);
                ((IClientChannel)channel).Close();
                return result;
            }
            catch (Exception)
            {
                ((IClientChannel)channel).Abort();
                throw;
            }
        }
    }
}