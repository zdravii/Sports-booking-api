using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IUseCase
    {
        public string Id { get; }
        public string Name { get; }
    }
}
