using System;
namespace Assignment_1
{
    public class StationNotFoundException : Exception
    {
        public StationNotFoundException(string message) : base(message) {}
    }
}