using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2
{
    public delegate void WithdrawalLimitExceeded(object source, WithdrawalEventArgs e);
    public class Withdraw
    {
        private IBankRepository _bankRepo;
        public event WithdrawalLimitExceeded OnLimitExceeded;

        public Withdraw(IBankRepository bankRepo)
        {
            this._bankRepo = bankRepo;
        }

        public async Task WithdrawMoney(int amount)
        {
            if (amount > 5500)
            {
                WithdrawalEventArgs e = new WithdrawalEventArgs();
                OnWithdrawLimitExceeded(e);
            }
            int balance = await _bankRepo.GetBalanceAmount("ABC");
            int newBalance =  DoWithdraw(balance, amount);
            if (newBalance < 5000)
            {
                WithdrawalEventArgs e = new WithdrawalEventArgs();
                OnWithdrawLimitExceeded(e);
            }
            await UpdateBalance(newBalance);
        }

        private void OnWithdrawLimitExceeded(WithdrawalEventArgs e)
        {
            if (OnLimitExceeded != null)
            { 
            e.nessage = "Lower limit rwched";
            OnLimitExceeded(this, e);
        }
        }        

        private async Task UpdateBalance(int newBalance)
        {
            await _bankRepo.UpdateBalance("ABC", newBalance);
        }

        private int DoWithdraw(int balance, int amount)
        {            
            return balance - amount;
        }

    
        public int Add (int a, int b)

        {
            int result = 0;
            if (a >= 0)
            {
                
                result = a+b;
            }
           
            return result;
        }

        
    }
}
