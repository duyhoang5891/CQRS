﻿using CQRS.Core.Commands;

namespace CQRS.Core.Messages
{
    public interface ICommandDispather
    {
        void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand;
        Task SendAsync(BaseCommand command);
    }
}
