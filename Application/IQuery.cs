using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IQuery<TRequest,TResponse> : IUseCase
        where TResponse : class
    {
        TResponse Execute(TRequest request);
    }

    public interface IQuery<TResponse> : IUseCase
        where TResponse : class
    {
        TResponse Execute();
    }
}
