using System;

namespace OrderBot
{
    public class Order
    {
        private string _size;

        public string Size{
            get => _size;
            set => _size = value;
        }
    }
}
