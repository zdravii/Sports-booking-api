using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation
{
    public abstract class UseCase
    {
        protected readonly DataBaseContext _db;
        protected UseCase(DataBaseContext db)
        {
            _db = db;
        }
    }
}
