﻿using InventoryManagement.Settings.HttpException;

namespace InventoryManagement.Settings.Validations
{
    public class Notification
    {
        public Notification(string message) => Message = message;

        public string Message { get; }
    }
}
