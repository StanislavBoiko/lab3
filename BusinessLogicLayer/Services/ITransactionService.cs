﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services
{
    public interface ITransactionService
    {
        void CreateTransaction(Transaction transaction);
        Transaction GetTransactionById(int transactionId);
    }
}
