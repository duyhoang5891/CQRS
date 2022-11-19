using System;
namespace CQRS.Core.Domain
{
    public interface IEventConsumer
    {
        void Consume(string topic);
    }
}

