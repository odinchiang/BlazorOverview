using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorOverview.Models;

namespace BlazorOverview.Services
{
    public class MyNoteWebApiService : IMyNoteService
    {
        public HttpClient Client { get; }

        /// <summary>
        /// 使用建構式注入方式，注入 HttpClient 類別執行個體，以便可以呼叫 WebApi
        /// </summary>
        /// <param name="httpClient"></param>
        public MyNoteWebApiService(HttpClient httpClient)
        {
            Client = httpClient;
        }

        /// <summary>
        /// 建立一筆新記事紀錄
        /// </summary>
        /// <param name="myNote"></param>
        /// <returns></returns>
        public async Task CreateAsync(MyNote myNote)
        {
            var content = JsonSerializer.Serialize(myNote);
            using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
            {
                await Client.PostAsync("/api/MyNote", stringContent);
            }
        }

        /// <summary>
        /// 刪除記事紀錄
        /// </summary>
        /// <param name="myNote"></param>
        /// <returns></returns>
        public async Task DeleteAsync(MyNote myNote)
        {
            await Client.DeleteAsync($"/api/MyNote/{myNote.Id}");
        }

        /// <summary>
        /// 查詢所有記事紀錄
        /// </summary>
        /// <returns></returns>
        public async Task<List<MyNote>> RetriveAsync()
        {
            var content = await Client.GetStringAsync("api/MyNote");
            var allMyNotes = JsonSerializer.Deserialize<List<MyNote>>(content);
            return allMyNotes;
        }

        /// <summary>
        /// 修改記事紀錄
        /// </summary>
        /// <param name="origMyNote"></param>
        /// <param name="myNote"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MyNote origMyNote, MyNote myNote)
        {
            var content = JsonSerializer.Serialize(myNote);
            using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
            {
                await Client.PutAsync($"/api/MyNote/{myNote.Id}", stringContent);
            }
        }
    }
}
