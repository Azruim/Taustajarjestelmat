using System;
namespace GameWebApi
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) {}
    }
}