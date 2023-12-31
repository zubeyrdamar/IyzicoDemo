﻿using IyzicoApp.Entity;

namespace IyzicoApp.DataAccess.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCart(string Username);
    }
}
