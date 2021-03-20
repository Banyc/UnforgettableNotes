using System.Collections.Generic;
using System;
using Xunit;
using UnforgettableMemo.Shared.Models;

namespace UnforgettableMemo.Shared.Tests
{
    public class UnitTest1
    {
        // [Fact]
        // public void TestJsonPersistence()
        // {
        //     MemoScheduler scheduler = GetMemoSchedulerWithTwoMemos();
        //     var memos = scheduler.Memos;

        //     scheduler.Save();
        //     scheduler = GetMemoSchedulerWithTwoMemos();

        //     Assert.Equal(2, scheduler.Memos.Count);
        //     Assert.Equal(memos[0].Content, scheduler.Memos[0].Content);
        //     Assert.Equal(memos[1].Content, scheduler.Memos[1].Content);
        // }

        // [Fact]
        // public void TestReview()
        // {
        //     MemoScheduler scheduler = GetMemoSchedulerWithTwoMemos();

        //     scheduler.OrderByRetrievability();

        //     var firstMemo = scheduler.Memos[0];

        //     // keep review the first one
        //     scheduler.Memos[0].Review();
        //     scheduler.Memos[0].Review();
        //     scheduler.Memos[0].Review();

        //     // the first one should be more learnt than the second one and be swapped down
        //     scheduler.OrderByRetrievability();

        //     Assert.Equal(firstMemo.Content, scheduler.Memos[1].Content);
        // }

        // public MemoScheduler GetMemoSchedulerWithTwoMemos()
        // {
        //     MemoScheduler scheduler = GetMemoSchedulerWithJsonPersistence();

        //     scheduler.Memos.Clear();
        //     scheduler.Memos.Add(new Memo()
        //     {
        //         Content = "test1"
        //     });
        //     scheduler.Memos.Add(new Memo()
        //     {
        //         Content = "test2"
        //     });

        //     return scheduler;
        // }

        // public MemoScheduler GetMemoSchedulerWithJsonPersistence()
        // {
        //     MemoSchedulerFactory factory = new MemoSchedulerFactory("testPersistence/");
        //     return factory.GetMemoScheduler();
        // }
    }
}
