using System.Collections.Generic;
using System;
using Xunit;
using UnforgettableMemo.Shared.Models;

namespace UnforgettableMemo.Shared.Tests
{
    public class UnitTest1
    {
        [Fact]
        public MemoScheduler GetMemoSchedulerWithJsonPersistence()
        {
            MemoSchedulerFactory factory = new MemoSchedulerFactory("testPersistence/");
            return factory.GetMemoScheduler();
        }

        [Fact]
        public void TestJsonPersistence()
        {
            MemoScheduler scheduler = GetMemoSchedulerWithJsonPersistence();

            scheduler.Memos.Clear();
            scheduler.Memos.Add(new Memo()
            {
                Content = "test1"
            });
            scheduler.Memos.Add(new Memo()
            {
                Content = "test2"
            });
            var memos = scheduler.Memos;

            scheduler.Save();
            scheduler.Load();

            Assert.Equal(2, scheduler.Memos.Count);
            Assert.Equal(memos[0].Content, scheduler.Memos[0].Content);
            Assert.Equal(memos[1].Content, scheduler.Memos[1].Content);
        }

        
    }
}
