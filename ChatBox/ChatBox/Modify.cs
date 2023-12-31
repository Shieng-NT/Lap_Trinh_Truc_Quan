﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ChatBox
{
    class Modify
    {
        private IMongoCollection<Account> accountCollection;

        public List<Account> GetAccounts(string email)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.Email, email);
            var accounts = accountCollection.Find(filter).ToList();
            return accounts;
        }

        public long CountAccounts(string email)
        {
            var filter = Builders<Account>.Filter.Eq("Email", email);
            long count = accountCollection.CountDocuments(filter);
            return count;
        }

        public void InsertAccount(string email, string password)
        {
            var newAccount = new Account { Email = email, Password = password }; // Tạo đối tượng Account mới

            accountCollection.InsertOne(newAccount); 
        }

        public async Task<long> CountAccountsAsync(string email)
        {
            var filter = Builders<Account>.Filter.Eq("Email", email);
            var count = await accountCollection.CountDocumentsAsync(filter);
            return count;
        }

        private static string loggedInUserEmail; // Biến global để lưu trữ email của người dùng đã đăng nhập

        public static void SetLoggedInUserEmail(string email)
        {
            loggedInUserEmail = email;
        }

        public static string GetLoggedInUserEmail()
        {
            return loggedInUserEmail;
        }


    }
}
