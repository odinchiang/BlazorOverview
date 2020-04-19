using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorOverview.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorOverview.Services
{
    public class MyNoteDbService : IMyNoteService
    {
        public MyNoteDbContext MyNoteDbContext { get; set; }

        /// <summary>
        /// 使用建構式注入方式，注入 MyNoteDbContext 類別執行個體，以便可以存取 SQLite 資料庫
        /// </summary>
        /// <param name="myNoteDbContext"></param>
        public MyNoteDbService(MyNoteDbContext myNoteDbContext)
        {
            MyNoteDbContext = myNoteDbContext;
        }

        /// <summary>
        /// 查詢所有記事紀錄
        /// </summary>
        /// <returns></returns>
        public async Task<List<MyNote>> RetriveAsync()
        {
            return await MyNoteDbContext.MyNotes.ToListAsync();
        }

        /// <summary>
        /// 建立一筆新記事紀錄
        /// </summary>
        /// <param name="myNote"></param>
        /// <returns></returns>
        public async Task CreateAsync(MyNote myNote)
        {
            await MyNoteDbContext.MyNotes.AddAsync(myNote);
            await MyNoteDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 修改記事紀錄
        /// </summary>
        /// <param name="origMyNote"></param>
        /// <param name="myNote"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MyNote origMyNote, MyNote myNote)
        {
            var fooItem = await MyNoteDbContext.MyNotes.FirstOrDefaultAsync(x => x.Id == origMyNote.Id);
            if (fooItem != null)
            {
                fooItem.Title = myNote.Title;
                await MyNoteDbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 刪除記事紀錄
        /// </summary>
        /// <param name="myNote"></param>
        /// <returns></returns>
        public async Task DeleteAsync(MyNote myNote)
        {
            var note = await MyNoteDbContext.MyNotes.FirstOrDefaultAsync(x => x.Id == myNote.Id);
            MyNoteDbContext.MyNotes.Remove(note);
            await MyNoteDbContext.SaveChangesAsync();
        }
    }
}
