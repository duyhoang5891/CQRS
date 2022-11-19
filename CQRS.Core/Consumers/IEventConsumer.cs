namespace CQRS.Core.Consumers
{
    internal interface IEventConsumer
    {
        void Consume(string topic);
    }
}
