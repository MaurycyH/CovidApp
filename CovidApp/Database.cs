using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CovidApp
{
    public class Database
    {
        readonly SQLiteAsyncConnection mDatabase;
        public int ActualUserId { get; set; }
        public Database(string dbPath)
        {
            mDatabase = new SQLiteAsyncConnection(dbPath);
            mDatabase.CreateTableAsync<UserDB>().Wait();
            mDatabase.CreateTableAsync<RequestDB>().Wait();

        }

        //Read All Requests 
        public Task<List<RequestDB>> GetRequestsAsync()
        {
            return mDatabase.Table<RequestDB>().ToListAsync();
        }
        public Task<List<RequestDB>> GetUserRequestsAsync()
        {
            return mDatabase.Table<RequestDB>().Where(i => i.UserId == App.Database.ActualUserId).ToListAsync();
        }

        //Read one Item
        public Task<UserDB> GetUserUsername(string userUsername)
        {
            return mDatabase.Table<UserDB>().Where(i => i.Username == userUsername).FirstOrDefaultAsync();
        }

        public Task<int> SavePersonAsync(UserDB userDB)
        {
            if (userDB.ID != 0)
            {
                return mDatabase.UpdateAsync(userDB);
            }
            else
            {
                return mDatabase.InsertAsync(userDB);

            }
        }
        public Task<int> SaveRequestAsync(RequestDB RequestDb)
        {
            if (RequestDb.ID != 0)
            {
                return mDatabase.UpdateAsync(RequestDb);
            }
            else
            {
                return mDatabase.InsertAsync(RequestDb);

            }
        }

        public async Task<bool> UpdateRequest(int requestId)
        {
            var Query = await mDatabase.QueryAsync<RequestDB>($"SELECT * FROM RequestDB WHERE ID ='{requestId}'");
            if (Query[0].UserId != 0)
            {
                return true;
            }
            else
            {
                RequestDB requestDB = new RequestDB
                {
                    ID = Query[0].ID,
                    Title = Query[0].Title,
                    Description = Query[0].Description,
                    BuyList = Query[0].BuyList,
                    Latitude = Query[0].Latitude,
                    Longitude = Query[0].Longitude,
                    UserId = App.Database.ActualUserId
                };
                await SaveRequestAsync(requestDB);
                return false;
            }
        }
    }
}
