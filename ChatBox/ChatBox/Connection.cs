﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MongoDB.Driver;
using ChatBox;
using ChatBox.ViewModel;

namespace ChatBox
{
    class Connection
    {
        //private static string stringConnection= @"mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/Chatbox?retryWrites=true&w=majority";
        private IMongoDatabase database;
        string databaseName = "AccountInfo";
        string collectionName = "Account";
        string connectionString = "mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/QLChatbox?retryWrites=true&w=majority";
        public Connection(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            if (client != null)
            {
                database = client.GetDatabase(databaseName);
                accountCollection = database.GetCollection<Account>(collectionName); // Thay "AccountCollectionName" bằng tên collection thực tế trong cơ sở dữ liệu của bạn
            }

        }

        private IMongoCollection<Account> accountCollection;
        public async Task<long> CountAccountsAsync(string email)
        {
            var filter = Builders<Account>.Filter.Eq("Email", email);
            long count = await accountCollection.CountDocumentsAsync(filter);
            return count;
        }
        public void InsertAccount(string email, string password)
        {
            var newAccount = new Account { Email = email, Password = password };
            accountCollection.InsertOne(newAccount);
        }

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

        private static string loggedInUserEmail; // Biến global để lưu trữ email của người dùng đã đăng nhập

        private static string loggedInUserPassword;
        public static void SetLoggedInUserEmail(string email)
        {
            loggedInUserEmail = email;
        }
        public static void SetLoggedInUserPass(string pass)
        {
            loggedInUserPassword = pass;
        }

        public static string GetLoggedInUserEmail()
        {
            return loggedInUserEmail;
        }
        public static string GetLoggedInUserPass()
        {
            return loggedInUserPassword;
        }
        public void InsertOrUpdateAccountInfo(string email, string password, string user, string birthday, string introduce)
        {
            var filter = Builders<Account>.Filter.Eq("Email", email);
            var update = Builders<Account>.Update
                .Set("Password", password)
                .Set("User", user)
                .Set("Birthday", birthday)
                .Set("Introduce", introduce);
            var options = new UpdateOptions { IsUpsert = true };
            accountCollection.UpdateOne(filter, update, options);
        }

        // Lấy thông tin từ MongoDB dựa trên email đã đăng nhập
        public static async Task<Account> GetUserInfoFromMongoDB(string userEmail)
        {
            Connection connection = new Connection("mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/AccountInfo?retryWrites=true&w=majority", "AccountInfo");
            return await connection.GetUserInfoByEmail(userEmail);
        }

        public async Task<Account> GetUserInfoByEmail(string email)
        {
            // Kết nối tới MongoDB và thực hiện truy vấn để lấy thông tin người dùng theo email
            // Code dưới đây là một phần của việc truy vấn từ MongoDB, có thể cần được thay đổi tùy theo thư viện hoặc cách bạn sử dụng MongoDB trong ứng dụng của mình

            var filter = Builders<Account>.Filter.Eq("Email", email);
            var userInfo = await accountCollection.Find(filter).FirstOrDefaultAsync();

            return userInfo;
        }


    }
}